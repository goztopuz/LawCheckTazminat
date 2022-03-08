using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class DestekSonIsgucu : ContentPage
    {
        public DestekSonIsgucu(IsgucuKayip _kayip)
        {
            InitializeComponent();

            this.BindingContext = new DestekSonIsgucuViewModel(_kayip);

        }
    }
}
