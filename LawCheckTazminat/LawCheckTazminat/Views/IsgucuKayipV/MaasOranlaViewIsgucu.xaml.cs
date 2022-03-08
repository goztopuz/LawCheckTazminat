using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class MaasOranlaViewIsgucu : ContentPage
    {
        public MaasOranlaViewIsgucu(IsgucuKayip _kayip, List<AsgariUcretTablosu> aL)
        { 
            InitializeComponent();
            this.BindingContext = new MaasOranlaViewIsgucuModel(_kayip, aL);

        }
    }
}
