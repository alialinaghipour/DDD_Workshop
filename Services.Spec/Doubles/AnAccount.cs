public class AnAccount
{
    private string _id = Guid.NewGuid().ToString();
    private decimal _balance = 0;

    public AnAccount WithId(string id)
    {
        _id = id;
        return this;
    }

    public AnAccount WithBalance(decimal balance)
    {
        _balance = balance;
        return this;
    }

    public Account Please()
        => new Account(_id, _balance);
}