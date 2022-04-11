using AutoMapper;
using FM_API.DTOS;

namespace FM_API.Profiles
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            AllowNullCollections = true;

            CreateMap<Estimaciones, EstimacionesDTO>().ReverseMap();
            CreateMap<Gastos, GastosDTO>().ReverseMap();
            CreateMap<Ingresos, IngresosDTO>().ReverseMap();
            CreateMap<Presupuesto, PresupuestoDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Transacciones, TransaccionesDTO>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>().ReverseMap();
        }
    }
}
