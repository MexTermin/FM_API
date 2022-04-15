using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FM_API.Entities
{
    public class Budget
    {
        [Key]
        public long Id { get; set; }

        public int Mes { get; set; }

        public int Agno { get; set; }

        public virtual ICollection<Transaction> Transacciones { get; set; }

        public virtual ICollection<Estimate> Estimaciones { get; set; }
    }
}
