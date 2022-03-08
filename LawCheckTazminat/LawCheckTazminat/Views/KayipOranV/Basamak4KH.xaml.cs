using LawCheckTazminat.Models;
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
    public partial class Basamak4KH : ContentPage
    {
        public Basamak4KH(KayipOranHesap _kh,  AltKategori2 _altKat2)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.KayipOranB.Basamak4KHViewModel(_kh, _altKat2);

        }
    }
}