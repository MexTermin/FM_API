using FMAPI.DTOS;
#nullable disable


namespace FM_API.DTOS
{
    public class EstimateDTO
    {
        public long Id { get; set; }

        public long Id_category { get; set; }

        public int Plan { get; set; }

        public long Id_budget { get; set; }

        public virtual Category Category { get; set; }

        public BudgetDTO Budget { get; set; }

        public ICollection<EstimateSpentDTO> Expenses { get; set; }

        public ICollection<EstimateIncomeDTO> Income { get; set; }
    }
}
