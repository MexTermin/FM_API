#nullable disable

namespace FM_API.DTOS
{
    public class BudgetDTO
    {
        public long Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public ICollection<TransactionDTO> Transactions { get; set; }

        public ICollection<EstimateDTO> Estimates { get; set; }
    }
}
