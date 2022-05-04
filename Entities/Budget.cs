using FMAPI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM_API.Entities
{
    public class Budget : SoftDelete
    {
        [Key]
        public long Id { get; set; }

        public int Month { get; set; }

        public long Year { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}
