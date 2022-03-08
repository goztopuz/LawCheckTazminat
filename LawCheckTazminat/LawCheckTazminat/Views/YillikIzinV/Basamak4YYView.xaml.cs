using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class Basamak4YYView : ContentPage
    {
        public Basamak4YYView(YillikIzin _yillik)
        {
            InitializeComponent();

            this.BindingContext = new Basamak4YYiewModel(_yillik);
        }
    }
}
