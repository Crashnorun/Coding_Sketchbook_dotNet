using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Node : SubNode
    {
        /*  public int NodeIndex { get; set; }
          public int X { get; set; }
          public int Y { get; set; }
          public int Z { get; set; }*/
        public string NodeName { get; set; }
        public new int X { get; set; }
        public string AnotherName { get; set; }
    }


    public class SubNode
    {
        public int NodeIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

    }

}
