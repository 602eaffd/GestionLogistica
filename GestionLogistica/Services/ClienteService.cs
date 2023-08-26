using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Services
{
    
    public class ClienteService : IClienteService
    {
        public readonly GestionLogisticaContext _context;
        private readonly IMapper _mapper;
        


        public ClienteService(GestionLogisticaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Respuesta> GetAll()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                List<Cliente> listaClientes = await _context.Clientes.ToListAsync();

                if (listaClientes != null && listaClientes.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipos encontradas correctamente";
                    respuesta.Data = listaClientes;
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

        
        public async Task<Respuesta> GetById(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Cliente encontrado con éxito";
                    respuesta.Data = cliente;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear el equipo: " + ex.Message;
            }

            return respuesta;   
        }
        

        public async Task<Respuesta> Create(ClienteDTO clienteNuevo)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(clienteNuevo);
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                respuesta.Exito = 1;
                respuesta.Mensaje = "Equipo creado con éxito";
                respuesta.Data = cliente;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear el equipo: " + ex.Message;
            }
            return respuesta;
        }

        public async Task<Respuesta> Update(ClienteDTO actualizarCliente, int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {

                //var existeEquipo = await GetById(id);
                var existeCliente = _context.Clientes.FindAsync(id);
                if (existeCliente != null)
                {
                    _mapper.Map(actualizarCliente, existeCliente); // Actualizar propiedades de la entidad existente
                    await _context.SaveChangesAsync();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipo actualizado con éxito";
                    respuesta.Data = existeCliente;
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
                //var eliminarCliente = await GetById(id);
                var eliminarCliente = _context.Clientes.FirstOrDefault(e => e.ClienteId == id);
                if (eliminarCliente != null)
                {
                    _context.Clientes.Remove(eliminarCliente);
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
