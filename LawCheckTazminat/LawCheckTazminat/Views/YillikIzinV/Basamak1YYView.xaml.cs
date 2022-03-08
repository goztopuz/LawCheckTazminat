using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class Basamak1YYView : ContentPage
    {
        public Basamak1YYView(YillikIzin yy)
        {
            InitializeComponent();
            this.BindingContext = new Basamak1YYViewModel(yy);
        }
    }
}
