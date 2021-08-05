using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalSettings
{
    public partial class MainPage : ContentPage
    {

        private bool hasPassword;

        public MainPage()
        {
            InitializeComponent();
        }

        private bool CheckCellularConnection()
        {
            var profiles = Connectivity.ConnectionProfiles;
            return profiles.Count() == 1 && profiles.Contains(ConnectionProfile.Cellular);
        }

        private bool useCellularNetwork;
        protected override async void OnAppearing()
        {
            if(CheckCellularConnection())
            {
                if (!Preferences.ContainsKey("UseCellularNetwork"))
                {
                    bool result = await DisplayAlert("Warning",
                        "Do you agree on using cellular data when Wi-Fi is not available?",
                        "Yes", "No");
                    Preferences.Set("UseCellularNetwork", result);
                    useCellularNetwork = result;
                }
                else
                    useCellularNetwork = Preferences.Get("UseCellularNetwork", false);
            }


            // Assuming you have declared a bool field called hasPassword
            string password = await SecureStorage.GetAsync("Password");
            if (password != null)
                hasPassword = true;
        }

        private async void OkButton_Clicked(object sender, System.EventArgs e)
        {
            // Perform password validation here...

            await SecureStorage.SetAsync("Password", PasswordEntry.Text);
        }

        private async Task CheckAppFirstRun()
        {
            if(VersionTracking.IsFirstLaunchEver)
            {
                string password = await SecureStorage.GetAsync("password");
                if (password != null)
                    SecureStorage.Remove("password");
            }
        }
    }
}
