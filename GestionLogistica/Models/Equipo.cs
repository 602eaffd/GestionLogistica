using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public bool CargadorEquipo { get; set; }
        public int EmpresaId { get; set; }
        public string Detalles { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Propietario { get; set; } = null!;
        public string CurrentUser { get; set; } = null!;
        public string LastUser { get; set; } = null!;
        [JsonIgnore]
        public virtual Empresa Empresa { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Gestionenvio> Gestionenvios { get; set; }
    }
}
