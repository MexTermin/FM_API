#nullable disable

namespace FMAPI.DTOS
{
    public class EstimateSpentDTO
    {
        public long Id { get; set; }

        public long Id_Estimate { get; set; }

        public long Id_Spent { get; set; }

        public Spent Spent { get; set; }
    }
}
