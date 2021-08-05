using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SafeArea
{
    public static class Extensions
    {
        public static void SetIPhoneSafeArea(this ContentPage page)
        {
            string phoneModel = DeviceInfo.Model;

            //DeviceInfo.Model => Real device Model  
            //iPhone7,1  => iPhone 6+  
            //iPhone7,2  => iPhone 6  
            //iPhone8,1  => iPhone 6S  
            //iPhone8,2  => iPhone 6S+  
            //iPhone8,4  => iPhone SE  
            //iPhone9,1  => iPhone 7  
            //iPhone9,2  => iPhone 7+  
            //iPhone9,3  => iPhone 7  
            //iPhone9,4  => iPhone 7+  
            //iPhone10,1 => iPhone 8  
            //iPhone10,2 => iPhone 8+  
            //iPhone10,3 => iPhone X  
            //iPhone11,2 => iPhone XS  
            //iPhone11,4 => iPhone XS Max  
            //iPhone11,8 => iPhone XR  
            //iPhone12,1 => iPhone 11
            //iPhone12,3 => iPhone 11 Pro
            //iPhone12,5 => iPhone 11 Pro Max
            //iPhone12,8 => iPhone SE (2nd generation)

            if (phoneModel.ToLower().Contains("iphone11") ||
                phoneModel.ToLower() == "iphone10,3" ||
                phoneModel.ToLower().Contains("iphone12"))
                page.On<iOS>().SetUseSafeArea(true);
        }
    }
}
