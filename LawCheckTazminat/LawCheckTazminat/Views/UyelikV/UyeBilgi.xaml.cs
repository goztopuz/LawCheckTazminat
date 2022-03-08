using LawCheckTazminat.ViewModels.UyelikB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.UyelikV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UyeBilgi : ContentPage
    {

        UyeOlViewModel vm;
        public UyeBilgi()
        {
            InitializeComponent();

            this.BindingContext =vm= new UyeOlViewModel();

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

       //     vm.GetPriceCommand.Execute("-");
        }
    }
}