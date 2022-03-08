using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class MaasOranlaBrutViewIsGucu : ContentPage
    {
        public MaasOranlaBrutViewIsGucu(IsgucuKayip _kayip, List<AsgariUcretTablosu> aL)
        {
            InitializeComponent();

            this.BindingContext = new MaasOranlaBrutViewIsGucuModel(_kayip, aL);
            
        }
    }
}
