using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GestionLogistica.Models
{
    public partial class Gestionenvio
    {
        public int GestionId { get; set; }
        public int EquipoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string Observaciones { get; set; } = null!;
        public double MontoAsegurado { get; set; }
        public bool Empaque { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DireccionRemitente { get; set; } = null!;
        public string DireccionDestinatario { get; set; } = null!;
        public string TipoEnvio { get; set; } = null!;
        public int NumeroTicket { get; set; }
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public virtual Empresa Empresa { get; set; } = null!;
        [JsonIgnore]
        public virtual Equipo Equipo { get; set; } = null!;
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
