using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public  class Basamak3BYYViewModel : ViewModelBase
    {

        public Basamak3BYYViewModel(YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;
        }


        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            // İlerle İşlemi

            if (SayfaKontrol() == false)
            {
                IsBusy = false;
                return;
            }


     
            var sayfa = new Basamak4YYView(YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;
        }

        private bool SayfaKontrol()
        {
            bool durum = true;

            if (YY.BrutUcret < 0)
            {
                this.HataMesaji = "Brüt Ücret Negatif Olamaz";
                durum = false;
            }
            if (YY.izindeAlinanRaporSayisi < 0)
            {
                this.HataMesaji = "Rapor Sayısı Negatif Olamaz.";
                durum = false;
            }

            return durum;
        }


        public ICommand IptalCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }


        private string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

    }
}
