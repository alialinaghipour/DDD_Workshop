using Microsoft.AspNetCore.Mvc;

namespace Presentation.Mvc.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountOrchestrator accountOrchestrator;
        private readonly AccountQueries accountQueries;

        public AccountsController(
            AccountOrchestrator accountOrchestrator,
            AccountQueries accountQueries)
        {
            this.accountOrchestrator = accountOrchestrator;
            this.accountQueries = accountQueries;
        }

        [HttpGet]
        public IActionResult OpenAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpenAccount(OpenAccountCommand command)
        {
            accountOrchestrator.OpenAccount(command);

            return View();
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = accountQueries.GetAll();
            return View(accounts);
        }
    }
}
