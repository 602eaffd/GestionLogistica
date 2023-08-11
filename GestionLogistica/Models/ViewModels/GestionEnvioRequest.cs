namespace GestionLogistica.Models.ViewModels
{
    public class GestionEnvioRequest
    {
        public int GestionId { get; set; }
        public int ClienteId { get; set; }
        public int EquipoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaGestion { get; set; } = DateTime.Now;
        public DateTime FechaLlegada { get; set; } = DateTime.Now;
        public string Observaciones { get; set; } = string.Empty;
        public double MontoAsegurado { get; set; } = 0;
        public bool Empaque { get; set; } = true;
    }

}
