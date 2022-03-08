using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class BasamakSonGCView : ContentPage
    {
        public BasamakSonGCView(Models.GeceCalisma _gece)
        {

            InitializeComponent();
            this.BindingContext = new BasamakSonGCViewModel(_gece);

        }
    }
}
