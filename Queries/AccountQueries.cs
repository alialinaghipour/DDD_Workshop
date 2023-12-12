public class AccountQueries
{
    Accounts accounts;
    public AccountQueries(Accounts accounts)
    =>this.accounts = accounts;
    public BalanceViewModel? GetBalanceForAccount(string accountId)
    {
        var theAccount= accounts.FindById(accountId);
        if(theAccount is null) return null;
        return new BalanceViewModel(Id: theAccount.Id.Id, Balance:theAccount.Balance.Value);
    }

    public IEnumerable<GetAllAccountsViewModel> GetAll()
    {
        return accounts.GetAll()
            .Select(_ => new GetAllAccountsViewModel(_.Id.Id, _.Balance.Value));
    }
}