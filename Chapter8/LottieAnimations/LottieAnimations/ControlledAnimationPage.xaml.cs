using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LottieAnimations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlledAnimationPage : ContentPage
    {
        public ControlledAnimationPage()
        {
            InitializeComponent();
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            animationView.PlayAnimation();
        }

        private void PauseButton_Clicked(object sender, EventArgs e)
        {
            animationView.PauseAnimation();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            animationView.StopAnimation();
        }
    }
}