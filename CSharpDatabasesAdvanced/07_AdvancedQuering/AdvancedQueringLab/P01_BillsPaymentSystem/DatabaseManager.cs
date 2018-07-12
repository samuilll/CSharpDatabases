using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P01_BillsPaymentSystem
{
  public  class DatabaseManager
    {
        private BillsPaymentSystemContext db;

        public DatabaseManager(BillsPaymentSystemContext database)
        {
            this.db = database;
        }

        public  void ResetDatabase()
        {
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            SeedData();
        }

        private  void SeedData()
        {
            var users = new User[]
                {
                    new User("Samuil","Dechev","sam@abv.th","HOndaCivik"),
                    new User("Ivana","Ginova","Gosho@abv.th","sdfdsfdsf"),
                    new User("Mahamud","Beshirov","Gundsi@abv.th","Ininin")

                };

            var creditCards = new CreditCard[]
                {
                    new CreditCard(23456.23m,10000,DateTime.Parse("2017-07-07")),
                    new CreditCard(23456.23m,234,DateTime.Parse("2012-07-07")),
                    new CreditCard(23456.23m,234,DateTime.Parse("1992-07-07")),
                    new CreditCard(123123.23m,43545,DateTime.Parse("1952-07-07")),
                    new CreditCard(123213.23m,56,DateTime.Parse("1942-07-07")),
                    new CreditCard(24342.23m,5555,DateTime.Parse("1982-07-07")),
                    new CreditCard(35654.23m,345,DateTime.Parse("1972-07-07")),
                    new CreditCard(33333.23m,444,DateTime.Parse("1962-07-07")),
                    new CreditCard(56767.23m,3455,DateTime.Parse("1952-07-07"))


                };
            var bankAccounts = new BankAccount[]
                {
                    new BankAccount(32000.00m,"FIB","OIUOIUOIU"),
                    new BankAccount(234324.00m,"CCB","JLJLKJLK"),
                    new BankAccount(2342344.00m,"KTB","BMNBMNBNB")

                };
            var paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod(PaymentMethodType.CreditCard,users[0],null,creditCards[0]),
                    new PaymentMethod(PaymentMethodType.CreditCard,users[0],null,creditCards[5]),
                    new PaymentMethod(PaymentMethodType.CreditCard,users[0],null,creditCards[4]),
                    new PaymentMethod(PaymentMethodType.CreditCard,users[0],null,creditCards[3]),
                    new PaymentMethod(PaymentMethodType.BankAccount,users[0],bankAccounts[0],null),
                     new PaymentMethod(PaymentMethodType.CreditCard,users[1],null,creditCards[1]),
                     new PaymentMethod(PaymentMethodType.BankAccount,users[1],bankAccounts[1],null),
                     new PaymentMethod(PaymentMethodType.CreditCard,users[2],null,creditCards[2]),
                     new PaymentMethod(PaymentMethodType.BankAccount,users[2],bankAccounts[2],null),

                };

            db.Users.AddRange(users);
            db.CreditCards.AddRange(creditCards);
            db.BankAccounts.AddRange(bankAccounts);
            db.PaymentMethods.AddRange(paymentMethods);

            db.SaveChanges();
        }

        public  void PayBills(int userId, decimal amount)
        {
            var bankAccounts = db.PaymentMethods
                .Where(pm => pm.Type == PaymentMethodType.BankAccount && pm.UserId == userId)
                .Select(pm => pm.BankAccount)
                .OrderBy(ba=>ba.BankAccountId)
                .ToList();

            var creditCars = db.PaymentMethods
                            .Where(pm => pm.Type == PaymentMethodType.CreditCard && pm.UserId == userId)
                            .Select(pm => pm.CreditCard)
                            .OrderBy(cc=>cc.CreditCardId)
                            .ToList();
        
            var allMoney = bankAccounts.Sum(ba => ba.Balance) + creditCars.Sum(ca => ca.LimitLeft);

            bool canPay = amount <= allMoney;

            if (canPay)
            {
                WithdrawMoney(amount, bankAccounts, creditCars);
                db.SaveChanges();
            }

            else
            {
                throw new ArgumentException("Insufficient funds!");
            }        
        }

        private  void WithdrawMoney(decimal amount, List<BankAccount> bankAccounts, List<CreditCard> creditCars)
        {
            while (amount > 0)
            {
                foreach (var account in bankAccounts)
                {
                    if (amount <= account.Balance)
                    {
                        account.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= account.Balance;
                        account.Withdraw(account.Balance);
                    }
                }
                foreach (var card in creditCars)
                {
                    if (amount <= card.LimitLeft)
                    {
                        card.Withdraw(amount);
                        amount = 0;
                        return;
                    }
                    else
                    {
                        amount -= card.LimitLeft;
                        card.Withdraw(card.LimitLeft);
                    }
                }
            }

        }
    }
}
