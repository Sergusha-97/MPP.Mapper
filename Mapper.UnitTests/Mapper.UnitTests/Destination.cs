using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.UnitTests
{
    internal class Destination
    {
        public long FirstProperty { get; set; }
        public string SecondProperty { get; }
        public string ThirdProperty { get; set; }
        public long FourthProperty { get; set; }
        public Dto Dto { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            var comparrigDestination = obj as Destination;
            if (comparrigDestination == null) return false;
            return IsElementsEqual(comparrigDestination);
        }

        private bool IsElementsEqual(Destination obj)
        {
            return this.FirstProperty == obj.FirstProperty &&
                   this.SecondProperty == obj.SecondProperty &&
                   this.ThirdProperty == obj.ThirdProperty &&
                   this.FourthProperty == obj.FourthProperty &&
                   this.Dto == obj.Dto;
        }
    }
}
