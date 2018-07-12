using System;
using System.Collections.Generic;
using System.Text;

namespace DBAppsIntroductionProject
{
  public  class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

     public   string FirstName { get; }

      public  string LastName { get; }

       

    }
}
