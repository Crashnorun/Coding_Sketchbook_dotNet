#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using mTypes;

using Newtonsoft.Json;
#endregion

namespace Rooms_Extract_Data
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        //string FileName = @"C:\Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\Revit\Rooms_Extract_Data\Models\Simple_Room_01.rvt";
        public string FileName = Properties.Resources.FileName_BasicRooms;
        public List<mPoint> pts = new List<mPoint>();
        public List<mRoom> Rooms = new List<mRoom>();

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;              // access to the main window and active document
            Application app = uiapp.Application;                        // current application

            if (uiapp.ActiveUIDocument == null) uiapp.OpenAndActivateDocument(FileName);

            UIDocument uidoc = uiapp.ActiveUIDocument;                  //  current active project
            Document doc = uidoc.Document;                              // databse level document

            // Access current selection
            // Selection sel = uidoc.Selection;

            // Retrieve elements from database
            /*FilteredElementCollector col = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Room));*/

            FilteredElementCollector col = new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .OfCategory(BuiltInCategory.OST_Rooms);

            // Filtered element collector is iterable
            foreach (Element e in col)
            {
                Debug.Print(e.Name);
                Room rm = e as Room;
                BoundingBoxXYZ boundingBox = rm.ClosedShell.GetBoundingBox();
                XYZ minPt = boundingBox.Min;
                XYZ maxPt = boundingBox.Max;

                Options options = new Options();
                options.ComputeReferences = true;
                //options.View = doc.ActiveView;
                options.IncludeNonVisibleObjects = true;
                options.DetailLevel = ViewDetailLevel.Fine;

                mRoom room = new mRoom()
                {
                    Name = rm.Name,
                    Number = rm.Number,
                    MinPt = new mPoint(minPt.X, minPt.Y, minPt.Z),
                    MaxPt = new mPoint(maxPt.X, maxPt.Y, maxPt.Z),
                    Height = rm.UnboundedHeight,
                    Perimiter = rm.Perimeter,
                    Area = rm.Area,
                    Volume = rm.Volume,
                    LevelName = rm.Level.Name,
                    UpperLevelName = rm.UpperLimit == null? null:rm.UpperLimit.Name
                };


                GeometryElement geo = rm.get_Geometry(options);
                foreach (GeometryObject obj in geo)
                {
                    Solid solid = obj as Solid;
                    if (solid != null)
                    {
                        EdgeArray ea = solid.Edges;
                        if (ea != null)
                        {
                            foreach (Edge ed in ea)
                            {
                                Curve crv = ed.AsCurve();
                                Line ln = crv as Line;
                                if (ln != null)
                                {
                                    Debug.Print(string.Format("Startpoint {0} Endpoint {1}", ln.GetEndPoint(0).ToString(), ln.GetEndPoint(1)));
                                }
                            }
                        }

                        FaceArray faceArr = solid.Faces;
                        if (faceArr != null)
                        {
                            foreach (Face face in faceArr)
                            {
                                mFace mface = new mFace();

                                IList<CurveLoop> crvLoops = face.GetEdgesAsCurveLoops();
                                foreach (CurveLoop crvLoop in crvLoops)
                                {
                                    CurveLoopIterator iterator = crvLoop.GetCurveLoopIterator();


                                    for (int i = 0; i < crvLoop.NumberOfCurves(); i++)
                                    {
                                        iterator.MoveNext();
                                        Curve crv = iterator.Current;

                                        Line ln = crv as Line;
                                        Arc arc = crv as Arc;
                                        HermiteSpline spln = crv as HermiteSpline;
                                        NurbSpline nrbs = crv as NurbSpline;

                                        if (ln != null)
                                        {
                                            XYZ temp = ln.GetEndPoint(0);
                                            mPoint stpt = new mPoint(temp.X, temp.Y, temp.Z);
                                            Debug.Print(stpt.ToString());
                                            pts.Add(stpt);

                                            temp = ln.GetEndPoint(1);
                                            mPoint endpt = new mPoint(temp.X, temp.Y, temp.Z);
                                            Debug.Print(endpt.ToString());
                                            pts.Add(endpt);

                                            mLine mline = new mLine(stpt, endpt);
                                            mface.Lines.Add(mline);
                                        }
                                        else if (arc != null)
                                        {
                                            XYZ temp = arc.GetEndPoint(0);
                                            mPoint stpt = new mPoint(temp.X, temp.Y, temp.Z);
                                            Debug.Print(stpt.ToString());
                                            pts.Add(stpt);

                                            double startParam = arc.GetEndParameter(0);
                                            double endParam = arc.GetEndParameter(1);
                                            double midParam = (startParam + endParam) / 2;

                                            temp = arc.Evaluate(midParam, false);
                                            mPoint midpt = new mPoint(temp.X, temp.Y, temp.Z);
                                            Debug.Print(midpt.ToString());
                                            pts.Add(midpt);

                                            temp = arc.GetEndPoint(1);
                                            mPoint endpt = new mPoint(temp.X, temp.Y, temp.Z);
                                            Debug.Print(endpt.ToString());
                                            pts.Add(endpt);

                                            mCurve mcrv = new mCurve(stpt, midpt, endpt);
                                            mface.Curves.Add(mcrv);
                                        }
                                        else if (spln != null)
                                        {
                                            mSpline spl = new mSpline();

                                            foreach (XYZ pt in spln.ControlPoints)
                                            {
                                                mPoint stpt = new mPoint(pt.X, pt.Y, pt.Z);
                                                spl.Points.Add(stpt);
                                            }
                                            mface.Splines.Add(spl);
                                        } else if(nrbs != null)
                                        {
                                            // need to implement nurbs curve
                                        }
                                    }
                                }
                                room.Faces.Add(mface);
                            }
                        }
                    }
                }
                Rooms.Add(room);

                WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, rm.Id);
                Debug.Print(string.Format("Creator: {0}, | Last changed by: {1} | Owner: {2}", 
                    info.Creator, info.LastChangedBy, info.Owner));
            }

            string val = JsonConvert.SerializeObject(Rooms, Formatting.Indented);
            System.IO.File.WriteAllText(Environment.CurrentDirectory + @"\myRooms.rooms", val);
            Debug.Print(Environment.CurrentDirectory + @"\myRooms.rooms");

            // Modify document within a transaction

            /*  using (Transaction tx = new Transaction(doc))
              {
                  tx.Start("Transaction Name");
                  foreach (Element e in col)  e.Name = "Charlie";
                  tx.Commit();
              }*/

            return Result.Succeeded;
        }
    }
}
