using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak3IGView : ContentPage
    {
        public Basamak3IGView(Models.IsgucuKayip _isgucu)
        {

            InitializeComponent();
            this.BindingContext = new Basamak3IGViewModel(_isgucu);

        }

        private void drpRaporGun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpRaporAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpKusurat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpTrafikKazasi_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
        }
    }
}