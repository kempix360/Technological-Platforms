using System.Xml.Serialization;

namespace laboratory_9;

public class Engine
{
    [XmlElement("displacement")]
    public double? Displacement { get; set; }
    [XmlElement("horsePower")]
    public int? HorsePower { get; set; }
    [XmlAttribute("model")]
    public string? Model { get; set; }
    
    public Engine() { }
    public Engine(double displacement, int power, string fuelType)
    {
        Displacement = displacement;
        HorsePower = power;
        Model = fuelType;
    }
}