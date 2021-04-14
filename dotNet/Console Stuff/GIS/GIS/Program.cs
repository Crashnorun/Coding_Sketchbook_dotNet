using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GIS
{
    class Program
    {
        public static string filePath = @"C:\Users\Cportelli\Downloads\DA_WISE_GML\DeliveryArea.shx";

        static void Main(string[] args)
        {

            ReadShapeFile(filePath);

        }



        public static void ReadShapeFile(string FilePath)
        {
            List<string> AttNames = new List<string>();

            IFeatureSet featureSet = FeatureSet.Open(FilePath);
            featureSet.FillAttributes();
            DataTable datTab = featureSet.DataTable;
            foreach (DataColumn col in (InternalDataCollectionBase)datTab.Columns)
                AttNames.Add(col.ColumnName);

            for (int i = 0; i < featureSet.Features.Count; i++)
            {
                List<List<Point>> PtsSet = new List<List<Point>>();
                Dictionary<string, string> attDict = new Dictionary<string, string>();
                IFeature feature = featureSet.GetFeature(i);

                string geometryType = feature.BasicGeometry.GeometryType;
                //Console.WriteLine("\tGeometry Type: " + geometryType);
                Console.WriteLine(" ");
                Console.WriteLine(AttNames[0] + " " + datTab.Rows[i][AttNames[0]]);

                int numGeo = feature.NumGeometries;

                for (int j = 0; j < numGeo; j++)
                {
                    List<Point> pts = new List<Point>();
                    IList<Coordinate> coordinates = feature.BasicGeometry.GetBasicGeometryN(j).Coordinates;
                    featureSet.GetShape(i, true);
                    foreach (Coordinate coordinate in (IEnumerable<Coordinate>)coordinates)
                    {
                        if (coordinate.Z.ToString() != "NaN")
                            pts.Add(new Point(coordinate.X, coordinate.Y, coordinate.Z));
                        else
                            pts.Add(new Point(coordinate.X, coordinate.Y, 0.0));

                        //Console.WriteLine(coordinate.ToString());
                        Console.WriteLine(string.Format("{0},{1},0", coordinate.X, coordinate.Y));
                    }
                    PtsSet.Add(pts);
                }

                for (int j = 0; j < AttNames.Count; ++j)
                    attDict.Add(AttNames[j], datTab.Rows[i][AttNames[j]].ToString());
                
            }
        }


    }
}
