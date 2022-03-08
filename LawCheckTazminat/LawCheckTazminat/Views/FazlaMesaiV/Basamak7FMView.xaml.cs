using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak7FMView : ContentPage
    {
        public Basamak7FMView(FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new Basamak7FMViewModel(_fm);

        }

        void chkMesaiVar_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            var chk = (CheckBox)sender;

            var vm = BindingContext as Basamak7FMViewModel;
            vm.YenidenToplaCommand.Execute(chk);

        }

        void menuItemCikar_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void lstHaftalar_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;

        }

        void chkMesaiVar_CheckedChanged_1(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }
    }
}
