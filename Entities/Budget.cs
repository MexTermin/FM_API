using FMAPI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FM_API.Entities
{
    public class Budget : SoftDelete
    {
        [Key]
        public long Id { get; set; }

        public int Month { get; set; }

        public long Id_budgetYear { get; set; }

        [ForeignKey("Id_budgetYear")]
        public virtual BudgetYears BudgetYears { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}
