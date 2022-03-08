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
    public partial class Basamak5KH : ContentPage
    {
        public Basamak5KH(KayipOranHesap _kh)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.KayipOranB.Basamak5KHViewModel(_kh);

        }
    }
}