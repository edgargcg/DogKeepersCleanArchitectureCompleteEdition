using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Services
{
    public class RaceService : IRaceService
    {

        private readonly IRaceRepository raceRepository;

        public RaceService(IRaceRepository raceRepository)
        {
            this.raceRepository = raceRepository;
        }

        public async Task<List<Race>> GetList()
        {
            var races = await raceRepository.GetList();

            return races;
        }

    }
}
