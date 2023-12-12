public record TransferDraftViewModel(
    string creditAccountId,
    string debitAccountId,
    decimal balance);

public record TransactionsViewModel(
    string transcriptId,
    string creditAccountId,
    string debitAccountId,
    decimal balance,
    string status);


public class TransactionQueries
{
    readonly Transactions transactions;
    public TransactionQueries(Transactions transactions)
    {
        this.transactions = transactions;
    }
    public IEnumerable<TransferDraftViewModel> AllDrafts()
    => transactions.All()
        .Where(t => t.Status == TransferStatus.Draft)
        .Select(t => new TransferDraftViewModel(
            t.TransferRequest.Parties.CreditAccountId.Id,
            t.TransferRequest.Parties.DebitAccountId.Id,
            t.TransferRequest.Amount.Value
        ));

    public IEnumerable<TransactionsViewModel> All()
   => transactions.All()
       .Select(t => new TransactionsViewModel(
           t.Id.Id,
           t.TransferRequest.Parties.CreditAccountId.Id,
           t.TransferRequest.Parties.DebitAccountId.Id,
           t.TransferRequest.Amount.Value,
           t.Status == TransferStatus.Draft ? "Drafted" : "Commited"
       ));

}