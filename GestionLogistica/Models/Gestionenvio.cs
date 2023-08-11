using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Gestionenvio
    {
        public int GestionId { get; set; }
        public int ClienteId { get; set; }
        public int EquipoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string? Observaciones { get; set; }
        public double MontoAsegurado { get; set; }
        public bool Empaque { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Equipo Equipo { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
