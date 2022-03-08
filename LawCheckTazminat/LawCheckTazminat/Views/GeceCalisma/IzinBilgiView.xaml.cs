using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class IzinBilgiView : ContentPage
    {
        public IzinBilgiView(GeceMesaiKisiIzinKayitlari mm, Models.GeceCalisma _gece)
        {
            InitializeComponent();
            this.BindingContext = new IzinBilgiViewModel(mm, _gece);
        }
    }
}
