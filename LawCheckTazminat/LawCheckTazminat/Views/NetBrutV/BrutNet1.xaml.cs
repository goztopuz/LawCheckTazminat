using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.NetBrutV
{

    
    public partial class BrutNet1 : ContentPage
    {
        int sayfaAcildi = 0;
        public BrutNet1()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.NetBrut.BrutNet1ViewModel();

        }

        void drpEvliBekar_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
       
            

            if(drpEvliBekar.Text=="Evli")
            {
                this.drpEsCalisma.IsVisible = true;
                lblEsCalisma.IsVisible = true;

            }
            else if(drpEvliBekar.Text=="Bekar")
            {
                this.drpEsCalisma.IsVisible = false;
                lblEsCalisma.IsVisible = false;

            }

        }


    }
}
