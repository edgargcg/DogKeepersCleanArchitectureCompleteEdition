using System.ComponentModel.DataAnnotations;

namespace DogKeepers.Shared.QueryFilters
{
    public class SignInQueryFilter
    {

        [Required]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
