using DogKeepers.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Repositories
{
    public interface IRaceRepository
    {

        Task<List<Race>> GetList();

    }
}
