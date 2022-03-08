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
    public partial class Basamak6KH : ContentPage
    {
        public Basamak6KH(KayipOranHesap kH)
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.KayipOranB.Basamak6KHViewModel(kH);
        }
    }
}