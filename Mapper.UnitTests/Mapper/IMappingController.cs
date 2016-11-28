using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IMappingController
    {
        Func<TSource,TDestination> GenerateFunc<TSource, TDestination>(IEnumerable<IPropertiesPair> propertiesList ) 
            where TDestination : new();
    }
}
