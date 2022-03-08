using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasamakDY1View : ContentPage
    {
        public BasamakDY1View(LawCheckTazminat.Models.DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext =new Basamak1DYViewModel(dyy);
        }


        protected override void OnAppearing()
        {
            //if (txtDogumYil.Text == "0" || txtDogumYil.Text == "1")
            //{
            //    txtDogumYil.Text = "";
            //}

            //if (txtVefatYil.Text == "0" || txtVefatYil.Text == "1")
            //{
            //    txtVefatYil.Text = "";
            //}
        }


            private void drpVefatAy_SelectedIndexChanged(object sender, EventArgs e)
            {
                    //txtVefatAy.Text = drpVefatAy.SelectedItem.ToString();
                     ////drpDogumAy.IsVisible = false;
              }

        private void txtVefatGun_Focused(object sender, FocusEventArgs e)
        {
        
        }

        private void drpVefatGun_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtVefatGun.Text = drpVefatGun.SelectedItem.ToString();
        }

        private void txtDogumTarihi_Focused(object sender, FocusEventArgs e)
        {
            //drpDogumGun.IsVisible = true;
            //drpDogumGun.Focus();
            //drpDogumGun.IsVisible = false;
        }

        private void txtDogumAy_Focused(object sender, FocusEventArgs e)
        {
            //drpDogumAy.IsVisible = true;
            //drpDogumAy.Focus();
            //drpDogumAy.IsVisible = false;
        }

        private void drpDogumGun_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtDogumTarihi.Text = drpDogumGun.SelectedItem.ToString();

        }

        private void drpDogumAy_SelectedIndexChanged(object sender, EventArgs e)
        {

            //txtDogumAy.Text = drpDogumAy.SelectedItem.ToString();

        }

        private void txtCinsiyet_Focused(object sender, FocusEventArgs e)
        {
            //drpCinsiyet.IsVisible = true;
            //drpCinsiyet.Focus();
            //drpCinsiyet.IsVisible = false;
        }

        private void drpCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCinsiyet.Text = drpCinsiyet.SelectedItem.ToString();
        }
    }
}