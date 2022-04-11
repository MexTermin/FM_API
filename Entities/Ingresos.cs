using System.ComponentModel.DataAnnotations;

namespace FM_API.Entities
{
    public class Ingresos
    {
        [Key]
        public long Id { get; set; }
    
        public int Monto { get; set; }  
    }
}
