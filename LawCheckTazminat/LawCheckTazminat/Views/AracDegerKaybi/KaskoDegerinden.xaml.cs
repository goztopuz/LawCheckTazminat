using LawCheckTazminat.ViewModels.AracDegerKaybi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.AracDegerKaybi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KaskoDegerinden : ContentPage
    {
        public KaskoDegerinden()
        {
            InitializeComponent();

            this.BindingContext = new KaskoDegerindenViewModel();

        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as KaskoDegerindenViewModel ;
            string ss = "";
            vm.MarkaSecimCommand.Execute(ss);
        }

        private void drpModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as KaskoDegerindenViewModel;
            var ss = "";
            vm.ModelSecimCommand.Execute(ss);

        }
    }
}