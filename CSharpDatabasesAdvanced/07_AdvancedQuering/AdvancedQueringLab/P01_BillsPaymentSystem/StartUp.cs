using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Linq;

namespace P01_BillsPaymentSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var db1 = new BillsPaymentSystemContext();
            var db2 = new BillsPaymentSystemContext();

            var user1 = db1.Users.First();
            var user2 = db2.Users.First();

            user1.FirstName = "Hinko";

            ;
            return;

            using (var db = new BillsPaymentSystemContext())
            {
               var manager = new DatabaseManager(db);

                // manager.ResetDatabase();

                //var users = db.Users.ToList();

                //var user1 = users[0];
                //var user2 = users[1];

                //var user3 = new User("Piotr","Karamazov","fon@abv.nh","fonzon");

                //var state3 = db.Entry(user3).State = EntityState.Added;

                //var state1 = db.Entry(user1).State = EntityState.Detached;

                //var state2 = db.Entry(user2).State;

                //user1.FirstName = "Samuil";

                //user2.FirstName = "Bogdan";

                //var query = @"exec proc_ChangeFirstName {0},{1}";

                //var result = db.Database.ExecuteSqlCommand(query,1,"Samuil");

                // Console.WriteLine(result);

                var methods = db.PaymentMethods.ToList();


                Console.WriteLine(methods[0].User.FirstName);
               // db.SaveChanges();


                foreach (var e in db.Users.ToList())
                {
                    Console.WriteLine(e);
                }


                ;
                // manager.PayBills(1, 266000);
                //  new Engine().Run(db);             
            }
           
        }
    
    }
}
