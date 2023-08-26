namespace GestionLogistica.Models.DTOs
{
    public class DashboardActulizarGestionByEmpresaDTO
    {
        public int GestionId { get; set; }
        public string SerialEquipo { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime FechaLlegada { get; set; }
        public double MontoAsegurado { get; set; }
        public bool Empaque { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionRemitente { get; set; }
        public string DireccionDestinatario { get; set; }
        public string TipoEnvio { get; set; }
        public int NumeroTicket { get; set; }
        public string EstadoEquipo { get; set; }
        public string Observaciones { get; set; }
        //public string CurrentUser { get; set; }
        public string LastUser { get; set; }
        // public string NombreEmpresa { get; set; }
    }
}
