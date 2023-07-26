using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicMath;
namespace FodyTests
{
    /// <summary>
    /// This project references the BasicMath project
    /// This project contains the Costura Fody nuget package
    /// When the nuget package was not installed, the project would compile with the Basic math dll in the out put folder
    /// Once the Fody package was installed, this project combiled with only the exe, and the masic math dll was embedded.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BasicMath.Math.Add(12, 12));

            


            Console.WriteLine("---- DONE ----");
            Console.ReadKey();
        }
    }
}
