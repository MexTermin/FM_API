using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = FMAPI.Entities.Type;

#nullable disable

namespace FM_API.Entities
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; } // importe

        public long Id_category { get; set; }
        [ForeignKey("Id_category")]
        public virtual Category Category { get; set; }

        public long Id_budget { get; set; }
        [ForeignKey("Id_budget")]
        public virtual Budget Budget { get; set; }

        public long Id_type { get; set; }
        [ForeignKey("Id_type")]
        public virtual Type Type { get; set; }

    }
}
