using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Repositories
{
    public interface IBaseRepository
    {

        Task<int> Count(string command);

    }
}
