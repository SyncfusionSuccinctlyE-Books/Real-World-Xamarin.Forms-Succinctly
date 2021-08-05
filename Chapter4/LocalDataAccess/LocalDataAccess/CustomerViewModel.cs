using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace LocalDataAccess
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }


        private DataService dataAccess;

        public CustomerViewModel()
        {
            dataAccess = new DataService();

            var customers = dataAccess.GetCustomers();

            Customers =
              new ObservableCollection<Customer>(customers);

            if (!Customers.Any())
                AddCustomer();
        }

        private void AddCustomer()
        {
            var newCustomer = new Customer
            {
                CompanyName = "Company name...",
                PhysicalAddress = "Address...",
                Country = "Country..."
            };

            Customers.Add(newCustomer);
            dataAccess.SaveCustomer(newCustomer);
        }

        public Command SaveAllCommand
        {
            get
            {
                return new Command(() =>
                dataAccess.SaveAllCustomers(Customers));
            }
        }

        public Command AddNewCommand
        {
            get
            {
                return new Command(() =>
                AddCustomer());
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedCustomer != null)
                    {
                        Customers.Remove(SelectedCustomer);
                        dataAccess.DeleteCustomer(SelectedCustomer);
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
