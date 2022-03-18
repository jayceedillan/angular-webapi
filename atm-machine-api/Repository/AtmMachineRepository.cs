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

        public async Task<UsersDto> getPinNo(int pinNo)
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
            ).Where(x => x.pinNo == pinNo).FirstOrDefaultAsync();

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

        public async Task<IEnumerable<UsersTransactionHistoryDto>> getAllTransactionsById(int id)
        {

            var allTransaction = await dbContext.UserTransactionHistories.Select(user =>
            new UsersTransactionHistoryDto()
            {
                userId = user.userId,
                amount = user.amount,
                transactionDate = user.transactionDate,
                typeOfTransaction = user.typeOfTransaction
            }).Where(x => x.userId == id).ToListAsync();

            return allTransaction;
        }

        public async Task<IEnumerable<UsersTransactionHistoryDto>> getAllTransactions()
        {

            var allTransaction = await dbContext.UserTransactionHistories.Select(user =>
            new UsersTransactionHistoryDto()
            {
                userId = user.userId,
                amount = user.amount,
                transactionDate = user.transactionDate,
                typeOfTransaction = user.typeOfTransaction
            }).ToListAsync();

            return allTransaction;
        }

        public async Task<bool> AddTransaction(UsersTransactionHistoryDto usersTransactionHistoryDto)
        {
            var usersTransactionHistory = new UsersTransactionHistory();
            usersTransactionHistory.amount = usersTransactionHistoryDto.amount;
            // usersTransactionHistory.pinNo = usersTransactionHistoryDto.pinNo;
            usersTransactionHistory.transactionDate = new DateTime();
            usersTransactionHistory.userId = usersTransactionHistoryDto.userId;
            usersTransactionHistory.typeOfTransaction = usersTransactionHistoryDto.typeOfTransaction;
            dbContext.UserTransactionHistories.Add(usersTransactionHistory);

            var user = dbContext.Users.Where(x => x.id == usersTransactionHistoryDto.userId).FirstOrDefault();

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

        public async Task<int> getCurrentBalanceByUser(int id)
        {
            var user = await dbContext.Users.Where(x => x.id == id).FirstOrDefaultAsync();
            return user.balance;
        }
    }

}