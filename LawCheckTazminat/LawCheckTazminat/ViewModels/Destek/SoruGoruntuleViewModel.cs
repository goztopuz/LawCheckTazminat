using System;
using System.Windows.Input;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;
using LawCheckTazminat.Models;

namespace LawCheckTazminat.ViewModels.Destek
{
    public class SoruGoruntuleViewModel : ViewModelBase
    {
        public SoruGoruntuleViewModel(DestekTalep _dd)
        {

            Konu = _dd.konu;

            Tarih = _dd.tarih.ToShortDateString();

            Soru = _dd.soru;



        }

        private string _konu;
        public string Konu
        {
            get => _konu;
            set
            {
                _konu = value;
                OnPropertyChanged();
            }
        }

        private string _tarih;
        public string Tarih
        {
            get => _tarih;
            set
            {
                _tarih = value;
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
            if(IsBusy== true)
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
