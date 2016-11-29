using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class MapperConfiguration 
    {
        private readonly Dictionary<MappingElement, List<PropertiesPair>> _configDict;

        public MapperConfiguration()
        {
            _configDict = new Dictionary<MappingElement, List<PropertiesPair>>();
        }
        public MapperConfiguration Register<TSource, TDestination>
            (Expression<Func<TSource, object>> sourcePropertyExpr,
            Expression<Func<TDestination, object>> destPropertyExpr) where TDestination : new()
        {
            if (sourcePropertyExpr == null)
            {
                throw new ArgumentNullException(nameof(sourcePropertyExpr));
            }
            if (destPropertyExpr == null)
            {
                throw new ArgumentNullException(nameof(destPropertyExpr));
            }
            PropertyInfo destProperty = GetPropertyInfo(destPropertyExpr);
            if (!destProperty.CanWrite)
            {
                throw new ArgumentException("Destination property can't write");
            }
            PropertyInfo sourceProperty = GetPropertyInfo(sourcePropertyExpr);
            Type sourceType = sourceProperty.PropertyType;
            Type destType = destProperty.PropertyType;
            if (!TypeCompatibilityChecker.Check(sourceType, destType))
            {
                throw new ArgumentException("The properties types are incompatible");
            }
            MappingElement mappingElement = new MappingElement()
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
            };
            PropertiesPair propertiesPair = new PropertiesPair(sourceProperty,destProperty);
            if (_configDict.ContainsKey(mappingElement))
            {
                _configDict[mappingElement].Add(propertiesPair);
            }
            else
            {
                List<PropertiesPair> propertiesPairsList = new List<PropertiesPair>()
                {
                    propertiesPair
                };
                _configDict.Add(mappingElement,propertiesPairsList);
            }
            return this;
        }
        private  PropertyInfo GetPropertyInfo<TSource, TProperty>(
                    Expression<Func<TSource, TProperty>> propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                var body = propertyLambda.Body as UnaryExpression;
                if (body != null)
                {
                    member =  (MemberExpression)body.Operand;
                }
                else
                {
                    throw new ArgumentException(
                        $"Expression '{propertyLambda.ToString()}' refers to a method, not a property.");
                }
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' refers to a field, not a property.");
            return propInfo;
        }

        internal IEnumerable<PropertiesPair> GetPropertiesPairsList<TSource, TDestination>()
        {
            MappingElement mappingElement= new MappingElement()
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
                
            };
            if (_configDict.ContainsKey(mappingElement))
            {
                return _configDict[mappingElement];
            }
            else
            {
                return new List<PropertiesPair>();
            }
        }
    }
}
