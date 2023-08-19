using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class AuthDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
