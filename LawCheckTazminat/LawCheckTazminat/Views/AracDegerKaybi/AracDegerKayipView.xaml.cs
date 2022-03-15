using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.AracDegerKaybi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AracDegerKayipView : ContentPage
    {
        public AracDegerKayipView()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.AracDegerKaybi.AracDegerKaybiViewModel();
        }

        public async Task OpenBrowser()
        {
            try
            {
                Uri uri = new Uri("https://www.tsb.org.tr/tr/kasko-deger-listesi");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        async void  btnKaskoDeger_Clicked(System.Object sender, System.EventArgs e)
        {
            await OpenBrowser();
        }
    }
}