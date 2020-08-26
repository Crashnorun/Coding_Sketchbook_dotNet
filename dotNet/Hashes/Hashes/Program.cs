using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashes
{
    class Program
    {
        static void Main(string[] args)
        {
            HashString("charlie");
            HashString("charlie");
            HashString("Charlie");

            //HashSet();

            ReadDirectorySize();


            Console.ReadKey();
        }


        public static void HashString(string value)
        {
            int i = value.GetHashCode();
            Console.WriteLine(string.Format("{0} : {1}", value, i));
        }


        public static void HashSet()
        {
            List<int> nums = new List<int>();
            HashSet<object> hashes = new HashSet<object>();

            for (int i = 0; i < 5; i++)
            {
                nums.Add(i);
                hashes.Add(i);
                hashes.Add(i.ToString());
            }

            var eles = new HashSet<Type>(hashes.Select(t => t.GetType())).ToList();

            double num;

            num = (double)nums[0];
        }



        public static void ReadDirectorySize()
        {
            string root = @"C:\Users\cportelli\Documents\Personal";
            string[] dirs = Directory.GetDirectories(root);

            foreach (string dir in dirs)
            {
                DirectoryInfo info = new DirectoryInfo(dir);
                try
                {
                    long totalSize = info.EnumerateFiles().Sum(file => file.Length);
                    
                    if(totalSize > 100)
                    {
                        info.EnumerateDirectories();
                        Console.WriteLine(string.Format("{0} : {1}", dir, totalSize));
                    }
                }
                catch (Exception ex) { }
            }
        }

    }

    #region ---- NOTES ----

    /*
     * Hash Code definition - https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netcore-3.1
     */

    #endregion


}
