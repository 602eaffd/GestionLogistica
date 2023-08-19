using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;

namespace GestionLogistica.Services.Interfaces
{
    public interface IClienteService
    {
        Task<Respuesta> GetAll();
        Task<Respuesta> GetById(int id);
        Task<Respuesta> Create(ClienteDTO nuevaCliente);

        Task<Respuesta> Update(ClienteDTO clienteModel, int id);

        Task<Respuesta> Delete(int id);
    }
}
