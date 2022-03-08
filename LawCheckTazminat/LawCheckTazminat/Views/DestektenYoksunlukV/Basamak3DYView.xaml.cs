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
    public partial class Basamak3DYView : ContentPage
    {
        public Basamak3DYView(DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext = new Basamak3DYViewModel(dyy);

        }

        private void txtRaporGun_Focused(object sender, FocusEventArgs e)
        {
            //drpRaporGun.IsVisible = true;
            //drpRaporGun.Focus();
            //drpRaporGun.IsVisible = false;

        }

        private void txtRaporAy_Focused(object sender, FocusEventArgs e)
        {
            //drpRaporAy.IsVisible = true;
            //drpRaporAy.Focus();
            //drpRaporAy.IsVisible = false;

        }

        private void drpRaporAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtRaporAy.Text = drpRaporAy.SelectedItem.ToString();

        }

        private void drpRaporGun_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtRaporGun.Text = drpRaporGun.SelectedItem.ToString();
        }

        private void txtTrafikKazasi_Focused(object sender, FocusEventArgs e)
        {
            drpTrafikKazasi.IsVisible = true;
            drpTrafikKazasi.Focus();
            drpTrafikKazasi.IsVisible = false;
        }

        private void drpTrafikKazasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTrafikKazasi.Text = drpTrafikKazasi.SelectedItem.ToString();


            var vm = BindingContext as Basamak3DYViewModel;
            // vm.MasrafSilCommand.Execute(yy);

            //if(drpTrafikKazasi.SelectedItem.ToString()=="Evet")
            //{

            //    txtYasamTablosu.Text = "TRH-2010";
            //}
            //else
            //{
            //    txtYasamTablosu.Text = "PMF-1931";
            //}


        }

        private void drpKusurat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtYasYuvarla.Text = drpKusurat.SelectedItem.ToString();
        }

        private void txtYasYuvarla_Focused(object sender, FocusEventArgs e)
        {
            drpKusurat.IsVisible = true;
            drpKusurat.Focus();
            drpKusurat.IsVisible = false;

        }

        private void drpTrafikKazasi_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
