using System.Threading.Tasks;
using Wedding.Shared.Models;

namespace Wedding.Interfaces
{
    public interface IguestsService
    {
        Task<IEnumerable<Guest>?> Getguests();

        Task<bool> Updateguest(Guest guest);
    }
}
