using FMAPI.DTOS;
#nullable disable

namespace FM_API.DTOS
{
    public class EstimateDTO
    {
        public long Id { get; set; }

        public long Id_category { get; set; }

        public long Id_type { get; set; }

        public int Plan { get; set; }

        public long Id_budget { get; set; }

        public CategoryDTO Category { get; set; }

        public BudgetDTO Budget { get; set; }

        public TypeDTO Type { get; set; }
    }

    public class EstimateRequestDTO
    {
        public long Id_category { get; set; }

        public int Plan { get; set; }

        public long Id_budget { get; set; }

        public long Id_Type { get; set; }

    }
}
