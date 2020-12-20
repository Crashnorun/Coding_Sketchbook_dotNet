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
            Person p2 = new Person("Bob", 12);

            GetAllPropertyNamesAndValues(p);

            Person p3 = new Person(p);





            Console.ReadKey();
        }


        public static double Add(double num1, double num2)
        {
            Console.WriteLine("Entered: " + MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(string.Format("\tnum1 : {0} | num2: {1}", num1, num2));

            Console.WriteLine("\tExiting: " + MethodBase.GetCurrentMethod().Name + " : " + num1 + num2);
            return num1 + num2;
        }


        public static void GetAllPropertyNamesAndValues(object T)
        {
            Console.WriteLine(T.GetType().Name);

            PropertyInfo[] propInfo = T.GetType().GetProperties();
            foreach (PropertyInfo info in propInfo)
            {
                // if(info.GetType().IsGenericType && info.GetType().GetGenericTypeDefinition() == typeof(List<>))
                // if (info.PropertyType == typeof(List<>))
                if (info.PropertyType.Name.Contains("List"))
                    Console.WriteLine(string.Format("\t{0} ({1}<{2}>) : {3}", info.Name, info.PropertyType.Name, info.PropertyType.GetGenericArguments()[0].Name, info.GetValue(T)));
                else   // this also catches arrays
                    Console.WriteLine(string.Format("\t{0} ({1}) : {2}", info.Name, info.PropertyType.Name, info.GetValue(T)));
            }
        }
    }

    //---------------------------------------------------------------------------------

    public class Person
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<string> Children { get; set; }
        public decimal[] Nums { get; set; }

        #region ----CONSTRUCTORS----

        public Person() { }


        public Person(string Name, int Age)
        {
            this.Age = Age;
            this.FirstName = Name;
        }


        /// <summary>
        /// Create object from object
        /// </summary>
        /// <param name="person"></param>
        public Person(Person person)
        {
            PropertyInfo[] pinfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in pinfo)
            {
                p.SetValue(this, person.GetType().GetProperty(p.Name).GetValue(person));
            }
        }

        #endregion





    }
}
