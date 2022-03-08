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
    public partial class Basamak5DYView : ContentPage
    {
        public Basamak5DYView(Models.DestektenYoksunluk aktifDestek)
        {
            InitializeComponent();
            this.BindingContext = new Basamak5DYViewModel(aktifDestek);
        }

        private void chkElle_Toggled(object sender, ToggledEventArgs e)
        {
            if(chkElle.IsToggled==true)
            {
                stckEvlenme.IsVisible = true;
            }
            else
            {
                stckEvlenme.IsVisible = false;
            }
        }
    }
}