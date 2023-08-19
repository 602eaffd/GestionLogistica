using System;
using System.Collections.Generic;

namespace GestionLogistica.Models
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int EmpresaId { get; set; }
    }
}
