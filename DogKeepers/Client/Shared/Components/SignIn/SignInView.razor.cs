using DogKeepers.Client.Interfaces;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DogKeepers.Client.Shared.Components.SignIn
{
    public partial class SignInView
    {

        [Inject] private HttpClient httpClient { get; set; }
        [Inject] private ILoginService loginService { get; set; }
        [Inject] private DialogService dialogService { get; set; }

        private string Message = "";
        private bool LoadingAction = false;
        private SignInQueryFilter User = new SignInQueryFilter();

        private async Task DoSignIn()
        {
            LoadingAction = true;

            var apiResponse =
                await httpClient.PostAsJsonAsync("/api/user/authenticate", User);

            var response = await ApiResponse<JwtDto>.Create(apiResponse);
            if (response.Error == null)
            {
                await loginService.Login(response.Data);
                dialogService.Close(true);
            }
            else
                Message = response.Error.Detail;

            LoadingAction = false;
        }

    }
}
