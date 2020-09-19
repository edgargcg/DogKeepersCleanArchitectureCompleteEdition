using DogKeepers.Client.Interfaces;
using System;
using System.Timers;

namespace DogKeepers.Client.Helpers.Authentication
{
    public class TokenRefreshing : IDisposable
    {

        Timer timer;
        private readonly ILoginService loginService;

        public TokenRefreshing(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public void Start()
        {
            timer = new Timer();
            timer.Interval = 1000 * 60 * 4; // 1000 ms * 60 s * 4 = 4m
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Checking..");
            loginService.TaskVerifyRefreshToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
