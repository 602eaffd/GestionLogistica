using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Gestionenvios = new HashSet<Gestionenvio>();
        }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Rol { get; set; }
        public string Contraseña { get; set; } = null!;
        public string? Estado { get; set; }

        public virtual ICollection<Gestionenvio> Gestionenvios { get; set; }
    }
}
