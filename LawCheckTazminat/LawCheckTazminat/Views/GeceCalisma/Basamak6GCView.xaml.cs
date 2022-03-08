using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak6GCView : ContentPage
    {
        public Basamak6GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.GeceCalismaB.Basamak6GCViewModel(_gece);
            
        }

     async   void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            GeceMesaiKisiIzinKayitlari yy = (GeceMesaiKisiIzinKayitlari)mi.CommandParameter;

            string yazi = yy.BaslangicTar.ToShortDateString() + " - " + yy.BitisTar.ToShortDateString() + "  Arasındaki İzin Silinecektir";

            var durum = await DisplayAlert("Silme Uyarısı", yazi, "Evet", "Hayır");

            if (durum == true)
            {
                var durum2 = await DisplayAlert("Uyarı", "Silme İşlemini Onaylıyormusunuz?", "Sil", "İptal");

                if (durum2 == true)
                {


                    var vm = BindingContext as Basamak6GCViewModel;
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
    }
}
