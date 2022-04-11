using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FM_API.Entities
{
    public class Gastos
    {
        [Key]
        public long Id { get; set; }
        
        public int Monto { get; set; }
    }
}
