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

        /*
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
        }*/

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

                // Obtener el equipo correspondiente
                Equipo equipo = _context.Equipos.FirstOrDefault(e => e.EquipoId == nuevaGestion.EquipoId);
                if (equipo != null)
                {
                    // Crear una nueva instancia de EquipoHistorialUsuario
                    EquipoHistorialUsuario historialUsuario = new EquipoHistorialUsuario
                    {
                        EquipoId = equipo.EquipoId,
                        Usuario = equipo.CurrentUser,
                        FechaRegistro = DateTime.Now
                    };

                    //Actualizar estado de equipo 
                    equipo.Estado = nuevaGestion.EstadoEquipo;
                    // Agregar la instancia al historial de usuarios
                    equipo.EquipoHistorialUsuarios.Add(historialUsuario);

                    // Actualizar LastUser con el valor actual de CurrentUser
                    equipo.LastUser = equipo.CurrentUser;

                    // Actualizar CurrentUser con el valor de NombreCliente
                    equipo.CurrentUser = nuevaGestion.NombreCliente;
                }

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

        public async Task<Respuesta> GetGestionesFiltradasFecha(DateTime fechaInicio, DateTime fechaFin, int numeroPagina = 1, int elementosPorPagina = 10)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var gestionesFiltradas = _context.Gestionenvios
                    .Where(g => g.FechaGestion >= fechaInicio && g.FechaGestion <= fechaFin)
                    .Skip((numeroPagina - 1) * elementosPorPagina)
                    .Take(elementosPorPagina)
                    .Select(g => new DashboardDTO
                    {
                        GestionId = g.GestionId,
                        SerialEquipo = g.Equipo.Serial,
                        UsuarioId = g.UsuarioId,
                        FechaGestion = g.FechaGestion,
                        FechaLlegada = g.FechaLlegada,
                        MontoAsegurado = g.MontoAsegurado,
                        Empaque = g.Empaque,
                        NombreCliente = g.NombreCliente,
                        DireccionRemitente = g.DireccionRemitente,
                        DireccionDestinatario = g.DireccionDestinatario,
                        TipoEnvio = g.TipoEnvio,
                        NumeroTicket = g.NumeroTicket,
                        EstadoEquipo = g.Equipo.Estado,
                        NombreEmpresa = g.Empresa.NombreEmpresa,
                        Observaciones = g.Observaciones,              
                        ConfirmacionLlegada = g.ConfirmacionLlegada,

                        //CurrentUser = g.Equipo.CurrentUser,
                        //LastUser = g.Equipo.LastUser,
                    })
                    .ToList();
                if (gestionesFiltradas != null && gestionesFiltradas.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestiones encontradas con éxito";
                    respuesta.Data = gestionesFiltradas;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron gestiones";
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                // Manejar el error como desees
                throw new Exception($"Ocurrió un error al obtener las gestiones: {ex}");
            }
        }

        public async Task<Respuesta> GetGestionesFiltradasPorEmpresa(DateTime fechaInicio, DateTime fechaFin, string nombreEmpresa, int numeroPagina = 1, int elementosPorPagina = 10)
        {
            Respuesta respuesta = new Respuesta();
            try
            {

                var gestionesFiltradas = _context.Gestionenvios
                    .Where(g => g.FechaGestion >= fechaInicio && g.FechaGestion <= fechaFin)
                    .Where(g => g.Empresa.NombreEmpresa == nombreEmpresa)
                    .Skip((numeroPagina - 1) * elementosPorPagina)
                    .Take(elementosPorPagina)
                    .Select(g => new DashboardByEmpresaDTO
                    {
                        GestionId = g.GestionId,
                        SerialEquipo = g.Equipo.Serial,
                        UsuarioId = g.UsuarioId,
                        FechaGestion = g.FechaGestion,
                        FechaLlegada = g.FechaLlegada,
                        MontoAsegurado = g.MontoAsegurado,
                        Empaque = g.Empaque,
                        NombreCliente = g.NombreCliente,
                        DireccionRemitente = g.DireccionRemitente,
                        DireccionDestinatario = g.DireccionDestinatario,
                        TipoEnvio = g.TipoEnvio,
                        EstadoEquipo = g.Equipo.Estado,
                        NumeroTicket = g.NumeroTicket,
                        ConfirmacionLlegada = g.ConfirmacionLlegada,
                        //CurrentUser = g.Equipo.CurrentUser,
                        //LastUser = g.Equipo.LastUser,
                        Observaciones = g.Observaciones


                    })
                    .ToList();
                if (gestionesFiltradas != null && gestionesFiltradas.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestiones encontradas con éxito";
                    respuesta.Data = gestionesFiltradas;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron gestiones";
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                // Manejar el error como desees
                throw new Exception($"Ocurrió un error al obtener las gestiones: {ex}");
            }
        }

        public async Task<Respuesta> UpdateGestion(int gestionId, DashboardActulizarGestionByEmpresaDTO gestionActualizada)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                var gestionExistente = await _context.Gestionenvios
                    .Include(g => g.Equipo) // Incluir la entidad Equipo en la consulta
                    .FirstOrDefaultAsync(g => g.GestionId == gestionId);
                if (gestionId == gestionActualizada.GestionId)
                {
                    if (gestionExistente == null)
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = $"Gestión con ID: {gestionId} no encontrada.";
                        return respuesta;
                    }

                    // Actualiza los campos necesarios con los valores del DTO actualizado
                    //gestionExistente.Equipo.Serial = gestionActualizada.SerialEquipo;
                    //gestionExistente.UsuarioId = gestionActualizada.UsuarioId;
                    //gestionExistente.FechaGestion = gestionActualizada.FechaGestion;
                    gestionExistente.FechaLlegada = gestionActualizada.FechaLlegada;
                    gestionExistente.ConfirmacionLlegada = gestionActualizada.ConfirmacionLlegada;
                    //gestionExistente.Equipo.Estado = gestionActualizada.EstadoEquipo;
                    //gestionExistente.MontoAsegurado = gestionActualizada.MontoAsegurado;
                    //gestionExistente.Empaque = gestionActualizada.Empaque;
                    //gestionExistente.NombreCliente = gestionActualizada.CurrentUser;
                    //gestionExistente.DireccionRemitente = gestionActualizada.DireccionRemitente;
                    //gestionExistente.DireccionDestinatario = gestionActualizada.DireccionDestinatario;
                    //gestionExistente.TipoEnvio = gestionActualizada.TipoEnvio;
                    //gestionExistente.NumeroTicket = gestionActualizada.NumeroTicket;
                    //gestionExistente.Equipo.CurrentUser = gestionActualizada.CurrentUser;
                    //gestionExistente.Equipo.LastUser = gestionActualizada.LastUser;


                    // Actualiza el estado del equipo si el campo NuevoEstadoEquipo tiene un valor
                    if (!string.IsNullOrEmpty(gestionActualizada.EstadoEquipo))
                    {
                        gestionExistente.Equipo.Estado = gestionActualizada.EstadoEquipo;
                    }

                    await _context.SaveChangesAsync();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestión actualizada con éxito";
                    //respuesta.Data = gestionExistente;
                }
                else
                {
                    respuesta.Mensaje = $"EL ID: {gestionId} ingresado no coincide con la gestión: {gestionActualizada.GestionId} a actualizar";
                    respuesta.Exito = 0;
                }
                
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al actualizar la gestión: {ex}";
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
                if (listaGestionesEnvios != null && listaGestionesEnvios.Count > 0)
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

        public async Task<Respuesta> GetGestionesInfoFull()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                var gestionEnvios = _context.Gestionenvios
                    .Select(g => _mapper.Map<DashboardDTO>(g))
                    .ToList();
                    if (gestionEnvios != null)
                    {
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Gestión de envío actualizada correctamente";
                        respuesta.Data = gestionEnvios;
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró la gestión de envío";
                    }
                
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al actualizar la gestión: {ex}";

            }
            return respuesta;
        }

        /*
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
        }*/

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

            }

            // Puedes agregar más validaciones según tus requisitos
            // Por ejemplo, verificar rangos de fechas, observaciones no nulas, etc.
            */
            return true;
        }

        public Task<Respuesta> Update(GestionEnvioDTO actualizarGestion, int id)
        {
            throw new NotImplementedException();
        }
    }
}
