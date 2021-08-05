using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossPlatformCapabilities
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<App, bool>(this, 
                "ConnectionEvent", 
                ManageConnection);

            MessagingCenter.Subscribe<App, bool>(this,
                "BatteryEvent", ManageBatteryLevelChanged);
        }

        private void ManageBatteryLevelChanged(App arg1, bool arg2)
        {
            FileHelper.WriteData("test data");
        }

        private void ManageConnection(App arg1, bool arg2)
        {
            LayoutRoot.IsEnabled = arg2;
            ConnectionStatusBar.IsVisible = !arg2;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            FileHelper.WriteData("test data");

            await Task.Delay(2000);

            string result = FileHelper.ReadData();
        }
    }
}
