using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;
using SystemJsonTest;
using UTF8Json;

namespace JSON_Comparison
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Human> people = new List<Human>();

            for(int i = 0; i < 100000; i++)
                people.Add(new Human(i.ToString(), i.ToString(), i, DateTime.Now));

            string data = NewtonSoftTest.TestNewtonSoft.SerializeData(people);
            string data2 = SystemJsonTest.TestSystemJson.SerializeData(people);
            string data3 =UTF8Json.TestUTF8Json.SerializeData(people);
            Console.ReadKey();

        }
    }
}
