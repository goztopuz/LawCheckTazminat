using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class Basamak2YYView : ContentPage
    {
        public Basamak2YYView(YillikIzin _yy)
        {
            InitializeComponent();

            this.BindingContext = new Basamak2YYViewModel(_yy);

        }

        async void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {


            
            var mi = ((MenuItem)sender);
            YillikIzinIzinKayitlari yy = (YillikIzinIzinKayitlari)mi.CommandParameter;
            string yazi = "";

            if(yy != null)
            {
                yazi = yy.BaslangicTar.ToShortDateString() + " - " + yy.BitisTar.ToShortDateString() +" Arasındaki İzin Silinecektir";
            }

            var durum = await DisplayAlert("Silme Uyarısı",yazi,"Evet","Hayır");


            if(durum==true)
            {
                var durum2 = await DisplayAlert("Uyarı", "Silme İşlemini Onaylıyormusunuz?", "Sil", "İptal");

                if(durum2== true)
                {

                    var vm = BindingContext as Basamak2YYViewModel;
                    vm.IzinSilCommand.Execute(yy);

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

        void lstIzinler_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            lstIzinler.SelectedItem = null;

        }
    }
}
