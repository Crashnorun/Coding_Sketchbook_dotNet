using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    /// <summary>
    /// Node class that inherits from SubNode
    /// </summary>
    public class Node : SubNode
    {
        /*  public int NodeIndex { get; set; }
          public int X { get; set; }
          public int Y { get; set; }
          public int Z { get; set; }*/

        /// <summary>
        /// Node name
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// new X value, overrites inherited value
        /// </summary>
        public new int X { get; set; }

        /// <summary>
        /// Another node name, to make Node unique from SubNode
        /// </summary>
        public string AnotherName { get; set; }
    }

    /// <summary>
    /// SubNode class
    /// </summary>
    public class SubNode
    {
        /// <summary>
        /// Node index value
        /// </summary>
        public int NodeIndex { get; set; }

        /// <summary>
        /// X value
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y value
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Z value
        /// </summary>
        public int Z { get; set; }

    }

}
