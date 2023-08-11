using AutoMapper;
using GestionLogistica.Models;
using GestionLogistica.Models.ViewModels;

namespace GestionLogistica
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EquipoComputoRequest, Equipo>(); // POST
            CreateMap<Equipo, EquipoComputoRequest>(); // GET

            CreateMap<EmpresaRequest, Empresa>(); // POST
            CreateMap<Empresa, EmpresaRequest>(); // GET

            CreateMap<GestionEnvioRequest, Gestionenvio>(); // POST
            CreateMap<Gestionenvio, GestionEnvioRequest>(); // GET
        }
    }
}
