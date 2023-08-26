using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class DashboardByEmpresaDTO
    {
        public int GestionId { get; set; }
        [Required]
        public string SerialEquipo { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public DateTime FechaGestion { get; set; }
        [Required]
        public DateTime FechaLlegada { get; set; }
        [Required]
        public double MontoAsegurado { get; set; }
        [Required]
        public bool Empaque { get; set; }
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string DireccionRemitente { get; set; }
        [Required]
        public string DireccionDestinatario { get; set; }
        [Required]
        public string TipoEnvio { get; set; }
        [Required]
        public int NumeroTicket { get; set; }
        [Required]
        public string EstadoEquipo { get; set; }
        public bool ConfirmacionLlegada { get; set; }
        //public string CurrentUser { get; set; }
        //public string LastUser { get; set; }
        [Required]
        public string Observaciones { get; set; }
        // public string NombreEmpresa { get; set; }
    }
}
