using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class YYElleView : ContentPage
    {
        public YYElleView(YillikIzin _yillik)
        {
            InitializeComponent();

            this.BindingContext = new YYElleViewModel(_yillik);
        }
    }
}
