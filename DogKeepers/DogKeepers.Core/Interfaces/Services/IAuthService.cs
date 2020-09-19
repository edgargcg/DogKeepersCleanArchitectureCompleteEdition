using DogKeepers.Core.Entities;
using DogKeepers.Core.Response;
using DogKeepers.Shared.QueryFilters;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Response<Jwt>> SignIn(SignInQueryFilter model);

    }
}
