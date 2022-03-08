using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Destek
{
    public class YanitGoruntuleViewModel :ViewModelBase
    {
        public YanitGoruntuleViewModel(DestekYanit dY)
        {
            YanitKonu = dY.yanitBaslik;
            YanitTarihi = dY.yanitTarihi.ToShortDateString();
            Yanit = dY.yanit;      
            Soru= dY.soru;

        }

        private DestekYanit _destekYY;
        public DestekYanit DestekYY
        {
            get => _destekYY;
            set
            {
                DestekYY = value;
                OnPropertyChanged();
            }
        }

        private string _yanitKonu;
        public string YanitKonu
        {
            get => _yanitKonu;
            set
            {
                _yanitKonu = value;
            }
        }

        private string _yanitTarihi;
        public string YanitTarihi
        {
            get => _yanitTarihi;
            set
            {
                _yanitTarihi = value;
                OnPropertyChanged();
            }
        }

        private string _yanit;
        public string Yanit
        {
            get => _yanit;
            set
            {
                _yanit = value;
                OnPropertyChanged();
            }
        }

        private string _soru;
        public string Soru
        {
            get => _soru;
            set
            {
                _soru = value;
                OnPropertyChanged();
            }
        }

        public ICommand TamamCommand => new Command(OnTamam);
        async private void OnTamam(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }

        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;

        }

        public bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        string _hataMesaji = "";
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }

    }
}
