using System.Collections.Generic;
using System.Linq;

namespace ApiDataAbstraction.Model
{
    public class ProductProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string[] PossibleValues { get; set; }
        public bool IsVisible { get; set; }

        public ProductProperty(string name, string value, IEnumerable<string> possibleValues = null,
            bool isVisible = true)
        {
            Name = name;
            Value = value;
            PossibleValues = possibleValues?.ToArray();
            IsVisible = isVisible;
        }

        public static List<ProductProperty> CreateDefaultConfiguration()
        {
            return new List<ProductProperty>
            {
                new ProductProperty("ENGINE_FUEL_TYPE", "Diesel", new[] {"Diesel", "Petrol", "Hybrid"}),
                new ProductProperty("ENGINE_TRANS", "Manual", new[] {"Manual", "Auto"}),
                new ProductProperty("EXT_COLOR", "Red", new[] {"Red", "Green", "Blue"}),
                new ProductProperty("RADIO_TYPE", "MP3", new[] {"MP3", "CD", "GPS"}),
                new ProductProperty("RADIO_SPEAKERS", "6", new []{"2", "4", "6", "8"}),
                new ProductProperty("GPS_MAPS", null, null, false),
                new ProductProperty("EXT_HEADLIGHTS", "Normal"),
                new ProductProperty("CONF_NAME", "Michal Dymel"),
            };
        }
    }
}