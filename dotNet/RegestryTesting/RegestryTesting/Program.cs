using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegestryTesting
{
    class Program
    {
        public static string RegUser = "Volatile Environment";


        static void Main(string[] args)
        {
            // read value from CURRENT USER registry
            //Console.WriteLine(ReadCurrentUser());
            // read value from LOCAL MACHINE

            // Write new value to User registry 
            // CreateNewRegistryKey();

            // Update registry key
             UpdateRegistryKey();

            // delete registry key
            //DeleteRegistryKey();

            Console.ReadKey();
        }


        /// <summary>
        /// Get the current user name
        /// </summary>
        /// <returns></returns>
        public static string ReadCurrentUser()
        {
            //Computer\HKEY_CURRENT_USER\Volatile Environment
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegUser))
            {
                if (key == null) return string.Empty;
                var data = key.GetValue("USERNAME");
                if (data == null) return string.Empty;
                return data.ToString();
            }
        }


        /// <summary>
        /// Create a new registry subkey and two values
        /// </summary>
        public static void CreateNewRegistryKey()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Charlie");
            key.SetValue("Name", "Charlie");
            key.SetValue("Surname", "Portelli");
            key.Close();
        }


        /// <summary>
        /// Delete registry subkey
        /// Return tru on success, false on failure
        /// </summary>
        public static bool DeleteRegistryKey()
        {
            try
            {
                Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\Charlie");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Update registry sub key
        /// </summary>
        public static bool UpdateRegistryKey()
        {
            // need to set second parameter to true if indenting to write back to the key.
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Charlie",true))
            {
                try
                {
                    if (key == null) return false;

                    // registry keys are not case senstivie Name or NAME
                    var data = key.GetValue("Name");
                    if (data == null) return false;

                    key.SetValue("NAME", data.ToString().ToUpper());
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }


    }

    /*
     * NOTE:
     * References:
     * Reading user info from registry: https://stackoverflow.com/questions/28451096/how-to-find-logged-in-user-from-windows-registry
     * 
     * Registry Keys are not case sensitive
     * 
     * Registry info: https://www.c-sharpcorner.com/UploadFile/f9f215/windows-registry/
     */
}
