using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    readonly AccountOrchestrator accountOrchestrator;
    private readonly AccountQueries accountQueries;

    public AccountsController(
        AccountOrchestrator accountOrchestrator,
        AccountQueries accountQueries)
    {
        this.accountOrchestrator = accountOrchestrator;
        this.accountQueries = accountQueries;
    }
    [HttpPost]
    public void OpenAccount(OpenAccountCommand command)
        => accountOrchestrator.OpenAccount(command);
    
    [HttpGet]
    public IEnumerable<Queries.GetAllAccountsViewModel> GetAll()
    {
        return accountQueries.GetAll();
    }
}