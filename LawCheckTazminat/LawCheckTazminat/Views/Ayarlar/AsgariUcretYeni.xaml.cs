using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.Ayarlar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsgariUcretYeni : ContentPage
    {
        public AsgariUcretYeni(ObservableCollection<AsgariUcretTablosu> aL, AsgariUcretTablosu aa)
        {
            InitializeComponent();

            this.BindingContext = new AsgariUcretYeniViewModel(aL, aa);

        }
 
        private void txtDonemi_Focused(object sender, FocusEventArgs e)
        {
            drpDonemi.IsVisible = true;
            drpDonemi.Focus();
            drpDonemi.IsVisible = false;
        }

        private void drpDonemi_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    txtDonemi.Text = drpDonemi.SelectedItem.ToString();
        }
    }
}