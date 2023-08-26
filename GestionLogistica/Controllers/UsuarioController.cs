using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLogistica.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly IMapper _mapper;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(GestionLogisticaContext db, IMapper mapper, UsuarioService usuarioService)
        {
            _db = db;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        public async Task<ActionResult<Respuesta>> Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta = await _usuarioService.GetAll();
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Gestionenvio>> GetById(int id)
        {
            Respuesta respuesta = new Respuesta();
            var cliente = await _usuarioService.GetById(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(UsuarioDTO nuevoUsuario)
        {
            Respuesta respuesta = new Respuesta();
            if (nuevoUsuario == null)
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _usuarioService.Create(nuevoUsuario);
            }
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Respuesta>> Update(UsuarioDTO actualizarUsuario, int id)
        {
            Respuesta respuesta = new Respuesta();

            if (actualizarUsuario == null && !IsValidId(id))
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _usuarioService.Update(actualizarUsuario, id);
            }
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> Delete(int id)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = await _usuarioService.Delete(id);
            return Ok(respuesta);
        }


        private bool IsValidId(int id)
        {
            return id > 0; // Por ejemplo, podría ser positivo para ser válido

        }
    }
}
