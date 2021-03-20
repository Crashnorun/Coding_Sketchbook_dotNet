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
            // add two numbers
            Console.WriteLine(Add(1, 2));

            Person p = new Person("Charlie", 40);
            Person p2 = new Person("Bob", 12);
            Person p3 = new Person(p);


            // get all properties and values
            GetAllPropertyNamesAndValues(p);


            // iterate over all properties using an index
            IterateOverPropertiesUsingIndex(p);


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

            Node n = new Node() { NodeIndex = 10, NodeName = "C", X = 1, Y = 2, Z = 3, AnotherName = "v" };
            SubNode sn = new SubNode() { NodeIndex = 10, X = 1, Y = 2, Z = 3 };

            SubNode sn2 = n;

            GetAllPropertyNamesAndValues(n);
            GetAllPropertyNamesAndValues(sn);


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

            // get all the properties
            //PropertyInfo[] propInfo = T.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly| BindingFlags.FlattenHierarchy);
            PropertyInfo[] propInfo = T.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo info in propInfo)
            {
                // if(info.GetType().IsGenericType && info.GetType().GetGenericTypeDefinition() == typeof(List<>))
                // if (info.PropertyType == typeof(List<>))
               

                foreach (CustomAttributeData att in info.CustomAttributes)
                {
                   Console.WriteLine(string.Format("{0}" , att.AttributeType.Name));
                    
                    foreach(CustomAttributeNamedArgument arg in att.NamedArguments)
                    {
                        Console.WriteLine(arg.MemberName + " " + arg.MemberInfo + " " + arg.TypedValue);
                    }
                }

                string publicPrivate = info.GetAccessors(true)[0].IsPublic ? "Public" : "Private";

                if (info.PropertyType.Name.Contains("List"))
                    Console.WriteLine(string.Format("\t{0} ({1} {2}<{3}>) : {4}", info.Name, publicPrivate, info.PropertyType.Name, info.PropertyType.GetGenericArguments()[0].Name, info.GetValue(T)));
                else   // this also catches arrays
                {
                    Console.WriteLine(string.Format("\t{0} ({1} {2}) : {3}", info.Name, publicPrivate, info.PropertyType.Name, info.GetValue(T)));
                }

            }

            Console.WriteLine("");

            FieldInfo[] fieldInfo = T.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (FieldInfo info in fieldInfo)
            {
                string publicPrivate = info.IsPublic ? "Public" : "Private";
                Console.WriteLine(string.Format("\t{0} ({1} {2}) : {3}", info.Name, publicPrivate, info.FieldType.Name, info.GetValue(T)));
            }



            BooleanTests();


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


        public static void BooleanTests()
        {
            Console.WriteLine(string.Format("1 = {0}", Convert.ToBoolean(1)));
            Console.WriteLine(string.Format("0 = {0}", Convert.ToBoolean(0)));
        }





    }
}
