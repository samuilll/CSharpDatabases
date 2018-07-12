using Demo_01.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo_01
{
    class Program
    {
        static void Main(string[] args)
        {
           // using (var db = new SoftUniDbContext())
          //  {
            //    var towns = db.Towns
            //        .Include(t => t.Addresses)
            //        .ThenInclude(a => a.Employees)
            //        .OrderByDescending(t => t.Addresses.Count)
            //        .ToList();

            //    foreach (var town in towns)
            //    {
            //        Console.WriteLine($"{town} has {town.Addresses.Count} addresses");

            //        foreach (var address in town.Addresses)
            //        {
            //            Console.WriteLine($"On {address.AddressText} live:");

            //            foreach (var employee in address.Employees)
            //            {
            //                Console.WriteLine(employee.LastName);
            //            }
            //        }
            //    }
            //}

        }
    }
}
