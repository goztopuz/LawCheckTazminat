using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class Basamak8IGView : ContentPage
    {
        public Basamak8IGView(IsgucuKayip _kayip)
        {
            InitializeComponent();
            this.BindingContext = new Basamak8IGViewModel(_kayip);

        }

        
    }
}
