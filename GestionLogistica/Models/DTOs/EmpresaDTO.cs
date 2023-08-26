using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.DTOs
{
    public class EmpresaDTO
    {
        public int EmpresaId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre de la empresa debe ser menor a 50 caracteres.")]
        public string NombreEmpresa { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El correo de la empresa debe ser menor a 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Debes ingresar una dirección de correo válida")]
        public string ContactoEmpresa { get; set; }
    }
}
