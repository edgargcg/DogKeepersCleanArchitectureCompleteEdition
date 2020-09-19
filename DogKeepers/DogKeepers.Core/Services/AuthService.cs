using DogKeepers.Core.Entities;
using DogKeepers.Core.Exceptions;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Core.Interfaces.Utils;
using DogKeepers.Core.Response;
using DogKeepers.Shared.QueryFilters;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Core.Services
{
    public class AuthService : IAuthService
    {

        private readonly IJwtUtil jwtUtil;
        private readonly IUserRepository userRepository;
        private readonly IAdministratorRepository administratorRepository;

        public AuthService(IJwtUtil jwtUtil, IUserRepository userRepository, IAdministratorRepository administratorRepository)
        {
            this.jwtUtil = jwtUtil;
            this.userRepository = userRepository;
            this.administratorRepository = administratorRepository;
        }

        public async Task<Response<Jwt>> SignIn(SignInQueryFilter model)
        {
            User user = await userRepository.GetAuth(model);
            Administrator administrator = await administratorRepository.GetAuth(model);

            if (user == null && administrator == null)
                throw new BusinessException("Los datos que ingresaste no coinciden con ninguna cuenta");

            var data = 
                new {
                    Id = user != null ? user.Id : administrator.Id,
                    Role = user != null ? "user" : "administrator"
                };

            var token = jwtUtil.Generate(data);
            if (token == null)
                throw new BusinessException("Los datos que ingresaste no coinciden con ninguna cuenta");

            token.User = user;
            token.Administrator = administrator;

            return new Response<Jwt>(true, "", token);
        }
    }
}
