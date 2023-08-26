using System.ComponentModel.DataAnnotations;

namespace GestionLogistica.Models.ViewModels
{
    public class EquipoDTO
    {
        public int EquipoId { get; set; }
        [Required]
        public string Modelo { get; set; } = string.Empty; 
        [Required]
        public string Cpu { get; set; } = string.Empty; // Corregir aquí
        [Required]
        public string Ram { get; set; } = string.Empty;
        [Required]
        public string Marca { get; set; } = string.Empty;
        [Required]
        public bool CargadorEquipo { get; set; } = true;
        [Required]
        public string Serial { get; set; } = string.Empty;
        [Required]
        public string Detalles { get; set; } = string.Empty;
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required]
        public string Propietario { get; set; } = string.Empty;
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public string CurrentUser { get; set; } = string.Empty; 
        [Required]
        public string LastUser { get; set; } = string.Empty;
    }
}
