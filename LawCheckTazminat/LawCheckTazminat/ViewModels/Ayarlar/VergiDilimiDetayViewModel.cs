using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class VergiDilimiDetayViewModel:ViewModelBase
    {

        ObservableCollection<VergiDilimleri> Liste = new ObservableCollection<VergiDilimleri>();

        VergiDilimlerService islem = new VergiDilimlerService();

        public VergiDilimiDetayViewModel(VergiDilimleri _vv, ObservableCollection<VergiDilimleri> _liste)
        {
            this.VD = new VergiDilimleri();
            this.VD = _vv;
            Liste = _liste;

        }

        private VergiDilimleri _vd;
        public VergiDilimleri VD
        {
            get => _vd;
            set
            {
                _vd = value;
                OnPropertyChanged();
            }
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

        private decimal _miktar1;
        public decimal Miktar1
        {
            get => _miktar1;
            set
            {
                _miktar1 = value;
                OnPropertyChanged();
            }
        }

        private decimal _miktar2;
        public decimal Miktar2
        {
            get => _miktar2;
            set
            {
                _miktar2 = value;
                OnPropertyChanged();
            }
        }

        private decimal _miktar3;
        public decimal Miktar3
        {
            get => _miktar3;
            set
            {
                _miktar3 = value;
                OnPropertyChanged();
            }
        }
        public string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }


        private bool SayfaKontrol()
        {
            bool _durum = true;

            // Bu tarihli Vergi Dilimi Var mı?




            //1. Mik 2. den küçük mü?
            if(Miktar2<= Miktar1)
            {
                this.HataMesaji = "%20'lik Dilim Miktarı % 15'lik Dilimden Büyük Olmalıdır.";
                _durum = false;
            }

            // 2. Mik 3.den küçük mü?

            if(Miktar3<= Miktar2)
            {
                this.HataMesaji = "%27'lik Dilim Miktarı % 20'lik Dilimden Büyük Olmalıdır.";
                _durum = false;
            }

            if(Miktar1<=0 || Miktar2<=0 || Miktar3 <= 0)
            {
                this.HataMesaji = "Girilen Miktarlarda Hata Var";

                _durum = false;
            }
       

            return _durum;
        }
        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet()
        {

            if (IsBusy == true)
            {
                return;
            }
            if (SayfaKontrol() == false)
            {
                return;
            }
            IsBusy = true;


        

            // --- Kaydetme İşlemi.....


            VergiDilimleri tt = new VergiDilimleri();
            tt.Id = Yil;
            tt.Vd1Miktar = Miktar1;
            tt.Vd1Yuzde = 15;
            tt.Vd2Miktar = Miktar2;
            tt.Vd2Yuzde = 20;
            tt.Vd3Miktar = Miktar3;
            tt.Vd3Yuzde = 27;
            tt.Vd4Yuzde = 35;

            await islem.AddItemAsync(tt);

            MessagingCenter.Send<VergiDilimleri>(tt, "VergiDilimleriYenile");


            this.HataMesaji = "";

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


    }
}
