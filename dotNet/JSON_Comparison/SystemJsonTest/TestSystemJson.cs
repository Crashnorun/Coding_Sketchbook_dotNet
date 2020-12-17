using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClasses;

namespace SystemJsonTest
{
    public class TestSystemJson
    {

        public static string SerializeData(List<Human> People)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

        /*    System.Text.Json.JsonSerializerOptions opts = new System.Text.Json.JsonSerializerOptions();
            opts.WriteIndented = true;

            string data = System.Text.Json.JsonSerializer.Serialize(People,opts); 
            */
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("System.Test.Json RunTime " + elapsedTime);

            return "";
        }




    }
}
