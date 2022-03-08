using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.YillikIzinV
{
    public partial class YillikIzinEkleDuzenleView : ContentPage
    {
        public YillikIzinEkleDuzenleView(YillikIzinIzinKayitlari _kayit ,YillikIzin _yy )
        {

            InitializeComponent();

            this.BindingContext = new ViewModels.YillikIzinB.YillikIzınEkleDuzenleViewModel(_kayit, _yy);

        }
    }
}
