using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem
{
    public class Engine
    {
       
        internal void Run(BillsPaymentSystemContext db)
        {
            Console.WriteLine("Please enter the Id of the user:");

            int userId = int.Parse(Console.ReadLine());


            var user = db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new {
                    Name = string.Concat(u.FirstName," ", u.LastName),
                    CreditCards = u.PaymentMethods.Where(pm=>pm.Type==PaymentMethodType.CreditCard).Select(pm=>pm.CreditCard),
                    BankAccounts = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.BankAccount).Select(pm => pm.BankAccount)
                }).FirstOrDefault();

            if (user==null)
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            Console.WriteLine($"User: {user.Name}");

            foreach (var creditCard in user.CreditCards)
            {
                Console.WriteLine(creditCard);
            }
            foreach (var bankAccount in user.BankAccounts)
            {
                Console.WriteLine(bankAccount);
            }

        }
    }
}
