using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Testing_Build_Events
{

    internal class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {

            Console.WriteLine("Entering: " + MethodBase.GetCurrentMethod().Name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tChanging the color to green");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tChanging the color to red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tResetting color to white");


            ClipBoardExampleText();

            ClipBoardExampleObject();


            Console.ReadKey();
        }


        /// <summary>
        /// Example on copying / pasting a custom object to clipboard
        /// </summary>
        public static void ClipBoardExampleObject()
        {
            Console.WriteLine("Entering: " + MethodBase.GetCurrentMethod().Name);

            Person me = new Person();
            me.Name = "Ron";
            Person you = new Person();

            // set custom object
            System.Windows.Forms.Clipboard.SetData(me.GetType().Name, me);

            // retrieve custom object
            if (System.Windows.Forms.Clipboard.ContainsData(me.GetType().Name))
            {
                you = System.Windows.Forms.Clipboard.GetData(me.GetType().Name) as Person;
                Console.WriteLine("\tObject copied from clipboard:" + you.ToString());
            }
            else Console.WriteLine("Object is not of type " + typeof(Person).Name);
        }


        /// <summary>
        /// Example on copying / pasting text to clipboard
        /// </summary>
        public static void ClipBoardExampleText()
        {
            Console.WriteLine("Entering: " + MethodBase.GetCurrentMethod().Name);
            // set text
            System.Windows.Forms.Clipboard.SetText("John");

            // get text
            string him = System.Windows.Forms.Clipboard.GetText();
            Console.WriteLine("\tText from clipboard: " + him);
        }
    }


    /// <summary>
    /// Custom object to set with
    /// </summary>
    [Serializable]
    public class Person : System.Windows.Forms.IDataObject
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() { }



        #region ---- INHERITED FROM IDATAOBJECT ----

        public object GetData(string format, bool autoConvert)
        {
            throw new NotImplementedException();
        }

        public object GetData(string format)
        {
            throw new NotImplementedException();
        }

        public object GetData(Type format)
        {
            throw new NotImplementedException();
        }

        public void SetData(string format, bool autoConvert, object data)
        {
            throw new NotImplementedException();
        }

        public void SetData(string format, object data)
        {
            throw new NotImplementedException();
        }

        public void SetData(Type format, object data)
        {
            throw new NotImplementedException();
        }

        public void SetData(object data)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(string format, bool autoConvert)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(string format)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(Type format)
        {
            throw new NotImplementedException();
        }

        public string[] GetFormats(bool autoConvert)
        {
            return new string[] { "Serializable" };
        }

        public string[] GetFormats()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
