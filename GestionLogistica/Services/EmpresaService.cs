using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services
{
    
    public class EmpresaService : IEmpresaService
    {
        public readonly GestionLogisticaContext _context;
        private readonly IMapper _mapper;


        public EmpresaService(GestionLogisticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Respuesta> GetAll()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                List<Empresa> listaEmpresas = await _context.Empresas.ToListAsync();

                if (listaEmpresas != null && listaEmpresas.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresas encontradas correctamente";
                    respuesta.Data = listaEmpresas;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron empresas";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar las empresas: " + ex.Message;
            }
            return respuesta;
        }


        public async Task<Respuesta> GetById(int id)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                if (!IsValidId(id))
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "El ID no puede ser nulo.";
                }
                else
                {
                    var gestionEnvio = await _context.Empresas.FindAsync(id);
                    if (gestionEnvio != null)
                    {
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Empresa encontrada correctamente";
                        respuesta.Data = gestionEnvio;
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró la empresa";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al buscar las gestiones: {ex}";

            }
            return respuesta;
        }



        public async Task<Respuesta> Create(EmpresaDTO EmpresaNueva)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Empresa empresa = _mapper.Map<Empresa>(EmpresaNueva);
                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                respuesta.Exito = 1;
                respuesta.Mensaje = "Empresa creada con éxito";
                respuesta.Data = empresa;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear la empresa: " + ex.Message;
            }
            return respuesta;
        }
       
        public async Task<Respuesta> Update(EmpresaDTO empresa, int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                //var existeEmpresa = await GetById(id);
                var existeEmpresa = _context.Empresas.FirstOrDefault(e => e.EmpresaId == id);
                if (existeEmpresa != null)
                {
                    _mapper.Map(empresa, existeEmpresa); // Actualizar propiedades de la entidad existente
                    await _context.SaveChangesAsync();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa actualizada con éxito";
                    respuesta.Data = existeEmpresa;
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

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                //var eliminarEmpresa = await GetById(id);
                var eliminarEmpresa = _context.Empresas.FirstOrDefault(e => e.EmpresaId == id);
                if (eliminarEmpresa != null)
                {
                    _context.Empresas.Remove(eliminarEmpresa);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa eliminada con éxito";
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

        private bool IsValidId(int id)
        {
            return id > 0; // Por ejemplo, podría ser positivo para ser válido

        }
    }
}
