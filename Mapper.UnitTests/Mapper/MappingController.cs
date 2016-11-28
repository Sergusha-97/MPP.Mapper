using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class MappingController : IMappingController
    {
        public Func<TSource, TDestination> GenerateFunc<TSource, TDestination>(TSource source, IEnumerable<IPropertiesPair> propertiesList )
            where TDestination : new()
        {
            throw new NotImplementedException();
        }
    }
}
