using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionLogistica.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Equipos = new HashSet<Equipo>();
            Gestionenvios = new HashSet<Gestionenvio>();
        }

        public int EmpresaId { get; set; }
        public string NombreEmpresa { get; set; } = null!; 
        public string ContactoEmpresa { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Equipo> Equipos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Gestionenvio> Gestionenvios { get; set; }
    }
}
