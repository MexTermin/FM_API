using FM_API.DTOS;

#nullable disable

namespace FMAPI.DTOS
{
    public class BudgetYearsDTO
    {
        public long Id { get; set; }

        public int Year { get; set; }

        public virtual ICollection<BudgetDTO> Budgets { get; set; }
    }
}
