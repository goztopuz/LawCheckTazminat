using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LawCheckTazminat.Models;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class YakinKisiEkle : ContentPage
    {

        public YakinKisiEkle(Models.IsgucuKayip _kayip, Models.YakinIsgucu _yakin, ObservableCollection<YakinIsgucu> Liste)
        {
            InitializeComponent();

            if (_yakin.yakinlik == "Anne")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                //  txtCinsiyet.Text = "Kadın";
                drpCinsiyet.SelectedItem = "Kadın";
            }
            else if (_yakin.yakinlik == "Baba")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                //   txtCinsiyet.Text = "Erkek";
                drpCinsiyet.SelectedItem = "Erkek";
            }
            else if (_yakin.yakinlik == "Eş")
            {
                stackEs.IsVisible = true;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = false;
            }
            else if (_yakin.yakinlik == "Çocuk")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = true;
                stackAnneBaba.IsVisible = false;

            }
 

            if (txtDogumYil.Text == "0" || txtDogumYil.Text == "1")
            {
                txtDogumYil.Text = "";
            }

            this.BindingContext = new ViewModels.IsgucuKayipB.YakinKisiEkleViewModel(_kayip, _yakin,Liste);


        }


        private void drpYakinlik_SelectedIndexChanged(object sender, EventArgs e)
        {
                //txtYakinlik.Text = drpYakinlik.SelectedItem.ToString();
            if (drpYakinlik.SelectedItem.ToString() == "Anne")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                //txtCinsiyet.Text = "Kadın";
                drpCinsiyet.SelectedItem = "Kadın";

            }
            else if (drpYakinlik.SelectedItem.ToString() == "Baba")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
              //  txtCinsiyet.Text = "Erkek";
                drpCinsiyet.SelectedItem = "Erkek";
            }
            else if (drpYakinlik.SelectedItem.ToString() == "Eş")
            {
                stackEs.IsVisible = true;
                drpEsCalisiyor.SelectedItem = "ÇalışMIyor";

                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = false;
            }
            else if (drpYakinlik.SelectedItem.ToString() == "Çocuk")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = true;
                stackAnneBaba.IsVisible = false;
                txtDestekCikis.Focus();
                txtAd.Focus();

            }
        }



    }
}
