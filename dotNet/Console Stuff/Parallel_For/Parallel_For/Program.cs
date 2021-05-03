using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

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

            // create list of ints
            for (int i = 0; i < 10000000; i++)
            {
                nums.Add(i);
                //newNumsOld.Add(i);
            }
            Console.WriteLine("newNumsOld " + nums.Count);


            List<int> AddedNums = new List<int>();
            List<int> SubtractedNums = new List<int>();
            List<double> DividedNums = new List<double>();


            List<MethodInfo> meths = new List<MethodInfo>();
            meths.Add(typeof(Program).GetMethod("Add"));
            meths.Add(typeof(Program).GetMethod("Subtract"));
            meths.Add(typeof(Program).GetMethod("Divide"));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Parallel.ForEach(meths, meth =>
             {
                 var obj = meth.Invoke(null, new object[] { nums });
                 if (obj.GetType() == typeof(List<double>)) DividedNums = (List<double>)obj;
                 else
                 {
                     if (meth.Name == "Add") AddedNums = (List<int>)obj;
                     else SubtractedNums = (List<int>)obj;
                 }
             });
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            // https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke?view=net-5.0

            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            foreach (MethodInfo meth in meths)
            {
                var obj = meth.Invoke(null, new object[] { nums });
            }
            watch2.Stop();
            Console.WriteLine(watch2.ElapsedMilliseconds);

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


        public static List<double> Divide(List<int> nums)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            List<double> newNums = new List<double>();
            foreach (int num in nums) newNums.Add(num / 2.0);
            return newNums;
        }


        public static void CompareParallelLoops()
        {
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
        }

    }
}
