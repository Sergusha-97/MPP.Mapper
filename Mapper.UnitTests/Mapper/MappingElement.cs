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
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            var comparrigMappingElement = obj as MappingElement;
            if (comparrigMappingElement == null) return false;
            return IsElementsEqual(comparrigMappingElement);
        }

        private bool IsElementsEqual(MappingElement obj)
        {
            return this.Source == obj.Source &&
                   this.Destination == obj.Destination;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                hash = hash * 23 + (Source == null ? 0 : Source.GetHashCode());
                hash = hash * 23 + (Destination == null ? 0 : Destination.GetHashCode());
                return hash;
            }
        }
    }
}
