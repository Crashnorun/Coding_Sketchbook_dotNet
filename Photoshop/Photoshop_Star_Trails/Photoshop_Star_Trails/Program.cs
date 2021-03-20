using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photoshop;
using System.Drawing;

namespace Photoshop_Star_Trails
{
    class Program
    {
        public static string FolderPath = @"G:\2020_10_09_Pennsylvania_Sky";
        public static int startImage = 4478;
        public static int endImage = 4556;
        public static string prefix = "IMG_";
        public static string suffix = "_A.jpg";

        static void Main(string[] args)
        {

            Photoshop.Application app = new Photoshop.Application();
            app.Preferences.RulerUnits = Photoshop.PsUnits.psPixels;

            Bitmap img = (Bitmap)Bitmap.FromFile(string.Format("{0}\\{1}{2}{3}", FolderPath, prefix, startImage, suffix));
            int width = img.Width;
            int height = img.Height;
            Console.WriteLine("Image dimensions: " + width + " x " + height);

            Console.WriteLine("Creating new document and setting it as active document");
            Photoshop.Document newDoc = app.Documents.Add(width, height);

            double opacity = 100; // 100 / ((endImage - startImage) + 1);

            for (int i = startImage; i <= endImage; i++)
            {
                // app.ActiveDocument = newDoc;
                //ArtLayer newLayer = newDoc.ArtLayers.Add();
                //newLayer.Name = string.Format("{0}{1}{2}", prefix, i, suffix);
                //Console.WriteLine("\tAdding new layer: " + newLayer.Name + " to new Doc");

                // newLayer.Opacity = 10;
                //Console.WriteLine("\tSetting opacity of " + newLayer.Name + " to 10%");

                // get the image
                Console.WriteLine("\tOpening photo: " + string.Format("{0}\\{1}{2}{3}", FolderPath, prefix, i, suffix));
                Photoshop.Document doc = app.Open(string.Format("{0}\\{1}{2}{3}", FolderPath, prefix, i, suffix));

                // ArtLayer layer = doc.ActiveLayer;
                ArtLayer artLayer = doc.ArtLayers[1];                                       // get the image
                artLayer.Duplicate(newDoc, 1);                                              // copy layer to new document

                app.ActiveDocument = newDoc;                                                // make new document active
                ArtLayer newLayer = newDoc.ArtLayers[1];                                    // get copied layer
                newLayer.Opacity = opacity;                                                      // set opacity of copied layer
                newLayer.Name = string.Format("{0}{1}{2}", prefix, i, suffix);              // set name of copied layer
                newLayer.BlendMode = PsBlendMode.psMultiply;
                // opacity--;

                Console.WriteLine("Closing: " + string.Format("{0}\\{1}{2}{3}", FolderPath, prefix, i, suffix));
                doc.Close();                                                                // close original photo

            }

            Console.WriteLine("----DONE----");
        }
    }
}
