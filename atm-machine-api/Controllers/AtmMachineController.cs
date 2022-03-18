using Microsoft.AspNetCore.Mvc;
using atm_machine_api.Data;
using atm_machine_api.Repository;
using atm_machine_api.Models;
using atm_machine_api.Dto;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace atm_machine_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmMachineController : ControllerBase
    {

        readonly IConfiguration configuration;
        readonly IAtmMachineRepository iAtmMachineRepository;
        public AtmMachineController(IConfiguration _configuration, IAtmMachineRepository _iAtmMachineRepository)
        {
            iAtmMachineRepository = _iAtmMachineRepository;
            configuration = _configuration;
        }

        [HttpGet("{pinNo}")]
        public async Task<ActionResult<AuthDto>> getUserByPin(int pinNo)
        {
            var usersDto = await iAtmMachineRepository.getPinNo(pinNo);

            if (usersDto != null)
            {

                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", usersDto.id.ToString()),
                        new Claim("name",usersDto.firstName),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);


                var authDto = new AuthDto();
                authDto.token = new JwtSecurityTokenHandler().WriteToken(token);
                authDto.id = usersDto.id;
                return Ok(authDto);
            }

            return BadRequest("Invalid credentials");
        }


        [HttpGet]
        [Route("getUsers"), Authorize]
        public async Task<ActionResult<IEnumerable<Users>>> getUsers()
        {
            var users = await iAtmMachineRepository.getAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("getAllTransactions"), Authorize]
        public async Task<ActionResult<IEnumerable<UsersTransactionHistoryDto>>> getAllTransactions(int pinNo)
        {
            var userTransactions = await iAtmMachineRepository.getAllTransaction(pinNo);
            return Ok(userTransactions);
        }

        [HttpPost]
        [Route("addTransaction"), Authorize]
        public async Task<ActionResult<UsersTransactionHistoryDto>> addTransaction(UsersTransactionHistoryDto usersTransactionHistoryDto)
        {
            var result = await iAtmMachineRepository.AddTransaction(usersTransactionHistoryDto);

            if (result)
            {
                return Ok(usersTransactionHistoryDto);
            }
            return BadRequest(new { message = "error while saving" });
        }

    }
}