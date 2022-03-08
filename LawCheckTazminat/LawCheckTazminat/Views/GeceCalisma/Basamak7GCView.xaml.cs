using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak7GCView : ContentPage
    {
        public Basamak7GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();
            this.BindingContext = new Basamak7GCViewModel(_gece);

        }

        void lstHaftalar_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
        }

        void chkMesaiVar_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }
    }
}
