using DogKeepers.Core.Entities;
using DogKeepers.Core.Response;
using DogKeepers.Shared.QueryFilters;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Services
{
    public interface IUserService
    {

        Task<Response<User>> Post(SignUpQueryFilter model);
        Task<Response<Jwt>> SignIn(SignInQueryFilter model);

    }
}
