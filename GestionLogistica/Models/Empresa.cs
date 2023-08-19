using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Equipos = new HashSet<Equipo>();
        }

        public int EmpresaId { get; set; }
        public string NombreEmpresa { get; set; } = null!;
        public string ContactoEmpresa { get; set; } = null!;

        public virtual ICollection<Equipo> Equipos { get; set; }
    }
}
