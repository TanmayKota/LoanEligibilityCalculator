public static class FakeLoanDatabase
{
    public static List<UserAccount> Accounts = new List<UserAccount>
    {
        new UserAccount
        {
            UID = "1001",
            FullName = "Tanmay Kota",
            CurrentBalance = 6200.50,
            Transactions = new List<Transaction>
            {
                new Transaction { Date = DateTime.Today.AddMonths(-1), Amount = 3000, Description = "Salary" },
                new Transaction { Date = DateTime.Today.AddMonths(-1), Amount = -1200, Description = "Rent" },
                new Transaction { Date = DateTime.Today.AddMonths(-2), Amount = 3000, Description = "Salary" },
                new Transaction { Date = DateTime.Today.AddMonths(-2), Amount = -1400, Description = "Bills" },
                new Transaction { Date = DateTime.Today.AddMonths(-3), Amount = 3000, Description = "Salary" },
                new Transaction { Date = DateTime.Today.AddMonths(-3), Amount = -1000, Description = "Food" },
            }
        }
    };
}
