using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Example
{
    public class Program
    {

        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string logConfigFile = "NLog.config";
            string filePath = folderPath + @"\" + logConfigFile;

            Assembly ass = Assembly.GetExecutingAssembly();
            string fiPath = Path.GetDirectoryName(ass.CodeBase);
            

            //NLog.LogManager.Configuration.Variables["HostPlatform"] = "Bob";
            //logger.SetProperty("HostPlatform", "Ron");
            logger.Properties["HostPlatform"] = Assembly.GetExecutingAssembly().FullName;
            logger.Debug("Debugging");
            logger.Error("Erroring");
            logger.Error(new Exception("hello exception").StackTrace, "Debuging exception");

            #region Calling Basic Functions

            MathFunctions.Add(1, 1.0);

            MathFunctions.Divide(10, 0);

            MathFunctions.Subtract(10, 20);

            MathFunctions.Add(new List<int> { 12, 12 });

            #endregion

            #region Extract GoogleSheet ID

            string URL = "https://docs.google.com/spreadsheets/d/1bB4BzHTexDvW53PDTlX9nFqHLznicL7YqXP8ACFqJIs/edit#gid=0";
            Uri uriResult;
            bool result = Uri.TryCreate(URL, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                Console.WriteLine("URL input is not valid");
                logger.Error("URL input is not valid");
            }

            // parse url for sheet ID
            string[] vals = new Uri(URL).Segments;

            string SheetID = string.Empty;

            foreach (string str in vals)
            {
                if (str.Length > SheetID.Length) SheetID = str;
            }
            SheetID = SheetID.Remove(SheetID.Length - 1, 1);

            #endregion


            Person bob = new Person("bob", 34);
            CallingPerson(bob);

            TestMultipleLogFiles();


            Console.ReadKey();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="ParamValues"></param>
        /// <References>https://forums.asp.net/t/1421332.aspx?How+to+get+current+method+parameter+values+programatically+
        /// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke?view=netcore-3.1
        /// https://stackoverflow.com/questions/37519896/check-nlog-minlevel-before-logging
        /// </References>
        public static void InvokeMethod(NLog.Logger Logger, MethodBase Method, params object[] ParamValues)
        {
            Logger.SetProperty("Name", Method.ReflectedType.FullName + "." + Method.Name);

            // get the method name
            Logger.Debug("Method name: " + Method.Name);

            // get the method parameters
            ParameterInfo[] pars = Method.GetParameters();

            // print the parameter names and their values
            for (int i = 0; i < ParamValues.Length; i++)
            {

                // if a parameter is a nested list
                if (pars[i].ParameterType.Name.Contains("List") && Logger.IsEnabled(NLog.LogLevel.Debug))
                {
                    // recursiverly write out the entire list.
                }
                // if a parameter is an object
                else if (pars[i].GetType() == typeof(object).BaseType)
                {

                }
                else Logger.Debug(string.Format("\t Parameter: {0}, Value: {1}", pars[i].Name, ParamValues[i]));
            }

            // MethodInfo methodInfo = (MethodInfo)Method;
        }


        public static void CallingPerson(Person person)
        {
            InvokeMethod(logger, MethodBase.GetCurrentMethod(), person);
        }


        public static void TestMultipleLogFiles()
        {
            string fileName = @"C:\Logs\NLog_Example.txt";
            FileInfo fi = new FileInfo(fileName);

            do
            {

                logger.Properties["HostPlatform"] = Assembly.GetExecutingAssembly().FullName;
                logger.Debug("Debugging");
                logger.Error("Erroring");
                logger.Error(new Exception("hello exception").StackTrace, "Debuging exception");

                #region Calling Basic Functions

                MathFunctions.Add(1, 1.0);

                MathFunctions.Divide(10, 0);

                MathFunctions.Subtract(10, 20);

                MathFunctions.Add(new List<int> { 12, 12 });

                #endregion

                Person bob = new Person("bob", 34);
                CallingPerson(bob);

                fi = new FileInfo(fileName);

            } while ((fi.Length / 1048576) < 10);
        }


    }
}
