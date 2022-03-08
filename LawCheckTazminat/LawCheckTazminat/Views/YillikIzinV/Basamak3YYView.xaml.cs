using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class Basamak3YYView : ContentPage
    {
        public Basamak3YYView(YillikIzin _yy)
        {
            InitializeComponent();
            this.BindingContext = new Basamak3YYViewModel(_yy);
        }
    }
}
