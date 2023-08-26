using AutoMapper;
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
    public class GestionEnvioController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly IMapper _mapper; //Readonly es para que se pueda modificar
        public GestionEnvioController(GestionLogisticaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ObtenerGestionesEnvio()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                List<Gestionenvio> listaGestionesEnvios = _db.Gestionenvios.ToList();
                List<GestionEnvioRequest> gestionesRequest = _mapper.Map<List<GestionEnvioRequest>>(listaGestionesEnvios);

                if (listaGestionesEnvios != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestiones de envío encontradas correctamente";
                    respuesta.Data = gestionesRequest;
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar las gestiones de envío: " + ex.Message;
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerGestionEnvioPorId(int id)
        {
            Respuesta respuesta = new Respuesta(); // Crear una respuesta para devolver al cliente
            try
            {
                // Buscar la gestión de envío en la base de datos por su ID
                Gestionenvio gestionEnvio = _db.Gestionenvios.FirstOrDefault(g => g.GestionId == id);

                if (gestionEnvio != null)
                {
                    // Mapear la gestión de envío encontrada a un objeto GestionEnvioRequest usando AutoMapper
                    GestionEnvioRequest gestionEnvioRequest = _mapper.Map<GestionEnvioRequest>(gestionEnvio);

                    respuesta.Exito = 1; // Indicar éxito en la respuesta
                    respuesta.Mensaje = "Gestión de envío encontrada"; // Agregar mensaje de éxito
                    respuesta.Data = gestionEnvioRequest; // Agregar los datos mapeados a la respuesta
                }
                else
                {
                    respuesta.Exito = 0; // Indicar fallo en la respuesta
                    respuesta.Mensaje = "Gestión de envío no encontrada"; // Agregar mensaje de error
                    return NotFound(respuesta); // Devolver respuesta con status code 404 (Not Found)
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0; // Indicar fallo en la respuesta
                respuesta.Mensaje = "Ocurrió un error al buscar la gestión de envío: " + ex.Message; // Agregar mensaje de error detallado
                return BadRequest(respuesta); // Devolver respuesta con status code 400 (Bad Request)
            }

            return Ok(respuesta); // Devolver respuesta con status code 200 (OK)
        }


        [HttpPost]
        public IActionResult CrearGestionEnvio(GestionEnvioRequest gestionEnvioRequest)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                // Mapeamos el GestionEnvioRequest recibido al modelo Gestionenvio
                Gestionenvio gestionEnvio = _mapper.Map<Gestionenvio>(gestionEnvioRequest);

                // Agregamos el nuevo Gestionenvio a la base de datos
                _db.Gestionenvios.Add(gestionEnvio);
                _db.SaveChanges();

                // Preparamos la respuesta exitosa
                respuesta.Exito = 1;
                respuesta.Mensaje = "Gestión de envío creada exitosamente";
                respuesta.Data = gestionEnvio;
            }
            catch (Exception ex)
            {
                // Preparamos la respuesta de error en caso de excepción
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear la gestión de envío: " + ex.Message;
                return BadRequest(respuesta); // Devolvemos un código 400 Bad Request
            }
            return Ok(respuesta); // Devolvemos un código 200 OK con la respuesta exitosa
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarGestionesEnvio(int id, GestionEnvioRequest gestionEnvioRequest)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                // Buscamos la gestión de envío existente por su ID
                Gestionenvio gestionEnvio = _db.Gestionenvios.FirstOrDefault(g => g.GestionId == id);

                if (gestionEnvio != null)
                {
                    // Actualizamos los datos de la gestión de envío con los valores del GestionEnvioRequest
                    _mapper.Map(gestionEnvioRequest, gestionEnvio);

                    _db.Entry(gestionEnvio).State = EntityState.Modified;
                    _db.SaveChanges();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestión de envío actualizada exitosamente";
                    respuesta.Data = gestionEnvio;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Gestión de envío no encontrada";
                    return NotFound(respuesta); // Devolvemos un código 404 Not Found
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al actualizar la gestión de envío: " + ex.Message;
                return BadRequest(respuesta); // Devolvemos un código 400 Bad Request
            }
            return Ok(respuesta); // Devolvemos un código 200 OK con la respuesta exitosa
        }


        [HttpDelete("{id}")]
        public IActionResult EliminarGestionesEnvio(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                // Buscamos la gestión de envío existente por su ID
                Gestionenvio gestionEnvio = _db.Gestionenvios.FirstOrDefault(g => g.GestionId == id);

                if (gestionEnvio != null)
                {
                    _db.Gestionenvios.Remove(gestionEnvio);
                    _db.SaveChanges();

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Gestión de envío eliminada exitosamente";
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Gestión de envío no encontrada";
                    return NotFound(respuesta); // Devolvemos un código 404 Not Found
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al eliminar la gestión de envío: " + ex.Message;
                return BadRequest(respuesta); // Devolvemos un código 400 Bad Request
            }
            return Ok(respuesta); // Devolvemos un código 200 OK con la respuesta exitosa
        }


    }
}
