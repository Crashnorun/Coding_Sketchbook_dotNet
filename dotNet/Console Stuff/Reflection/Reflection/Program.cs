using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{

    /*NOTES: The properties are listed in the order they are written in the class
     * 
     *TODO: 2. Instantiate a class / object from a string using the Create instance method
     * https://stackoverflow.com/questions/752/how-to-create-a-new-object-instance-from-a-type
     *  
     */



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

            IterateOverPropertiesUsingIndex(p);

            Person p3 = new Person(p);


            // Test invoking a method that has no input parameters using reflection
            int length = (int)p3.GetType().GetMethod("NameLength").Invoke(p3, null);
            Console.WriteLine(length);

            // test invoking a method that has input parameters using reflection
            int newAge = (int)p3.GetType().GetMethod("AddAge").Invoke(p3, new object[] { 10 });
            Console.WriteLine(newAge);

            Dictionary<string, int> vals = new Dictionary<string, int>();

            vals.Add("a", 1);
            vals.Add("z", 2);
            vals.Add("h", 2);

            Console.WriteLine(vals.Keys);

            Console.ReadKey();
        }


        /// <summary>
        /// add two numbers
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static double Add(double num1, double num2)
        {
            Console.WriteLine("Entered: " + MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(string.Format("\tnum1 : {0} | num2: {1}", num1, num2));

            Console.WriteLine("\tExiting: " + MethodBase.GetCurrentMethod().Name + " : " + num1 + num2);
            return num1 + num2;
        }


        /// <summary>
        /// get all properties and their vales
        /// </summary>
        /// <param name="T"></param>
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

            Console.WriteLine("");

            FieldInfo[] fieldInfo = T.GetType().GetFields();
            foreach (FieldInfo info in fieldInfo)
            {
                Console.WriteLine("\t" + info.FieldType.Name);
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }


        public static void IterateOverPropertiesUsingIndex(object T)
        {
            Console.WriteLine(T.GetType().Name);
            PropertyInfo[] propInfo = T.GetType().GetProperties();
            for (int i = 0; i < propInfo.Length; i++)
                Console.WriteLine(string.Format("\tIndex: {0} | Property: {1}", i, propInfo[i].Name));

            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }
    }


    //---------------------------------------------------------------------------------

    public class Person
    {

        #region ----PROPERTIES----

        private const string PrivateConstantString = "PRIVATE CONSTANT STRING";
        public const string PublicConstantString = "PUBLIC CONSTANT STRING";
        public string PersonName { get; set; }
        public int PersonAge { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<string> Children { get; set; }
        public decimal[] Nums { get; set; }

        #endregion


        #region ----CONSTRUCTORS----

        public Person() { }

        #endregion


        #region ----CONSTRUCTORS----

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

        
        #region ----METHODS----


        public int NameLength()
        {
            return PersonName.Length;
        }



        public int AddAge(int AddYears)
        {
            return PersonAge + AddYears;
        }

        #endregion
    }
}
