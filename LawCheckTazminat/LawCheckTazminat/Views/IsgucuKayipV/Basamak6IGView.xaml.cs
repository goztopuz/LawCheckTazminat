using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class Basamak6IGView : ContentPage
    {
        public Basamak6IGView(IsgucuKayip _kayip)
        {
            InitializeComponent();
            this.BindingContext = new Basamak6IGViewModel(_kayip);

            
        }


        private void menuItemCikar_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            YakinIsgucu yy = (YakinIsgucu)mi.CommandParameter;

           var vm = BindingContext as Basamak6IGViewModel;
            vm.YakinSilCommand.Execute(yy);

        }

    }
}
