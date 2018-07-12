using Cars.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cars.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CarsDbContext())
            {
                ResetDatabase(db);
            }
        }

        private static void ResetDatabase(CarsDbContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.Migrate();

            Seed(db);
        }

        private static void Seed(CarsDbContext db)
        {
            throw new NotImplementedException();
        }
    }
}
