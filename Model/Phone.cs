using System.ComponentModel;

namespace csc13001_data_binding.Model;

public class Phone : INotifyPropertyChanged
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
}
