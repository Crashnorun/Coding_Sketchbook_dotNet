using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Runtime.InProcess;
using Rhino.Geometry;

namespace RhinoInsideTest
{
    internal class Program
    {

        static Program()
        {
            RhinoInside.Resolver.Initialize(); ;
        }








        [System.STAThread]
        static void Main(string[] args)
        {

            try
            {
                using (new RhinoCore(args))
                {
                    Sphere s = new Sphere(Point3d.Origin, 12);
                    Brep b = s.ToBrep();
                    MeshingParameters mp = new MeshingParameters(0.5);
                    Mesh[] m = Mesh.CreateFromBrep(b, mp);
                    Console.WriteLine(m[0].Vertices.Count);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
