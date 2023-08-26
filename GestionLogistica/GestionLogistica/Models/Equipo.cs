using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            Gestionenvios = new HashSet<Gestionenvio>();
        }

        public int EquipoId { get; set; }
        public string Serial { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Cpu { get; set; } = null!;
        public string Ram { get; set; } = null!;
        public bool? CargadorEquipo { get; set; }

        public virtual ICollection<Gestionenvio> Gestionenvios { get; set; }
    }
}
