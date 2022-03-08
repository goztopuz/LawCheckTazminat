using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KisiKayitlariIGView : ContentPage
    {
        KisiKayitlariIGViewModel vm;
        public KisiKayitlariIGView(string kisiId)
        {
            InitializeComponent();
            vm = new KisiKayitlariIGViewModel(kisiId);
            this.BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if(App.EskiKontrol==true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlariIGViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");         
            }
   
        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
           IsgucuKayip  yy = (IsgucuKayip)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.
                DisplayActionSheet("Kayıt Silinecektir.", "İptal", "Sil", "");
            if (cevap1 == "Sil")
            {
                //--------------------------------
                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if (cevap2 == true)
                {
                    var vm = BindingContext as KisiKayitlariIGViewModel;
                    vm.IsgucuSilCommand.Execute(yy.Id);
                }
            }


        }
    }
}