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
    public partial class MasrafEkle : ContentPage
    {
        public MasrafEkle(DestektenYoksunluk dyy, Masraf mm)
        {
            InitializeComponent();

            this.BindingContext = new MasrafEkleViewModel(dyy,mm);
        }
    }
}