using DogKeepers.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Services
{
    public interface ISizeService
    {

        Task<List<Size>> GetList();

    }
}
