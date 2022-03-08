using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.Destek;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Destek
{
    public partial class YeniDestekView : ContentPage
    {
        public YeniDestekView()
        {
            InitializeComponent();

            this.BindingContext = new YeniDestekViewModel();
        }
    }
}
