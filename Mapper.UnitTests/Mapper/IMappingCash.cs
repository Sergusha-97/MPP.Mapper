using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IMappingCash
    {
        void Add(IMappingElement elem, Delegate lambda);
        bool Contains(IMappingElement elem);
        Delegate Get(IMappingElement elem);
    }
}
