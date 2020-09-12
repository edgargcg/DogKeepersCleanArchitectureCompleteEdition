using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DogKeepers.Client.Shared.Components.SignUp
{
    public partial class SignUpView
    {

        [Inject] private HttpClient httpClient { get; set; }
        [Inject] private DialogService dialogService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }


        private string Message = "";
        private bool LoadingAction = false;
        private SignUpQueryFilter User = new SignUpQueryFilter();

        private async Task Create()
        {
            LoadingAction = true;
            var apiResponse =
                await httpClient.PostAsJsonAsync("api/user/signup", User);

            var response = await ApiResponse<UserDto>.Create(apiResponse);
            if (response.Error == null)
            {
                navigationManager.NavigateTo("/SignIn");
            }
            else
            {
                Message = response.Error.Detail;
                //notificationService.Notify(
                //    new NotificationMessage()
                //    {
                //        Severity = NotificationSeverity.Warning,
                //        Duration = 3000,
                //        Detail = response.Error.Detail,
                //        Summary = "Atención.."
                //    }
                //);
                //await Task.Delay(3000);
            }
            LoadingAction = false;
        }

    }
}
