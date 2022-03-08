using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak5IGView : ContentPage
    {
        public Basamak5IGView(IsgucuKayip _kayip)
        {
            InitializeComponent();
            this.BindingContext = new Basamak5IGViewModel(_kayip);

        }

        private void menuItemCikar_Clicked(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            KayipOran yy = (KayipOran)mi.CommandParameter;

            var vm = BindingContext as Basamak5IGViewModel;
            vm.SilKayipCommand.Execute(yy);


        }
    }
}