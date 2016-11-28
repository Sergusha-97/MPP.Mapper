using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IPropertiesPair
    {
         PropertyInfo SourceProperty { get;  }
         PropertyInfo DestinationProperty { get; }
    }
}
