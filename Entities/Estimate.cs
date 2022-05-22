using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = FMAPI.Entities.Type;

#nullable disable

namespace FM_API.Entities
{
    public class Estimate
    {
        [Key]
        public long Id { get; set; }

        public int Plan { get; set; }

        public long Id_category { get; set; }
        [ForeignKey("Id_category")]
        public virtual Category Category { get; set; }

        public long Id_budget { get; set; }
        [ForeignKey("Id_budget")]
        public virtual Budget Budget { get; set; }

        public long Id_Type { get; set; }
        [ForeignKey("Id_Type")]
        public virtual Type Type { get; set; }
    }
}
