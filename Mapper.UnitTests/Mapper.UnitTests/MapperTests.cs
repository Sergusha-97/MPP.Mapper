using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Mapper.UnitTests
{
    [TestFixture]
    internal class MapperTests
    {
        private static readonly Source Source = new Source()
        {
            FirstProperty = 5,
            SecondProperty = "5",
            ThirdProperty = 5.0,
            FourthProperty = 1,
            Dto = new SubDto() { i = 1 }
        };
        private static readonly Destination DestExpectedWithoutConfiguration = new Destination()
        {
            FirstProperty = 5,
            FourthProperty = 1,
            Dto = Source.Dto
        };
        private static readonly Destination DestExpectedWithConfiguration = new Destination()
        {
            FirstProperty = 5,
            ThirdProperty = "5",
            FourthProperty = 5,
            Dto = Source.Dto
        };

        [Test]
        public void IsValidMappingParam_Null_ThrowsException()
        {
            Mapper mapper = new Mapper();

            var ex = Assert.Catch<ArgumentNullException>(()=>mapper.Map<object,object>(null));
        }
        [Test]
        public void Mapping_WithoutConfiguration()
        {
            Mapper mapper = new Mapper();

            Destination dest = mapper.Map<Source, Destination>(Source);

            Assert.AreEqual(DestExpectedWithoutConfiguration, dest);
        }
        [Test]
        public void Mapping_NoCompatibleProperties_ReturnsDefaultOf()
        {
            Mapper mapper = new Mapper();

            Destination dest = mapper.Map<Dto, Destination>(new Dto() {i = 1});

            Assert.AreEqual(null, dest);
        }
        [Test]
        public void Mapping_WithConfiguration()
        {
            MapperConfiguration mconfig = new MapperConfiguration();
            mconfig.Register<Source, Destination>(s => s.SecondProperty, f => f.ThirdProperty).
                    Register<Source, Destination>(s => s.FirstProperty, f => f.FourthProperty);
            Mapper mapper = new Mapper(mconfig);

            Destination dest = mapper.Map<Source, Destination>(Source);

            Assert.AreEqual(DestExpectedWithConfiguration, dest);
        }
        [Test]
        public void IsValidMapperConfigurationParam_Null_ThrowsException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() => new Mapper(configuration: null));
        }

    }
}
