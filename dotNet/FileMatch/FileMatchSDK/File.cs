using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileMatchSDK
{
    public class File : MP3File
    {

        #region ---- PROPERTIES ----

        /// <summary>
        /// FileName and extension, returns NULL if FilePath is not set
        /// </summary>
        public string FileName
        {
            get { return string.IsNullOrEmpty(_filePath) ? null : Path.GetFileName(_filePath); }
            set { _fileName = value; }
        }
        private string _fileName;

        /// <summary>
        /// FileName without extension. Returns null if FileName is not set
        /// </summary>
        public string FileNameOnly
        {
            get
            {
                return string.IsNullOrEmpty(_fileName) ? null : this.FileName.Remove(_fileName.Length - this.FileExtension.Length, this.FileExtension.Length);
            }
        }

        public string FileExtension
        {
            get { return string.IsNullOrEmpty(_filePath) ? null : Path.GetExtension(_filePath); }
        }

        /// <summary>
        /// Full file path with name and extension
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        private string _filePath;

        /// <summary>
        /// Get the folder path
        /// </summary>
        public string FolderPath
        {
            get
            {
                return string.IsNullOrEmpty(_filePath) ? null : Path.GetDirectoryName(_filePath);
            }
        }

        /// <summary>
        /// File size in bytes. Return -1 if null
        /// </summary>
        public long FileSize
        {
            get
            {
                return string.IsNullOrEmpty(_filePath) ? -1 : new FileInfo(_filePath).Length;
            }
        }

        /// <summary>
        /// Returns the date created. Returns default DATETIME() if Filepath is not set
        /// </summary>
        public DateTime Created
        {
            get
            {
                return string.IsNullOrEmpty(_filePath) ? new DateTime() : System.IO.File.GetCreationTime(_filePath);
            }
        }

        /// <summary>
        /// Returns the date modified. Returns default DATETIME() if Filepath is not set
        /// </summary>
        public DateTime Modified
        {
            get
            {
               return string.IsNullOrEmpty(_filePath)? new DateTime(): System.IO.File.GetLastWriteTime(_filePath);
            }
        }

        #endregion


        #region ---- CONSTRUCTOR ----

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public File() { }

        /// <summary>
        /// Create a new FilePath object
        /// </summary>
        /// <param name="FilePath">String that represents the full file path of the file</param>
        public File(string FilePath)
        {
            if (!System.IO.File.Exists(FilePath))
                throw new Exception("File does not exist: " + FilePath);

            this._filePath = FilePath;
        }
       
        #endregion

    }
}
