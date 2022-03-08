using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.KayipOranV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak1 : ContentPage
    {
        public Basamak1()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.KayipOranB.Basamak1ViewModel();
        }
    }
}