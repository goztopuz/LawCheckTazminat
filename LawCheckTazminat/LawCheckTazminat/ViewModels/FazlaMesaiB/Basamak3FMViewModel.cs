using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak3FMViewModel:ViewModelBase
    {
        public Basamak3FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;
            this.FMSaat2 = this.FM.HaftaCalismaSaat2;
            this.FMDus = this.FM.HaftasonuDus;
        }

        private FazlaMesai _fazlaMesai;

        public FazlaMesai FM
        {
            get => _fazlaMesai;
            set
            {
                _fazlaMesai = value;
                OnPropertyChanged();
            }
        }

        private double _fmSaat2;
        public double FMSaat2
        {
            get => _fmSaat2;
            set
            {
                _fmSaat2 = value;
                OnPropertyChanged();
            }
        }

        private bool _fmDus;
        public bool FMDus
        {
            get => _fmDus;
            set
            {
                _fmDus = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckSaatDusCommand => new Command(OnCheckSaatDus);

        async  private void OnCheckSaatDus()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            // İşlem...
            FMSaat2 = FM.HaftaCalismaSaat - 7.5;


            IsBusy = false;
        }


        private bool SayfaKonrol()
        {
            bool _durum = true;


            if(this.FM.BasTarihHaftaSonu>FM.BitTarihHaftaSonu)
            {

                this.HataMesaji = "Başlangıç Bitiş Tarihinden Sonra Olamaz";

                _durum = false;


            }


            return _durum;

        }


        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            FM.FazlaMesaiMiktar2 = FMSaat2;
            FM.HaftasonuDus = FMDus;


            if(SayfaKonrol()== false)
            {
                IsBusy = false;
                return;
            }

            this.HataMesaji = "";

            var basamak4 = new Basamak4FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak4);

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
