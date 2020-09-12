using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Core.Response;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogKeepers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SignUpQueryFilter model)
        {
            var response = await userService.Post(model);
            var apiResponse = new ApiResponse<UserDto>(
                response.IsDone,
                response.Message,
                mapper.Map<User, UserDto>(response.Data),
                null
            );

            return Ok(apiResponse);
        }


    }
}
