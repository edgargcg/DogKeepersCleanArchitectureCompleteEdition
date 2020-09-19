using DogKeepers.Core.Entities;
using DogKeepers.Shared.QueryFilters;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Repositories
{
    public interface IAdministratorRepository
    {

        Task<Administrator> GetAuth(SignInQueryFilter model);

    }
}
