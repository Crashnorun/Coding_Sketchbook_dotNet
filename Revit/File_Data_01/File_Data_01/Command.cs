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

namespace File_Data_01
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        List<string> log = new List<string>();

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            if (uidoc == null)
            {
                Debug.Print("Could not find any open Revit documents");
                return Result.Cancelled;
            }
            Document doc = uidoc.Document;

            string filename = Path.GetFileName(doc.PathName);
            string folder = Path.GetDirectoryName(doc.PathName);
            log.Add("File name: " + filename);
            Debug.Print("FileName: " + filename);

            FamilySymbolData(doc);
            FamilyInstanceData(doc);
            DetailLineData(doc);
            ViewData(doc);

            if (log.Count > 0 && doc != null) File.WriteAllLines(folder + @"\Element Data Log.csv", log);

            MessageBox.Show("Data Export Complete","Data Export",MessageBoxButtons.OK);
            Debug.Print("---- DONE ----");

            /*  using (Transaction tx = new Transaction(doc))
              {
                  tx.Start("Transaction Name");
                  tx.Commit();
              }*/

            return Result.Succeeded;
        }


        /// <summary>
        ///  find all family symbol data
        /// </summary>
        /// <param name="doc">Active revit document</param>
        public void FamilySymbolData(Document doc)
        {
            Debug.Print("---- FAMILY SYBMOL DATA ----");

            if (doc == null) return;

            //log.Add(Environment.NewLine);
            log.Add("Family Symbol Data");
            log.Add("Element ID, Family Name, Name, Creator, Last Changed, Owner, Family File Path");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                 .OfClass(typeof(FamilySymbol));

            foreach (Element e in col)
            {
                FamilySymbol sym = e as FamilySymbol;

                try
                {
                    Document famDoc = doc.EditFamily(sym.Family);

                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, sym.Id);

                    if (famDoc == null)
                    {
                        log.Add(sym.Id + "," + sym.FamilyName + "," + sym.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner + "," + "NA");
                        Debug.Print(string.Format("Element ID: {0}, FamilyName: {1}, Name: {2}, Creator: {3}, Owner: {4}, Last Edited By: {5}, FilePath: {6}",
                        sym.Id, sym.FamilyName, sym.Name, info.Creator, info.Owner, info.LastChangedBy, "NA"));
                    }
                    else
                    {
                        log.Add(sym.Id + "," + sym.FamilyName + "," + sym.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner + "," + famDoc.PathName);
                        Debug.Print(string.Format("Element ID: {0}, FamilyName: {1}, Name: {2}, Creator: {3}, Last Edited By: {4}, Owner: {5}, FilePath: {6}",
                        sym.Id, sym.FamilyName, sym.Name, info.Creator, info.Owner, info.LastChangedBy, famDoc.PathName));
                    }
                }
                catch (Exception ex)
                {
                    if (sym == null) log.Add(ex.Message);
                    else log.Add(sym.Id + "," + ex.Message);
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
            log.Add("Family Instance Data");
            log.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance));

            foreach (Element e in col)
            {
                FamilyInstance inst = e as FamilyInstance;

                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, inst.Id);

                    log.Add(inst.Id + "," + inst.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Element ID: {0}, Name: {1}, Creator: {2}, Last Edited By: {3}, Owner: {4}",
                    inst.Id, inst.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (inst == null) log.Add(ex.Message);
                    else log.Add(inst.Id + "," + ex.Message);
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
            log.Add("Detail Line Data");
            log.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
                .OfClass(typeof(CurveElement));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    log.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Element ID: {0}, Name: {1}, Creator: {2}, Last Edited By: {3}, Owner: {4}",
                    e.Id, e.Name, info.Creator, info.LastChangedBy, info.Owner));

                }
                catch (Exception ex)
                {
                    if (e == null) log.Add(ex.Message);
                    else log.Add(e.Id + "," + ex.Message);
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
            log.Add("View Data");
            log.Add("Element ID, Name, Creator, Last Changed, Owner");

            FilteredElementCollector col = new FilteredElementCollector(doc)
             .OfClass(typeof(Autodesk.Revit.DB.View));

            foreach (Element e in col)
            {
                try
                {
                    WorksharingTooltipInfo info = WorksharingUtils.GetWorksharingTooltipInfo(doc, e.Id);

                    log.Add(e.Id + "," + e.Name + "," + info.Creator + "," + info.LastChangedBy + "," + info.Owner);

                    Debug.Print(string.Format("Name: {0}, Creator: {1}, Last Edited By: {2}, Owner: {3}",
                    e.Name, info.Creator, info.LastChangedBy, info.Owner));
                }
                catch (Exception ex)
                {
                    if (e == null) log.Add(ex.Message);
                    else log.Add(e.Id + "," + ex.Message);
                }
                //log.Add(Environment.NewLine);
            }
            //log.Add(Environment.NewLine);
            Debug.Print("---- VIEW DATA ----");
        }
    }
}

/*
 * References
 * Family file path: https://adndevblog.typepad.com/aec/2012/09/accessing-the-path-a-revit-family-document-from-the-family-instance.html
 * Worksharing properties: https://www.revitapidocs.com/2019/c6d2a047-8f18-103a-804d-cd2a0ff43c40.htm
 */
