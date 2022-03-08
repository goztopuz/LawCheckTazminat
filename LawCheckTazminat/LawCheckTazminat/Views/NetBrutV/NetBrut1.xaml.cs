using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.NetBrutV
{
    public partial class NetBrut1 : ContentPage
    {
        public NetBrut1()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.NetBrut.NetBrut1ViewModel();

        }

        void drpEvliBekar_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (drpEvliBekar.Text == "Evli")
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
