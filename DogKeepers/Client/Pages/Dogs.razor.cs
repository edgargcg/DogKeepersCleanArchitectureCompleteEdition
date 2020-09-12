using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.Metadata;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DogKeepers.Client.Pages
{
    public partial class Dogs
    {

        [Inject] private HttpClient httpClient { get; set; }

        public bool IsLoading = true;
        public int? TotalDogs = (int?)null;
        public List<DogDto> DogsList = null;
        public List<RaceDto> RacesList = null;
        public List<SizeDto> SizesList = null;
        public PaginationMetadata Pagination = null;
        public DogListQueryFilter Filters = new DogListQueryFilter() { Random = 0};

        protected override async Task OnInitializedAsync()
        {
            await GetRaces();
            await GetSizes();
            await GetDogs();
        }

        private async Task GetRaces()
        {
            var apiResponse =
              await httpClient.GetAsync("api/Race");

            var response = await ApiResponse<List<RaceDto>>.Create(apiResponse);

            RacesList = response.Data;
        }

        private async Task GetSizes()
        {
            var apiResponse =
                await httpClient.GetAsync("api/Size");

            var response = await ApiResponse<List<SizeDto>>.Create(apiResponse);

            SizesList = response.Data;
        }

        private async Task GetDogs(bool searching = false)
        {
            StateHasChanged();

            if (searching)
            {
                DogsList = null;
                IsLoading = true;
            }

            Filters.ForcePageNumber =
                Pagination == null || searching
                    ? 1
                    : Filters.PageNumber;

            var filters = $"?Name={Filters.Name}&SizeId={Filters.SizeId}&RaceId={Filters.RaceId}&PageNumber={Filters.PageNumber}";

            var apiResponse =
               await httpClient.GetAsync($"api/Dog{filters}");

            var response = await ApiResponse<List<DogDto>>.Create(apiResponse);

            DogsList = response.Data;
            Pagination = response.PaginationMetadata;
            TotalDogs = response.PaginationMetadata.TotalCount;

            IsLoading = false;
        }

        public async Task OnClick()
        {
            await GetDogs(true);
        }

        public async Task SelectedPage(int page)
        {
            this.Filters.ForcePageNumber = page;

            await GetDogs();
        }

        void OnChangeRadzenDropDown() {
            StateHasChanged();
        }

    }
}
