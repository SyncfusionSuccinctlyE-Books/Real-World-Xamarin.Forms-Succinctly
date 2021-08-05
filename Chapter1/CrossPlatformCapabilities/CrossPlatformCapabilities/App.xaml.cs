using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace CrossPlatformCapabilities
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Battery.EnergySaverStatusChanged += Battery_EnergySaverStatusChanged;

            MainPage = new BrowserEmailSample();
        }

        private void Battery_EnergySaverStatusChanged(object sender, 
            EnergySaverStatusChangedEventArgs e)
        {
            // Remove the ChargeLevel check if your app
            // implements background services
            MessagingCenter.Send(this, "BatteryEvent", 
                e.EnergySaverStatus == EnergySaverStatus.On 
                && Battery.ChargeLevel <= 0.2);
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            MessagingCenter.Send(this, "ConnectionEvent", e.NetworkAccess == NetworkAccess.Internet);
        }

        protected override void OnStart()
        {
            MessagingCenter.Send(this, "ConnectionEvent", Connectivity.NetworkAccess == NetworkAccess.Internet);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
