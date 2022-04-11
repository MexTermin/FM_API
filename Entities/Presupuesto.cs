using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FM_API.Entities
{
    public class Presupuesto
    {
        [Key]
        public long Id { get; set; }

        public int Mes { get; set; }

        public int Agno { get; set; }

        public virtual ICollection<Transacciones> Transacciones { get; set; }

        public virtual ICollection<Estimaciones> Estimaciones { get; set; }
    }
}
