using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LottieAnimations
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ControlledAnimationPage();
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
