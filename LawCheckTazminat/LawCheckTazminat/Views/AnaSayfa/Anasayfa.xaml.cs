using LawCheckTazminat.ViewModels.AnaSayfa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.AnaSayfaV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Anasayfa : ContentPage
    {

        AnasayfaViewModel vm;
        public Anasayfa()
        {
            InitializeComponent();
            this.BindingContext = vm =new AnasayfaViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.CheckStatusCommand.Execute("-");
        }
    }
}