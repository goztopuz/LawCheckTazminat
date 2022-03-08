using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak8GCView : ContentPage
    {
        public Basamak8GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();
            this.BindingContext = new Basamak8GCViewModel(_gece);

        }
    }
}
