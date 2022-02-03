using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> generic = new List<string>();
            string s;

            var o = from g in generic
                    where g != null
                    select g;

            Console.ReadKey();
        }


        public static void GetName(string input, Type type)
        {
           // Console.WriteLine((type.GetType()) input.name); ;
        }
    }


    public class Dog<T>
    {
        public string name = "dog";
        public string breed = "maltese";
    }

    public class Cat
    {
        public string name = "cat";
        public string color = "brown";
    }



}
