using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services.Interfaces
{
    public interface IEquipoService
    {
        Task<Respuesta> GetAll();
        Task<Respuesta> GetById(int id);
        Task<Respuesta> Create(EquipoDTO nuevaEmpresa);

        Task<Respuesta> Update(EquipoDTO empresaModel, int id);

        Task<Respuesta> Delete(int id);
    }
}
