using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Events_01
{
    class Program
    {
        /// <summary>
        /// Represents a method that will handel an event that has no event data
        /// </summary>
        public static EventHandler testEvent;


        static void Main(string[] args)
        {
            // Subscribe to the event. 
            // Add function TestEvent_OnKeyPressed to the event
            testEvent += TestEvent.TestEvent_OnKeyPressed;

            ConsoleKeyInfo key = Console.ReadKey();
            RunEvent(key);

            Console.ReadKey();
        }


        private static void RunEvent(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Spacebar)
            {
                // Invoke only if the function is not null.
                //testEvent?.Invoke(MethodBase.GetCurrentMethod(), EventArgs.Empty);
                testEvent?.Invoke(MethodBase.GetCurrentMethod(), EventArgs.Empty);
            }
        }
    }
}


/*
 * Reference: https://www.youtube.com/watch?v=OuZrhykVytg
 */
