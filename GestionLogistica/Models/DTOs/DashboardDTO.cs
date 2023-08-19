namespace GestionLogistica.Models.DTOs
{
    public class DashboardDTO
    {
        public int GestionId { get; set; } //futuro
        public string NombreEmpresa { get; set; }
        public string NombreCliente { get; set; }
        public string SerialEquipo { get; set; }
        public string Direccion { get; set; }
        public string NombreUsuario { get; set; }
        public double ValorAsegurado { get; set; }
    }
}
