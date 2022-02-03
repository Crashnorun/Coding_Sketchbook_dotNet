using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Events_01
{
    public class TestEvent
    {
        
        public static void TestEvent_OnKeyPressed(object sender, EventArgs e)
        {
            MethodInfo info = sender as MethodInfo;

            Console.WriteLine("This event was called by " + info.Name);
        }
    }
}
