using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services
{
    public class EquipoService : IEquipoService
    {
        public readonly GestionLogisticaContext _context;
        private readonly IMapper _mapper;


        public EquipoService(GestionLogisticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Respuesta> GetAll()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                List<Equipo> listaEquipos = await _context.Equipos.ToListAsync();

                if (listaEquipos != null && listaEquipos.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipos encontradas correctamente";
                    respuesta.Data = listaEquipos;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron equipos";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar los equipos: " + ex.Message;
            }

            return respuesta;
        }


        public async Task<Equipo> GetById(int id)
        {
            return await _context.Equipos.FindAsync(id);
        }



        public async Task<Respuesta> Create(EquipoDTO equipoNuevo)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Equipo equipo = _mapper.Map<Equipo>(equipoNuevo);
                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                respuesta.Exito = 1;
                respuesta.Mensaje = "Equipo creado con éxito";
                respuesta.Data = equipo;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear el equipo: " + ex.Message;
            }
            return respuesta;
        }

        public async Task<Respuesta> Update(EquipoDTO equipo, int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existeEquipo = await GetById(id);
                //var existeEmpresa = _context.Empresas.FirstOrDefault(e => e.EmpresaId == id);
                if (existeEquipo != null)
                {
                    _mapper.Map(equipo, existeEquipo); // Actualizar propiedades de la entidad existente
                    await _context.SaveChangesAsync();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipo actualizado con éxito";
                    respuesta.Data = existeEquipo;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Equipo no encontrado";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al actualizar el equipo: " + ex.Message;
            }
            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var eliminarEquipo = await GetById(id);
                //var existeEmpresa = _context.Empresas.FirstOrDefault(e => e.EmpresaId == id);
                if (eliminarEquipo != null)
                {
                    _context.Equipos.Remove(eliminarEquipo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipo eliminado con éxito";
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Empresa no encontrada";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al actualizar la empresa: " + ex.Message;
            }
            return respuesta;
        }
    }
}
