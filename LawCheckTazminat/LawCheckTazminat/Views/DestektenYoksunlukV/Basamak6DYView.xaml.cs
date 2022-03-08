using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak6DYView : ContentPage
    {
        public Basamak6DYView(DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext = new Basamak6DYViewModel(dyy);
        }

        private void menuItemCikar_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Masraf yy = (Masraf)mi.CommandParameter;

            var vm = BindingContext as Basamak6DYViewModel;
            vm.MasrafSilCommand.Execute(yy);

        }
    }
}