using System;
using csc13001_data_binding.Model;
using csc13001_data_binding.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace csc13001_data_binding.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhonePage : Page
    {
        public PhoneViewModel ViewModel { get; set; } = new PhoneViewModel();
        public PhonePage()
        {
            this.InitializeComponent();
        }

        private async void addPhoneButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            errorMessage.Visibility = Visibility.Collapsed;
            errorUpdateMessage.Visibility = Visibility.Collapsed;
            // Create a StackPanel to host the input controls.
            StackPanel panel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 0),
                Spacing = 10
            };

            // TextBox for Name (string)
            TextBox nameTextBox = new TextBox
            {
                Header = "Name",
                PlaceholderText = "Input new name",
            };

            // TextBox for Manufacturer (string)
            TextBox manufacturerTextBox = new TextBox
            {
                Header = "Manufacturer",
                PlaceholderText = "Input new manufacturer",
            };

            // TextBox for Price (float)
            TextBox priceTextBox = new TextBox
            {
                Header = "Price",
                PlaceholderText = "Input new price",
            };

            // Add the TextBoxes to the panel.
            panel.Children.Add(nameTextBox);
            panel.Children.Add(manufacturerTextBox);
            panel.Children.Add(priceTextBox);
            ContentDialog dialog = new ContentDialog()
            {
                XamlRoot = this.XamlRoot,
                Title = "Input new phone",
                PrimaryButtonText = "Add",
                SecondaryButtonText = "Cancel",
                Content = panel
            };

            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                string name = nameTextBox.Text;
                string manufacturer = manufacturerTextBox.Text;
                decimal price;
                if (!string.IsNullOrEmpty(name)
                    && !string.IsNullOrEmpty(manufacturer)
                    && !string.IsNullOrEmpty(priceTextBox.Text)
                    && decimal.TryParse(priceTextBox.Text, out price)
                    )
                {
                    ViewModel.AddPhone(new Phone
                    {
                        Id = ViewModel.Phones.Count + 1,
                        Name = name,
                        Manufacturer = manufacturer,
                        Price = price,
                        Image = "/Assets/phone.png"
                    });
                }
                else
                {
                    errorUpdateMessage.Text = "Invalid input!";
                    errorUpdateMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void deletePhoneButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var phone = (Phone)phonesGridView.SelectedItem;
            if (phone == null)
            {
                errorMessage.Visibility = Visibility.Visible;
                return;
            }
            ViewModel.DeletePhone(phone.Id);
        }

        private async void updatePhoneButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var phone = (Phone)phonesGridView.SelectedItem;
            if (phone == null)
            {
                errorMessage.Visibility = Visibility.Visible;
                return;
            }
            // Create a StackPanel to host the input controls.
            StackPanel panel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 0),
                Spacing = 10
            };

            // TextBox for Name (string)
            TextBox nameTextBox = new TextBox
            {
                Header = "Name",
                PlaceholderText = "Input new name",
                Text = phone.Name
            };

            // TextBox for Manufacturer (string)
            TextBox manufacturerTextBox = new TextBox
            {
                Header = "Manufacturer",
                PlaceholderText = "Input new manufacturer",
                Text = phone.Manufacturer
            };

            // TextBox for Price (float)
            TextBox priceTextBox = new TextBox
            {
                Header = "Price",
                PlaceholderText = "Input new price",
                Text = phone.Price.ToString(),
            };

            // Add the TextBoxes to the panel.
            panel.Children.Add(nameTextBox);
            panel.Children.Add(manufacturerTextBox);
            panel.Children.Add(priceTextBox);
            ContentDialog dialog = new ContentDialog()
            {
                XamlRoot = this.XamlRoot,
                Title = "Input your changes",
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Cancel",
                Content = panel
            };

            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                string name = nameTextBox.Text;
                string manufacturer = manufacturerTextBox.Text;
                decimal price;
                if (!string.IsNullOrEmpty(name)
                    && !string.IsNullOrEmpty(manufacturer)
                    && !string.IsNullOrEmpty(priceTextBox.Text)
                    && decimal.TryParse(priceTextBox.Text, out price)
                    )
                {
                    if (string.Equals(name, phone.Name) && string.Equals(manufacturer, phone.Manufacturer) && price == phone.Price)
                    {
                        errorUpdateMessage.Text = "No changes detected!";
                        errorUpdateMessage.Visibility = Visibility.Visible;
                        return;
                    }
                    ViewModel.UpdatePhone(new Phone
                    {
                        Id = phone.Id,
                        Name = name,
                        Manufacturer = manufacturer,
                        Price = price,
                    });
                }
                else
                {
                    errorUpdateMessage.Text = "Invalid input!";
                    errorUpdateMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void phonesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            errorMessage.Visibility = Visibility.Collapsed;
            errorUpdateMessage.Visibility = Visibility.Collapsed;
        }
    }
}
