using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class BasamakYYSonView : ContentPage
    {
        public BasamakYYSonView(YillikIzin _yillik)
        {
            InitializeComponent();

            this.BindingContext = new BasamakYYSonViewModel(_yillik);
        }
    }
}
