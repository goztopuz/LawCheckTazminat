using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wiz1DYView : ContentPage
    {
        public Wiz1DYView(string kisiId)
        {
            InitializeComponent();
            this.BindingContext = new Wiz1DYViewModel(kisiId);

        }
    }
}