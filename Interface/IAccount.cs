using FoodAplication.Dtos;
using FoodAplication.Models;

namespace FoodAplication.Interface
{
    public interface IAccount
    {
        Task<Account> GetAccountByEmail(string email);
        Task<Account> GetAccountByPhoneNumber(string phone);
        Task<object> RegisterAccount(Account account);
        Task<object> LoginAccount(LoginDto login);
    }
}
