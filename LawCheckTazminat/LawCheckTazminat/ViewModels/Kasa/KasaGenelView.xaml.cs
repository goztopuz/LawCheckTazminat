using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Kasa
{
    public partial class KasaGenelView : ContentPage
    {
        public KasaGenelView()
        {
            InitializeComponent();
        }

        void clickToShowPopup_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        void btnAra_Clicked(System.Object sender, System.EventArgs e)
        {
            popupLayout.PopupView.IsFullScreen = true;
            popupLayout.IsOpen=true;

        }

        void btnIptal_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
