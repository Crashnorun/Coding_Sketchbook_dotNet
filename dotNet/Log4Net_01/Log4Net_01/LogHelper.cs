using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Log4Net_01
{
    public class LogHelper
    {

        public static bool Active = false;


        public static log4net.ILog GetLogger([CallerFilePath]string filename = "")
        {
            // this will return the full file path where the class file was compiled
            return log4net.LogManager.GetLogger(filename);
        }


        public static void SetLogger(log4net.ILog log, string FileDirectory = @"C:\Logs\CFLogs.txt", bool ResetAppenders = false)
        {
            Active = System.IO.File.Exists(FileDirectory);                      // if file exist start logging

            // delete any pre existing appenders
            if (ResetAppenders) log.Logger.Repository.ResetConfiguration();

            // create Pattern Layout
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = "%date [%thread] %username %property{log4net:HostName} %level %logger - %m%newline%exception";
            layout.ActivateOptions();

            // create Appender
            log4net.Appender.FileAppender appender = new log4net.Appender.FileAppender();
            appender.Name = "FileAppender";
            appender.File = FileDirectory;
            appender.AppendToFile = true;
            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            appender.Layout = layout;                                           // set appender layout
            appender.ActivateOptions();

            // assign appender to logger
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)log.Logger;
            l.Level = l.Hierarchy.LevelMap["ALL"]; // log4net.Core.Level.All;
            l.Repository.Configured = true;
            l.Hierarchy.Root.AddAppender(appender);
            appender.ActivateOptions();
        }
    }



}
