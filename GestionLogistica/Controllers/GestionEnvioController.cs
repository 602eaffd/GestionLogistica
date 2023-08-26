using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
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
        public class GestionEnvioController : ControllerBase
        {
            private readonly GestionLogisticaContext _context;
            private readonly IMapper _mapper;
            private readonly GestionEnvioService _gestionEnvioService;


            public GestionEnvioController(GestionLogisticaContext db, IMapper mapper, GestionEnvioService gestionEnvioService)
            {
                _context = db;
                _mapper = mapper;
                _gestionEnvioService = gestionEnvioService;
            }

            [HttpGet]
            [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
            public async Task<ActionResult<Respuesta>> Get()
            {
                Respuesta respuesta = new Respuesta();
                respuesta = await _gestionEnvioService.GetAll();
                return Ok(respuesta);
            }

            [HttpGet("{id}")]
            [ProducesResponseType(typeof(Respuesta), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(Respuesta), StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<Gestionenvio>> GetById(int id)
            {
                Respuesta respuesta = new Respuesta();
                var cliente = await _gestionEnvioService.GetById(id);
                return Ok(cliente);
            }

        [HttpGet("gestionesByFecha")]
        public async Task<ActionResult<IEnumerable<DashboardDTO>>> GestionesPorFecha(
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin,            [FromQuery] int numeroPagina = 1,
        [FromQuery] int elementosPorPagina = 10)
        {
            try
            {
                var gestionesFiltradas = await _gestionEnvioService.GetGestionesFiltradasFecha(
                    fechaInicio, fechaFin, numeroPagina, elementosPorPagina);

                return Ok(gestionesFiltradas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error al obtener las gestiones: {ex.Message}");
            }
        }

        [HttpGet("gestionesByEmpresa&Fecha")]
        public async Task<ActionResult<IEnumerable<DashboardDTO>>> GestionesPorEmpresaFecha(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromQuery] string nombreEmpresa,
            [FromQuery] int numeroPagina = 1,
            [FromQuery] int elementosPorPagina = 10)
        {
            try
            {
                var gestionesFiltradas = await _gestionEnvioService.GetGestionesFiltradasPorEmpresa(
                    fechaInicio, fechaFin, nombreEmpresa, numeroPagina, elementosPorPagina);

                return Ok(gestionesFiltradas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error al obtener las gestiones: {ex.Message}");
            }
        }



        [HttpPost]
            public async Task<ActionResult<Respuesta>> Crear(GestionEnvioDTO nuevaGestionEnvio)
            {
                Respuesta respuesta = new Respuesta();
                if (nuevaGestionEnvio == null)
                {
                    respuesta.Mensaje = "Se debe ingresar los datos válidos";
                    return BadRequest(respuesta);
                }
                else
                {
                    respuesta = await _gestionEnvioService.Create(nuevaGestionEnvio);
                }
                return Ok(respuesta);
            }
        
        /*
            [HttpPut("{id}")]
            public async Task<ActionResult<Respuesta>> Update(GestionEnvioDTO actualizarGestionEnvio, int id)
            {
                Respuesta respuesta = new Respuesta();

                if (actualizarGestionEnvio == null)
                {
                    respuesta.Mensaje = "Se debe ingresar los datos válidos";
                    return BadRequest(respuesta);
                }
                else
                {
                    respuesta = await _gestionEnvioService.Update(actualizarGestionEnvio, id);
                }
                return Ok(respuesta);
            }
        */

            [HttpPut("actualizarByEmpresa/{id}")]
            public async Task<ActionResult<Respuesta>> Update(int id, [FromBody] DashboardActulizarGestionByEmpresaDTO gestionActualizada)
            {
                Respuesta respuesta = new Respuesta();

                try
                {
                    respuesta = await _gestionEnvioService.UpdateGestion(id, gestionActualizada);

                    if (respuesta.Exito == 1)
                    {
                        return Ok(respuesta);
                    }
                    else
                    {
                        return BadRequest(respuesta);
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = $"Ocurrió un error al actualizar la gestión:[ {ex} ]";
                    return BadRequest(respuesta);
                }
            }



        [HttpDelete("{id}")]
            public async Task<ActionResult<Respuesta>> Delete(int id)
            {
                Respuesta respuesta = new Respuesta();
                respuesta = await _gestionEnvioService.Delete(id);
                return Ok(respuesta);
            }
        
        
            private bool IsValidId(int id)
            {
                return id > 0; // Por ejemplo, podría ser positivo para ser válido

            }
       
        

        
    }
}

