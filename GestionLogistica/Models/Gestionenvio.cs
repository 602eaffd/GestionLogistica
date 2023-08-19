using System;
using System.Collections.Generic;

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
        public string? NombreCliente { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public virtual Equipo Equipo { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
