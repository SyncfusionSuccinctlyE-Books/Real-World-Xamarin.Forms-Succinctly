using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiometricAuthentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void PasswordLoginButton_Clicked(object sender, EventArgs e)
        {
            bool isPasswordSet = await IsPasswordSetAsync();
            if (!isPasswordSet)
            {
                await DisplayAlert("Error", "Password not set, register first", "OK");
                await Navigation.PopAsync();
                return;
            }

            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                bool localValidation = 
                    await IsLocalPasswordValidationPassing(PasswordEntry.Text);
                if (localValidation)
                {
                    await DisplayAlert("Success", "Authenticated!", "OK");
                    // Do login here...
                }
            }
        }

        private async Task<bool> IsLocalPasswordValidationPassing(string password)
        {
            string localPassword = await SecureStorage.GetAsync("P");
            return localPassword == password;
        }

        private async void BiometricLoginButton_Clicked(object sender, EventArgs e)
        {
            bool isPasswordSet = await IsPasswordSetAsync();
            if (!isPasswordSet)
            {
                await DisplayAlert("Error", "Password not set, register first", 
                    "OK");
                await Navigation.PopAsync();
                return;
            }

            bool biometricAuthAvailability = await CheckIfBiometricAuthIsAvailableAsync();
            if (!biometricAuthAvailability)
            {
                await DisplayAlert("Error", "Biometric authentication is not available.", 
                    "OK");
                return;
            }

            await BiometricAuthenticationAsync();
        }

        private async Task BiometricAuthenticationAsync()
        {
            var authRequest = new AuthenticationRequestConfiguration("Biometric authentication",
                "Login with fingerprint or face ID");


            FingerprintAuthenticationResult result =
                await CrossFingerprint.Current.AuthenticateAsync(authRequest);
            if (result.Authenticated)
            {
                await DisplayAlert("Success", "Authenticated!", "OK");
                // Do login here...
            } 
            else
                await DisplayAlert("Error", $"Reason: {result.ErrorMessage}", "OK");
        }

        private async Task<bool> IsPasswordSetAsync()
        {
           
            string result = await SecureStorage.GetAsync("P");
            return result != null;
        }

        private async Task<bool> CheckIfBiometricAuthIsAvailableAsync()
        {
            FingerprintAvailability isBiometricAuthAvailable =
                await CrossFingerprint.Current.GetAvailabilityAsync();
            return isBiometricAuthAvailable == FingerprintAvailability.Available;
        }
    }
}