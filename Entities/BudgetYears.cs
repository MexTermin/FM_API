using FMAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FMAPI.Entities
{
    public class BudgetYears : SoftDelete
    {
        [Key]
        public long Id { get; set; }  
        
        public int Year { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
