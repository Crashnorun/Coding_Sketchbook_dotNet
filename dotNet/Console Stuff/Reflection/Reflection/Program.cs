using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        public static string Name = "Charlie";

        static void Main(string[] args)
        {
            Console.WriteLine(Add(1, 2));

            Console.WriteLine(Name);

            Person p = new Person("Charlie", 40);

            PropertyInfo[] pInfo = p.GetType().GetProperties();
            foreach (PropertyInfo info in pInfo)
            {
                var val = info.GetValue(p).ToString();

                // Type pType = info.GetType();
                // string val = pType.GetProperty(info.Name).GetValue(p).ToString();

                Console.WriteLine("\t" + info.Name + " : " + val);
            }




            Console.ReadKey();
        }

        public static double Add(double num1, double num2)
        {
            Console.WriteLine("Entered: " + MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(string.Format("\tnum1 : {0} | num2: {1}", num1, num2));

            return num1 + num2;
        }
    }

    public class Person
    {
        public string PersonName { get; set; }
        public int PersonAge { get; set; }

        public Person(string Name, int Age)
        {
            this.PersonAge = Age;
            this.PersonName = Name;
        }
    }
}
