using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FM_API.Entities
{
    public class Estimaciones
    {
        [Key]
        public long Id { get; set; }

        public string Categoria { get; set; }

        public int Plan { get; set; }

        public long Id_presupuesto { get; set; }

        public long Id_gastos { get; set; }

        public long Id_ingresos { get; set; }

        [ForeignKey("Id_presupuesto")]
        public virtual Presupuesto Presupuesto { get; set; }

        [ForeignKey("Id_gastos")]
        public virtual ICollection<Gastos> Gastos { get; set; }

        [ForeignKey("Id_ingresos")]
        public virtual ICollection<Ingresos> Ingresos { get; set; }
    }
}
