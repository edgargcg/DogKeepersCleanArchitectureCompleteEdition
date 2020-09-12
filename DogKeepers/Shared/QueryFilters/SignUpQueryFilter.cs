using System;
using System.ComponentModel.DataAnnotations;

namespace DogKeepers.Shared.QueryFilters
{
    public class SignUpQueryFilter
    {

        [Required(ErrorMessage = "Este dato es requerido")]
        [MinLength(5, ErrorMessage = "El nombre debe tener al menos 5 letras")]
        [MaxLength(90, ErrorMessage = "El nombre debe tener máximo 90 letras")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese un correo elelectrónico válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public DateTime? Birthday { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        [Phone(ErrorMessage = "Ingrese un teléfono válido")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        [Compare("Password", ErrorMessage = "No coinciden las contraseñas")]
        public string ConfirmPassword { get; set; }

    }
}
