using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Pointers
{
    internal class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Human() { }

        public Human(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public override string ToString()
        {
            return Name+ " " + Age;
        }
    }
}
