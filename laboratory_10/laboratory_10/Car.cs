namespace laboratory_10;
public class Car
{
    public string? Model { get; set; }
    public Engine? Motor { get; set; }
    public int? Year { get; set; }
    
    public string EngineDetails => $"Model: {Motor.Model}, Displacement: {Motor.Displacement:N1}, Power: {Motor.HorsePower}";
    
    public Car() { }
    public Car(string model, Engine engine, int year)
    {
        Model = model;
        Motor = engine;
        Year = year;
    }
}