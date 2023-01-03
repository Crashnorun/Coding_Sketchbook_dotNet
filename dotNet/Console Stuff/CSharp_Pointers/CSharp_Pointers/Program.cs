using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Pointers
{
    internal class Program
    {
        static string delimiter = "\n ---------------- \n";

        static void Main(string[] args)
        {

            AssigningValuesToVariables();
            Console.WriteLine(delimiter);

            Pointers();
            Console.WriteLine(delimiter);

            Console.ReadKey();
        }


        static void AssigningValuesToVariables()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            int A = 100;
            int B = A;

            Console.WriteLine("A = " + A);
            Console.WriteLine("B = " + B);
            /* A = 100
             * B = 100
             */


            A = 101;

            Console.WriteLine("A = " + A);
            Console.WriteLine("B = " + B);
            /* A = 101
             * B = 100
             */
        }


        /// <summary>
        /// Pointers need to use the keyword UNSAFE
        /// https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0227?f1url=%3FappId%3Droslyn%26k%3Dk(CS0227)
        /// https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0214?f1url=%3FappId%3Droslyn%26k%3Dk(CS0214)
        /// 
        /// <Reference>https://www.codingunit.com/cplusplus-tutorial-pointers-reference-and-dereference-operators</Reference>
        /// </summary>
        unsafe static void Pointers()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            // this declares a variable
            Human h1 = new Human("Bob", 10);

            // this declares a pointer
            Human* ptr_h2 = &h1;

            // print the values as is
            Console.WriteLine("H1 = " + h1.ToString());
            Console.Write("ptr_H2 = ");
            Console.WriteLine((long)ptr_h2);


            // & gets a pointer from a value
            Console.Write("Address of H1 = ");
            Console.WriteLine((long)&h1);   // print the address of h1

            // * gets the value from a pointer
            Console.Write("Value of ptr_H2 = ");
            Console.WriteLine(*ptr_h2);     // print the value of the pointer

        }
    }
}
