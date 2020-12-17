using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;
using System.Diagnostics;


namespace NewtonSoftTest
{
    public class TestNewtonSoft
    {

        public static string SerializeData(List<Human> People)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("NewtonSoft RunTime " + elapsedTime);

            return data;
        }
    }
}
