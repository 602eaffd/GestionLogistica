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
            CreateMap<Gestionenvio, DashboardActulizarGestionByEmpresaDTO>()
            .ForMember(dest => dest.SerialEquipo, opt => opt.MapFrom(src => src.Equipo.Serial))
            .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
            .ForMember(dest => dest.EstadoEquipo, opt => opt.MapFrom(src => src.Equipo.Estado));
            // Mapea otros campos aquí si es necesario


            CreateMap<ClienteDTO, Cliente>(); // POST
            CreateMap<Cliente, ClienteDTO>(); // GET

            CreateMap<UsuarioDTO, Usuario>(); // POST
            CreateMap<Usuario, UsuarioDTO>(); // GET*/
        }
    }
}
