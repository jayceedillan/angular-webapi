using System.ComponentModel.DataAnnotations;

namespace atm_machine_api.Dto
{
    public class AuthDto
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int currentBalance { get; set; }

        [Required]
        public string token { get; set; }
    }
}