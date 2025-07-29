public class Transaction
{
    public DateTime Date { get; set; }
    public double Amount { get; set; } // +ve = income, -ve = expense
    public string Description { get; set; }
}
