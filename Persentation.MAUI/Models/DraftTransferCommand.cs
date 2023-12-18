namespace Persentation.MAUI;

public class DraftTransferCommand
{
    public string TransactionId { get; set; }
    public string CreditAccountId { get; set; }
    public string DebitAccountId { get; set; }
    public decimal Amount { get; set; }
}