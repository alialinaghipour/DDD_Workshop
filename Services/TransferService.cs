public class TransferService : ITransferService
{
    private readonly Accounts _accounts;

    public TransferService(Accounts accounts)
    {
        this._accounts = accounts;
    }

    public void Transfer(string creditAccountId, string debitAccountId, decimal amount)
    {
        var creditAccount = _accounts.FindById(creditAccountId);
        var debitAccount = _accounts.FindById(debitAccountId);

        if (debitAccount is null)
        {
            debitAccount = new Account(debitAccountId);
            _accounts.Add(debitAccount);
        }

        if(creditAccount is null) throw new InvalidOperationException($"Credit account with the id '{creditAccountId}' not found.");

        creditAccount.Credit(amount);
        debitAccount.Debit(amount);

        _accounts.Update(creditAccount);
        _accounts.Update(debitAccount);
    }
}