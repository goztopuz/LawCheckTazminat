using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class MaasOranlaGCBrutView : ContentPage
    {
        public MaasOranlaGCBrutView(Models.GeceCalisma _gc, List<AsgariUcretTablosu> aL)
        {
            InitializeComponent();

            this.BindingContext = new MaasOranlaGCBrutViewModel(_gc, aL);

        }
    }
}
