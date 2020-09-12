using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ISizeService sizeService;

        public SizeController(IMapper mapper, ISizeService sizeService)
        {
            this.mapper = mapper;
            this.sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var sizes = await sizeService.GetList();

            var response = new ApiResponse<List<SizeDto>>(
                true,
                "",
                mapper.Map<List<Size>, List<SizeDto>>(sizes),
                null
            );

            return Ok(response);
        }

    }
}
