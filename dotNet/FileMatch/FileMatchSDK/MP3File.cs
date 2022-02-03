using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMatchSDK
{
    public abstract class MP3File
    {

        #region ---- PROPERTIES ----

        public string BandName { get; set; }

        public string AlbumName { get; set; }

        public string TrackName { get; set; }

        public int TrackNumber { get; set; }

        public int TrackLength { get; set; }

        #endregion



        #region ---- CONSTRUCTORS ----

        public MP3File() { }


        public MP3File(string FilePath)
        {

        }

        #endregion


    }
}
