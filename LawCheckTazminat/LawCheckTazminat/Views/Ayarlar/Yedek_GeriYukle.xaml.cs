using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class Yedek_GeriYukle : ContentPage
    {
        public Yedek_GeriYukle()
        {
            InitializeComponent();
            this.BindingContext = new YedekGeriYukleViewModel();
        }

       async void btnGeriYukle_Clicked(System.Object sender, System.EventArgs e)
        {



            PickOptions aa = new PickOptions();

            aa.PickerTitle="Yedeği Seçin";


            string yazi = "Geri Yükleme İşlemini Onaylıyormusunuz?";
            var durum = await DisplayAlert(" Uyarısı", yazi, "Evet", "Hayır");


            if (durum == true)
            {
                var durum2 = await DisplayAlert("Uyarı", "SON UYARI! Geri Yükleme İşlemini Onaylıyormusunuz?", "Onayla", "İptal");

                if (durum2 == true)
                {

                    await PickAndShow(aa);

                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }







        }

        async Task PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var veriler =await result.OpenReadAsync();

                    DependencyService.Get<IGeriYukle>().GeriYukle2(veriler);

                 }
                
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}
