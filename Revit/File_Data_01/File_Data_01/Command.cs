#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

/*
 * TODO: Add a header to the file with Revit file name, time stamp when data was extracted, summary of all elements
 * TODO: Add progress bar
 * TODO: Include, gridlines, levels, model lines, linked files, sheets
 * TODO: standardize the output list of data. Some of the functions below use different number of columns
 * 
 * CSV format: Element ID, Family Name, Name, Creator, Last Changed, Owner, Family File Path
 */


namespace File_Data_01
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        #region ---- PROPERTIES ----

        /// <summary>
        /// Contains the CSV output saved to the file
        /// </summary>
        List<string> ModelData = new List<string>();

        #endregion

        #region ---- ENTRY POINT ----

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            if (uidoc == null)
            {
                MessageBox.Show("No open Revit files found. Please open a file and try again", "Data Export", MessageBoxButtons.OK);
                Debug.Print("Could not find any open Revit documents");
                return Result.Cancelled;
            }
            Document doc = uidoc.Document;

            FamilySymbolData(doc);
            FamilyInstanceData(doc);
            DetailLineData(doc);
            ViewData(doc);
            LevelData(doc);
            GridData(doc);
            SheetData(doc);

            string filename = Path.GetFileName(doc.PathName);
            string folder = Path.GetDirectoryName(doc.PathName);
            ModelData.Add("File name: " + filename);
            Debug.Print("FileName: " + filename);

            if (ModelData.Count > 0 && doc != null) File.WriteAllLines(folder + @"\" + LogFileName(filename), ModelData);

            MessageBox.Show("Data Export Complete", "Data Export", MessageBoxButtons.OK);
            Debug.Print("---- DONE ----");

            /*  using (Transaction tx = new Transaction(doc))
              {
                  tx.Start("Transaction Name");
                  tx.Commit();
              }*/

            return Result.Succeeded;
        }

        #region ---- METHODS ----
        #endregion
        /// <summary>
        ///  find all family symbol data in the active revit document
        /// </summary>
        /// <param name="doc">Active revit document</param>
        public void FamilySymbolData(Document doc)
        {
            Debug.Print("---- FAMILY SYBMOL DATA ----");

            if (doc == null) return;

            //log.Add(Environment.NewLine);
            ModelData.Add("Family Symbol Data");
            ModelData.Add("Element ID, Family Name, Name, Creator, Last Changed, Owner, Family File Path, Created, Last Accessed");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                 .OfClass(typeof(FamilySymbol));

            foreach (Element e in col)
            {
                FamilySymbol sym = e as FamilySymbol;

                try
                {
                    Document famDoc;
                    if (sym.GetType() == typeof(PanelType))
                    {
                        PanelType panelType = sym as PanelType;
                        famDoc = panelType.Document;
                    }
                    else if (sym.GetType() == typeof(MullionType))
                    {
                        MullionType mullionType = sym as MullionType;
                        famDoc = mullionType.Document;
                    }
                    else famDoc = doc.EditFamily(sym.Family);

                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, sym.Id);

                    if (famDoc == null)
                    {
                        ModelData.Add(sym.Id + "," + sym.FamilyName + "," + sym.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner + "," + "NA");
                        Debug.Print(string.Format("Element ID: {0}, FamilyName: {1}, Name: {2}, Creator: {3}, Owner: {4}, Last Edited By: {5}, FilePath: {6}",
                        sym.Id, sym.FamilyName, sym.Name, info.Creator, info.Owner, info.LastChangedBy, "NA"));
                    }
                    else
                    {
                        string created = string.Empty, edited = string.Empty;
                        if (File.Exists(famDoc.PathName)) 
                        {
                            created = File.GetCreationTime(famDoc.PathName).Date.ToString();
                            edited = File.GetLastAccessTime(famDoc.PathName).Date.ToString();
                        }

                        ModelData.Add(sym.Id + "," + sym.FamilyName + "," + sym.Name + "," +
                            info.Creator + "," + info.LastChangedBy + "," + info.Owner + "," +
                            famDoc.PathName + "," + created + "," + edited);

                        Debug.Print(string.Format("Element ID: {0}, FamilyName: {1}, Name: {2}, Creator: {3}, Last Edited By: {4}, Owner: {5}, FilePath: {6}, Created: {7}, Last Accessed: {8}",
                        sym.Id, sym.FamilyName, sym.Name, info.Creator, info.Owner, info.LastChangedBy, famDoc.PathName, created, edited));
                    }
                }
                catch (Exception ex)
                {
                    if (sym == null) ModelData.Add(ex.Message);
                    else ModelData.Add(sym.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- FAMILY SYBMOL DATA ----");
        }


        /// <summary>
        /// Find all family instance data
        /// </summary>
        /// <param name="doc">Active revit document</param>
        public void FamilyInstanceData(Document doc)
        {
            if (doc == null) return;

            Debug.Print("---- FAMILY INSTANCE DATA ----");

            //log.Add(Environment.NewLine);
            ModelData.Add("Family Instance Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance));

            foreach (Element e in col)
            {
                FamilyInstance inst = e as FamilyInstance;

                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, inst.Id);

                    ModelData.Add(inst.Id + "," + inst.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Element ID: {0}, Name: {1}, Creator: {2}, Last Edited By: {3}, Owner: {4}",
                    inst.Id, inst.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (inst == null) ModelData.Add(ex.Message);
                    else ModelData.Add(inst.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- FAMILY INSTANCE DATA ----");
        }


        /// <summary>
        /// Find all detail line data
        /// </summary>
        /// <param name="doc">Active revit document</param>
        public void DetailLineData(Document doc)
        {
            if (doc == null) return;

            Debug.Print("---- DETAIL LINE DATA ----");

            //log.Add(Environment.NewLine);
            ModelData.Add("Detail Line Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                .OfClass(typeof(CurveElement));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Element ID: {0}, Name: {1}, Creator: {2}, Last Edited By: {3}, Owner: {4}",
                    e.Id, e.Name, info.Creator, info.LastChangedBy, info.Owner));

                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- DETAIL LINE DATA ----");
        }


        /// <summary>
        /// Find all view data
        /// </summary>
        /// <param name="doc"></param>
        public void ViewData(Document doc)
        {
            if (doc == null) return;

            Debug.Print("---- VIEW DATA ----");

            //log.Add(Environment.NewLine);
            ModelData.Add("View Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.View));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- VIEW DATA ----");
        }


        public void LevelData(Document doc)
        {
            if (doc == null) return;
            Debug.Print("---- LEVEL DATA ----");
            ModelData.Add("Level Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.Level));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- LEVEL DATA ----");
        }


        public void GridData(Document doc)
        {
            if (doc == null) return;
            Debug.Print("---- GRID DATA ----");
            ModelData.Add("Grid Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.Grid));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- GRID DATA ----");
        }


        public void SheetData(Document doc)
        {
            if (doc == null) return;
            Debug.Print("---- SHEET DATA ----");
            ModelData.Add("Sheet Data");
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.ViewSheet));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- GRID DATA ----");
        }


        public void GenericElementData(Document doc, string elementType)
        {
            if (doc == null) return;
            Debug.Print("---- {0} DATA ----", elementType.ToUpper());
            ModelData.Add(string.Format("{0} Data", elementType));
            ModelData.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.Level));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    ModelData.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) ModelData.Add(ex.Message);
                    else ModelData.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- {0} DATA ----", elementType.ToUpper());
        }


        /// <summary>
        /// Format the log file name
        /// </summary>
        /// <param name="FileName">Revit file name</param>
        /// <returns>Formatted log file name</returns>
        public string LogFileName(string FileName)
        {
            //string date = DateTime.Now.ToString("yyMMdd_H:mm:ss");
            string date = DateTime.Now.ToString("yyMMdd");
            return string.Format("{0}_{1}_Element Data Log.csv", date, FileName);
        }

        #endregion
    }
}

/*
 * References
 * Family file path: https://adndevblog.typepad.com/aec/2012/09/accessing-the-path-a-revit-family-document-from-the-family-instance.html
 * Worksharing properties: https://www.revitapidocs.com/2019/c6d2a047-8f18-103a-804d-cd2a0ff43c40.htm
 */
