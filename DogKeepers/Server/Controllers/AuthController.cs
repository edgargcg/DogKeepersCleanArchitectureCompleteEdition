using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogKeepers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] SignInQueryFilter model)
        {
            var response = await authService.SignIn(model);

            var apiResponse = new ApiResponse<JwtDto>(
                response.IsDone,
                response.Message,
                mapper.Map<Jwt, JwtDto>(response.Data),
                null
            );

            return Ok(apiResponse);
        }

        [Authorize]
        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == "Id").Value;
            var userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;
            



            return Ok(new { userRole });
        }

    }
}
