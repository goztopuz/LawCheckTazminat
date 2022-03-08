using LawCheckTazminat.ViewModels.Ayarlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.Ayarlar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HakkimizdaView : ContentPage
    {
        public HakkimizdaView()
        {

            InitializeComponent();
            this.BindingContext = new HakkimizdaViewModel();

        }


        public async Task OpenBrowser()
        {
            try
            {
                Uri uri = new Uri("https://www.bulutservisleri.com/tazminapp.html");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

      async private void Button_Clicked(object sender, EventArgs e)
        {
         await   OpenBrowser();
        }

       async private void ImageButton_Clicked(object sender, EventArgs e)
        {

            await OpenBrowser();

        }
    }
}