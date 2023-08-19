using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        public string Rol { get; set; } = "Usuario";
        [Required]
        public string Contraseña { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";
    }
}
