using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public  class KidemTavanDuzenleViewModelDuzenle: ViewModelBase
    {
        Services.KidemTavanService islem = new Services.KidemTavanService();

        KidemTavan KidemT = new KidemTavan();
        public KidemTavanDuzenleViewModelDuzenle(KidemTavan kk)
        {
            Yil = kk.yil;
            Donem = "Ocak-Haziran";
            if(kk.donem==2)
            {
                Donem = "Temmuz-Aralık";
            }
            Miktar = kk.miktar;

            KidemT = kk;
        }

        private int _yil;
        public int Yil
        {
            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }

        private string _donem;
        public string Donem
        {
            get => _donem;
            set
            {
                _donem = value;
                OnPropertyChanged();
            }
        }

        private decimal _miktar;
        public decimal Miktar
        {
            get => _miktar;
            set
            {
                _miktar = value;
                OnPropertyChanged();
            }
        }



        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(Object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            // Kaydetme İşlemi

            KidemT.miktar = Miktar;
            islem.Duzenle(KidemT);
            MessagingCenter.Send<KidemTavan>(KidemT, "YenileKidemTavan");
            await App.Current.MainPage.Navigation.PopModalAsync();

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

        private string _hataMesaji = "";
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
