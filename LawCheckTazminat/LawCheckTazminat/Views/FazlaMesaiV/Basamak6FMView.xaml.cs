using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak6FMView : ContentPage
    {
        public Basamak6FMView(FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new Basamak6FMViewModel(_fm);
        }

    async    void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            FazlaMesaiKisiIzinKayitlari yy = (FazlaMesaiKisiIzinKayitlari)mi.CommandParameter;


            string yazi = yy.BaslangicTar.ToShortDateString() + " - " + yy.BitisTar.ToShortDateString() + "  Arasındaki İzin Silinecektir";


            var durum = await DisplayAlert("Silme Uyarısı", yazi, "Evet", "Hayır");

            if (durum == true)
            {
                var durum2 = await DisplayAlert("Uyarı", "Silme İşlemini Onaylıyormusunuz?", "Sil", "İptal");

                if (durum2 == true)
                {

                    var vm = BindingContext as Basamak6FMViewModel;
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
