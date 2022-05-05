using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FMAPI.Entities
{
    public class EstimateSpent
    {
        [Key]
        public long Id { get; set; }

        public long Id_Estimate { get; set; }

        public long Id_Spent { get; set; }

        [ForeignKey("Id_Estimate")]
        public Estimate Estimate { get; set; }

        [ForeignKey("Id_Spent")]
        public Spent Spent { get; set; }

    }
}
