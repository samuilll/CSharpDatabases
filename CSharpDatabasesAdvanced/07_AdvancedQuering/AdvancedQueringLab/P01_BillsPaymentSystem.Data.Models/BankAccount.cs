using System;
using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public BankAccount()
        {

        }
        public BankAccount(decimal balance, string bankName, string swiftCode)
        {
            Balance = balance;
            BankName = bankName;
            SWIFTCode = swiftCode;
        }

        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SWIFTCode { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            return $"-- ID: {this.BankAccountId}{Environment.NewLine}" +
                   $"--- Balance: {this.Balance:f2}{Environment.NewLine}" +
                   $"--- Bank: {this.BankName}{Environment.NewLine}" +
                   $"--- SHIFT: {this.SWIFTCode}";
        }

        public void Withdraw(decimal amount)
        {
            if (amount>this.Balance)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }


    }
}
