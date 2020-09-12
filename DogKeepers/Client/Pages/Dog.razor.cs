using DogKeepers.Client.Shared.Components.DogCard;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DogKeepers.Client.Pages
{
    public partial class Dog
    {

        [Inject] private HttpClient httpClient { get; set; }
        [Inject] private NavigationManager navManager { get; set; }

        [Parameter] public int id { get; set; }

        private Boolean IsLoading;
        private DogDto DogData = null;
        private string DogPicture = null;
        private DogDto DogSuggestion = null;
        private string DogPictureType = null;

        protected override async Task OnParametersSetAsync()
        {
            IsLoading = true;
            await GetSuggestion();
            await GetDogInformation();
        }

        //protected override async Task OnInitializedAsync()
        //{
        //    IsLoading = true;
        //    await GetSuggestion();
        //    await GetDogInformation();
        //}

        private async Task GetDogInformation()
        {
            DogData = null;

            var apiResponse =
                await httpClient.GetAsync($"api/Dog/{id}");

            var response = await ApiResponse<DogDto>.Create(apiResponse);

            if (response.Error != null)
                navManager.NavigateTo("/dogs");
            else
            {
                DogData = response.Data;
                DogPicture = Convert.ToBase64String(DogData.PictureFile);
                DogPictureType = DogData.Extension == "svg" ? "svg+xml" : DogData.Extension;
                IsLoading = false;
            }
        }

        private async Task GetSuggestion()
        {
            DogSuggestion = null;

            var apiResponse =
                await httpClient.GetAsync($"api/Dog?random=1");

            var response = await ApiResponse<List<DogDto>>.Create(apiResponse);
            DogSuggestion = response.Data.FirstOrDefault();            
        }

    }
}
