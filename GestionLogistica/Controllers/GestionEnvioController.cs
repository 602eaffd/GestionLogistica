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
            private readonly GestionLogisticaContext _db;
            private readonly IMapper _mapper;
            private readonly GestionEnvioService _gestionEnvioService;

            public GestionEnvioController(GestionLogisticaContext db, IMapper mapper, GestionEnvioService gestionEnvioService)
            {
                _db = db;
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
                /*if (!IsValidId(id))
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "El ID no puede ser nulo.";
                    return BadRequest(respuesta);
                }*/
                var cliente = await _gestionEnvioService.GetById(id);
                return Ok(cliente);
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
        
            [HttpPut("{id}")]
            public async Task<ActionResult<Respuesta>> Update(GestionEnvioDTO actualizarGestionEnvio, int id)
            {
                Respuesta respuesta = new Respuesta();

                if (actualizarGestionEnvio == null && !IsValidId(id))
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
        /*
        [HttpGet]
        [Route("/FindDashboard")]
        public IActionResult FindDashboard()
        {
            var clientes = _db.Clientes.ToList();
            var usuarios = _db.Usuarios.ToList();
            var equipos = _db.Equipos.ToList();
            var gestionEnvios = _db.Gestionenvios.ToList();

            List<DashboardDTO> dashboardData = new List<DashboardDTO>();
            
            foreach (var cliente in clientes)
            {
                var usuario = usuarios.FirstOrDefault(u => u.UsuarioId == cliente.ClienteId);
                var equipo = equipos.FirstOrDefault(e => e.EquipoId == equipo.);
                var gestionEnvio = gestionEnvios.FirstOrDefault(g => g.ClienteId == cliente.ClienteId && g.EquipoId == cliente.IdEquipo && g.UsuarioId == cliente.IdUsuario);

                if (usuario != null && equipo != null && gestionEnvio != null)
                {
                    DashboardDTO dash = new DashboardDTO
                    {
                        NombreCliente = cliente.Nombre,
                        SerialEquipo = equipo.Serial,
                        NombreUsuario = usuario.Nombre,
                        ValorAsegurado = gestionEnvio.MontoAsegurado
                    };
                    dashboardData.Add(dash);
                }
            }
           
            Respuesta respuesta = new Respuesta();
            respuesta.Data = dashboardData;

            return Ok(respuesta);
        }
        */

        
    }
}

