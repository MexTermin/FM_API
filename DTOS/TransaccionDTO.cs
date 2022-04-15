#nullable disable

namespace FM_API.DTOS
{
    public class TransaccionDTO
    {
        public long Id { get; set; }

        public string Categoria { get; set; }

        public string Descripcion { get; set; }

        public int Importe { get; set; }

        public long Id_presupuesto { get; set; }

        public long Id_gastos { get; set; }

        public long Id_ingresos { get; set; }

        public PresupuestoDTO Presupuesto { get; set; }

        public ICollection<GastoDTO> Gastos { get; set; }

        public ICollection<IngresoDTO> Ingresos { get; set; }
    }
}
