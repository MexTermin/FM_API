#nullable disable

using FMAPI.DTOS;

namespace FM_API.DTOS
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public long Id_category { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public CategoryDTO Category { get; set; }

        public int Amount { get; set; }  // importe

        public long Id_budget { get; set; }

        public BudgetDTO Budget { get; set; }

        public long Id_Type { get; set; }

        public TypeDTO Type { get; set; }
    }

    public class TransactionRequestDTO
    {
        public long Id { get; set; }

        public long Id_category { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }  // importe

        public long Id_budget { get; set; }

        public long Id_Type { get; set; }

    }
}
