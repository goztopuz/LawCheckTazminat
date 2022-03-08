using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
    public  class BasamakSonKHViewModel : ViewModelBase
    {

        public BasamakSonKHViewModel(KayipOranHesap _kh)
        {
            this.KH = new KayipOranHesap();
            this.KH = _kh;

            Hesapla();
        }


        private void Hesapla()
        {

        }


        private double _kayipMiktar3738;
        public double KayipMiktar3738
        {
            get => _kayipMiktar3738;
            set
            {
                _kayipMiktar3738 = value;
                OnPropertyChanged();
            }
        }

        private double _kayipMiktar;
        public double KayipMiktar
        {
            get => _kayipMiktar;
            set
            {
                _kayipMiktar = value;
                OnPropertyChanged();
            }

        }


        private string _kayipSimge;
        public string KayipSimge
        {
            get => _kayipSimge;
            set
            {
                _kayipSimge = value;
                OnPropertyChanged();
            }
        }


        private KayipOranHesap _kh;
        public KayipOranHesap KH
        {
            get => _kh;
            set
            {
                _kh = value;
                OnPropertyChanged();
            }
        }

        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        private async void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //GC.duzenlemede = true;

            //----
            var sayfa = new LawCheckTazminat.Views.KayipOranV.Basamak1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



            this.HataMesaji = "";
            IsBusy = false;
        }
        public ICommand BitisCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            var sayfa = new Views.AnaSayfaV.Anasayfa();

            this.HataMesaji = "";
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
