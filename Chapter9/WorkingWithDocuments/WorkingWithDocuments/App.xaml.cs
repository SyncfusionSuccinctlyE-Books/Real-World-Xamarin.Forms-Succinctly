using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkingWithDocuments
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.
                SyncfusionLicenseProvider.
                RegisterLicense("YOUR-LICENSE-KEY-GOES-HERE");

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
