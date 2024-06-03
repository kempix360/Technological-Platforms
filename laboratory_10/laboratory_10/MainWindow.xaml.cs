using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace laboratory_10
{
    public partial class MainWindow : Window
    {
        private static List<Car> MyCars = new()
        {
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

        private BindingList<Car> _myCarsBindingList;
        private List<Car> _tempCars;
        private SortableBindingList _carList = new SortableBindingList(MyCars);


        public MainWindow()
        {
            InitializeComponent();

            _myCarsBindingList = new BindingList<Car>(MyCars);
            CarDataGrid.ItemsSource = _myCarsBindingList;
            
            // task 1
            var queryResult = GetEngineTypeAverageHppl();
            string messageText = "";

            foreach (var item in queryResult)
            {
                messageText += $"{item.engineType}: {item.avgHPPL}\n";
            }

            MessageBox.Show(messageText);
            
            // task 2
            Func<Car, Car, int> arg1 = Func;
            Predicate<Car> arg2 = Predicate;
            Action<Car> arg3 = Action;
            MyCars.Sort(new Comparison<Car>(arg1));
            MyCars.FindAll(arg2).ForEach(arg3);
        }
        
        private static int Func(Car car, Car car2)
        {
            if (car.Motor.HorsePower > car2.Motor.HorsePower) {
                return 1;
            }

            if (car.Motor.HorsePower < car2.Motor.HorsePower) {
                return -1;
            }
            return 0;
        }

        private static bool Predicate(Car car)
        {
            return car.Motor.Model == "TDI";
        }

        private static void Action(Car car)
        {
            MessageBox.Show("2. Model: " + car.Model + " Engine: " + car.EngineDetails + " Year: " + car.Year);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text) || SearchBox.Text == "")
            {
                _myCarsBindingList = new BindingList<Car>(_carList);
                CarDataGrid.ItemsSource = _myCarsBindingList;
                return;
            }

            string query = SearchBox.Text;
            ComboBoxItem selectedItem = (ComboBoxItem)SearchComboBox.SelectedItem;
            string searchBy = selectedItem?.Content.ToString();

            _tempCars = _carList.Find(query, searchBy);
            _myCarsBindingList = new BindingList<Car>(_tempCars);
            CarDataGrid.ItemsSource = _myCarsBindingList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addCarWindow = new AddCarWindow();
            if (addCarWindow.ShowDialog() == true)
            {
                var newCar = new Car
                {
                    Model = addCarWindow.CarModel,
                    Motor = new Engine
                    {
                        Model = addCarWindow.EngineModel,
                        Displacement = addCarWindow.Displacement,
                        HorsePower = addCarWindow.HorsePower
                    },
                    Year = addCarWindow.Year
                };
                _tempCars = _carList.AddElement(newCar);
                _carList = new SortableBindingList(_tempCars);
                _myCarsBindingList = new BindingList<Car>(_tempCars);
                CarDataGrid.ItemsSource = _myCarsBindingList;
            }
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarDataGrid.SelectedItem is Car selectedCar)
            {
                _tempCars = _carList.RemoveElement(selectedCar);
                _carList = new SortableBindingList(_tempCars);
                _myCarsBindingList = new BindingList<Car>(_tempCars);
                CarDataGrid.ItemsSource = _myCarsBindingList;
            }
            else {
                MessageBox.Show("Please select a car to remove.");
            }
        }
        
        private void DisplayEverythingButton_Click(object sender, RoutedEventArgs e)
        {
            _myCarsBindingList = new BindingList<Car>(_carList);
            CarDataGrid.ItemsSource = _myCarsBindingList;
        }
        
        private IEnumerable<dynamic> GetEngineTypeAverageHppl()
        {
            // Query expression syntax
            var querySyntax = from car in MyCars
                where car.Model == "A6"
                let engineType = car.Motor.Model == "TDI" ? "diesel" : "petrol"
                let hppl = car.Motor.HorsePower / car.Motor.Displacement
                group hppl by engineType into g
                orderby g.Average() descending 
                select new
                {
                    engineType = g.Key,
                    avgHPPL = g.Average()
                };

            // Method-based query syntax
            var methodSyntax = MyCars
                .Where(car => car.Model == "A6")
                .Select(car => new
                {
                    engineType = car.Motor.Model == "TDI" ? "diesel" : "petrol",
                    hppl = car.Motor.HorsePower / car.Motor.Displacement
                })
                .GroupBy(car => car.engineType)
                .Select(g => new
                {
                    engineType = g.Key,
                    avgHPPL = g.Average(car => car.hppl)
                })
                .OrderByDescending(car => car.avgHPPL);

            return querySyntax.Concat(methodSyntax).Distinct();
        }
        private void CarDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;

            string columnName = e.Column.SortMemberPath;

            switch (columnName)
            {
                case "Model":
                    _myCarsBindingList = new BindingList<Car>(_myCarsBindingList.OrderBy(car => car.Model).ToList());
                    break;
                case "EngineDetails":
                    _myCarsBindingList = new BindingList<Car>(_myCarsBindingList.OrderBy(car => car.EngineDetails).ToList());
                    break;
                case "Year":
                    _myCarsBindingList = new BindingList<Car>(_myCarsBindingList.OrderBy(car => car.Year).ToList());
                    break;
            }
            CarDataGrid.ItemsSource = MyCars;
        }
        
        private void Sort_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)SortComboBox.SelectedItem;
                string? sortBy = selectedItem.Content.ToString();
                _myCarsBindingList = new BindingList<Car>(_carList.Sort(sortBy));
                CarDataGrid.ItemsSource = _myCarsBindingList;
            }
        }
    }
}
