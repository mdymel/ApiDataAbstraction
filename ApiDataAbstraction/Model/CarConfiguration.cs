using ApiDataAbstraction.Model.Enums;

namespace ApiDataAbstraction.Model
{
public class CarConfiguration
{
    [CarProperty("CONF_NAME")]
    public string CustomerName { get; set; }
    
    [CarProperty]
    public EngineConfiguration Engine { get; set; }
    [CarProperty]
    public ExteriorConfiguration Exterior { get; set; }
    [CarProperty]
    public InteriorConfiguration Interior { get; set; }
}

public class EngineConfiguration
{
    [CarProperty("ENGINE_FUEL_TYPE")]
    public EngineType Type { get; set; }
    [CarProperty("ENGINE_TRANS")]
    public TransmissionType Transmission { get; set; }
}

public class ExteriorConfiguration
{
    [CarProperty("EXT_COLOR")]
    public ColorType Color { get; set; }
    [CarProperty("EXT_COLOR", CarProperty.PropertyType.PossibleValues)]
    public ColorType[] ColorsPossible { get; set; }
    [CarProperty("EXT_HEADLIGHTS")]
    public HeadLightsType HeadLights { get; set; }
}

public class InteriorConfiguration
{
    [CarProperty("RADIO_TYPE")]
    public RadioType Radio { get; set; }
    [CarProperty("GPS_MAPS")]
    public GpsMapsRegion GpsMaps { get; set; }
    [CarProperty("GPS_MAPS", CarProperty.PropertyType.IsVisible)]
    public bool GpsMapsVisible { get; set; }
    [CarProperty("RADIO_SPEAKERS")]
    public int NumberOfSpeakers { get; set; }
}
}