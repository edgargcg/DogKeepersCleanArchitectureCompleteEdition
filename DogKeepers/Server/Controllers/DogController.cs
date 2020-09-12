using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.Metadata;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tsp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IDogService dogService;

        public DogController(
            IDogService dogService,
            IMapper mapper
        )
        {
            this.mapper = mapper;
            this.dogService = dogService;
        }

        [HttpGet(Name = nameof(GetAll))]
        public async Task<IActionResult> GetAll([FromQuery] DogListQueryFilter model)
        {
            var response = await dogService.GetList(model);

            var apiResponse = new ApiResponse<List<DogDto>>(
                response.IsDone,
                response.Message,
                mapper.Map<List<Dog>, List<DogDto>>(response.Data),
                new PaginationMetadata()
                {
                    CurrentPage = response.Data.CurrentPage,
                    PageSize = response.Data.PageSize,
                    TotalCount = response.Data.TotalCount,
                    TotalPages = response.Data.TotalPages,
                    HasNextPage = response.Data.HasNextPage,
                    HasPreviousPage = response.Data.HasPreviousPage,
                    NextPageNumber = response.Data.NextPageNumber,
                    PreviousPageNumber = response.Data.PreviousPageNumber
                }
            );

            return Ok(apiResponse);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var response = await dogService.Get(id);
            var apiResponse = new ApiResponse<DogDto>(
                response.IsDone,
                response.Message,
                mapper.Map<Dog, DogDto>(response.Data),
                null
            );

            string file = response.Data.Picture.Picture;

            apiResponse.Data.Type = "image";
            apiResponse.Data.Extension = file.Substring(file.LastIndexOf('.') + 1);
            
            return Ok(apiResponse);
        }

    }
}
