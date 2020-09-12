using DogKeepers.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Services
{
    public interface IRaceService
    {

        Task<List<Race>> GetList();

    }
}
