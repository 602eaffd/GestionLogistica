using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services;
using GestionLogistica.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLogistica.Controllers
{ 
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly IMapper _mapper;
        private readonly EmpresaService _empresaService;

        public EmpresaController(GestionLogisticaContext db, IMapper mapper, EmpresaService empresaService)
        {
            _db = db;
            _mapper = mapper;
            _empresaService = empresaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        public async Task<ActionResult<Respuesta>> Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta = await _empresaService.GetAll();
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Respuesta), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empresa>> GetById(int id)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Mensaje = "Empresa encontrada con éxito";
            respuesta.Exito = 1;
            var empresa = await _empresaService.GetById(id);
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(EmpresaDTO nuevaEmpresa)
        {
            Respuesta respuesta = new Respuesta();
            if (nuevaEmpresa == null)
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _empresaService.Create(nuevaEmpresa);
            }
            return Ok(respuesta);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Respuesta>> Update(EmpresaDTO actualizarEmpresa, int id)
        {
            Respuesta respuesta = new Respuesta();
            
            if (actualizarEmpresa == null && !IsValidId(id))
            {
                respuesta.Mensaje = "Se debe ingresar los datos válidos";
                return BadRequest(respuesta);
            }
            else
            {
                respuesta = await _empresaService.Update(actualizarEmpresa, id);
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
            respuesta = await _empresaService.Delete(id);
            return Ok(respuesta);
        }

        private bool IsValidId(int id)
        {
            return id > 0; // Por ejemplo, podría ser positivo para ser válido

        }
    }
}
