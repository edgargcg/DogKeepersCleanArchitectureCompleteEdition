using DogKeepers.Core.Entities;
using DogKeepers.Core.Exceptions;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Core.Response;
using DogKeepers.Shared.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DogKeepers.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Response<User>> Post(SignUpQueryFilter model)
        {
            var existsUser = await userRepository.GetByEmailPhone(model.Email, model.Phone);

            if (existsUser)
                throw new BusinessException("Ya existe un usuario con el correo electrónico y/o teléfono que indició");

            var user = await userRepository.Post(model);

            if(user == null)
                throw new BusinessException("Ha ocurrido un error al intentar registrarlo, intente nuevamente");

            return new Response<User>(true, "Usuario registrado", user); ;
        }
    }
}
