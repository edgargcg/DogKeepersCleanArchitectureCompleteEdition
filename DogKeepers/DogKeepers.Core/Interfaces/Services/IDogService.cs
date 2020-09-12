using DogKeepers.Core.Entities;
using DogKeepers.Core.Response;
using DogKeepers.Core.Utils;
using DogKeepers.Shared.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Services
{
    public interface IDogService
    {

        Task<Response<PagedList<Dog>>> GetList(DogListQueryFilter model);
        Task<Response<Dog>> Get(long id);

    }

}
