namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_new_debit_account(
        string debitAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator,
        TransactionQueries queries)
    {
        var creditAccount = Build.AnAccount.WithBalance(20000).Please();
        accountOrchestrator.OpenAccount(
            creditAccount.Id,
            creditAccount.Balance.ConvertToPositive());

        orchestrator.Transfer(creditAccount.Id, debitAccountId, 10000);

        queries.GetBalanceForAccount(debitAccountId)
            .Should()
            .BeEquivalentTo(new { Balance = 10000 });
    }
    
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_old_debit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator,
        TransactionQueries queries)
    {
        var creditAccount = Build.AnAccount.WithBalance(20000).Please();
        accountOrchestrator.OpenAccount(
            creditAccount.Id,
            creditAccount.Balance);
        var debitAccount = Build.AnAccount.Please();
        accountOrchestrator.OpenAccount(
            creditAccount.Id,
            creditAccount.Balance);

        orchestrator.Transfer(creditAccount.Id, debitAccount.Id, 10000);

        queries.GetBalanceForAccount(debitAccount.Id)
            .Should()
            .BeEquivalentTo(new { Balance = 10000 });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_to_the_credit_account(
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator,
        AccountOrchestrator accountOrchestrator,
        TransactionQueries queries)
    {
        var creditAccount = Build.AnAccount.WithBalance(25000).Please();

        accountOrchestrator.OpenAccount(creditAccount.Id, creditAccount.Balance);

        orchestrator.Transfer(creditAccount.Id, "dummy", 10000);

        queries.GetBalanceForAccount(creditAccount.Id).Should().BeEquivalentTo(new { Balance = 15000 });
    }

    [Theory, AutoMoqData]
    public void throw_exception_when_credit_account_not_found(
        string creditAccountId,
        [Frozen] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator orchestrator)
    {
        var actual =()=> orchestrator.Transfer(creditAccountId, "dummy", 10000);

        actual.Should().ThrowExactly<InvalidOperationException>();
    }
}