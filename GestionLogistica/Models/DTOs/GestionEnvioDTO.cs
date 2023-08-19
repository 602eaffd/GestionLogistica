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
        public DateTime FechaGestion { get; set; } = DateTime.Now;
        public DateTime FechaLlegada { get; set; } = DateTime.Now;
        [Required]
        public string Observaciones { get; set; } = string.Empty;
        [Required]
        public double MontoAsegurado { get; set; } = 0;
        [Required]
        public bool Empaque { get; set; } = true;
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NombreCliente { get; set; }
    }

}
