using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformCapabilities
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilePickerPage : ContentPage
    {
        public FilePickerPage()
        {
            InitializeComponent();
        }

        private async void PickButton_Clicked(object sender, EventArgs e)
        {
            // Uncomment for custom file type support

            //var fileType = new FilePickerFileType(new Dictionary<DevicePlatform, 
            //    IEnumerable<string>>
            //{
            //    { DevicePlatform.iOS, new[] { "public.my.doc.extension" } },
            //    { DevicePlatform.Android, new[] { "application/doc" } },
            //    { DevicePlatform.UWP, new[] { ".doc", ".doc" } },
            //});

            var result = await PickAndShowAsync(
                new PickOptions
                {
                    PickerTitle = "Pick a file",
                });

            ImageNameLabel.Text = result.FileName;
            PickedImage.Source = result.Image;
        }

        private async Task<FileSelection> PickAndShowAsync(PickOptions options)
        {
            try
            {
                FileResult result = await FilePicker.PickAsync(options);
                FileSelection fileResult = new FileSelection();

                if (result != null)
                {
                    fileResult.FileName = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg",
                        StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        fileResult.Image =
                            ImageSource.FromStream(() => stream);
                    }
                }

                return fileResult;
            }
            catch (Exception ex)
            {
                return null;
                // The user canceled or something went wrong
            }
        }
    }

    public class FileSelection
    {
        public string FileName { get; set; }
        public ImageSource Image { get; set; }
    }
}