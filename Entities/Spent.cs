using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FM_API.Entities
{
    public class Spent
    {
        [Key]
        public long Id { get; set; }
        
        public int Amount { get; set; }
    }
}
