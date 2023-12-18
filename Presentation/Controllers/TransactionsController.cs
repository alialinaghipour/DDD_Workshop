using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    readonly TransactionOrchestrator transactionOrchestrator;
    private readonly TransactionQueries transactionQueries;

    public TransactionsController(
        TransactionOrchestrator transactionOrchestrator,
        TransactionQueries transactionQueries)
    {
        this.transactionOrchestrator = transactionOrchestrator;
        this.transactionQueries = transactionQueries;
    }

    [HttpPost("draft")]
    public void Draft([FromBody] DraftTransferCommand command)
        => transactionOrchestrator.DraftTransfer(command);

    [HttpPost("commit")]
    public void Commit([FromBody] CommitTransferCommand command)
        => transactionOrchestrator.CommitTransfer(command);

    [HttpGet]
    public IEnumerable<TransactionsViewModel> GetAll()
    {
        return transactionQueries.All();
    }
}