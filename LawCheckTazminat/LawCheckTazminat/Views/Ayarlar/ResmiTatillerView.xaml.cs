using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class ResmiTatillerView : ContentPage
    {
        public ResmiTatillerView()
        {
            InitializeComponent();

            this.BindingContext = new ResmiTatillerViewModel();

        }

        void drpYil_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var kontrol = (Picker)sender;
            int yil =(int) kontrol.SelectedItem;
            var vm = BindingContext  as ResmiTatillerViewModel;
            vm.ListeleCommand.Execute(yil);   
        }

        void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {

            var mi = ((MenuItem)sender);
            ResmiTatiller tt = (ResmiTatiller) mi.CommandParameter;
            
            var vm = BindingContext as ResmiTatillerViewModel;

            vm.SilKayitCommand.Execute(tt);


        }



    }
}
