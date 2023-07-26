using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoCommonTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point3d pt = CreatePoint();
            Console.WriteLine(pt);

            Console.WriteLine("---- DONE ----");
            Console.ReadKey();
        }


        public static Point3d CreatePoint()
        {
            return new Point3d(3, 3, 3);
        }


    }
}
