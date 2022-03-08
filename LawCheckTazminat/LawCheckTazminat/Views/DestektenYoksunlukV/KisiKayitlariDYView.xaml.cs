using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KisiKayitlariDYView : ContentPage
    {
        public KisiKayitlariDYView(string kisiId)
        {
            InitializeComponent();
            BindingContext = new KisiKayitlariDYViewModel(kisiId);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            if(App.EskiKontrol== true)
            {
                App.EskiKontrol = false;
                var vm2 = BindingContext as KisiKayitlariDYViewModel;
                vm2.EskiKayitKontrolCommand.Execute("m");
            }

       
        }

        private async void menuItemCikar_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DestektenYoksunluk yy = (DestektenYoksunluk)mi.CommandParameter;

            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Kayıt Silinecektir.", "İptal","Sil", "");
            if (cevap1 == "Sil")
            {
                //---------

                var cevap2 = await Application.Current.MainPage.
                    DisplayAlert("Silme", "Silme İşlemini Onaylıyormusunuz?", "Evet", "Hayır");

                if(cevap2==true)
                {
                    var vm = BindingContext as KisiKayitlariDYViewModel;
                    vm.DestektenYoksunlukSilCommand.Execute(yy.Id);
                }
            }      
        
        }

    }
}