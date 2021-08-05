using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiometricAuthentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void SavePasswordButton_Clicked(object sender, EventArgs e)
        {
            // Add more validation logic here...
            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await SecureStorage.SetAsync("P", PasswordEntry.Text);
                await DisplayAlert("Success", "Password saved", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}