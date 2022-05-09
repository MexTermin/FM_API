using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMAPI.Entities
{
    public class TransactionSpent
    {
        [Key]
        public long Id { get; set; }

        public long Id_Transaction { get; set; }

        public long Id_Spent { get; set; }

        [ForeignKey("Id_Transaction")]
        public Transaction Transaction { get; set; }

        [ForeignKey("Id_Spent")]
        public Spent Spent { get; set; }
    }
}
