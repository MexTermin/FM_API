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

        public ICollection<Estimate_SpentDTO> Expenses { get; set; }

        public ICollection<Estimate_IncomeDTO> Income { get; set; }
    }
}
