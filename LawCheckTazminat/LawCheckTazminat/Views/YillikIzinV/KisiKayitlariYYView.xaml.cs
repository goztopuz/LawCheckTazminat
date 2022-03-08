using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class KisiKayitlariYYView : ContentPage
    {
        public KisiKayitlariYYView(string _kisiId)
        {
            InitializeComponent();
            this.BindingContext = new KisiKayitlariYYViewModel(_kisiId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (App.EskiKontrol == true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlariYYViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");
            }


        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Models.YillikIzin yy = (Models.YillikIzin)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Kayıt Silinecektir.", "İptal", "Sil", "");
            if (cevap1 == "Sil")
            {
                //---------

                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if (cevap2 == true)
                {
                    var vm = BindingContext as KisiKayitlariYYViewModel;
                    vm.YYSilCommand.Execute(yy.Id);
                }
            }
        }
    }
}
