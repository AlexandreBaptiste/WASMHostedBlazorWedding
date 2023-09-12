using Wedding.Shared.Models;

namespace Wedding.Interfaces
{
    public interface IGuestService
    {
        Task<IEnumerable<Guest>?> Getguests();
    }
}