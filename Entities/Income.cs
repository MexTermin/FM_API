using System.ComponentModel.DataAnnotations;

namespace FM_API.Entities
{
    public class Income
    {
        [Key]
        public long Id { get; set; }
    
        public int Monto { get; set; }  
    }
}
