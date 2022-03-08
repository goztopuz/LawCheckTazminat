using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class YillikIzinYYViewModel: ViewModelBase
    {
        private decimal BrutUcret;
        private int VergiYil;
        private decimal VergiMatrah;

        Services.HesaplaVergi islemVergi = new Services.HesaplaVergi();


        public YillikIzinYYViewModel(YillikIzin _yillik)
        {
            BrutUcret = YY.BrutUcret;
            VergiYil = YY.VergiYil;
            VergiMatrah = YY.VergiMatrah;
            
        }


        private void Hesapla()
        {

            decimal _brutToplam = 0;

            decimal _gunlukUcret = 0;
            Double damgaVergi = 0.00759;



            _gunlukUcret = BrutUcret / 30;

            Toplam = (decimal)GunSay* _gunlukUcret;

            SGK = Toplam * Convert.ToDecimal(0.14);
            DamgaV =Toplam * Convert.ToDecimal(damgaVergi);
            Issizlik = Toplam * (Convert.ToDecimal(0.01));

            decimal dusecekMiktar = YY.SGK + YY.DamgaV + YY.Issizlik;

            decimal mik1 = YY.Toplam - dusecekMiktar;
            decimal gelirVergi = islemVergi.VergiHesapla(
                Convert.ToDecimal(YY.VergiMatrah), mik1, Convert.ToInt32(YY.VergiYil));

            YY.Vergi = gelirVergi;

            YY.Net = YY.Toplam - YY.Vergi - dusecekMiktar;

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

        private double _gunsay;
        public double GunSay
        {
            get => _gunsay;
            set
            {
                _gunsay = value;
                OnPropertyChanged();
            }
        }

        private decimal _toplam;
        public decimal Toplam
        {
            get => _toplam;
            set
            {
                _toplam = value;
                OnPropertyChanged();
            }        
        }

        private decimal _sgk;
        public decimal SGK
        {
            get => _sgk;
            set
            {
                _sgk = value;
                OnPropertyChanged();
            }
        }

        private decimal _vergi;
        public Decimal Vergi
        {
            get => _vergi;
            set
            {
                _vergi = value;
                OnPropertyChanged();
            }
        }

        private decimal _damgaV;
        public decimal DamgaV
        {
            get => _damgaV;
            set
            {
                _damgaV = value;
                OnPropertyChanged();
            }
        }

        private decimal _issizlik;
        public decimal Issizlik
        {
            get => _issizlik;
            set
            {
                _issizlik = value;
                OnPropertyChanged();
            }
        }

        private decimal _net;
        public decimal Net
        {
            get => _net;
            set
            {
                _net = value;
                OnPropertyChanged();
                    }
        }


        public ICommand HesaplaCommand => new Command(OnHesapla);
        private async void OnHesapla(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            //-----------------------


            this.HataMesaji = "";
            IsBusy = false;
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
