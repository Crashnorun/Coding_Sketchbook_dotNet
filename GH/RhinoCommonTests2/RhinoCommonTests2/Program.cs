using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;


namespace RhinoCommonTests2

{/// <summary>
 /// this project originally referenced the rhinocommon nuget package and didn't work
 /// for some reason when referencing the rhinocommon nuget package, it does not copy over, eventhough copylocal = true
 /// the issue here is due to McNeel doing 'witchcraft' on the build. See bullet point #3 in the link below
 /// https://developer.rhino3d.com/guides/rhinocommon/using-nuget/
 /// 
 /// I removed the rhinocommon nuget package and referenced the rhinocommon dll
 /// that does copy local
 /// 
 /// I tried installing fody costura and that didn't work, for some reason it rolls back once it gets to
 /// For adding package 'System.Runtime.InteropServices.RuntimeInformation.4.3.0' to project 'RhinoCommonTests2' that targets 'net48'.
 ///   For adding package 'System.Runtime.InteropServices.RuntimeInformation.4.3.0' to project 'RhinoCommonTests2' that targets 'net48'.
 ///   Adding package 'System.Runtime.InteropServices.RuntimeInformation.4.3.0' to folder 'C:\Users\PortelliC\OneDrive - Perkins and Will\Documents\GitHub\Coding_Sketchbook_dotNet\GH\RhinoCommonTests2\packages'
 ///   Install failed.Rolling back...
 ///   Package 'System.Runtime.InteropServices.RuntimeInformation.4.3.0' does not exist in project 'RhinoCommonTests2'
 /// I have a hunch that this is because rhinocommon nuget package was installed first.
 /// 
 /// I installed fody costura on another VS project, and copied the files from that package folder to this project's package folder
 /// I relaunched Visual Studio, and reinstalled Costura. The installation went well
 /// I compiled the project and executed it successfully.
 /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {

            Point3d pt = new Point3d(12, 34, 56);
            Console.WriteLine(pt);


            Sphere s = new Sphere(Point3d.Origin, 12);
            Brep b = s.ToBrep();
            MeshingParameters mp = new MeshingParameters(0.5);
            Mesh[] m = Mesh.CreateFromBrep(b, mp);
            Console.WriteLine(m[0].Vertices.Count);

           // Brep rep = new Brep();


            Console.WriteLine("---- DONE ----");
            Console.ReadKey();
        }
    }
}
