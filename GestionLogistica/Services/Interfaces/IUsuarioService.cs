using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;

namespace GestionLogistica.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Respuesta> GetAll();
        Task<Respuesta> GetById(int id);
        Task<Respuesta> Create(UsuarioDTO nuevoUsuario);

        Task<Respuesta> Update(UsuarioDTO actualizarUsuario, int id);

        Task<Respuesta> Delete(int id);
    }
}
