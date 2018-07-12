using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Initializer;
using System;

namespace HospitalStartUp
{
    class StartUp
    {
        static void Main(string[] args)
        {

            //DatabaseInitializer.ResetDatabase();
            using (var db = new HospitalContext())
            {
                //foreach (var item in db.Vi)
                //{

                //}
            }
        }
    }
}
