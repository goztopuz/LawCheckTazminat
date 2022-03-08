using LawCheckTazminat.ViewModels.KidemIhbarB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.KidemIhbarV
{
    public partial class KisiKayitlariKIView : ContentPage
    {
        public KisiKayitlariKIView(string _kisiId)
        {
            InitializeComponent();
            this.BindingContext = new KisiKayitlariKIViewModel(_kisiId);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (App.EskiKontrol == true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlariKIViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");
            }


        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            Models.KidemIhbar yy = (Models.KidemIhbar)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Kayıt Silinecektir.", "İptal", "Sil", "");
            if (cevap1 == "Sil")
            {
                //---------

                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if (cevap2 == true)
                {
                    var vm = BindingContext as KisiKayitlariKIViewModel;
                    vm.KidemIhbarSilCommand.Execute(yy.Id);
                }
            }


        }
    }
}
