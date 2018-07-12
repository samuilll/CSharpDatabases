using System;
using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
   public class CreditCard
    {
        public CreditCard()
        {
                
        }
        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
            Limit = limit;
            MoneyOwed = moneyOwed;
            ExpirationDate = expirationDate;
        }

        public int CreditCardId { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public override string ToString()
        {
            return $"-- ID: {this.CreditCardId}{Environment.NewLine}" +
                   $"--- Limit: {this.Limit:f2}{Environment.NewLine}" +
                   $"--- MoneyOwed: {this.MoneyOwed:f2}{Environment.NewLine}" +
                   $"--- Limit Left: {this.LimitLeft}"+
                   $"--- Expiration Date: {this.ExpirationDate.Year}/{this.ExpirationDate.Month}";

        }

        public void Withdraw(decimal amount)
        {
            if (amount > this.LimitLeft)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            this.MoneyOwed -= amount;
        }

    }
}
