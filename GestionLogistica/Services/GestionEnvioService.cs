using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services
{
    
    public class GestionEnvioService : IGestionEnvioService
    {
        private readonly GestionEnvioService _service;
        //private readonly ILogger _logger;
        private readonly GestionLogisticaContext _context;
        private readonly IMapper _mapper;


        public GestionEnvioService(GestionLogisticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Respuesta> Create(GestionEnvioDTO nuevaGestion)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                if (!IsValidDto(nuevaGestion))
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Validar los datos ingresados";
                }
                Gestionenvio gestionEnvio = _mapper.Map<Gestionenvio>(nuevaGestion);
                _context.Gestionenvios.Add(gestionEnvio);
                await _context.SaveChangesAsync();
                respuesta.Exito = 1;
                respuesta.Mensaje = "Gestión de envío creada con éxito";
                respuesta.Data = gestionEnvio;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al crear la gestión: {ex}";

            }
            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
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
                    var gestionEnvio = await _context.Gestionenvios.FindAsync(id);
                    if (gestionEnvio != null)
                    {
                        _context.Gestionenvios.Remove(gestionEnvio);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Gestión de envío eliminada correctamente";
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró la gestión de envío";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al eliminar las gestiones: {ex}";

            }
            return respuesta;
        }

        public async Task<Respuesta> GetAll()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                List<Gestionenvio> listaGestionesEnvios = await _context.Gestionenvios.ToListAsync();
                if(listaGestionesEnvios != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestiones de envío encontradas correctamente";
                    respuesta.Data = listaGestionesEnvios;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron gestiones de envío";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al buscar las gestiones: {ex}";
                
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
                    var gestionEnvio = await _context.Gestionenvios.FindAsync(id);
                    if (gestionEnvio != null)
                    {
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Gestiones de envío encontradas correctamente";
                        respuesta.Data = gestionEnvio;
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró la gestión de envío";
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

        public async Task<Respuesta> Update(GestionEnvioDTO actualizarGestion, int id)
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
                    var existeGestionCliente = await _context.Gestionenvios.FindAsync(id);
                    if (existeGestionCliente != null)
                    {
                        _mapper.Map(actualizarGestion, existeGestionCliente);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Gestión de envío actualizada correctamente";
                        respuesta.Data = existeGestionCliente;
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró la gestión de envío";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al actualizar la gestión: {ex}";

            }
            return respuesta;
        }

        private bool IsValidId(int id)
        {
            return id > 0; // Por ejemplo, podría ser positivo para ser válido

        }

        private bool IsValidDto(GestionEnvioDTO dto)
        {
            Respuesta respuesta = new Respuesta();
            if (dto == null)
            {
                return false;
            }

            // Verificar propiedades requeridas u otros criterios de validación
            /*if (dto.ClienteId <= 0 || dto.EquipoId <= 0 || dto.UsuarioId <= 0)
            {

                return false;

            }*/

            // Puedes agregar más validaciones según tus requisitos
            // Por ejemplo, verificar rangos de fechas, observaciones no nulas, etc.

            return true;
        }

    }
}
