using FM_API.DTOS;

namespace FMAPI.DTOS
{
    public class TransactionSpentDTO
    {
        public long Id { get; set; }

        public long Id_Transaction { get; set; }

        public long Id_Spent { get; set; }

        public TransactionDTO Transaction { get; set; }

        public SpentDTO Spent { get; set; }
    }
}
