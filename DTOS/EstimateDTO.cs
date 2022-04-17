#nullable disable

namespace FM_API.DTOS
{
    public class EstimateDTO
    {
        public long Id { get; set; }

        public string Categoria { get; set; }

        public int Plan { get; set; }

        public long Id_budget { get; set; }

        public long Id_spent { get; set; }

        public long Id_income { get; set; }

        public BudgetDTO Budget { get; set; }

        public ICollection<SpentDTO> Expenses { get; set; }

        public ICollection<IncomeDTO> Income { get; set; }
    }
}
