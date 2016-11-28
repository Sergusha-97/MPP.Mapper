using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Mapper : IMapper
    {
        private readonly IMappingCash _cash;
        private readonly IMappingController _controller;
        private Type _sourceType;
        private Type _destinationType;
        public Mapper(IMappingCash cash, IMappingController controller)
        {
            if (cash == null)
            {
                throw new ArgumentNullException(nameof(cash));
            }
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }
            _cash = cash;
            _controller = new MappingController();
        }

        public Mapper() : this(new MappingCash(),new MappingController())
        {
            
        }
        public TDestination Map<TSource, TDestination>(TSource source) where  TDestination : new()
        {
            _sourceType = typeof(TSource);
            _destinationType = typeof(TDestination);
            var mappingElement = CreateMappingElement();
            if (_cash.Contains(mappingElement))
            {
                return ((Func<TSource,TDestination>)_cash.Get(mappingElement))(source);
            }
            var properties = GetPropertyPairsList();
            Func<TSource, TDestination> lambda = _controller.GenerateFunc<TSource, TDestination>(source, properties);
            _cash.Add(mappingElement,lambda);
            return lambda(source);
        }

        private IMappingElement CreateMappingElement()
        {
          return new MappingElement()
          {
             Destination = _destinationType,
             Source = _sourceType
          };   
        }

        private IEnumerable<PropertiesPair> GetPropertyPairsList()
        {
            var properties = 
                (from destProperty
                 in _destinationType.GetProperties()
                 where destProperty.CanWrite
                 from sourceProperty in _sourceType.GetProperties()
                 where destProperty.Name == sourceProperty.Name
                 && TypeCompatibilityChecker.Check(sourceProperty.PropertyType, destProperty.PropertyType)
                 select new PropertiesPair(sourceProperty, destProperty)).ToList();
            return properties;
        }
    }
}
