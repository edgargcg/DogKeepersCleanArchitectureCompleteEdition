using DogKeepers.Shared.QueryFilters;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Net.Http;

namespace DogKeepers.Client.Shared.Components.SignIn
{
    public partial class SignInView
    {

        [Inject] private HttpClient httpClient { get; set; }
        [Inject] private DialogService dialogService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        private string Message = "";
        private bool LoadingAction = false;
        private SignInQueryFilter User = new SignInQueryFilter();

        private void DoSignIn()
        {
            Console.WriteLine("Login");
        }

    }
}
