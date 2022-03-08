using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Destek
{
    public class YeniDestekViewModel: ViewModelBase
    {

        DestekTalepService islem = new DestekTalepService();
        public YeniDestekViewModel()
        {
       
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

        public string _mesaj;
        public string Mesaj
        {
            get => _mesaj;

            set
            {
                _mesaj = value;
                OnPropertyChanged();
            }
        }

        public string _eposta;
        public string Eposta
        {
            get => _eposta;
            set
            {
                _eposta = value;
                OnPropertyChanged();
            }
        }

        public string _telefon;
        public string Telefon
        {
            get => _telefon;
            set
            {
                _telefon = value;
                OnPropertyChanged();
            }
        }


        public ICommand KaydetCommand => new Command(OnDestekKaydet);
        async private void OnDestekKaydet(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }

            IsBusy = true;

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Internet Bağlantı Hatası", "Tamam");
                return;
            }

            //KAYDETME Kodları

            DestekTalep _destekTalep = new DestekTalep();

            _destekTalep.Id = Guid.NewGuid().ToString();
            _destekTalep.konu = Konu;
            _destekTalep.soru = Mesaj;
            _destekTalep.tarih = DateTime.Now;
            _destekTalep.eposta = Eposta;
            _destekTalep.telefon = Telefon;

            _destekTalep.ek2 = "LawCheck1.0";



            await islem.Ekle(_destekTalep);
           


            //   MessagingCenter.Send
            MessagingCenter.Send<DestekTalep>(_destekTalep, "YenileDestekListele");


            await Application.Current.MainPage.DisplayAlert("Mesaj Alınd", "Mesajınız" +
               "Sisteme Alınmıştır. En Kısa Sürede Size Geri Dönülecektir.", "Tamam");

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
