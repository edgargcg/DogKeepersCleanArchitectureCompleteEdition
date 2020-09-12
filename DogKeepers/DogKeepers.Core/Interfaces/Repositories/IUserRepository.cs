using DogKeepers.Core.Entities;
using DogKeepers.Shared.QueryFilters;
using System.Threading.Tasks;

namespace DogKeepers.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<User> Get(int id);
        Task<User> Post(SignUpQueryFilter model);
        Task<User> GetAuth(SignInQueryFilter model);
        Task<bool> GetByEmailPhone(string email, string phone);

    }
}
