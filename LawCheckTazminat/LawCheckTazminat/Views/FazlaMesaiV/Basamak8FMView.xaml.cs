using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak8FMView : ContentPage
    {
        public Basamak8FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak8FMViewModel(_fm);

        }

        void chkMesaiVar_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {

            //var chk = (CheckBox)sender;

            //var vm = BindingContext as Basamak8FMViewModel;
            //vm.HaftalariToplaCommand.Execute(chk);

        }

        void lstHaftalar_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
