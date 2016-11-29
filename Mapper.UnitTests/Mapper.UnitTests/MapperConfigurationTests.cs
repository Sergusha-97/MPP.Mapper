using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Mapper.UnitTests
{
    [TestFixture]
    internal class MapperConfigurationTests
    {
        [Test]
        public void IsValidRegisterParam_Null_ThrowsException()
        {
            MapperConfiguration mconfig = new MapperConfiguration();

            var ex = Assert.Catch<ArgumentNullException>(() => mconfig.Register<object, object>(null,null));
        }
        [Test]
        public void Register_DestinationPropertyReadonly_ThrowsException()
        {
            MapperConfiguration mconfig = new MapperConfiguration();

            var ex = Assert.Catch<ArgumentException>(() => mconfig.Register<Source, Destination>
                    (s => s.SecondProperty, f => f.SecondProperty));
        }
        [Test]
        public void Register_IncompatibleTypes_ThrowsException()
        {
            MapperConfiguration mconfig = new MapperConfiguration();

            var ex = Assert.Catch<ArgumentException>(() => mconfig.Register<Source, Destination>
                    (s => s.SecondProperty, f => f.FirstProperty));
        }
        [Test]
        public void Register_ExpressionRefersToMethod_ThrowsException()
        {
            MapperConfiguration mconfig = new MapperConfiguration();

            var ex = Assert.Catch<ArgumentException>(() => mconfig.Register<Source, TestClass>
                    (s => s.SecondProperty, t => t.GetName()));
        }
        [Test]
        public void Register_ExpressionRefersToField_ThrowsException()
        {
            MapperConfiguration mconfig = new MapperConfiguration();

            var ex = Assert.Catch<ArgumentException>(() => mconfig.Register<Source, TestClass>
                    (s => s.SecondProperty, t => t.Name));
        }
    }
}
