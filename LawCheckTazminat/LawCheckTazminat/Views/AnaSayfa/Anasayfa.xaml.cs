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
        Helpers.AbonelikYontemi ab;
        public Anasayfa()
        {
            InitializeComponent();
            this.BindingContext = vm =new AnasayfaViewModel();
            ab = new Helpers.AbonelikYontemi();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.AppStatus = "PRO";

       //     ab.RestoreCode();
            //    ab.AbonelikKontrol();
            //  vm.CheckStatusCommand.Execute("-");
           vm.UcretsizYaziCheck.Execute("");
        }
    }
}