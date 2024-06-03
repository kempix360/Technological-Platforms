using System.Xml.Linq;
using System.Xml.XPath;

namespace laboratory_9;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        List<Car> myCars = new List<Car>(){
            new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
            new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
            new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
            new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
            new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
            new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
            new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
            new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
            new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
        };
        
        // task 1
        var query1 = from car in myCars
            where car.Model == "A6"
            let engineType = car.Motor.Model == "TDI" ? "diesel" : "petrol"
            let hppl = (double)car.Motor.HorsePower / car.Motor.Displacement
            select new { engineType, hppl };
        
        var groupedQuery = from car in query1
            group car by car.engineType into grouped
            select new
            {
                EngineType = grouped.Key,
                AvgHppl = grouped.Average(c => c.hppl)
            };
        
        Console.WriteLine("\nQuery results:");
        foreach (var group in groupedQuery)
        {
            Console.WriteLine($"{group.EngineType}: {group.AvgHppl}");
        }
        
        // task 2
        
        SerializeToXml(myCars, "myCars.xml");
        List<Car> deserializedCars = DeserializeFromXml<List<Car>>("myCars.xml");
        
        Console.WriteLine("\nDeserialized cars:");
        foreach (var car in deserializedCars)
        {
            Console.WriteLine($"Model: {car.Model}, Engine: {car.Motor.Displacement}L {car.Motor.HorsePower}HP {car.Motor.Model}, Year: {car.Year}");
        }
        
        
        // task 3
        XPath("myCars.xml");
        
        // task 4
        CreateXmlFromLinq(myCars);
        
        // task 5
        GenerateXhtml(myCars);
        
        // task 6
        ModifyXmlDocument("myCars.xml");
    }
    
    private static void SerializeToXml<T>(T obj, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("cars"));

        using (TextWriter writer = new StreamWriter(fileName))
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            serializer.Serialize(writer, obj, namespaces);
        }
    }
    
    private static T DeserializeFromXml<T>(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("cars"));
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        {
            return (T)serializer.Deserialize(fileStream);
        }
    }
    
    private static void XPath(string fileName)
    {
        XElement rootNode = XElement.Load(fileName);
        
        double avgHP = (double) rootNode.XPathEvaluate("sum(//car[engine/@model != 'TDI']/engine/horsePower) div count(//car[engine/@model != 'TDI'])");
        Console.WriteLine($"\nAverage power of cars with engines other than TDI: {avgHP}");
        
        IEnumerable<string> models = rootNode.XPathSelectElements("./car/model").Select(x => x.Value).Distinct();
        Console.WriteLine("Unique car models:");
        foreach (var model in models)
        {
            Console.WriteLine(model);
        }
    }

    private static void CreateXmlFromLinq(List<Car> myCars)
    {
        IEnumerable<XElement> nodes = myCars?
            .Select(n =>
                new XElement("car",
                    new XElement("model", n.Model),
                    new XElement("engine",
                        new XAttribute("model", n.Motor.Model),
                        new XElement("displacement", n.Motor.Displacement),
                        new XElement("horsePower", n.Motor.HorsePower)),
                    new XElement("year", n.Year)));
        XElement rootNode = new XElement("cars", nodes);
        rootNode.Save("CarsFromLinq.xml");
    }
    
    private static void GenerateXhtml(List<Car> cars)
    {
        XElement xhtml = new XElement("html",
            new XElement("head"),
            new XElement("body",
                new XElement("h2", "Car List"),
                new XElement("table", new XAttribute("border", "1"),
                    new XElement("thead",
                        new XElement("tr",
                            new XElement("th", "Model"),
                            new XElement("th", "Engine Model"),
                            new XElement("th", "Displacement"),
                            new XElement("th", "Horse Power"),
                            new XElement("th", "Year")
                        )
                    ),
                    new XElement("tbody",
                        cars.Select(car =>
                            new XElement("tr",
                                new XElement("td", car.Model),
                                new XElement("td", car.Motor.Model),
                                new XElement("td", car.Motor.Displacement),
                                new XElement("td", car.Motor.HorsePower),
                                new XElement("td", car.Year)
                            )
                        )
                    )
                )
            )
        );

        xhtml.Save("Table_XHTML.html");
        //Console.WriteLine("XHTML table generated successfully.");
    }
    
    private static void ModifyXmlDocument(string fileName)
    {
        XElement xmlDoc = XElement.Load("myCars.xml");
        foreach (var element in xmlDoc.Descendants("horsePower"))
        {
            element.Name = "hp";
        }
        
        foreach (var carElement in xmlDoc.Elements("car"))
        {
            string year = carElement.Element("year")?.Value;
            if (year != null)
            {
                carElement.Element("model")?.Add(new XAttribute("year", year));
                carElement.Element("year")?.Remove();
            }
        }
        xmlDoc.Save("myCarsModified.xml");
    }
}