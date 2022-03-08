using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak5GCView : ContentPage
    {
        public Basamak5GCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.GeceCalismaB.Basamak5GCViewModel(_gece);
        }
    }
}
