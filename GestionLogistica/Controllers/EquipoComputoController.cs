using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EquipoComputoController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly EquipoService _equipoService;
        private readonly IMapper _mapper; //Readonly es para que se pueda modificar
        public EquipoComputoController(GestionLogisticaContext db, IMapper mapper, EquipoService equipoService)
        {
            _db = db;
            _mapper = mapper;
            _equipoService = equipoService; 
        }

        [HttpGet]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        public async Task<ActionResult<Respuesta>> Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta = await _equipoService.GetAll();
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Equipo>> GetById(int id)
        {
            Respuesta respuesta = new Respuesta();
            if (!IsValidId(id))
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "El ID no puede ser nulo.";
                return BadRequest(respuesta);
            }
            respuesta.Mensaje = "Empresa encontrada con éxito";
            respuesta.Exito = 1;
            var equipo = await _equipoService.GetById(id);
            return Ok(equipo);
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(EquipoDTO nuevoEquipo)
        {
            Respuesta respuesta = new Respuesta();
            if (nuevoEquipo == null)
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }else if(nuevoEquipo.EmpresaId == null){
                respuesta.Mensaje = "El campo empresaId no puede ser nulo";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _equipoService.Create(nuevoEquipo);
            }
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Respuesta>> Update(EquipoDTO actualizarEquipo, int id)
        {
            Respuesta respuesta = new Respuesta();

            if (actualizarEquipo == null && !IsValidId(id))
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _equipoService.Update(actualizarEquipo, id);
            }
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> Delete(int id)
        {
            Respuesta respuesta = new Respuesta();
            if (!IsValidId(id))
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "El ID no puede ser nulo.";
                return BadRequest(respuesta);
            }
            respuesta.Mensaje = "Empresa eliminada con éxito";
            respuesta.Exito = 1;
            respuesta = await _equipoService.Delete(id);
            return Ok(respuesta);
        }

        private bool IsValidId(int id)
        {
            return id > 0; // Por ejemplo, podría ser positivo para ser válido

        }
    }
}
