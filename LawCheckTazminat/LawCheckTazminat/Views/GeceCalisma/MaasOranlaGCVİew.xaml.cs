using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class MaasOranlaGCVİew : ContentPage
    {
        public MaasOranlaGCVİew(Models.GeceCalisma _gc, List<AsgariUcretTablosu> aL)
        {
            InitializeComponent();
            this.BindingContext = new MaasOranlaGCViewModel(_gc, aL);
        }
    }
}
