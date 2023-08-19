using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.DTOs;
using GestionLogistica.Models.ViewModels;

namespace GestionLogistica
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<EquipoDTO, Equipo>(); // POST
            CreateMap<Equipo, EquipoDTO>(); // GET

            CreateMap<EmpresaDTO, Empresa>(); // POST
            CreateMap<Empresa, EmpresaDTO>(); // GET

            CreateMap<GestionEnvioDTO, Gestionenvio>(); // POST
            CreateMap<Gestionenvio, GestionEnvioDTO>(); // GET

            CreateMap<ClienteDTO, Cliente>(); // POST
            CreateMap<Cliente, ClienteDTO>(); // GET

            CreateMap<UsuarioDTO, Usuario>(); // POST
            CreateMap<Usuario, UsuarioDTO>(); // GET
        }
    }
}
