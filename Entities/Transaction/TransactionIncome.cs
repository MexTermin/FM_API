using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMAPI.Entities
{
    public class TransactionIncome
    {
        [Key]
        public long Id { get; set; }

        public long Id_Transaction { get; set; }

        public long Id_Income { get; set; }

        [ForeignKey("Id_Transaction")]
        public Transaction Transaction { get; set; }

        [ForeignKey("Id_Income")]
        public Income Income { get; set; }
    }
}
