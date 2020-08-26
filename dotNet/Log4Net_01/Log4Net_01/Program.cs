using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

 // [assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net_02.config", Watch = true)]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Log4Net_01
{
    class Program
    {

        #region ---- PROPERTIES ----

        // good pratice is to create a logger for each class and name the logger the same as the class. The class name can be hard coded or can use reflection
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Program.cs");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // private static readonly log4net.ILog log = LogHelper.GetLogger();               // this will return the full file path where the class file was compiled

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
             LogHelper.SetLogger(log, @"C:\Logs\Log4Net_RollingFileLog_Prog.txt", true);
            log.Debug("Entering: Main");


            Console.WriteLine(MathFunctions.Add(10, 23.5));

            Console.WriteLine(MathFunctions.Subtract(10, 23.5));

            Console.WriteLine(MathFunctions.Divide(10, 12));

            Console.WriteLine(MathFunctions.Divide(10, 0));

            log.Info("Maintenance: water under the bridge");
            log.Warn("Maintenance: water is hot");
            log.Fatal("Maintenance: water is bad");

            Console.ReadLine();

            /*if (!DoesDBExist())                                                                 // check if db exists
            {
                if (!CreateDB())                                                                // create db
                {
                    System.Diagnostics.Debug.Print("Unable to create DB");
                    return;
                }
            }*/
        }


        #region ---- DB Methods ----

        /// <summary>
        /// Determin of the datbase already exists
        /// </summary>
        /// <returns>TRUE if the database is found, FALSE if not found</returns>
        static bool DoesDBExist()
        {
            //return File.Exists(@"c:\Logs\CrashnorunLogs.mdf");
            // return File.Exists(@"C:\Users\cportelliKD\Documents\Personal\GitHub\Coding_Sketchbook\dotNet\Log4Net_01\Log4Net_01\CrashnorunLogs.mdf");
            return File.Exists(ConfigurationManager.AppSettings["LogDirectory"] + Properties.Resources.DBName + Properties.Resources.AccessSuffix);
        }

        static bool DoesTableExist()
        {
            return false;
        }

        static bool CreateDB()
        {
            string mainCon = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainCon);
            string sqlquery = "create database " + Properties.Resources.DBName;
            SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();

            int i = sqlcommand.ExecuteNonQuery();

            sqlconn.Close();

            return false;
        }

        static bool CreateTable()
        {
            return false;
        }

        #endregion 

    }

    enum DBType
    {
        Unknown = 0,
        LogFile = 1,
        SQL = 2,
        SQLLite = 3,
        MSAccess = 4
    };

}



/*
 * References: 
 * https://www.youtube.com/watch?v=2lAdQ_QwNww
 * https://www.codeproject.com/Articles/140911/log-net-Tutorial
 * Log4net documentation: https://logging.apache.org/log4net/
 * Config examples: https://logging.apache.org/log4net/release/config-examples.html
 * SQL Example: https://www.c-sharpcorner.com/article/configure-log4net-with-database-tutorial-for-beginners/
 * SQL example video: https://www.youtube.com/watch?v=sBgfLluBkX0
 * Create basic SQL db video: https://www.youtube.com/watch?v=GVV-LUcmCOE&t=156s
 * Create sql db programatically: https://www.youtube.com/watch?v=Tvw0fyhGPL4
 * 
 * adding log appender programatically: http://mail-archives.apache.org/mod_mbox/logging-log4net-user/200602.mbox/%3CDDEB64C8619AC64DBC074208B046611C769745@kronos.neoworks.co.uk%3E
 *  https://stackoverflow.com/questions/308436/log4net-programmatically-specify-multiple-loggers-with-multiple-file-appenders
 *  
 *  
 * Steps:
 * 1. Add nuget package
 * 2. Modify config file
 * 3. Add assembly reference
 * 4. Add log property
 * 5. Use logger
 */
