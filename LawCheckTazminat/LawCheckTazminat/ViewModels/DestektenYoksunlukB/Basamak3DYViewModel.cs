using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public  class Basamak3DYViewModel : ViewModelBase
    {
        public Basamak3DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;

            //if(dyy.Duzenlemede==true)
            //{
            //    RaporYil = dyy.raporTarihi.Year;
            //    _raporAy2 = dyy.raporTarihi.Month.ToString();
            //    _raporGun2 = dyy.raporTarihi.Day.ToString();


            //}
            //else
            //{
            //    RaporYil = DateTime.Now.Year;
            //    _raporAy2 = DateTime.Now.Month.ToString();
            //    _raporGun2 = DateTime.Now.Day.ToString();
            //}
            _trafik = dyy.trafikKazasi;
            _yasYuvarla = dyy.yasiYuvarla;

            TrafikPMF = dyy.trafikPMF;
            TrafikTRH = dyy.trafikTRh;

        }
        private Models.DestektenYoksunluk _destektenYoksunluk;
        public Models.DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;

            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
        }


     string _trafik;
     public  string Trafik
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
            get=> _trafikTRH;
            set{

                _trafikTRH = value;
                OnPropertyChanged();
            }
        }


        int _yasYuvarla;
        public int  YasYuvarla
        {
            get => _yasYuvarla;
            set
            {
                _yasYuvarla = value;
                OnPropertyChanged();

            }
        }

        

        int _raporYil;
        public int RaporYil
        {

            get => _raporYil;
            set
            {
                _raporYil = value;
                OnPropertyChanged();

            }
        }

        int _raporAy;
        public int RaporAy
        {
            get => _raporAy;
            set
            {
                _raporAy = value;
                OnPropertyChanged();
            }
        }

        int _raporGun;
        public int RaporGun
        {
            get => _raporGun;
            set
            {
                _raporGun = value;
                OnPropertyChanged();
            }
        }

        string _raporGun2;
        public string RaporGun2
        {
            get => _raporGun2;
            set
            {
                _raporGun2 = value;
                OnPropertyChanged();
            }        
        }

        string _raporAy2;
        public string RaporAy2
        {
            get => _raporAy2;
            set
            {
                _raporAy2 = value;
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


        public ICommand TrafikSecimCommand => new Command<string>(OnTrafikSecim);
        private void OnTrafikSecim(string secim)
        {
            if(secim== "Evet")
            {
                Trafik = "Evet";
            }
            else
            {
                Trafik = "Hayır";
            }
        }


        public ICommand IptalCommand => new Command(OnIptal);

        async private void OnIptal(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;

        }

        private bool SayfaKontrol()
        {
            bool durum = true;

            if(AktifDestek.raporTarihi<AktifDestek.vefatTarihi)
            {
                this.HataMesaji = "Rapor Tarihi - Vefat Tarihinden Önce";
                durum = false;
            }

            return durum;

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
               // var dTar = new DateTime(RaporYil, RaporAy, RaporGun);
                AktifDestek.trafikKazasi = Trafik;
                AktifDestek.trafikPMF = TrafikPMF;
                AktifDestek.trafikTRh = TrafikTRH;
            //    AktifDestek.raporTarihi = dTar;
            }
            catch
            {
                this.HataMesaji = "Rapor Tarihi Hatası";
                IsBusy = false;
                return;

            }
            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }
            
            var basamak4 = new Basamak4DYView(this.AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak4);
            this.HataMesaji = "";
            IsBusy = false;
        }



    }
}
