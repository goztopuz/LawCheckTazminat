using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class MaasOranlaBrutViewFM : ContentPage
    {
        public MaasOranlaBrutViewFM(FazlaMesai _fm, List<AsgariUcretTablosu> aL)
        {
            InitializeComponent();

            this.BindingContext = new MaasOranlaBrutFMViewModel(_fm, aL);

        }
    }
}
