using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;

namespace GestionLogistica.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<Respuesta> GetAll();
        Task<Respuesta>GetById (int id);
        Task<Respuesta> Create(EmpresaDTO nuevaEmpresa);

        Task<Respuesta> Update(EmpresaDTO empresaModel, int id);

        Task<Respuesta> Delete(int id);
    }
}
