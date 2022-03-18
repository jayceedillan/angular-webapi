using atm_machine_api.Data;
using atm_machine_api.Dto;
using Microsoft.EntityFrameworkCore;
using atm_machine_api.Models;
namespace atm_machine_api.Repository
{
    public class AtmMachineRepository : IAtmMachineRepository
    {
        readonly ApplicationDBContext dbContext;
        public AtmMachineRepository(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<UsersDto> getPinNo(int PinNo)
        {
            var userDto = await (from user in dbContext.Users
                                 select new UsersDto()
                                 {
                                     id = user.id,
                                     balance = user.balance,
                                     cardNo = user.cardNo,
                                     firstName = user.firstName,
                                     lastName = user.lastName,
                                     pinNo = user.pinNo,
                                 }
            ).FirstOrDefaultAsync();

            return userDto;
        }

        public async Task<IEnumerable<UsersDto>> getAllUsers()
        {
            return await dbContext.Users.Select(user => new UsersDto()
            {
                id = user.id,
                balance = user.balance,
                cardNo = user.cardNo,
                firstName = user.firstName,
                lastName = user.lastName,
                pinNo = user.pinNo,
            }).ToListAsync();
        }

        public async Task<IEnumerable<UsersTransactionHistoryDto>> getAllTransaction(int pinNo)
        {
            var allTransaction = await dbContext.UsersTransactionHistories.Select(user =>
            new UsersTransactionHistoryDto()
            {
                pinNo = user.pinNo,
                amount = user.amount,
                transactionDate = user.transactionDate,
                typeOfTransaction = user.typeOfTransaction
            }).Where(x => x.pinNo == pinNo).ToListAsync();

            return allTransaction;
        }

        public async Task<bool> AddTransaction(UsersTransactionHistoryDto usersTransactionHistoryDto)
        {
            var usersTransactionHistory = new UsersTransactionHistory();
            usersTransactionHistory.amount = usersTransactionHistoryDto.amount;
            usersTransactionHistory.pinNo = usersTransactionHistoryDto.pinNo;
            usersTransactionHistory.typeOfTransaction = usersTransactionHistoryDto.typeOfTransaction;
            dbContext.UsersTransactionHistories.Add(usersTransactionHistory);

            var user = dbContext.Users.Where(x => x.pinNo == usersTransactionHistoryDto.pinNo).FirstOrDefault();

            if (user != null)
            {

                if (usersTransactionHistoryDto.typeOfTransaction == "withdrawal")
                {
                    user.balance = user.balance - usersTransactionHistoryDto.amount;
                }
                else
                {
                    user.balance = user.balance + usersTransactionHistoryDto.amount;
                }
            }

            try
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}