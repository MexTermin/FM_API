#nullable disable

namespace FM_API.DTOS
{
    public class EstimacionesDTO
    {
        public long Id { get; set; }

        public string Categoria { get; set; }

        public int Plan { get; set; }

        public long Id_presupuesto { get; set; }

        public long Id_gastos { get; set; }

        public long Id_ingresos { get; set; }

        public PresupuestoDTO Presupuesto { get; set; }

        public ICollection<GastosDTO> Gastos { get; set; }

        public ICollection<IngresosDTO> Ingresos { get; set; }
    }
}
