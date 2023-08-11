using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int EmpresaId { get; set; }
        public string NombreEmpresa { get; set; } = null!;
        public string ContactoEmpresa { get; set; } = null!;

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
