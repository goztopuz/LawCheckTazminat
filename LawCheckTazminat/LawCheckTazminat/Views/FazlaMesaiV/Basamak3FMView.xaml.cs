using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak3FMView : ContentPage
    {
        public Basamak3FMView(FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new Basamak3FMViewModel(_fm);
        }

        void chkHSonuFMCakisma_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Perform required operation after examining e.Value

            var vm = BindingContext as Basamak3FMViewModel;
           // vm.MasrafSilCommand.Execute(yy);
        }
    }
}
