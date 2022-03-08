using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YakinKisiEkle : ContentPage
    {
        public YakinKisiEkle(ObservableCollection<Yakin> liste, Yakin _yakin, DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext = new YakinKisiEkleViewModel(liste, _yakin, dyy);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (txtYakinlik.Text == "Anne")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                txtCinsiyet.Text = "Kadın";
            }
            else if (txtYakinlik.Text == "Baba")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                txtCinsiyet.Text = "Erkek";
            }
            else if (txtYakinlik.Text == "Eş")
            {
                stackEs.IsVisible = true;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = false;
            }
            else if (txtYakinlik.Text == "Çocuk")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = true;
                stackAnneBaba.IsVisible = false;

            }
            //if(txtDogumTarihi.Text=="0" || txtDogumTarihi.Text=="1")
            //{
            //    txtDogumTarihi.Text = "";
            //}
            //if (txtDogumAy.Text == "0" || txtDogumAy.Text=="1")
            //{
            //    txtDogumAy.Text = "";
            //}

            //if(txtDogumYil.Text=="0" || txtDogumYil.Text=="1")
            //{
            //    txtDogumYil.Text = "";
            //}

        }


            private void txtYakinlik_Focused(object sender, FocusEventArgs e)
        {
            drpYakinlik.IsVisible = true;
            drpYakinlik.Focus();
            drpYakinlik.IsVisible = false;

        }

        private void drpYakinlik_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtYakinlik.Text = drpYakinlik.SelectedItem.ToString(); 
            if(drpYakinlik.SelectedItem.ToString()=="Anne")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                //txtCinsiyet.Text = "Kadın";
                drpCinsiyet.SelectedItem = "Kadın";

            }else if(drpYakinlik.SelectedItem.ToString() =="Baba")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = true;
                txtCinsiyet.Text = "Erkek";
                drpCinsiyet.SelectedItem = "Erkek";
            }else if( drpYakinlik.SelectedItem.ToString()=="Eş")
            {
                stackEs.IsVisible = true;
                drpEsCalisiyor.SelectedItem = "ÇalışMIyor";

                stackCocuk.IsVisible = false;
                stackAnneBaba.IsVisible = false;
            }else if(drpYakinlik.SelectedItem.ToString()=="Çocuk")
            {
                stackEs.IsVisible = false;
                stackCocuk.IsVisible = true;
                stackAnneBaba.IsVisible = false;
                txtDestekCikis.Focus();
                txtAd.Focus();

            }
        }

        private void txtDogumAy_Focused(object sender, FocusEventArgs e)
        {
            //drpDogumAy.IsVisible = true;
            //drpDogumAy.Focus();
            //drpDogumAy.IsVisible = false;

        }

        private void drpDogumAy_SelectedIndexChanged(object sender, EventArgs e)
        {
       //     txtDogumAy.Text = drpDogumAy.SelectedItem.ToString();
        }

        private void drpDogumGun_SelectedIndexChanged(object sender, EventArgs e)
        {

       //     txtDogumTarihi.Text = drpDogumGun.SelectedItem.ToString();
        }
    
        private void txtDogumTarihi_Focused(object sender, FocusEventArgs e)
        {
            //drpDogumGun.IsVisible = true;
            //drpDogumGun.Focus();
            //drpDogumGun.IsVisible = false;
        }

        private void txtCinsiyet_Focused(object sender, FocusEventArgs e)
        {
            drpCinsiyet.IsVisible = true;
            drpCinsiyet.Focus();
            drpCinsiyet.IsVisible = false;
        }

        private void drpCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtCinsiyet.Text = drpCinsiyet.SelectedItem.ToString();
        }

        private void txtCocukOkuma_Focused(object sender, FocusEventArgs e)
        {
            drpCocukOkuma.IsVisible = true;
            drpCocukOkuma.Focus();
            drpCocukOkuma.IsVisible = false;
        }

        private void drpCocukOkuma_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCocukOkuma.Text = drpCocukOkuma.SelectedItem.ToString();
        }

        private void txtEsCalisma_Focused(object sender, FocusEventArgs e)
        {
            drpEsCalisiyor.IsVisible = true;
            drpEsCalisiyor.Focus();
            drpEsCalisiyor.IsVisible = false;
        }

        private void drpEsCalisiyor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtEsCalisma.Text = drpEsCalisiyor.SelectedItem.ToString();

        }

        private void txtAnneBabaDestekDurumu_Focused(object sender, FocusEventArgs e)
        {
            //drpAnneBabaMaas.IsVisible = true;
            //drpAnneBabaMaas.Focus();
            //drpAnneBabaMaas.IsVisible = false;

        }

        private void drpAnneBabaMaas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtAnneBabaDestekDurumu.Text = drpAnneBabaMaas.SelectedItem.ToString();
        }
    }

    }
