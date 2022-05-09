using FM_API.DTOS;

namespace FMAPI.DTOS
{
    public class TransactionIncomeDTO
    {
        public long Id { get; set; }

        public long Id_Transaction { get; set; }

        public long Id_Income { get; set; }

        public TransactionDTO Transaction { get; set; }

        public IncomeDTO Income { get; set; }
    }
}
