using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMatchSDK
{
    public class Folder
    {

        #region ---- PROPERTIES ----

        public string FolderPath {
            get { return _folderPath; }
        }
        private string _folderPath;

        #endregion



        #region ---- CONSTRUCTORS ----

        public Folder() { }


        public Folder(string FolderPath)
        {
            _folderPath = FolderPath;
        }
        #endregion

    }
}
