using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnalyzingActions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("Second page opened");
        }
        protected override void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                Analytics.TrackEvent("Second page closed");
                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}