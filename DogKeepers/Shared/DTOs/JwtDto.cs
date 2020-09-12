using System;

namespace DogKeepers.Shared.DTOs
{
    public class JwtDto
    {

        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public UserDto User { get; set; }

    }
}
