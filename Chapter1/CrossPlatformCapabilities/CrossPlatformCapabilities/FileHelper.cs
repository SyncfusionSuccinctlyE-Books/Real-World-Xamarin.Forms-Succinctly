using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CrossPlatformCapabilities
{
    public static class FileHelper
    {

        public static string ReadData()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(
                       Environment.SpecialFolder.LocalApplicationData),
                       "appdata.txt");

                string text = File.ReadAllText(fileName);

                return text;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool WriteData(string data)
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(
                       Environment.SpecialFolder.LocalApplicationData),
                       "appdata.txt");

                File.WriteAllText(fileName, data);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async static Task<string> ReadDataAsync()
        {
            try
            {
                var mainDir = FileSystem.AppDataDirectory;
                string localData;

                using (var stream = await FileSystem.OpenAppPackageFileAsync("appdata.txt"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        localData = await reader.ReadToEndAsync();
                    }
                }
                return localData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
