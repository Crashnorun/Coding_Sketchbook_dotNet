using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTypes
{
    public class mCurve
    {
        public mPoint StartPoint { get; set; }
        public mPoint MidPoint { get; set; }
        public mPoint EndPoint { get; set; }

        public mCurve() { }

        public mCurve(mPoint StPt, mPoint MidPt, mPoint EndPt)
        {
            this.StartPoint = StPt;
            this.MidPoint = MidPt;
            this.EndPoint = EndPt;
        }
    }
}
