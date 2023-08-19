using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLogistica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;

        public AuthController(IAuthService userService)
        {
            _userService = userService; 
        }
        
        [HttpPost]

        public IActionResult Autentificar([FromBody] AuthDTO user)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var userResponse = _userService.Auth(user);

                if (userResponse != null)
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Usuario encontrado correctamente";
                    respuesta.Data = userResponse;
                }
                else
                {
                    respuesta.Mensaje = "Usuario o contraseña incorrecta";
                    respuesta.Exito = 0;
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error al intentar loguear el usuario: " + ex.Message;
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }


    }
}
