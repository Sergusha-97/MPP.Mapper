using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IMappingElement
    {
         Type Source { get; set; }
         Type Destination { get; set; }
    }
}
