using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WorkingWithDocuments
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ShareButton_Clicked(object sender, EventArgs e)
        {
            await ShareAsync();
        }

        private async Task ShareAsync()
        {
            var fileStream = 
                typeof(App).GetTypeInfo().Assembly.
                GetManifestResourceStream("WorkingWithDocuments.SampleDoc.pdf");
            
            var cacheFile = 
                Path.Combine(FileSystem.CacheDirectory, 
                "SampleDoc.pdf");

            using (var file = new FileStream(cacheFile, 
                FileMode.Create, FileAccess.Write))
            {
                fileStream.CopyTo(file);
            }
            var request = new ShareFileRequest();
            request.Title = "Share document";
            request.File = new ShareFile(cacheFile);

            await Share.RequestAsync(request);            
        }

        private void OpenButton_Clicked(object sender, EventArgs e)
        {
            var fileStream = typeof(App).GetTypeInfo().
                Assembly.GetManifestResourceStream("WorkingWithDocuments.SampleDoc.pdf");

            PdfViewerControl.LoadDocument(fileStream);
        }
    }
}
