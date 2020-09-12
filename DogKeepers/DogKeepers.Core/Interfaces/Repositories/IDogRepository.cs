using DogKeepers.Core.Entities;
using DogKeepers.Shared.QueryFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Repositories
{
    public interface IDogRepository
    {

        Task<Tuple<int, List<Dog>>> GetList(DogListQueryFilter model);
        Task<Dog> Get(long id);

    }
}
