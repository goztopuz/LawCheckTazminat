using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class MaasOranlaFMView : ContentPage
    {
        List<AsgariUcretTablosu> aL2 = new List<AsgariUcretTablosu>();


        public MaasOranlaFMView(FazlaMesai _fm, List<AsgariUcretTablosu> aL)
        {
            InitializeComponent();
            this.BindingContext = new MaasOranlaFMViewModel(_fm, aL);

        }

        private void drpEvliBekar_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
    }
}
