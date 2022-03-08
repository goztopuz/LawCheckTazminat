using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class KisiKayitlariFMView : ContentPage
    {
        public KisiKayitlariFMView(string kisiId)
        {
            InitializeComponent();
            this.BindingContext = new KisiKayitlariFMViewModel(kisiId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if(App.EskiKontrol==true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlariFMViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");
            }
       
        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            FazlaMesai yy = (FazlaMesai)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Kayıt Silinecektir.", "İptal", "Sil", "");
            if (cevap1 == "Sil")
            {
                //---------

                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if (cevap2 == true)
                {
                    var vm = BindingContext as KisiKayitlariFMViewModel;
                    vm.FMSilCommand.Execute(yy.Id);
                }
            }

        }
    }
}
