using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTypes
{
    public class mRoom
    {

        public string LevelName { get; set; }

        public string LevelID { get; set; }

        public string UpperLevelName { get; set; }

        public double Height { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }


        public double Perimiter { get; set; }

        public double Area { get; set; }

        public double Volume { get; set; }

        public List<mFace> Faces { get; set; }

        public mPoint MaxPt { get; set; }
        public mPoint MinPt { get; set; }
        public Dictionary<string,object> Attributes { get; set; }

        public mRoom()
        {
            Faces = new List<mFace>();
        }

    }
}
