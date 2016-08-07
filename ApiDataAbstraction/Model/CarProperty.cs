using System;

namespace ApiDataAbstraction.Model
{
    public class CarProperty : Attribute
    {
        public enum PropertyType
        {
            Properties,
            Value,
            PossibleValues,
            IsVisible
        }

        public CarProperty()
        {
            Type = PropertyType.Properties;
        }

        public CarProperty(string name, PropertyType propertyType = PropertyType.Value)
        {
            Name = name;
            Type = propertyType;
        }

        public string Name { get; }
        public PropertyType Type { get; set; }
    }
}