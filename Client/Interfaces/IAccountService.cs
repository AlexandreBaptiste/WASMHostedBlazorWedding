using Wedding.Shared.Models;

namespace Wedding.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>?> GetAccounts();
    }
}