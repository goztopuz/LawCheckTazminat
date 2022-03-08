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
    public partial class Basamak1IGView : ContentPage
    {   
        public Basamak1IGView(IsgucuKayip _kayip)
        {
            InitializeComponent();

            this.BindingContext = new Basamak1IGViewModel(_kayip);
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

        private void drpKazaGun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpKazaAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpDogumGun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drpDogumAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void drpCinsiyet_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
        }
    }
}