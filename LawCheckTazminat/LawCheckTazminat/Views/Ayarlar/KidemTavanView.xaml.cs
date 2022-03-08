using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class KidemTavanView : ContentPage
    {
        public KidemTavanView()
        {
            InitializeComponent();
            this.BindingContext = new KidemTavanViewModel();
        }

      async  void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {

            var mi = ((MenuItem)sender);
            KidemTavan yy = (KidemTavan)mi.CommandParameter;
            string yazi = "";

            if (yy != null)
            {
                yazi = yy.baslangic.ToShortDateString() + " - " + yy.bitis.ToShortDateString() + " Arasındaki  Silinecektir";
            }

            var durum = await DisplayAlert("Silme Uyarısı", yazi, "Evet", "Hayır");

            if (durum == true)
            {
                var durum2 = await DisplayAlert("Uyarı", "Silme İşlemini Onaylıyormusunuz?", "Sil", "İptal");

                if (durum2 == true)
                {

                    var vm = BindingContext as KidemTavanViewModel;
                    vm.SilCommand.Execute(yy);

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
