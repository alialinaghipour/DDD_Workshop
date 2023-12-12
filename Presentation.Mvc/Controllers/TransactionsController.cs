using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Mvc.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionQueries transactionQueries;
        private readonly TransactionOrchestrator transactionOrchestrator;

        public TransactionsController(
            TransactionQueries transactionQueries,
            TransactionOrchestrator transactionOrchestrator)
        {
            this.transactionQueries = transactionQueries;
            this.transactionOrchestrator = transactionOrchestrator;
        }

        public IActionResult All()
        {
            var all = transactionQueries.All();

            return View(all);
        }

        [HttpGet]
        public IActionResult DraftTransfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DraftTransfer(DraftTransferCommand command)
        {
            transactionOrchestrator.DraftTransfer(command);
            return View();
        }

        [HttpGet]
        public IActionResult Commit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Commit(CommitTransferCommand command)
        {
            transactionOrchestrator.CommitTransfer(command);
            return View();
        }
    }
}
