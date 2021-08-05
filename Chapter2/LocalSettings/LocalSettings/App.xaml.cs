using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace LocalSettings
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        internal static DateTime timeOfLastUsage;
        protected override void OnStart()
        {
            timeOfLastUsage = Preferences.Get("TimeOfLastUsage", 
                DateTime.Now);
        }

        protected override void OnSleep()
        {
            Preferences.Set("TimeOfLastUsage", DateTime.Now);
        }

        protected override void OnResume()
        {

        }
    }
}
