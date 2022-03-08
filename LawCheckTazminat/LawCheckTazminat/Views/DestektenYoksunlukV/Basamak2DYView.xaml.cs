using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak2DYView : ContentPage
    {
        public Basamak2DYView(DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext = new Basamak2DYViewModel (dyy);

        }

        private void txtEmeklilikYas_Focused(object sender, FocusEventArgs e)
        {

        }

        private void txtCalisma_Focused(object sender, FocusEventArgs e)
        {
            drpCalisma.IsVisible = true;
            drpCalisma.Focus();
            drpCalisma.IsVisible = false;
        }

        private void drpCalisma_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCalisma.Text = drpCalisma.SelectedItem.ToString();

        }

        private void txtAskerlik_Focused(object sender, FocusEventArgs e)
        {
            drpAskerlik.IsVisible = true;
            drpAskerlik.Focus();
            drpAskerlik.IsVisible = false;

        }

        private void drpAskerlik_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAskerlik.Text = drpAskerlik.SelectedItem.ToString();

            if(drpAskerlik.SelectedItem.ToString()== "Yaptı")
            {
                stckAskerlik.IsVisible = false;
            }
            else
            {
                stckAskerlik.IsVisible = true;

            }
        }

        private void txtAskereGidisAy_Focused(object sender, FocusEventArgs e)
        {
            drpAskerlikGidisAy.IsVisible = true;
            drpAskerlikGidisAy.Focus();
            drpAskerlikGidisAy.IsVisible = false;
        }

        private void drpAskerlikGidisAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAskereGidisAy.Text = drpAskerlikGidisAy.SelectedItem.ToString();
        }
    }
}