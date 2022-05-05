using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMAPI.Entities
{
    public class EstimateIncome
    {
        [Key]
        public long Id { get; set; }

        public long Id_Estimate { get; set; }

        public long Id_Income { get; set; }

        [ForeignKey("Id_Estimate")]
        public Estimate Estimate { get; set; }

        [ForeignKey("Id_Income")]
        public Income Income { get; set; }
    }
}
