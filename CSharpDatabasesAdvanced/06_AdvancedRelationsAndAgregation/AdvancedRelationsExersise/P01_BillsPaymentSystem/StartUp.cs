using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;

namespace P01_BillsPaymentSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {

            using (var db = new BillsPaymentSystemContext())
            {
                var manager = new DatabaseManager(db);

                manager.ResetDatabase();

                manager.PayBills(1, 266000);
               //  new Engine().Run(db);             
            }
           
        }
    
    }
}
