using System;
using System.Collections.Generic;

public class LoanService : ILoanService
{
    public LoanResult EvaluateLoanEligibility(string uid)
    {
        // Find the user in the fake database
        UserAccount user = null;
        foreach (var account in FakeLoanDatabase.Accounts)
        {
            if (account.UID == uid)
            {
                user = account;
                break;
            }
        }

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Filter transactions from the last 3 months
        List<Transaction> recentTransactions = new List<Transaction>();
        DateTime cutoffDate = DateTime.Today.AddMonths(-3);
        foreach (var tx in user.Transactions)
        {
            if (tx.Date >= cutoffDate)
            {
                recentTransactions.Add(tx);
            }
        }

        // Separate income and expenses
        double totalIncome = 0;
        double totalExpenses = 0;
        int incomeCount = 0;
        int salaryIncomeCount = 0;
        int overdraftCount = 0;

        foreach (var tx in recentTransactions)
        {
            if (tx.Amount > 0)
            {
                totalIncome += tx.Amount;
                incomeCount++;

                if (tx.Description != null && tx.Description.ToLower().Contains("salary"))
                {
                    salaryIncomeCount++;
                }
            }
            else if (tx.Amount < 0)
            {
                totalExpenses += Math.Abs(tx.Amount);

                if (Math.Abs(tx.Amount) > user.CurrentBalance)
                {
                    overdraftCount++;
                }
            }
        }

        double avgIncome = totalIncome / 3.0;
        double avgExpense = totalExpenses / 3.0;
        bool isIncomeStable = salaryIncomeCount >= 3;
        bool lowSpending = avgExpense < (0.5 * avgIncome);

        // Calculate max loan amount
        double multiplier = isIncomeStable ? 10 : 6;
        double maxLoan = (avgIncome - avgExpense) * multiplier;

        // Calculate interest rate
        double rate = 10.0;
        if (user.CurrentBalance >= 5000)
        {
            rate -= 1.5;
        }
        if (lowSpending)
        {
            rate -= 1.0;
        }
        if (!isIncomeStable)
        {
            rate += 1.5;
        }
        if (overdraftCount > 0)
        {
            rate += 2.0;
        }

        // Return result
        LoanResult result = new LoanResult
        {
            MaxLoanAmount = Math.Round(maxLoan, 2),
            InterestRate = Math.Round(rate, 2)
        };

        return result;
    }
}
