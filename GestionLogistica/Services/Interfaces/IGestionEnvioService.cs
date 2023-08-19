using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;

namespace GestionLogistica.Services.Interfaces
{
    public interface IGestionEnvioService
    {
        Task<Respuesta> GetAll();
        Task<Respuesta> GetById(int id);
        Task<Respuesta> Create(GestionEnvioDTO nuevaGestion);

        Task<Respuesta> Update(GestionEnvioDTO actualizarGestion, int id);

        Task<Respuesta> Delete(int id);
    }
}
