public class UserAccount
{
    public string UID { get; set; }
    public string FullName { get; set; }
    public double CurrentBalance { get; set; }
    public List<Transaction> Transactions { get; set; }
}
