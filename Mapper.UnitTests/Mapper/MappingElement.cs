using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class MappingElement : IMappingElement
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }
}
