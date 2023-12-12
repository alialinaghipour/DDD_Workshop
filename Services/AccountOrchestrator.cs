public class AccountOrchestrator
{
    private readonly Accounts accounts;
    public AccountOrchestrator(Accounts accounts)
    {
        this.accounts = accounts;
    }
    public void OpenAccount(OpenAccountCommand command)
    {
        accounts.Add(new Account(command.AccountId, command.InitialBalance));
    }
}