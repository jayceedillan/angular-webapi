using atm_machine_api.Dto;
using atm_machine_api.Models;

namespace atm_machine_api.Repository
{
    public interface IAtmMachineRepository
    {
        Task<UsersDto> getPinNo(int PinNo);
        Task<IEnumerable<UsersDto>> getAllUsers();

        Task<IEnumerable<UsersTransactionHistoryDto>> getAllTransaction(int pinNo);
        Task<bool> AddTransaction(UsersTransactionHistoryDto usersTransactionHistoryDto);

    }
}