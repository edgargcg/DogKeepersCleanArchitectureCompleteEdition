using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {

        private readonly IRaceService raceService;
        private readonly IMapper mapper;

        public RaceController(IRaceService raceService, IMapper mapper)
        {
            this.raceService = raceService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = await raceService.GetList();

            var apiResponse = new ApiResponse<List<RaceDto>>(
                true,
                "",
                mapper.Map<List<Race>, List<RaceDto>>(response),
                new PaginationMetadata()
                {
                    CurrentPage = 0,
                    PageSize = 0,
                    TotalCount = 0,
                    TotalPages = 0,
                    HasNextPage = false,
                    HasPreviousPage = false,
                    NextPageNumber = 0,
                    PreviousPageNumber = 0
                }
            ); ;

            return Ok(apiResponse);
        }

    }
}
