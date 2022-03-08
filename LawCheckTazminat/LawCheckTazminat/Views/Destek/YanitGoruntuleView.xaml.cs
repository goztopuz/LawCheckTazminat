using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Destek;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.Destek
{
    public partial class YanitGoruntuleView : ContentPage
    {
        public YanitGoruntuleView(DestekYanit _yy)
        {
            InitializeComponent();

            this.BindingContext = new YanitGoruntuleViewModel(_yy);

        }
    }
}
