using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;

namespace GestionLogistica.Services.Interfaces
{
    public interface IAuthService
    {
        RespuestaUsuario Auth(AuthDTO model);
    }
}
