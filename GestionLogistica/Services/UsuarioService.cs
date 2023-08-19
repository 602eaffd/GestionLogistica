using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly GestionLogisticaContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(GestionLogisticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Respuesta> Create(UsuarioDTO nuevoUsuario)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(nuevoUsuario);
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                respuesta.Exito = 1;
                respuesta.Mensaje = "Usuario creado con éxito";
                respuesta.Data = usuario;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear el usuario: " + ex.Message;
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
                    var usuario = await _context.Usuarios.FindAsync(id);
                    if (usuario != null)
                    {
                        _context.Usuarios.Remove(usuario);
                        await _context.SaveChangesAsync();
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Usuario eliminado correctamente";
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No se encontró el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al eliminar el usuario: {ex}";

            }
            return respuesta;
        }

        public async Task<Respuesta> GetAll()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                List<Usuario> listaUsuarios = await _context.Usuarios.ToListAsync();
                if (listaUsuarios != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Usuarios encontrados correctamente";
                    respuesta.Data = listaUsuarios;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "No se encontraron usuarios";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al encontrar usuarios: {ex}";

            }
            return respuesta;
        }

        public async Task<Respuesta?> GetById(int id)
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
                    var usuario = await _context.Usuarios.FindAsync(id);
                    if (usuario != null)
                    {
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Usuario encontrado correctamente";
                        respuesta.Data = usuario;
                    }
                    else
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = $"No se encontró el usuario con ID: {id}";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al buscar el usuario: {ex}";

            }
            return respuesta;
        }

        public async Task<Respuesta> Update(UsuarioDTO actualizarUsuario, int id)
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
                    if (id != actualizarUsuario.UsuarioId)
                    {
                        respuesta.Exito = 0;
                        respuesta.Mensaje = $"El ID: {id} no coincide con el ID {actualizarUsuario.UsuarioId} ingresado";
                    }
                    else
                    {
                        var existeUsuario = await _context.Usuarios.FindAsync(id);
                        if (existeUsuario != null)
                        {
                            _mapper.Map(actualizarUsuario, existeUsuario);
                            await _context.SaveChangesAsync();
                            respuesta.Exito = 1;
                            respuesta.Mensaje = "Usuario actualizado correctamente";
                            respuesta.Data = existeUsuario;
                        }
                        else
                        {
                            respuesta.Exito = 0;
                            respuesta.Mensaje = $"No o se encontró el usuario con ID: {id}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = $"Ocurrió un error al actualizar el usuario: {ex}";

            }
            return respuesta;
        }

        private bool IsValidId(int id)
        {
            return id >= 0; // Por ejemplo, podría ser positivo para ser válido

        }
    }
}
