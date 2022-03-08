using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.IsgucuKayipV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
   public class Basamak3IGViewModel: ViewModelBase
    {
        public Basamak3IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = _kayip;

            Trafik = _kayip.trafikKazasi;

            //if (_kayip.duzenlemede == true)
            //{
            //    RaporYil = _kayip.raporTarihi.Year;
            //    RaporAy = _kayip.raporTarihi.Month;
            //    RaporGun = _kayip.raporTarihi.Day;
            //}
            //else
            //{
            //    RaporYil = DateTime.Now.Year;
            //    RaporAy = DateTime.Now.Month;
            //    RaporGun = DateTime.Now.Day;
            //}

            TrafikPMF = _kayip.trafikPMF;
            TrafikTRH = _kayip.trafikTRh;


        }

        string _trafik;
        public string Trafik
        {
            get => _trafik;
            set
            {
                _trafik = value;
                OnPropertyChanged();
            }
        }

        bool _trafikPMF;
        public bool TrafikPMF
        {
            get => _trafikPMF;
            set
            {
                _trafikPMF = value;
                OnPropertyChanged();
            }
        }

        bool _trafikTRH;
        public bool TrafikTRH
        {
            get => _trafikTRH;
            set
            {

                _trafikTRH = value;
                OnPropertyChanged();
            }
        }



        private IsgucuKayip _isgucu;
        public IsgucuKayip IsGucu
        {
            get => _isgucu;
            set
            {
                _isgucu = value;
                OnPropertyChanged();
            }
        }

        private int _gun;
        public int RaporGun
        {
            get => _gun;
            set
            {
                _gun = value;
                OnPropertyChanged();
            }
        }

        private int _ay;
        public int RaporAy
        {
            get => _ay;
            set
            {
                _ay = value;
                OnPropertyChanged();
            }
        }

        private int _yil;
        public int RaporYil
        {
            get => _yil;
            set
            {
                _yil = value;
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

        private bool SayfaKontrol()
        {
            bool _durum = true;
            if(IsGucu.raporTarihi<IsGucu.kazaTarihi)
            {
                this.HataMesaji = "Rapor Tarihi Kaza Tarihinden Önce Olamaz";
                _durum = false;
            }

            if(IsGucu.kusurOrani>100 || (IsGucu.kusurOrani<=0))
            {
                this.HataMesaji = "Kusur Oranı Hatalıdır";
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



            try
            {
                //var dTar = new DateTime(RaporYil, RaporAy, RaporGun);
                //IsGucu.raporTarihi = dTar;                                                                                                                                           

                IsGucu.trafikKazasi = Trafik;
                IsGucu.trafikPMF = TrafikPMF;
                IsGucu.trafikTRh = TrafikTRH;
            }
            catch
            {
                this.HataMesaji = "Rapor Tarihi Hatası";
            }

            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }


            this.HataMesaji = "";
            var basamak4 = new Basamk4IGView(this.IsGucu);
           await Application.Current.MainPage.Navigation.PushModalAsync(basamak4);

            IsBusy = false;
        }


    }
}
