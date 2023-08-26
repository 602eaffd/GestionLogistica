using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class EquipoHistorialUsuario
    {
        public int Id { get; set; }
        public int EquipoId { get; set; }
        public string Usuario { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }

        public virtual Equipo Equipo { get; set; } = null!;
    }
}
