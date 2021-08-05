using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CertificatePinning
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static void SetupCertificatePinningCheck()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback =
                                ValidateServerCertificate;
        }

        private static bool ValidateServerCertificate(object sender,
                        X509Certificate certificate,
                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
#if DEBUG
            return true;
#endif
            return EndpointConfiguration.PUBKEY.Replace(" ", null)
                   .ToUpper() == certificate?.GetPublicKeyString();
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
