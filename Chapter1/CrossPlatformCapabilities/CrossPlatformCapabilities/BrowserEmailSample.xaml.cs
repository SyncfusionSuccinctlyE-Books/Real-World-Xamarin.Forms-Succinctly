using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformCapabilities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserEmailSample : ContentPage
    {
        public BrowserEmailSample()
        {
            InitializeComponent();
        }


        private async void BrowserButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://www.microsoft.com", 
                BrowserLaunchMode.SystemPreferred);
        }

        private async void EmailButton_Clicked(object sender, EventArgs e)
        {          
            EmailMessage message = new EmailMessage();
            message.Subject = "Support request";
            message.To = new List<string> { "support@onecompany.com" };
            message.Cc = new List<string> { "myboss@mycompany.com" };
            message.BodyFormat = EmailBodyFormat.PlainText;
            message.Body = "We have problems with the Internet connection";

            await Email.ComposeAsync(message);
        }
    }
}