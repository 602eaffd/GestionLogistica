using GestionLogistica.Common;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.Respuesta;
using GestionLogistica.Models.ViewModels;
using GestionLogistica.Services.Interfaces;
using GestionLogistica.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionLogistica.Services
{
    
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        RespuestaUsuario userResponse = new RespuestaUsuario();
        private readonly GestionLogisticaContext _db;
        public AuthService(GestionLogisticaContext db, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public RespuestaUsuario Auth(AuthDTO model)
        {
            string spassword = Encrypt.GetSHA256(model.Contraseña);
            var usuarioBaseDatos = _db.Usuarios.Where(u => u.Email == model.Email && u.Contraseña == spassword).FirstOrDefault();
            
            if (usuarioBaseDatos == null)
            {
                return null;
            }
            userResponse.Email = usuarioBaseDatos.Email;
            userResponse.Token = GetToken(usuarioBaseDatos);
            return userResponse;
        }

        public string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
