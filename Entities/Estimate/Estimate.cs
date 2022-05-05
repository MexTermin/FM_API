using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FM_API.Entities
{
    public class Estimate
    {
        [Key]
        public long Id { get; set; }

        public int Plan { get; set; }

        public long Id_budget { get; set; }

        public long Id_category { get; set; }

        [ForeignKey("Id_category")]
        public virtual Category Category { get; set; }

        [ForeignKey("Id_budget")]
        public virtual Budget Budget { get; set; }

        public virtual ICollection<EstimateSpent> Expenses { get; set; }

        public virtual ICollection<EstimateIncome> Income { get; set; }
    }
}
