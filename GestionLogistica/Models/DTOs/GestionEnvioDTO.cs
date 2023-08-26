using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionLogistica.Models.DTOs
{
    public class GestionEnvioDTO
    {
        public int GestionId { get; set; }
        //[JsonIgnore]
        //public int ClienteId { get; set; }
        [Required]
        public int EquipoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public DateTime FechaGestion { get; set; } = DateTime.Now;
        //public DateTime FechaLlegada { get; set; } = DateTime.Now;
        [Required]
        public string Observaciones { get; set; } = string.Empty;
        [Required]
        public double MontoAsegurado { get; set; } = 0;
        [Required]
        public bool Empaque { get; set; } = true;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public string NombreCliente { get; set; } = string.Empty;
        [Required]
        public string DireccionRemitente { get; set; } = string.Empty;
        [Required]
        public string DireccionDestinatario { get; set; } = string.Empty;
        [Required]
        public string TipoEnvio { get; set; } = string.Empty;
        [Required]
        public int NumeroTicket { get; set; }
    }

}
