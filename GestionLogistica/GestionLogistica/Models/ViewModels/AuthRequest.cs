using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.ViewModels
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
