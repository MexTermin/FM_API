using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FM_API.Entities
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; } // importe

        public long Id_budget { get; set; }

        public long Id_spent { get; set; }

        public long Id_income { get; set; }

        public long Id_category { get; set; }

        [ForeignKey("Id_category")]
        public virtual Category Category { get; set; }

        [ForeignKey("Id_budget")]
        public virtual Budget Budget { get; set; }

        [ForeignKey("Id_spent")]
        public virtual ICollection<Spent> Expenses { get; set; }

        [ForeignKey("Id_income")]
        public virtual ICollection<Income> Income { get; set; }
    }
}
