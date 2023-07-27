using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace RhinoCommonTests3
{

    /// <summary>
    /// in this project I installed fody Costura first then attempted to install the rhinocommon nuget package
    /// 
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {

            Point3d pt = new Point3d(12, 34, 56);
            Console.WriteLine(pt);

            Console.WriteLine("---- DONE ----");
            Console.ReadKey();
        }
    }
}
