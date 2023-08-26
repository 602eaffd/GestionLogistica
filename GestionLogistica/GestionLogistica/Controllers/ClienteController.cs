using GestionLogistica.Models;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;

        public ClienteController(GestionLogisticaContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var listaClientes = _db.Clientes.ToList();

                if (listaClientes != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Clientes encontrados correctamente";
                    respuesta.Data = listaClientes;
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar los Clientes: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult ClientePorId(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Cliente cliente = _db.Clientes.FirstOrDefault(c => c.ClienteId == id);
                if(cliente != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Cliente encontrado correctamente";
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
                respuesta.Mensaje = "Ocurrió un error al buscar el Cliente: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }
        
        [HttpPost]
        public IActionResult CrearCliente(ClienteRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = oModel.NombreCliente;
                cliente.Direccion = oModel.Direccion;
                cliente.Celular = oModel.Celular;
                cliente.Correo = oModel.Correo;
                cliente.EmpresaId = oModel.IdEmpresa;
                _db.Clientes.Add(cliente); // Agrega el cliente a la base de datos
                _db.SaveChanges(); // Guarda los cambios en la base de datos
                respuesta.Exito = 1; // Indica que la operación se realizó con éxito
                respuesta.Mensaje = "Cliente agregado exitosamente";
                respuesta.Data = cliente;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al registrar los Clientes: " + ex.Message;
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, ClienteRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Cliente cliente = _db.Clientes.FirstOrDefault(c => c.ClienteId == id);
                if (cliente != null)
                {
                    cliente.Nombre = oModel.NombreCliente;
                    cliente.Direccion = oModel.Direccion;
                    cliente.Celular = oModel.Celular;
                    cliente.Correo = oModel.Correo;
                    cliente.EmpresaId = oModel.IdEmpresa;

                    _db.Entry(cliente).State = EntityState.Modified;
                    _db.SaveChanges();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Cliente actualizado exitosamente";
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
                respuesta.Mensaje = "Ocurrió un error al actualizar el Cliente: " + ex.Message;
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }



        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Cliente cliente = _db.Clientes.FirstOrDefault(c => c.ClienteId == id);
                if (cliente != null)
                {
                    _db.Clientes.Remove(cliente);
                    _db.SaveChanges();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Cliente eliminado exitosamente";
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
                respuesta.Mensaje = "Ocurrió un error al eliminar el Cliente: " + ex.Message;
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }


    }
}
