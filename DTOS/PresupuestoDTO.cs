#nullable disable

namespace FM_API.DTOS
{
    public class PresupuestoDTO
    {
        public long Id { get; set; }

        public int Mes { get; set; }

        public int Agno { get; set; }

        public ICollection<TransaccionDTO> Transacciones { get; set; }

        public ICollection<EstimacionDTO> Estimaciones { get; set; }
    }
}
