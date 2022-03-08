using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
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
    public partial class Basamak4BekarCocukDY : ContentPage
    {
        public Basamak4BekarCocukDY(DestektenYoksunluk dy)
        {
            InitializeComponent();

            this.BindingContext = new Basamak4BekarCocukDYViewModel(dy);

        }
    }
}