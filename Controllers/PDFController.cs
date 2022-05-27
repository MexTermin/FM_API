using FM_API.Controllers;
using FMAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

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
        public async Task<IActionResult> GeneratePDF(long id)
        {
            var budget = ((OkObjectResult)await _budgetController.GetById(id)).Value;
            var allEstimate = ((OkObjectResult)await _estimateController.GetByBudget(id)).Value;
            var allTransaction = ((OkObjectResult)await _transactionController.GetByBudget(id)).Value;
            PDFModel model = new();
            ViewAsPdf report = new ViewAsPdf(model);
            return report;
        }
    }
}
