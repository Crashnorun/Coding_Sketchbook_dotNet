using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTypes
{
    public class mLine
    {
        public mPoint StartPoint { get; set; }
        public mPoint EndPoint { get; set; }

        public mLine() { }

        public mLine(mPoint Stpt, mPoint EndPt)
        {
            this.StartPoint = Stpt;
            this.EndPoint = EndPt;
        }
    }
}
