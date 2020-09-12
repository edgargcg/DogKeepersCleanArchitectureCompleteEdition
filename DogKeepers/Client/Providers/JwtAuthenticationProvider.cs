using Blazored.LocalStorage;
using DogKeepers.Client.Interfaces;
using DogKeepers.Client.Options;
using DogKeepers.Shared.ApiResponse;
using DogKeepers.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace DogKeepers.Client.Providers
{
    public class JwtAuthenticationProvider : AuthenticationStateProvider, ILoginService
    {

        private readonly HttpClient httpClient;
        private readonly LocalStorageOption localStorageOption;
        private readonly ILocalStorageService localStorageService;
        private AuthenticationState anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthenticationProvider(
            ILocalStorageService localStorage,
            HttpClient httpClient,
            LocalStorageOption localStorageOption
        )
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorage;
            this.localStorageOption = localStorageOption;
        }

        public async Task Login(JwtDto token)
        {
            await localStorageService.SetItemAsync(localStorageOption.Token, token.Token);
            await localStorageService.SetItemAsync(localStorageOption.ExpirationDate, token.ExpirationDate.ToString());

            var authState = CreateAuthenticationState(token.Token);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await CleanJwtStorage();
            NotifyAuthenticationStateChanged(Task.FromResult(anonymous));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await localStorageService.GetItemAsync<string>(localStorageOption.Token);

            if (string.IsNullOrEmpty(jwt))
            {
                return anonymous;
            }

            var expirationTimeString = await localStorageService.GetItemAsync<string>(localStorageOption.ExpirationDate);
            DateTime expirationTime;

            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsExpiredToken(expirationTime))
                {
                    await CleanJwtStorage();
                    return anonymous;
                }

                if (IsRequiredRefreshToken(expirationTime))
                {
                    var token = await RefreshToken(jwt);

                    if (String.IsNullOrEmpty(token))
                    {
                        return anonymous;
                    }
                    else
                    {
                        jwt = token;
                    }
                }
            }

            return CreateAuthenticationState(jwt);
        }

        public async Task TaskVerifyRefreshToken()
        {
            var expirationTimeString = await localStorageService.GetItemAsync<string>(localStorageOption.ExpirationDate);
            DateTime expirationTime;

            if (DateTime.TryParse(expirationTimeString, out expirationTime))
            {
                if (IsExpiredToken(expirationTime))
                {
                    await Logout();
                }

                if (IsRequiredRefreshToken(expirationTime))
                {
                    var jwt = await localStorageService.GetItemAsync<string>(localStorageOption.Token);
                    var newJwt = await RefreshToken(jwt);

                    if (String.IsNullOrEmpty(newJwt))
                    {
                        await Logout();
                    }
                    else
                    {
                        var authState = CreateAuthenticationState(newJwt);
                        NotifyAuthenticationStateChanged(Task.FromResult(authState));
                    }
                }
            }
        }

        private async Task<string> RefreshToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var apiResponse =
                await
                httpClient.GetAsync(
                    $"Auth/"
                );

            var response = await ApiResponse<JwtDto>.Create(apiResponse);
            if (response.Error != null)
            {
                return null;
            }
            else
            {
                await localStorageService.SetItemAsync(localStorageOption.Token, response.Data.Token);
                await localStorageService.SetItemAsync(localStorageOption.ExpirationDate, response.Data.Token.ToString());

                return response.Data.Token;
            }
        }

        private bool IsRequiredRefreshToken(DateTime expirationTime)
        {
            return expirationTime.Subtract(DateTime.Now) < TimeSpan.FromMinutes((int)localStorageOption.MinutesLeftToRefresh);
        }

        private bool IsExpiredToken(DateTime expirationTime)
        {
            return expirationTime <= DateTime.Now;
        }

        private async Task CleanJwtStorage()
        {
            await localStorageService.RemoveItemAsync(localStorageOption.Token);
            await localStorageService.RemoveItemAsync(localStorageOption.ExpirationDate);

            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public AuthenticationState CreateAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var claims = ParseClaimsFromJwt(token);
            var user = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "JWT")));

            return user;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}
