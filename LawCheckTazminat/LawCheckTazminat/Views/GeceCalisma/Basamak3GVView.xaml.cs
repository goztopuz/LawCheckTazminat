using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak3GVView : ContentPage
    {
        public Basamak3GVView(Models.GeceCalisma _geceCalisma)
        {
            InitializeComponent();

            this.BindingContext = new Basamak3GCViewModel(_geceCalisma);
        }

        private void CheckKontrolDus(bool durum)
        {
            var vm = BindingContext as Basamak3GCViewModel;
            vm.KontrolDusCommand.Execute(durum);

        }

        private void CheckKontrolEkle(bool durum)
        {
            var vm = BindingContext as Basamak3GCViewModel;
            vm.KontrolEkleCommand.Execute(durum);

        }
        private void chkCakismaDus_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //CheckKontrolDus(e.Value);
        }

        private void chkCakismaEkle_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //CheckKontrolEkle(e.Value);
        }
    }
}
