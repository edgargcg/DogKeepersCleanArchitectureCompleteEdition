using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Services
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async Task<List<Size>> GetList()
        {
            var sizes = await sizeRepository.GetList();

            return sizes;
        }
    }
}
