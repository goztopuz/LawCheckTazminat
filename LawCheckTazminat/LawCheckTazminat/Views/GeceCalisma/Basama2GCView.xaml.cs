using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basama2GCView : ContentPage
    {
        public Basama2GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.GeceCalismaB.Basamak2GCViewModel(_gece);
        }
    }
}
