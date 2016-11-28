using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal static class TypeCompatibilityChecker
    {
        public static bool Check(Type source, Type destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var result = source == destination;
            if (!result)
            {
                result = IsCompatiblePrimitives(source, destination);
            }
            if (!result)
            {
                result = IsCompatibleRefTypes(source, destination);
            }
            return result;

        }

        private static bool IsCompatiblePrimitives(Type source, Type destination)
        {
            var result = false;
            if (source.IsValueType && source.IsPrimitive && destination.IsValueType && destination.IsPrimitive)
            {
                result =  IsCompatiblePrimitives(source, destination);
            }
            return result;
 
        }
        private static bool IsCompatibleRefTypes(Type source, Type destination)
        {
            var result = false;
            if (source.IsClass && !source.IsAbstract && destination.IsValueType && !destination.IsAbstract)
            {
                result = destination.IsAssignableFrom(source);
            }
            return result;

        }
        private static readonly Dictionary<Type, HashSet<Type>> _types = new Dictionary<Type, HashSet<Type>>()
        {
            {
                typeof(byte),
                new HashSet<Type>()
                {
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(sbyte),
                new HashSet<Type>()
                {
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(short),
                new HashSet<Type>()
                {
                    typeof(int),
                    typeof(long),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(ushort),
                new HashSet<Type>()
                {
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(int),
                new HashSet<Type>()
                {
                    typeof(long),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(uint),
                new HashSet<Type>()
                {
                    typeof(long),
                    typeof(ulong),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
           {
                typeof(long),
                new HashSet<Type>()
                {
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(ulong),
                new HashSet<Type>()
                {
                    typeof(long),
                    typeof(decimal),
                    typeof(float),
                    typeof(double)
                }
            },
            {
                typeof(float),
                new HashSet<Type>()
                {
                    typeof(double)
                }
            },
            {
                typeof(decimal),
                new HashSet<Type>()
                {
                    typeof(float),
                    typeof(double)
                }
            },
        };



    }
}
