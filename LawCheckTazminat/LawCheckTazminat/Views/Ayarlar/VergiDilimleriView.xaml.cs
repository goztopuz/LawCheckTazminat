using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class VergiDilimleriView : ContentPage
    {
        public VergiDilimleriView()
        {
            InitializeComponent();

            this.BindingContext = new VerhgiDilimleriViewModel();
        }
    }
}
