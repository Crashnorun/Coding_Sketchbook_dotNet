using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TestClasses;
using Utf8Json;

namespace UTF8Json
{
    public class TestUTF8Json
    {

        public static string SerializeData(List<Human> People)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // byte[] bytes = Utf8Json.JsonSerializer.Serialize(People);
            // string data = bytes.ToString();
            string data = Utf8Json.JsonSerializer.ToJsonString(People);

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("System.Test.Json RunTime " + elapsedTime);

            return data;
        }

    }
}
