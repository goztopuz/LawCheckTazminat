using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Destek;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Destek
{
    public partial class SoruGoruntule : ContentPage
    {
        public SoruGoruntule(DestekTalep _dd)
        {
            InitializeComponent();
            this.BindingContext = new SoruGoruntuleViewModel(_dd);
        }
    }
}
