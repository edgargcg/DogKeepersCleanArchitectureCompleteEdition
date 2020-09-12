using System;

namespace DogKeepers.Core.Entities
{
    public class Jwt
    {

        public string Token { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public User User { get; set; }

    }
}
