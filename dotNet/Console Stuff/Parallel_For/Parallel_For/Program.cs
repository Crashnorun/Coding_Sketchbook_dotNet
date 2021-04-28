using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Parallel_For
{
    class Program
    {



        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            List<int> newNumsForEach = new List<int>();
            List<int> newNumsFor = new List<int>();
            List<int> newNumsOld = new List<int>();

            for (int i = 0; i < 10000; i++)
            {
                nums.Add(i);
                newNumsOld.Add((int)Math.Pow(i, 2));
            }
            Console.WriteLine("newNumsOld " + nums.Count);


           /* Parallel.ForEach(nums, num =>
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                newNumsForEach.Add((int)Math.Pow(num, 2));
                //Console.WriteLine(num + " " + (int)Math.Pow(num, 2));
            });
            Console.WriteLine("newNumsForEach " + newNumsForEach.Count);*/


          /*  Parallel.For(0, nums.Count, num =>
            {
                Console.BackgroundColor = ConsoleColor.Red;
                newNumsFor.Add((int)Math.Pow(num, 2));
                //Console.WriteLine(num + " " + (int)Math.Pow(num, 2));
            });
            Console.WriteLine("newNumsFor " + newNumsFor.Count);*/


          /*  Console.BackgroundColor = ConsoleColor.Green;


            for (int i = 0; i < nums.Count; i++)
            {
                Console.WriteLine(string.Format("{0} \t {1} \t {2}", newNumsOld[i], newNumsFor[i], newNumsForEach[i]));
            }*/


            List<MethodInfo> meths = new List<MethodInfo>();
            meths.Add(typeof(Program).GetMethod("Add"));
            meths.Add(typeof(Program).GetMethod("Subtract"));

            Parallel.ForEach(meths, meth =>
            {
               var obj=  meth.Invoke(null, new object[] { nums });
            });

            //Console.ReadKey();
        }


        public static List<int> Subtract(List<int> nums)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            List<int> newNums = new List<int>();
            foreach (int num in nums) newNums.Add(num - num);
            return newNums;
        }


        public static List<int> Add(List<int> nums)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            List<int> newNums = new List<int>();
            foreach (int num in nums) newNums.Add(num + num);
            return newNums;
        }
    }
}
