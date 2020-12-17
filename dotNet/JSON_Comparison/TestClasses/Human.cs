using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClasses
{
    public class Human
    {

        #region ---- PROPERTIES ----

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }

        #endregion


        #region ---- CONSTRUCTORS ----

        public Human() { }

        public Human(string FirstName = "", string LastName ="", int Age = 0, DateTime BirthDate = new DateTime())
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Age = Age;
            this.BirthDate = BirthDate;
        }

        #endregion

    }
}
