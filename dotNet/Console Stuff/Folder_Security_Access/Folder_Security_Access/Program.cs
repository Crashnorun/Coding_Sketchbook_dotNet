using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Security_Access
{
    class Program
    {
        static void Main(string[] args)
        {
            string mydocuments = @"C:\Users\cportelli\Documents";
            string programFiles = @"C:\Program Files";
            string appdata = @"C:\Users\cportelli\AppData";
            string adminUser = @"C:\Users\Administrator";

            System.Security.AccessControl.DirectorySecurity dsMyDocs = Directory.GetAccessControl(mydocuments);
            System.Security.AccessControl.DirectorySecurity dsProgramFiles = Directory.GetAccessControl(programFiles);
            System.Security.AccessControl.DirectorySecurity dsAppData = Directory.GetAccessControl(appdata);
            System.Security.AccessControl.DirectorySecurity dsAdminUser = Directory.GetAccessControl(adminUser);

        }
    }
}



/*
 * The goal of this project is to test if the current user has access to certain folders on their C: Drive
 * 
 * MSDN: 
 * https://docs.microsoft.com/en-us/dotnet/api/system.security.accesscontrol.directorysecurity?view=net-5.0
 * 
 * 
 * Stakoverlfow posts:
 * https://stackoverflow.com/questions/1410127/c-sharp-test-if-user-has-write-access-to-a-folder
 * 
 * https://stackoverflow.com/questions/1281620/checking-for-directory-and-file-write-permissions-in-net
 */
