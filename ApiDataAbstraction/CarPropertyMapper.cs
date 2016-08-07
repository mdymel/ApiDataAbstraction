using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApiDataAbstraction.Model;

namespace ApiDataAbstraction
{
    public static class CarPropertyMapper
    {
        public static T Map<T>(List<ProductProperty> properties)
        {
            var model = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                var attribute =
                    (CarProperty)
                        propertyInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(CarProperty));
                if (attribute == null) continue;

                if (attribute.Type == CarProperty.PropertyType.Properties)
                {
                    MethodInfo methodInfo = typeof(CarPropertyMapper).GetMethod("Map");
                    MethodInfo genericMethod = methodInfo.MakeGenericMethod(propertyInfo.PropertyType);
                    object value = genericMethod.Invoke(null, new object[] {properties});
                    propertyInfo.SetValue(model, value);
                    continue;
                }

                ProductProperty productProperty = properties.FirstOrDefault(p => p.Name == attribute.Name);
                if (productProperty == null) continue;

                switch (attribute.Type)
                {
                    case CarProperty.PropertyType.Value:
                        string propertyValue = productProperty.Value;
                        if (propertyValue == null) continue;

                        object targetValue = ParsePropertyValue(propertyInfo.PropertyType, propertyValue);
                        if (targetValue == null)
                        {
                            continue;
                        }
                        propertyInfo.SetValue(model, targetValue);
                        break;

                    case CarProperty.PropertyType.PossibleValues:
                        var possibleValues = productProperty.PossibleValues;

                        Type elementType = propertyInfo.PropertyType.GetElementType();
                        Array targetArray = Array.CreateInstance(elementType, possibleValues.Length);

                        var i = 0;
                        foreach (string proposedValue in possibleValues)
                        {
                            if (elementType == typeof(int)) targetArray.SetValue(ParseInt(proposedValue), i++);
                            else if (elementType == typeof(string)) targetArray.SetValue(proposedValue, i++);
                            else if (elementType.IsEnum)
                                targetArray.SetValue(ParseEnum(elementType, proposedValue), i++);
                        }
                        propertyInfo.SetValue(model, targetArray);
                        break;

                    case CarProperty.PropertyType.IsVisible:
                        propertyInfo.SetValue(model, productProperty.IsVisible);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return model;
        }

        private static object ParsePropertyValue(Type propertyType, string propertyValue)
        {
            object targetValue = null;
            if (propertyType == typeof(int)) targetValue = ParseInt(propertyValue);
            else if (propertyType == typeof(string)) targetValue = propertyValue;
            else if (propertyType == typeof(bool))
                targetValue = propertyValue.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            else if (propertyType.IsEnum) targetValue = ParseEnum(propertyType, propertyValue);
            return targetValue;
        }

        private static object ParseEnum(Type enumType, string value)
        {
            if (string.IsNullOrEmpty(value)) return Enum.GetValues(enumType).GetValue(0);
            return Enum.Parse(enumType, value, true);
        }

        private static int ParseInt(string value)
        {
            int result;
            if (string.IsNullOrEmpty(value)) return 0;
            if (!int.TryParse(value, out result)) throw new Exception("Int parse unsuccessful");
            return result;
        }
    }
}