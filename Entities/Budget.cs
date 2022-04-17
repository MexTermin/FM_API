using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FM_API.Entities
{
    public class Budget
    {
        [Key]
        public long Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}
