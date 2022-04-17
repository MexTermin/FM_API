#nullable disable

namespace FM_API.DTOS
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public long Id_budget { get; set; }

        public long Id_spent { get; set; }

        public long Id_income { get; set; }

        public BudgetDTO Budget { get; set; }

        public ICollection<SpentDTO> Expenses { get; set; }

        public ICollection<IncomeDTO> Income { get; set; }
    }
}
