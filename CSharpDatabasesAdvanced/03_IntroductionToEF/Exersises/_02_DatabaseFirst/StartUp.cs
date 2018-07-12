using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace  P02_DatabaseFirst
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {

                var solution = new Solutions(db);

                solution.Task16();
            }

        }
    }
}
