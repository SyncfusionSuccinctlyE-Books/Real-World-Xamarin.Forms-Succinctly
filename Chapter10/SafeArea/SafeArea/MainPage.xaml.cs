using Xamarin.Forms;

namespace SafeArea
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.SetIPhoneSafeArea();
        }
    }
}
