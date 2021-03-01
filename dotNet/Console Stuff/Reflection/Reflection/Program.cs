﻿using System;
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


            // Test invoking a method that has no input parameters using reflection
            int length = (int)p3.GetType().GetMethod("NameLength").Invoke(p3, null);
            Console.WriteLine(length);

            // test invoking a method that has input parameters using reflection
            int newAge = (int)p3.GetType().GetMethod("AddAge").Invoke(p3, new object[] { 10 });
            Console.WriteLine(newAge);

            Console.ReadKey();
        }

        public static double Add(double num1, double num2)
        {
            Console.WriteLine("Entered: " + MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(string.Format("\tnum1 : {0} | num2: {1}", num1, num2));

            return num1 + num2;
        }


        public static void GetAllPropertyNamesAndValues(object T)
        {
            Console.WriteLine(T.GetType().Name);

            PropertyInfo[] propInfo = T.GetType().GetProperties();
            foreach (PropertyInfo info in propInfo)
                Console.WriteLine("\t" + info.Name + " : " + info.GetValue(T));

            Console.WriteLine("");

            FieldInfo[] fieldInfo = T.GetType().GetFields();
            foreach (FieldInfo info in fieldInfo)
                Console.WriteLine("\t" + info.Name + " : " + info.GetValue(null));
        }
    }

    public class Person
    {

        #region ----PROPERTIES----

        private const string PrivateConstantString = "PRIVATE CONSTANT STRING";
        public const string PublicConstantString = "PUBLIC CONSTANT STRING";
        public string PersonName { get; set; }
        public int PersonAge { get; set; }

        #endregion


        #region ----CONSTRUCTORS----

        public Person(string Name, int Age)
        {
            this.PersonAge = Age;
            this.PersonName = Name;
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
