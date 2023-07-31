using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.Runtime.InProcess;

namespace RhinoInsideTest2
{
    internal class Program
    {
        static Program()
        {
            try
            {
                RhinoInside.Resolver.Initialize();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }



        static void Main(string[] args)
        {
            try
            {
                using (new RhinoCore(args))
                {
                    // MeshABrep();
                }
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            


            Console.WriteLine("----DONE----");
            Console.ReadKey();
        }

        static void MeshABrep()
        {
            Sphere sphere = new Sphere(Point3d.Origin, 12);
            Brep rep = sphere.ToBrep();
            MeshingParameters mp = new MeshingParameters(0.5); ;
            Mesh[] msh = Mesh.CreateFromBrep(rep, mp);
            Console.WriteLine($"Mesh with {msh[0].Vertices.Count} verticies");
        }
    }
}
