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
    public partial class DestekSon : ContentPage
    {
        public DestekSon(DestektenYoksunluk dY)
        {

            InitializeComponent();
            this.BindingContext = new DestekSonViewModel(dY);
      
        }
    }
}