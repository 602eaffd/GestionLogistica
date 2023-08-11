namespace GestionLogistica.Models.ViewModels
{
    public class EquipoComputoRequest
    {
        public int EquipoId { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Cpu { get; set; } = string.Empty; // Corregir aquí
        public string Ram { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public bool CargadorEquipo { get; set; } = true;
        public string Serial { get; set; } = string.Empty;
    }
}
