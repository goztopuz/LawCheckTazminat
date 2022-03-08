using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using LawCheckTazminat.Views.DestektenYoksunlukV;
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
    public partial class Basamk4IGView : ContentPage
    {
        public Basamk4IGView(IsgucuKayip _kayip)
        {
            InitializeComponent();

            this.BindingContext = new Basamak4IGViewModel(_kayip);
        }

        private void drpHastane_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpHastane.SelectedItem.ToString()=="Yok")
            {
                stckHastane.IsVisible = false;
            }
            else
            {
                stckHastane.IsVisible = true;
            }

        }
    }
}