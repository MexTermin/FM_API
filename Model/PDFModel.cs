namespace FMAPI.Model
{
    public class PDFModel
    {
        public List<PDFDescriptionModel> Income { get; set; }
        public List<PDFDescriptionModel> Expenses { get; set; }
        public TotalIncomeModel TotalIncome { get; set; }
        public TotalSpentModel TotalSpent { get; set; }
    }
}
