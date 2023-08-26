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
    public class EmpresaController : ControllerBase
    {
        private readonly GestionLogisticaContext _db;
        private readonly IMapper _mapper;

        public EmpresaController(GestionLogisticaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ObtenerEmpresas() 
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var listaEmpresas = _db.Empresas.ToList();
                if(listaEmpresas != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresas encontradas correctamente";
                    respuesta.Data = listaEmpresas;
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al buscar las empresas: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult EmpresaPorId(int id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Empresa empresa = _db.Empresas.Find(id);
                if(empresa != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa encontrada con éxito";
                    respuesta.Data = empresa;
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
                respuesta.Mensaje = "Ocurrió un error al buscar la empresa: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult CrearEmpresa(EmpresaRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                Empresa empresa = _mapper.Map<Empresa>(oModel);
                _db.Empresas.Add(empresa);
                _db.SaveChanges();
                respuesta.Exito = 1;
                respuesta.Mensaje = "Empresa creada con éxito";
                respuesta.Data = empresa;
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al crear la empresa: " + ex.Message;
                return BadRequest();
            }
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEmpresa(int id, EmpresaRequest oModel)
        {
            Respuesta respuesta = new Respuesta();
            Empresa empresa = _db.Empresas.FirstOrDefault(e => e.EmpresaId == id);
            try
            {
                if (empresa != null)
                {
                    _mapper.Map(oModel, empresa);
                    _db.Entry(empresa).State = EntityState.Modified;
                    _db.SaveChanges();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa actualizada con éxito";
                    //Traer lista actualizada
                    respuesta.Data = empresa;
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
            return Ok(respuesta);
        }
        
        [HttpDelete("{id}")]
        public IActionResult EliminarEmpresa(int id)
        {
            Respuesta respuesta = new Respuesta();
            Empresa empresa = _db.Empresas.FirstOrDefault(e=>e.EmpresaId == id);
            try{
                if (empresa != null)
                {
                    _db.Empresas.Remove(empresa);
                    _db.SaveChanges();
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Empresa eliminada con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al eliminar la empresa: " + ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
