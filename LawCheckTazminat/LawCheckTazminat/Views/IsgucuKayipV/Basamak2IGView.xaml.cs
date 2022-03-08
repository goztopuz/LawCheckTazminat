using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak2IGView : ContentPage
    {
        public Basamak2IGView(IsgucuKayip _isgucu)
        {
            InitializeComponent();
            this.BindingContext = new Basamak2IGViewModel(_isgucu);
        }

        private void drpCalisma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpAskerlik_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpAskerlik.SelectedItem = drpAskerlik.SelectedItem.ToString();

            if (drpAskerlik.SelectedItem.ToString() == "Yaptı")
            {
                stckAskerlik.IsVisible = false;
            }
            else
            {
                stckAskerlik.IsVisible = true;

            }

        }

        private void drpAskerlikGidisAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpAskerlikGidisAy_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}