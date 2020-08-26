using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace Log4Net_01
{
    public class MathFunctions
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Add two numbers together
        /// </summary>
        /// <param name="Num1">First number</param>
        /// <param name="Num2">Second number</param>
        /// <returns>Result value</returns>
        public static double Add(double Num1, double Num2)
        {
            // get method name and inputs
            MethodInfo method = (MethodInfo)MethodBase.GetCurrentMethod();
            log.Debug("Method name: " + method.Name);

            ParameterInfo[] pars = method.GetParameters();
            foreach (ParameterInfo p in pars)
                log.Debug(string.Format("\t Parameter: {0}, Value: {1}", p.ToString(), p.DefaultValue));

            if (Num1 == double.NaN)
            {
                log.Error("Num1 is invalid");
                throw new ArgumentException("Num1 is not valid input");
            }
            if (Num2 == double.NaN)
            {
                log.Error("Num2 is invalid");
                throw new ArgumentException("Num2 is not valid input");
            }

            double result = Num1 + Num2;

            return result;
        }


        /// <summary>
        /// Subtract Num1 from Num2
        /// </summary>
        /// <param name="Num1">First number</param>
        /// <param name="Num2">Second number</param>
        /// <returns>Result value</returns>
        public static double Subtract(double Num1, double Num2)
        {
            // get method name and inputs
            MethodInfo method = (MethodInfo)MethodBase.GetCurrentMethod();
            log.Debug("Method name: " + method.Name);

            ParameterInfo[] pars = method.GetParameters();
            foreach (ParameterInfo p in pars)
                log.Debug(string.Format("\t Parameter: {0}, Value: {1}", p.ToString(), p.DefaultValue));

            if (Num1 == double.NaN)
            {
                log.Error("Num1 is invalid");
                throw new ArgumentException("Num1 is not valid input");
            }
            if (Num2 == double.NaN)
            {
                log.Error("Num2 is invalid");
                throw new ArgumentException("Num2 is not valid input");
            }

            double result = Num1 - Num2;

            return result;
        }


        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double Divide(double Num1, double Num2)
        {
            // get method name and inputs
            MethodInfo method = (MethodInfo)MethodBase.GetCurrentMethod();
            log.Debug("Method name: " + method.Name);

            ParameterInfo[] pars = method.GetParameters();
            foreach (ParameterInfo p in pars)
                log.Debug(string.Format("\t Parameter: {0}", p.ToString()));

            if (Num1 == double.NaN)
            {
                log.Error("Num1 is invalid");
                throw new ArgumentException("Num1 is not valid input");
            }
            if (Num2 == double.NaN)
            {
                log.Error("Num2 is invalid");
                throw new ArgumentException("Num2 is not valid input");
            }

            double x = 0;

            try { x = Num1 / Num2; }
            catch (DivideByZeroException ex) { log.Error("Developer: tried to divide by zero", ex); }

            return x;
        }


    }
}
