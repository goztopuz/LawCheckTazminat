using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.Destek;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Destek
{
    public partial class DestekListeView : ContentPage
    {
        public DestekListeView()
        {
            InitializeComponent();

            this.BindingContext = new DestekListeleViewModel();
        }
    }
}
