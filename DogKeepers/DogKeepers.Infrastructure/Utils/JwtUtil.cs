using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DogKeepers.Infrastructure.Utils
{
    public class JwtUtil : IJwtUtil
    {
        private readonly IConfiguration configuration;

        public JwtUtil(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Jwt Generate(User user)
        {
            if (configuration["Authentication:SecretKey"] == null)
                return null;

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredentials);


            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Creation", DateTime.Now.Ticks.ToString())
            };


            TimeSpan timeUntilMidnight = DateTime.Now.AddMinutes(6) - DateTime.Now;
            double secondsUntilMidnight = timeUntilMidnight.TotalSeconds;
            var expires = DateTime.Now.AddSeconds(secondsUntilMidnight);

            var jwtPayload = new JwtPayload(
                issuer: null,
                audience: null,
                claims,
                DateTime.Now,
                expires
            );

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new Jwt()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expires
            };
        }

    }
}
