using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Regex_01
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            Console.WriteLine("Enter string:");
            do
            {
                string input = Console.ReadLine();                                              // read input
                bool valid = Validation(input);                                                 // validate input

                if (valid)
                    Console.WriteLine("Your value is valid");
                else
                    Console.WriteLine("Your value is not valid");

                Console.WriteLine("Enter string:");

                key = Console.ReadKey();                                                        // wait for escape key
            } while (key.Key != ConsoleKey.Escape);

        }


        static bool Validation(string str, bool special = false)
        {
            int passsed = 0;
            List<char> letters1 = new List<char>();                                             // Capital letters
            List<char> letters2 = new List<char>();                                             // lower case letters
            List<char> numbers = new List<char>();                                              // numbers
            List<char> symbls = new List<char>();                                               // special characters

            for (int i = 48; i <= 57; i++)                                                      // populate numbers                   
                numbers.Add((char)i);

            for (int i = 65; i <= 90; i++)                                                      // populate capital letters
                letters1.Add((char)i);

            for (int i = 97; i <= 122; i++)                                                     // populate lower case letters
                letters2.Add((char)i);

            if (str.Length >= 8)                                                                // check length
                passsed++;
            else
                return false;

            for (int i = 0; i < str.Length; i++)
                if (letters1.Contains(str[i]))
                {
                    passsed++;
                    break;
                }

            if (passsed < 2) return false;

            for (int i = 0; i < str.Length; i++)
                if (letters2.Contains(str[i]))
                {
                    passsed++;
                    break;
                }

            if (passsed < 3) return false;

            for (int i = 0; i < str.Length; i++)
                if (numbers.Contains(str[i]))
                {
                    passsed++;
                    break;
                }

            if (special)
            {
                for (int i = 33; i <= 46; i++) symbls.Add((char)i);                             // populate special chars                   
                for (int i = 58; i <= 64; i++) symbls.Add((char)i);
                for (int i = 91; i <= 96; i++) symbls.Add((char)i);
                for (int i = 123; i <= 126; i++) symbls.Add((char)i);

                for (int i = 0; i < str.Length; i++)
                    if (symbls.Contains(str[i]))
                    {
                        passsed++;
                        break;
                    }

                if (passsed >= 5) return true;
            }
            if (passsed >= 4) return true;
            else return false;
        }
    }
}
