using System.Windows;

namespace laboratory_10;

public partial class AddCarWindow : Window
{
    public string CarModel { get; private set; }
    public string EngineModel { get; private set; }
    public double Displacement { get; private set; }
    public int HorsePower { get; private set; }
    public int Year { get; private set; }

    public AddCarWindow()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        CarModel = ModelTextBox.Text;
        EngineModel = EngineModelTextBox.Text;
        Displacement = double.Parse(DisplacementTextBox.Text);
        HorsePower = int.Parse(HorsePowerTextBox.Text);
        Year = int.Parse(YearTextBox.Text);
        
        DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
