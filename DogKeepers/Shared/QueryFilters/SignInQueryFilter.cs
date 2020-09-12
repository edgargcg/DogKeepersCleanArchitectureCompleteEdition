using System.ComponentModel.DataAnnotations;

namespace DogKeepers.Shared.QueryFilters
{
    public class SignInQueryFilter
    {

        [Required(ErrorMessage = "Este dato es necesario")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este dato es necesario")]
        public string Password { get; set; }

    }
}
