using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.ViewModels
{
    public class ClienteRequest
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public string NombreCliente { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public string Celular { get; set; } = string.Empty;

        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public int IdEmpresa { get; set; }
    }
}
