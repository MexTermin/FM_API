using AutoMapper;
using FM_API.DTOS;

namespace FM_API.Profiles
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            AllowNullCollections = true;

            CreateMap<Estimate, EstimacionDTO>().ReverseMap();
            CreateMap<Spent, GastoDTO>().ReverseMap();
            CreateMap<Income, IngresoDTO>().ReverseMap();
            CreateMap<Budget, PresupuestoDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Transaction, TransaccionDTO>().ReverseMap();
            CreateMap<User, UsuarioDTO>().ReverseMap();
            CreateMap<User, UsuarioResponseDTO>().ReverseMap();
        }
    }
}
