using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalDataAccess
{
    public partial class MainPage : ContentPage
    {
        private CustomerViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new CustomerViewModel();
            BindingContext = ViewModel;
        }
    }
}
