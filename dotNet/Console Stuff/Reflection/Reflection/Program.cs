using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
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
            
            TestReturningGeneric();

            
            //DeserialiseBinary();

            // Test how to convert 1 and 0 to true and false
            // BooleanTests();

            // regex testing
            RegexTesting();

            // testing logging entering and exiting a function
            // add two numbers
            Console.WriteLine(Add(1, 2));

            Person P0 = new Person();
            Console.WriteLine(P0.ToString());
            Person p = new Person("Charlie", 40);
            Console.WriteLine(p.ToString());
            Person p2 = new Person("Bob", 12);
            Person p3 = new Person(p);
            Person p4 = new Person("Charlie", 40);
            Person p5 = new Person(p);
            List<Person> people = new List<Person> { p, p2, p3, p4, p5 };

            Dictionary<string, string> vals2 = new Dictionary<string, string>();
            vals2.Add("FirstName", "Charlie");
            vals2.Add("LastName", "Port");
            Person p6 = new Person();
            PopulateAttributes(p6, vals2);


            IEnumerable<Person> unique = people.Distinct(new PersonComparer());
            HashSet<Person> unique2 = new HashSet<Person>(people);

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

            // testing inheritance 
            Node n = new Node() { NodeIndex = 10, NodeName = "C", X = 1, Y = 2, Z = 3, AnotherName = "v" };
            SubNode sn = new SubNode() { NodeIndex = 10, X = 1, Y = 2, Z = 3 };
            SubNode sn2 = n;

            GetAllPropertyNamesAndValues(n);
            GetAllPropertyNamesAndValues(sn);

            FindMemoryUsage();


            Console.ReadKey();
        }

        private static T TestReturningGeneric<T>(int i)
        {
            if (i == 0) return new object(10);
            else if (i == 1) return "hello";
            else return true;
        }

        public static void PopulateAttributes<T>(T obj, Dictionary<string, string> vals)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                var query = from att in vals
                            where att.Key == p.Name
                            select att.Value;

                if (query.Count() != 0)
                {
                    p.SetValue(obj, query.First());
                }
            }
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
                    Console.WriteLine(string.Format("{0}", att.AttributeType.Name));

                    foreach (CustomAttributeNamedArgument arg in att.NamedArguments)
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


        /// <summary>
        /// Test how to convert 1 and 0 to true and false
        /// </summary>
        public static void BooleanTests()
        {
            Console.WriteLine(string.Format("1 = {0}", Convert.ToBoolean(1)));
            Console.WriteLine(string.Format("0 = {0}", Convert.ToBoolean(0)));
        }

        /// <summary>
        /// Get the memory usage of the current process
        /// <Reference>https://stackoverflow.com/questions/2342023/how-to-measure-the-total-memory-consumption-of-the-current-process-programmatica</Reference>
        /// <Reference> https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.workingset64?view=net-5.0</Reference>
        /// </summary>
        public static void FindMemoryUsage()
        {
            Process currentProcess = Process.GetCurrentProcess();
            long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
            Console.WriteLine(totalBytesOfMemoryUsed);
        }


        public static void DeserialiseBinary()
        {
            string filePath = @"C:\Program Files\Computers and Structures\SAP2000 21\AISC.PRO";

            byte[] bytes = File.ReadAllBytes(filePath);

            // stream bytes
            using (StreamReader sr = new StreamReader(new MemoryStream(bytes)))
            {
                // try converting from json
                // var data =  JsonConvert.DeserializeObject(sr.ReadToEnd());
            }

            //--------------------------------

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter br = new BinaryFormatter();
                ms.Position = 0;
                var data = br.Deserialize(ms);
            }


            //   var str = System.Text.Encoding.Default.GetString(result);


            //--------------------------------

            BinaryFormatter f = new BinaryFormatter();
            using (FileStream str = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IFormatter br = new BinaryFormatter();
                var data = br.Deserialize(str);

                /*var data = f.Deserialize(str);
                str.Position = 0;*/

                StreamReader reader = new StreamReader(str);
                var result = reader.ReadToEnd();
            }
        }


        // used for regex testing
        public static void RegexTesting()
        {
            string value = " something_ is he-re ";
            Console.WriteLine(Regex.Replace(value, @"[^0-9a-zA-Z]+", ""));
        }

    }
}

