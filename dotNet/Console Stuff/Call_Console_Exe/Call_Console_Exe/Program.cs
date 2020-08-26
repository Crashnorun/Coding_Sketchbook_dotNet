using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call_Console_Exe
{
    class Program
    {
        #region ---- REFERENCES ----
        /*
         * https://stackoverflow.com/questions/6511978/run-console-application-in-c-sharp-with-parameters
         */

        #endregion

        static void Main(string[] args)
        {
            System.Diagnostics.Trace.WriteLine("Hellow world");
            // Start the child process.
            Process p = new Process();

            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;

            ProcessStartInfo info = new ProcessStartInfo(@"C: \Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\dotNet\Console Stuff\Call_Console_Exe\Add_Numbers_01\bin\Debug\Add_Numbers_01.exe", "-2 2");
            p.StartInfo = info;

            //p.StartInfo.FileName = @"C: \Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\dotNet\Console Stuff\Call_Console_Exe\Add_Numbers_01\bin\Debug\Add_Numbers_01.exe";
            //p.StartInfo.Arguments = "-2 2";

            p.Start();

            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

        }
    }
}
