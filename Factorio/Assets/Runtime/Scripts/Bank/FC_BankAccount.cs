using System.Collections.Generic;

public class FC_BankAccount
{
    public int accountNumber { get; private set; }
    public float balance { get; private set; }
    
    public List<FC_TransactionData> transactionHistory { get; private set; } = new();

    public FC_BankAccount(int accountNumber, float initialBalance)
    {
        this.accountNumber = accountNumber;
        balance = initialBalance;
    }

    public bool Deposit(float amount)
    {
        if (amount <= 0) return false;
        balance += amount;
        return true;
    }

    public bool Withdraw(float amount)
    {
        if (amount <= 0 || amount > balance) return false;
        balance -= amount;
        return true;
    }

    public bool Transfer(float amount, FC_BankAccount targetAccount)
    {
        if (Withdraw(amount))
        {
            targetAccount.Deposit(amount);
            return true;
        }
        return false;
    }
}