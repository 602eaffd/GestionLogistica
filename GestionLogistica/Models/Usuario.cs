using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<Gestionenvio> Gestionenvios { get; set; }
    }
}
