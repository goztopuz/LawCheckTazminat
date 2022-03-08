using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdresDefteri : ContentPage
    {
        public AdresDefteri(Contact _kk)
        {
            InitializeComponent();

            this.BindingContext = new AdresDefteriViewModel( _kk);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        
            //if (txtDogumTarihi.Text == "0" || txtDogumTarihi.Text == "1")
            //{
            //    txtDogumTarihi.Text = "";
            //}
            //if (txtDogumAy.Text == "0" || txtDogumAy.Text == "1")
            //{
            //    txtDogumAy.Text = "";
            //}

            if (txtDogumYil.Text == "0" || txtDogumYil.Text == "1")
            {
                txtDogumYil.Text = "";
            }
        }

        private void txtCinsiyet_Focused(object sender, FocusEventArgs e)
        {
            drpCinsiyet.IsVisible = true;
            drpCinsiyet.Focus();
           // drpCinsiyet.IsVisible = false;
        }

        private void drpCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtCinsiyet.Text = drpCinsiyet.SelectedItem.ToString();

        }

        private void txtDogumTarihi_Focused(object sender, FocusEventArgs e)
        {
            drpDogumGun.IsVisible = true;
            drpDogumGun.Focus();
            drpDogumGun.IsVisible = false;

        }

        private void txtDogumAy_Focused(object sender, FocusEventArgs e)
        {
            drpDogumAy.IsVisible = true;
            drpDogumAy.Focus();
            drpDogumAy.IsVisible = false;

        }

        private void drpDogumAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtDogumAy.Text = drpDogumAy.SelectedItem.ToString();
        }

        private void drpDogumGun_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtDogumTarihi.Text = drpDogumGun.SelectedItem.ToString();
        }

      
        private void txtIletisimTur2_Focused(object sender, FocusEventArgs e)
        {
            drpIletisimTur2.IsVisible = true;
            drpIletisimTur2.Focus();
            drpIletisimTur2.IsVisible = false;

        }

        private void txtIletisimTur1_Focused(object sender, FocusEventArgs e)
        {
            drpIletisimTur1.IsVisible = true;
            drpIletisimTur1.Focus();
            drpIletisimTur1.IsVisible = false;

        }

        private void drpIletisimTur1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtIletisimTur1.Text = drpIletisimTur1.SelectedItem.ToString();

        }

        private void drpIletisimTur2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtIletisimTur2.Text = drpIletisimTur2.SelectedItem.ToString();

        }
    }
}