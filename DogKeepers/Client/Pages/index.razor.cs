using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DogKeepers.Client.Pages
{
    public partial class Index
    {

        [Inject] private HttpClient httpClient { get; set; }

        List<DogDto> DogsList = null;
        bool IsLoading = true;

        protected override async Task OnInitializedAsync()
        {
            var apiResponse =
                await
                    httpClient.GetAsync("api/Dog?random=3");

            var response = await ApiResponse<List<DogDto>>.Create(apiResponse);
            DogsList = response.Data;
            IsLoading = false;
        }

    }
}
