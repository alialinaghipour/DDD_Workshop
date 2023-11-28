public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(
        string accountId,
        decimal balance,
        [Frozen] Accounts _,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator)
    {
        accountOrchestrator.OpenAccount(accountId, balance.ConvertToPositive());
        
        queries.GetBalanceForAccount(accountId)
            .Should()
            .BeEquivalentTo(new { Balance = balance.ConvertToPositive() });
    }

    [Theory, AutoMoqData]
    public void throw_exception_when_the_balance_is_less_then_zero(
        string accountId,
        decimal balance,
        [Frozen] Accounts _,
        AccountOrchestrator accountOrchestrator)
    {
        var actual =  ()=> accountOrchestrator
            .OpenAccount(accountId, balance.ConvertToNegative());

        actual.Should().ThrowExactly<InvalidOperationException>();
    }
}