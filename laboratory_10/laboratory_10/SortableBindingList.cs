namespace laboratory_10;

using System.ComponentModel;

public class SortableBindingList : BindingList<Car>
{
    private bool _bModel = false;
    private bool _bYear = false;
    private bool _bMotor = false;

    public SortableBindingList(List<Car> cars)
    {
        foreach (var car in cars)
        {
            Add(car);
        }
    }
    
    public List<Car> AddElement(Car car)
    {
        var matchingCars = this.ToList();
        matchingCars.Add(car);
        return matchingCars;
    }
    
    public List<Car> RemoveElement(Car car)
    {
        var matchingCars = this.ToList();
        matchingCars.Remove(car);
        return matchingCars;
    }
    public List<Car> Find(string text, string combo)
    {
        var matchingCars = new List<Car>();

        foreach (var car in this)
        {
            switch (combo)
            {
                case "Model":
                {
                    if (car.Model == text)
                    {
                        matchingCars.Add(car);
                    }

                    break;
                }
                case "Year":
                {
                    if (car.Year == int.Parse(text))
                    {
                        matchingCars.Add(car);
                    }

                    break;
                }
                case "Engine model":
                {
                    if (car.Motor.Model == text)
                    {
                        matchingCars.Add(car);
                    }

                    break;
                }
            }
        }

        return matchingCars;
    }

    public List<Car> Sort(string property)
    {
        var matchingCars = this.ToList();
        
        switch (property)
        {
            case "Model":
            {
                _bModel = !_bModel;
                if (_bModel) return matchingCars.OrderBy(car => car.Model).ToList();
                return matchingCars.OrderByDescending(car => car.Model).ToList();
            }
            case "Year":
            {
                _bYear = !_bYear;
                if (_bYear) return matchingCars.OrderBy(car => car.Year).ToList();
                return matchingCars.OrderByDescending(car => car.Year).ToList();
            }
            case "Engine":
            {
                _bMotor = !_bMotor;
                if (_bMotor) return matchingCars.OrderBy(car => car.Motor).ToList();
                return matchingCars.OrderByDescending(car => car.Motor).ToList();
            }
            default:
            {
                _bModel = !_bModel;
                if (_bModel) return matchingCars.OrderBy(car => car.Model).ToList();
                return matchingCars.OrderByDescending(car => car.Model).ToList();
            }
        }
    }
}