using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class KisiKayitlarGCView : ContentPage
    {
        public KisiKayitlarGCView(string _kisiId)
        {
            InitializeComponent();
            this.BindingContext = new KisiKayitlarGCViewModel(_kisiId);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (App.EskiKontrol == true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlarGCViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");
            }


        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            Models.GeceCalisma yy = (Models.GeceCalisma)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Kayıt Silinecektir.", "İptal", "Sil", "");
            if (cevap1 == "Sil")
            {
                //---------

                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if (cevap2 == true)
                {
                    var vm = BindingContext as KisiKayitlarGCViewModel;
                    vm.GCSilCommand.Execute(yy.Id);
                }
            }



        }
    }
}
