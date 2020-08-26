#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
#endregion

namespace Rooms_Extract_Data
{

    class App : IExternalApplication
    {
        // private string FileName = @"C:\Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\Revit\Rooms_Extract_Data\Models\Simple_Room_01.rvt";
        private string FileName = Properties.Resources.FileName_BasicRooms;

        public Result OnStartup(UIControlledApplication a)
        {
#if DEBUG
            a.ControlledApplication.ApplicationInitialized += new EventHandler<Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs>(OpenDoc);
            a.ControlledApplication.DocumentOpened += new EventHandler<DocumentOpenedEventArgs>(RunCommand);
#endif
            return Result.Succeeded;
        }

        private void OpenDoc(object sender, ApplicationInitializedEventArgs e)
        {
            System.Diagnostics.Process.Start(FileName);
        }

        private void RunCommand(object sender, DocumentOpenedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

      
    }
}

/*
 * Refernces:
 * Load file on startup: https://forums.autodesk.com/t5/revit-api-forum/how-to-load-a-rvt-file-automatically-when-start-the-revit/m-p/5500878
 * Revit geometry options: https://thebuildingcoder.typepad.com/blog/2010/01/geometry-options.html
 * 
 */
