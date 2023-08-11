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
    public class EquipoComputoController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly IMapper _mapper; //Readonly es para que se pueda modificar
        public EquipoComputoController(GestionLogisticaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ObtenerEquiposComputo()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var listaEquipos = _db.Equipos.ToList();
                if(listaEquipos != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Clientes encontrados correctamente";
                    respuesta.Data = listaEquipos;
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
        public IActionResult EquiposComputoPorId(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Equipo equipo = _db.Equipos.FirstOrDefault(e => e.EquipoId == id);
                if(equipo != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipo encontrado correctamente";
                    respuesta.Data = equipo;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Equipo no encontrado";
                    return NotFound(respuesta); // Devolver respuesta con status code 404 (Not Found)
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar el Equipo: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }


        [HttpPost]
        public IActionResult CrearEquipoComputo(EquipoComputoRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Equipo equipo = _mapper.Map<Equipo>(oModel);
                _db.Equipos.Add(equipo);
                _db.SaveChanges();
                respuesta.Exito = 1;
                respuesta.Mensaje = "Equipo agregado exitosamente";
                respuesta.Data = equipo;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al registrar los Clientes: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarEquipoComputo(int id, EquipoComputoRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Equipo equipo = _db.Equipos.FirstOrDefault(e => e.EquipoId == id);
                if(equipo != null)
                {
                    _mapper.Map(oModel, equipo);
                    _db.Entry(equipo).State = EntityState.Modified;
                    _db.SaveChanges();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa actualizada con éxito";
                    //Traer lista actualizada
                    respuesta.Data = equipo;
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Equipo no encontrado";
                    return NotFound(respuesta); // Devolver respuesta con status code 404 (Not Found)
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


        [HttpDelete("{id}")]
        public IActionResult EliminarEquipoComputo(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Equipo equipo = _db.Equipos.FirstOrDefault(e => e.EquipoId == id);
                if(equipo != null)
                {
                    _db.Equipos.Remove(equipo);
                    _db.SaveChanges();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Equipo eliminado correctamente";
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
                respuesta.Mensaje = "Ocurrió un error al buscar los Clientes: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }
    }
}
