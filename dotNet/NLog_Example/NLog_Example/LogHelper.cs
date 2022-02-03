using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Example
{
    public static class LogHelper
    {

        #region ---- PROPERTIES ----

        public static NLog.Logger Logger
        {
            get { return _logger; }
        }
        private static NLog.Logger _logger;

        #endregion


        #region ---- METHODS ----

        public static NLog.Logger InitializeLogger()
        {
            // create logger
            _logger = NLog.LogManager.GetCurrentClassLogger();
            return _logger;
        }


        public static NLog.Logger GetLogger()
        {
            return Logger;
        }


        /// <summary>
        /// Log method name and inputs only when log level is set to DEBUG or Trace
        /// </summary>
        /// <param name="Method"></param>
        /// <param name="ParamValues"></param>
        /// <References>https://forums.asp.net/t/1421332.aspx?How+to+get+current+method+parameter+values+programatically+
        /// https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke?view=netcore-3.1
        /// https://stackoverflow.com/questions/37519896/check-nlog-minlevel-before-logging
        /// </References>
        public static void LogMethodInputs(MethodBase Method, params object[] ParamValues)
        {
            /*
             * Log levels:
             * https://nlog-project.org/documentation/v4.4.0/html/T_NLog_LogLevel.htm
             * https://www.codeproject.com/Articles/10631/Introduction-to-NLog
             * Log Ordinal Values:
             *  Trace = 0
             *  Debug = 1
             *  Info = 2
             *  Warn = 3
             *  Error = 4
             *  Fatal = 5
             *  Off = 6
             */

            // only log if level is set to DEBUG
            // if (!NLog.LogManager.Configuration.LoggingRules[1].IsLoggingEnabledForLevel(NLog.LogLevel.Debug)) 
            if (NLog.LogManager.Configuration.LoggingRules[1].Levels[0].Ordinal > 1 &&
                NLog.LogManager.Configuration.LoggingRules[1].Levels[0].Ordinal != 6)
                return;

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
                    // recursiverly write out the entire list or
                    // write out list length and data type
                }
                // if a parameter is an object
                else if (pars[i].GetType() == typeof(object).BaseType)
                {
                    Logger.Debug(string.Format("\t Parameter: {0}, Value: {1}", pars[i].Name, ParamValues[i].GetType().Name));
                }
                else
                    Logger.Debug(string.Format("\t Parameter: {0}, Value: {1}", pars[i].Name, ParamValues[i]));
            }

            // MethodInfo methodInfo = (MethodInfo)Method;
        }

        #endregion



    }
}
