using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class PropertiesPair : IPropertiesPair
    {
        public PropertyInfo SourceProperty { get; private set; }
        public PropertyInfo DestinationProperty { get; private set; }

        public PropertiesPair(PropertyInfo sourcePropertyInfo, PropertyInfo destinationPropertyInfo)
        {
            SourceProperty = sourcePropertyInfo;
            DestinationProperty = destinationPropertyInfo;
        }
    }
}
