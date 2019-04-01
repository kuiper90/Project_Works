using System;
using System.Collections;
using System.Reflection;

namespace Works
{
    public class ObjectComparer
    {
        public static bool CompareObjects(object first, object second)
        {
            if (object.ReferenceEquals(second, null))
                return false;
            if (object.ReferenceEquals(first, second))
                return true;

            if (first.GetType() != second.GetType())
                return false;

            var propertiesInfo = second.GetType().GetProperties();
                        
            foreach (var value in propertiesInfo)
            {
                //Prior to .NET 4.5 you need to pass null as a second argument
                var firstPropInfo = value.GetValue(first, null);
                var secondPropInfo = value.GetValue(second, null);

                if (firstPropInfo == null && secondPropInfo == null)
                    continue;

                if ((firstPropInfo is IList && value.PropertyType.IsGenericType) || 
                    (secondPropInfo is IList && value.PropertyType.IsGenericType))
                {
                    dynamic one = firstPropInfo;
                    dynamic two = secondPropInfo;

                    if (one != null && one.Count > 0)
                    {
                        var res = false;
                        foreach (object valOne in one)
                        {
                            foreach (object valTwo in two)
                            {
                                if (CompareObjects(valOne, valTwo) == false)
                                    continue;
                                res = true;
                                break;
                            }
                        }
                        if (res == false)
                            return false;
                    }
                    else
                    {
                        var typeOfFirst = firstPropInfo.GetType();

                        if (typeOfFirst.IsValueType || firstPropInfo is string)
                        {
                            if (firstPropInfo.Equals(secondPropInfo) == false)
                                return false;
                        }
                        else
                        {
                            if (CompareObjects(firstPropInfo, secondPropInfo) == false)
                                return false;
                        }
                    }
                }

            }
            return true;
        }
    }
}
