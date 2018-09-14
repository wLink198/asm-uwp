using App3.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string filter = "";
        public ObservableCollection<Customer> Customers { get => Model.CustomerModel.GetCustomers(filter); set => Model.CustomerModel.SetCustomers(value); }
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SaveCustomer(object sender, RoutedEventArgs e)
        {
            Entity.Customer customer = new Entity.Customer();
            customer.Name = this.Name.Text;
            customer.Email = this.Email.Text;
            customer.Phone = this.Phone.Text;
            customer.Address = this.Address.Text;
            
            customer.Thumbnail = this.Thumbnail.Text;
            Model.CustomerModel.AddCustomer(customer);
        }

        private async void Filter(object sender, RoutedEventArgs e)
        {
            filter = await InputTextDialogAsync("Search for customers:");
            if (filter != null)
            {
                Customers.Clear();
                Customers = Model.CustomerModel.GetCustomers(filter);
            }      
        }

        private async Task<string> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return null;
        }
    }
}
