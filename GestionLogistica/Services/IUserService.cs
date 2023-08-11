using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;

namespace GestionLogistica.Services
{
    public interface IUserService
    {
        RespuestaUsuario Auth(AuthRequest model);
    }
}
