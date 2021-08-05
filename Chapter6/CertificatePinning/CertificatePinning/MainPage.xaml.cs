using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CertificatePinning
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async Task DoCallsAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.mycompanywebsite.com",
                                 UriKind.Absolute);

            try
            {
                // The following call causes ValidateServerCertificate to be executed   
                // before accessing the resource  
                var response = await client.GetAsync("/mydata");
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is WebException e && e.Status ==
                    WebExceptionStatus.TrustFailure)
                {
                    // The server certificate validation failed: potential attack  
                }
                else
                {
                    // Other network issues  
                }
            }
            catch (Exception)
            {
                // Other exceptions  
            }

        }
    }
}
