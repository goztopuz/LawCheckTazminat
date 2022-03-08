using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class Basamak7IGView : ContentPage
    {
        public Basamak7IGView(Models.IsgucuKayip _kayip)
        {
            InitializeComponent();
            this.BindingContext = new Basamak7IGViewModel(_kayip);
        }

        void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            MasrafIsgucu yy = (MasrafIsgucu)mi.CommandParameter;

            var vm = BindingContext as Basamak7IGViewModel;
            vm.MasrafSilCommand.Execute(yy);
        }
    }
}
