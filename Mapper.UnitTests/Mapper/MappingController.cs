using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class MappingController : IMappingController
    {
        public Func<TSource, TDestination> GenerateFunc<TSource, TDestination>
            (IEnumerable<IPropertiesPair> propertiesList)
            where TDestination : new()
        {
            if (propertiesList == null)
            {
                throw new ArgumentNullException(nameof(propertiesList));
            }
            Type creatingType = typeof(TDestination);
            var lambdaParam = Expression.Parameter(typeof(TSource), "source");
            var ctor = Expression.New(creatingType);
            var valueAssignments = GetValuesAssignments(propertiesList, lambdaParam);
            var memberInit = Expression.MemberInit(ctor, valueAssignments);
            return Expression.Lambda<Func<TSource, TDestination>>(memberInit, lambdaParam).Compile();
        }

        private IEnumerable<MemberBinding> GetValuesAssignments
            (IEnumerable<IPropertiesPair> propertiesList, Expression lambdaParam)
        {
            List<MemberBinding> valuesAssignments = new List<MemberBinding>();
            foreach (var propertyPair in propertiesList)
            {
                Expression expressionProperty = Expression.Property(lambdaParam, propertyPair.SourceProperty);
                expressionProperty = Expression.Convert
                    (expressionProperty,propertyPair.DestinationProperty.PropertyType);
               valuesAssignments.Add(Expression.Bind
                    (propertyPair.DestinationProperty, expressionProperty));
            }
            return valuesAssignments;
        }
    }
}
