using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.KayipOranB;
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
    public partial class Basamak3KH : ContentPage
    {
        public Basamak3KH(KayipOranHesap _kh, AltKategori1 altKategori1)
        {
            InitializeComponent();
            this.BindingContext = new Basamak3KHViewModel(_kh, altKategori1);
        }
    }
}