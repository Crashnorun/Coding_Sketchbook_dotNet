using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Add_Numbers_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first value:");
            string a = Console.ReadLine();

            double valA = ConvertNumber(a);
            if (valA.ToString() == double.NaN.ToString()) return;

            Console.WriteLine("Enter second value:");
            string b = Console.ReadLine();

            double valB = ConvertNumber(b);
            if (valB.ToString() == double.NaN.ToString()) return;
            
            Console.Write(valA + valB);
        }

        static double ConvertNumber(string val)
        {
            try
            {
                double valA = Convert.ToDouble(val);
                return valA;
            }
            catch (Exception ex)
            {
                Console.WriteLine("What you entered is not a number");
                return double.NaN;
            }
        }

    }
}

