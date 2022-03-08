using LawCheckTazminat.ViewModels.YillikIzinB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.YillikIzinV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YillikIzinNetBrutView : ContentPage
    {
        public YillikIzinNetBrutView()
        {
            InitializeComponent();

            this.BindingContext = new YillikIzinNetBrutViewModel();

        }

        private void drpEvliBekar_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (drpEvliBekar.Text == "Evli")
            {
                this.drpEsCalisma.IsVisible = true;
                lblEsCalisma.IsVisible = true;

            }
            else
            {
                this.drpEsCalisma.IsVisible = false;
                lblEsCalisma.IsVisible = false;

            }

        }
    }
}