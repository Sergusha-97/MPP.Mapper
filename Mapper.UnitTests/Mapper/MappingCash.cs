using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class MappingCash : IMappingCash
    {
        private readonly Dictionary<IMappingElement, Delegate> _cashBase;
        public MappingCash()
        {
            _cashBase = new Dictionary<IMappingElement, Delegate>();
        }
        public void Add(IMappingElement elem, Delegate lambda)
        {
            _cashBase.Add(elem, lambda);
        }

        public bool Contains(IMappingElement elem)
        {
           return _cashBase.ContainsKey(elem);
        }

        public Delegate Get(IMappingElement elem)
        {
            return _cashBase[elem];
        }
    }
}
