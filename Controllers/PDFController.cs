using FM_API.Controllers;
using FM_API.DTOS;
using FMAPI.Helpers;
using FMAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using System.Globalization;

namespace FMAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFController : Controller
    {
        private BudgetController _budgetController { get; set; }
        private TransactionController _transactionController { get; set; }
        private EstimateController _estimateController { get; set; }

        public PDFController(BudgetController budgetController, TransactionController transactionController, EstimateController estimateController)
        {
            _budgetController = budgetController;
            _transactionController = transactionController;
            _estimateController = estimateController;
        }

        [HttpGet("{id:long}")]
        public async Task<string> GeneratePDF(long id)
        {
            // Variables
            object? allEstimateResponse;
            object? allTransactionResponse;
            object? budgetResponse;
            int trueIncome = 0;
            int providedIncome = 0;
            int trueExpenses = 0;
            int providedExpenses = 0;
            var budget = new BudgetDTO();
            var ExpensesByCategory = new Dictionary<string, PDFDescriptionModel>();
            var IncomeByCategory = new Dictionary<string, PDFDescriptionModel>();
            IEnumerable<EstimateDTO> allEstimate = new List<EstimateDTO>();
            IEnumerable<TransactionDTO> allTransaction = new List<TransactionDTO>();

            // Estimate conf
            allEstimateResponse = ((OkObjectResult)await _estimateController.GetByBudget(id)).Value;
            if (allEstimateResponse != null) allEstimate = ((ResponseHelper<IEnumerable<EstimateDTO>>)allEstimateResponse).Body;

            // Budget conf
            budgetResponse = ((OkObjectResult)await _budgetController.GetById(id)).Value;
            budget = ((ResponseHelper<BudgetDTO>)budgetResponse).Body;

            // Transaction conf
            allTransactionResponse = ((OkObjectResult)await _transactionController.GetByBudget(id)).Value;
            if (allTransactionResponse != null) allTransaction = ((ResponseHelper<IEnumerable<TransactionDTO>>)allTransactionResponse).Body;

            foreach (EstimateDTO item in allEstimate)
            {
                if (item.Type.Name == "Gasto")
                {
                    if (ExpensesByCategory.ContainsKey(item.Category.Name))
                    {
                        ExpensesByCategory[item.Category.Name].Estimate += item.Plan;
                    }
                    else
                    {
                        ExpensesByCategory.Add(item.Category.Name, new PDFDescriptionModel { Estimate = item.Plan, category = item.Category.Name });
                    }
                    providedExpenses += item.Plan;
                }

                if (item.Type.Name == "Ingreso")
                {
                    if (IncomeByCategory.ContainsKey(item.Category.Name))
                    {
                        IncomeByCategory[item.Category.Name].Estimate += item.Plan;
                    }
                    else
                    {
                        IncomeByCategory.Add(item.Category.Name, new PDFDescriptionModel { Estimate = item.Plan, category = item.Category.Name });
                    }
                    providedIncome += item.Plan;
                }
            }

            foreach (TransactionDTO item in allTransaction)
            {
                if (item.Type.Name == "Gasto")
                {
                    if (ExpensesByCategory.ContainsKey(item.Category.Name))
                    {
                        ExpensesByCategory[item.Category.Name].Amount += item.Amount;
                    }
                    else
                    {
                        ExpensesByCategory.Add(item.Category.Name, new PDFDescriptionModel { Amount = item.Amount, category = item.Category.Name });
                    }
                    ExpensesByCategory[item.Category.Name].difference = Math.Abs(ExpensesByCategory[item.Category.Name].Amount - ExpensesByCategory[item.Category.Name].Estimate);
                    trueExpenses += item.Amount;
                }

                if (item.Type.Name == "Ingreso")
                {
                    if (IncomeByCategory.ContainsKey(item.Category.Name))
                    {
                        IncomeByCategory[item.Category.Name].Amount += item.Amount;
                    }
                    else
                    {
                        IncomeByCategory.Add(item.Category.Name, new PDFDescriptionModel { Amount = item.Amount, category = item.Category.Name });
                    }
                    IncomeByCategory[item.Category.Name].difference = Math.Abs(IncomeByCategory[item.Category.Name].Amount - IncomeByCategory[item.Category.Name].Estimate);
                    trueIncome += item.Amount;
                }
            }

            PDFModel model = new()
            {
                Expenses = ExpensesByCategory,
                Income = IncomeByCategory,
                ProvidedExpenses = providedExpenses,
                ProvidedIncome = providedIncome,
                TrueExpenses = trueExpenses,
                TrueIncome = trueIncome,
                Month = CultureInfo.CreateSpecificCulture("es").DateTimeFormat.GetMonthName(budget.Month),
                Categories = ExpensesByCategory.Keys.Union(IncomeByCategory.Keys)
            };
            model.LongestList = Math.Max(model.Expenses.Count(), model.Income.Count());
            ViewAsPdf report = new ViewAsPdf(model) { FileName = "reportefm.pdf" };
            return JsonConvert.SerializeObject(new Dictionary<string, dynamic>() { { "buffer", report.BuildFile(ControllerContext).Result }, { "type", "aplication/pdf" } });
        }
    }
}
