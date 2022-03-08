using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class Basamak5YYView : ContentPage
    {
        public Basamak5YYView(YillikIzin _yillik)
        {
            InitializeComponent();

            this.BindingContext = new Basamak5YYViewModel(_yillik);
        }
    }
}
