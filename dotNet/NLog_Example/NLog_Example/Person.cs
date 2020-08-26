using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Example
{
    public class Person
    {

        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public override string ToString()
        {
            return string.Format("{0} , Age: {1}", this.Name, this.Age);
        }

    }
}
