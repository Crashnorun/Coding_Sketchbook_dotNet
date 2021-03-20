using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class OptionalProperty : Attribute
    {
        public bool IsOptional { get; set; }

        public string Name { get; set; }

        public OptionalProperty(bool IsOptional)
        {
            this.IsOptional = IsOptional;
        }

        public OptionalProperty() { }
    }
}
