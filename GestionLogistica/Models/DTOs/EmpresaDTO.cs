using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class EmpresaDTO
    {
        public int EmpresaId { get; set; }
        [Required]
        public string NombreEmpresa { get; set; }
        [Required]
        public string ContactoEmpresa { get; set; }
    }
}
