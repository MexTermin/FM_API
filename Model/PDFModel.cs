namespace FMAPI.Model
{
    public class PDFModel
    {
        public string Month { get; set; }
        public Dictionary<string, PDFDescriptionModel> Income { get; set; }
        public Dictionary<string, PDFDescriptionModel> Expenses { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public int TrueIncome { get; set; }
        public int TrueExpenses { get; set; }
        public int ProvidedIncome { get; set; }
        public int ProvidedExpenses { get; set; }
        public int LongestList { get; set; }
    }
}
