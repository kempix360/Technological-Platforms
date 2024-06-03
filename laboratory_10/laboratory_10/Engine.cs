namespace laboratory_10;

using System;

public class Engine: IComparable<Engine>
{
    public double? Displacement { get; set; }
    public int? HorsePower { get; set; }
    public string? Model { get; set; }
    
    public Engine() { }
    public Engine(double displacement, int power, string fuelType)
    {
        Displacement = displacement;
        HorsePower = power;
        Model = fuelType;
    }
    
    public int CompareTo(Engine other)
    {
        if (!HorsePower.HasValue && !other.HorsePower.HasValue) return 0;
        if (!HorsePower.HasValue) return -1;
        if (!other.HorsePower.HasValue) return 1;
        
        return HorsePower.Value.CompareTo(other.HorsePower.Value);
    }
}