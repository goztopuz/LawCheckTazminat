using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak4DYView : ContentPage
    {
        public Basamak4DYView(DestektenYoksunluk dyy)
        {

    
            InitializeComponent();

            this.BindingContext = new Basamak4DYViewModel(dyy);
        }

        private void menuItemCikar_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Yakin yy = (Yakin)mi.CommandParameter;

            var vm = BindingContext as Basamak4DYViewModel;
            vm.YakinSilCommand.Execute(yy);

        }

    }
}