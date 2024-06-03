using System.Xml.Serialization;

namespace laboratory_9;
[XmlType("car")]
public class Car
{
    [XmlElement("model")]
    public string? Model { get; set; }
    [XmlElement("engine")]
    public Engine? Motor { get; set; }
    [XmlElement("year")]
    public int? Year { get; set; }
    
    public Car() { }
    public Car(string model, Engine engine, int year)
    {
        Model = model;
        Motor = engine;
        Year = year;
    }
}