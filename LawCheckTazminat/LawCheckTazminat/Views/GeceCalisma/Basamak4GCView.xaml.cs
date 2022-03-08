using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak4GCView : ContentPage
    {
        public Basamak4GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();

            this.BindingContext = new Basama4GCViewModel(_gece);
        }
    }
}
