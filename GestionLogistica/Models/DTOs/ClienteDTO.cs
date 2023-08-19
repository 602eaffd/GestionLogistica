using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class ClienteDTO
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public string Celular { get; set; } = string.Empty;

        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public int EmpresaId { get; set; }
    }
}
