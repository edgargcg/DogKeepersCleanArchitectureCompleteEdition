using System;

namespace DogKeepers.Core.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Adoptionrating { get; set; }
        public string ProfilePicture { get; set; }

    }
}
