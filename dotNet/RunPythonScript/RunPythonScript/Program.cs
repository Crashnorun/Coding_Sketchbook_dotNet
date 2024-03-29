﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPythonScript
{
    class Program
    {
        public static string PythonExePath = @"C:\Users\cportelli\AppData\Local\Programs\Python\Python37-32\python.exe";
        public static string fileName = @"C:\Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\dotNet\RunPythonScript\RunPythonScript\Add_Numbers_01.py";

        static void Main(string[] args)
        {
            // string result = run_cmd();
            // Console.WriteLine(result);

            string result = StartProcess();
            Console.WriteLine(result);

            Console.ReadKey();
        }

        // run a python script 
        public static string run_cmd()
        {
            // create process info
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = PythonExePath;

            // provide script and arguements
            processInfo.Arguments = string.Format("{0} {1} {2}", fileName, 3, 4);

            // process configuration
            processInfo.UseShellExecute = false;                                              // Do not use OS shell
            processInfo.CreateNoWindow = true;                                                // don't display new window
            processInfo.RedirectStandardOutput = true;                                        // Any output, generated by application will be redirected back
            processInfo.RedirectStandardError = true;                                         // Any error in standard output will be redirected back (for example exceptions)

            // execute process and get outputs
            using (Process process = Process.Start(processInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd();                        // Here are the exceptions from our Python script
                    string result = process.StandardOutput.ReadToEnd();                       // Here is the result of StdOut(for example: print "test")

                    if (stderr != null) Console.WriteLine(stderr);

                    process.WaitForExit();
                    process.Close();

                    return result;
                }
            }
        }


        public static string StartProcess()
        {
            // create process info
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = PythonExePath;

            // provide script and arguements
            processInfo.Arguments = string.Format("{0} MultiplyNumbers({1}, {2})", fileName, 3, 4);

            // process configuration
            processInfo.UseShellExecute = false;                                              // Do not use OS shell
            processInfo.CreateNoWindow = true;                                                // don't display new window
            processInfo.RedirectStandardOutput = true;                                        // Any output, generated by application will be redirected back
            processInfo.RedirectStandardError = true;                                         // Any error in standard output will be redirected back (for example exceptions)

            // execute process and get outputs
            using (Process process = Process.Start(processInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd();                        // Here are the exceptions from our Python script
                    string result = process.StandardOutput.ReadToEnd();                       // Here is the result of StdOut(for example: print "test")

                    if (stderr != null) Console.WriteLine(stderr);

                    process.WaitForExit();
                    process.Close();

                    return result;
                }
            }
        }

    }
}


#region ---- REFERENCES ----
/*
 * https://medium.com/better-programming/running-python-script-from-c-and-working-with-the-results-843e68d230e5
 * https://stackoverflow.com/questions/11779143/how-do-i-run-a-python-script-from-c
 * https://bytes.com/topic/python/insights/950783-two-ways-run-python-programs-c
 * https://code.msdn.microsoft.com/windowsdesktop/C-and-Python-interprocess-171378ee
 * https://www.youtube.com/watch?v=g1VWGdHRkHs
 */


#endregion
