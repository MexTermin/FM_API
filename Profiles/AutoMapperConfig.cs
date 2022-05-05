using AutoMapper;
using FM_API.DTOS;
using FMAPI.DTOS;

namespace FM_API.Profiles
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            AllowNullCollections = true;

            CreateMap<Estimate, EstimateDTO>().ReverseMap();
            CreateMap<EstimateSpent, EstimateSpentDTO>().ReverseMap();
            CreateMap<EstimateIncome, EstimateIncomeDTO>().ReverseMap();

            CreateMap<Spent, SpentDTO>().ReverseMap();
            CreateMap<Income, IncomeDTO>().ReverseMap();
            CreateMap<Budget, BudgetDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UsuarioResponseDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
