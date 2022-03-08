using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public   class Basamak4BekarCocukDYViewModel : ViewModelBase
    {

        public Basamak4BekarCocukDYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;

            //------------------------------------
            this.EvlenmeYasi = dyy.BekarCocukEvlenmeYas;

            int yilFark = DateTime.Now.Year - AktifDestek.dogumTarihi.Year;

            this.SuAnCalisiyor = dyy.BekarCocuk_SuAnCalisiyor;

            this.EvlenmeTarihi = dyy.dogumTarihi.AddYears(this.EvlenmeYasi);

            this.IseGirisYasi = dyy.BekarCocuk_CalismaBasYasi;
            this.IseGirisTarihi = dyy.dogumTarihi.AddYears(this.IseGirisYasi);

            if(dyy.Id == null || dyy.Id== "")
            {
                decimal aa = RaporTarihindekiAsgariUcret();
                this.IsOncesiMaas = aa;
                this.GelecekMaas = aa;

            }
            else
            {
                this.IsOncesiMaas = dyy.BekarCocuk_18OncesiMaas;
                this.GelecekMaas = dyy.BekarCocuk_GelecekCalismaUcreti;


            }


            this.SuAnkiMaas = dyy.BekarCocuk_SuAnkiUcret;





            if (dyy.Id !="" || dyy.Id != null)
            {
                // Çalışmıyorsa 1Maaş Bilgilerini Asgari ücretten  Olay Tarihinin  asgarisinden al ve diğer
                // aylara oranlayarak dağıt



            }
            

            if(yilFark>this.EvlenmeYasi)
            {
                this.EvlenmeYasi = yilFark + 2;
            }
            this.EvlenmeTarihi = dyy.dogumTarihi.AddYears(this.EvlenmeYasi);

            //--------------------
            this._iseGirisYasi = dyy.BekarCocuk_CalismaBasYasi;
            this.IseGirisTarihi = dyy.dogumTarihi.AddYears(this.IseGirisYasi);

            //---------------------------
            // Asgari Ücret-İş Öncesi
            //this.IsOncesiMaas = dyy.BekarCocuk_18OncesiMaas;

            // İş Sonrası -------


            //this.GelecekMaas = dyy.BekarCocuk_GelecekCalismaUcreti;



            this.CocukSayisi = dyy.BekarCocukCocukSayisi;
            this.IlkCocukSenesi = dyy.BekarCouckCocukIlkSene;
            this.CocuklarArasiYil = dyy.BekarCocukCocuklarArasiYil;
            this.CocukDestekCikisYasi = dyy.BearCocukCocukDestekCikisYasi;

        }


        private  decimal RaporTarihindekiAsgariUcret()
        {
            decimal _sonuc1 = 0;

            DateTime _tarih = AktifDestek.raporTarihi;

            string _yilBilgi = _tarih.Year + "-1";
            if (_tarih.Month > 6)
            {
                _yilBilgi = _tarih.Year + "-2";
            }

            AsgariUcretService islem2 = new AsgariUcretService();
            
            var asgariMaasListesi = islem2.GetItems2();

            var al1 = asgariMaasListesi.Find(o => o.yil == _yilBilgi);

            _sonuc1 = al1.net;
            

            return _sonuc1;
        }

        private int _evlenmeYasi;
        public int  EvlenmeYasi
        {
            get => _evlenmeYasi;
            set
            {
                _evlenmeYasi = value;
                OnPropertyChanged();
            }
        }

        private DateTime _evlenmeTarihi;
        public DateTime EvlenmeTarihi
        {
            get => _evlenmeTarihi;
            set
            {
                _evlenmeTarihi = value;
                OnPropertyChanged();
            }
        }


        private int _iseGirisYasi;
        public int IseGirisYasi
        {
            get => _iseGirisYasi;
            set
            {
                _iseGirisYasi = value;
                OnPropertyChanged();
            }
        }


        private DateTime _iseGirisTarihi;
        public DateTime IseGirisTarihi
        {
            get => _iseGirisTarihi;
            set
            {
                _iseGirisTarihi = value;
                OnPropertyChanged();
            }
        }


        private decimal _isOncesiMaas;
        public decimal IsOncesiMaas
        {
            get => _isOncesiMaas;
            set
            {
                _isOncesiMaas = value;
                OnPropertyChanged();
            }
        }


        private decimal _gelecekmaas;
        public Decimal GelecekMaas
        {
            get => _gelecekmaas;
            set
            {
                _gelecekmaas = value;
                OnPropertyChanged();
            }
        }

        private bool _suAnCalisiyor;
        public bool SuAnCalisiyor
        {
            get => _suAnCalisiyor;
            set
            {
                _suAnCalisiyor = value;
                OnPropertyChanged();
            }
        }


        private decimal _suAnkiMaas;
        public decimal SuAnkiMaas
        {
            get => _suAnkiMaas;
            set
            {
                _suAnkiMaas = value;
                OnPropertyChanged(); 
            }
        }

        //******************************************************************
        //---------------------------------------------------------


        public int _cocukSayisi;
        public int CocukSayisi
        {
            get => _cocukSayisi;
            set
            {
                _cocukSayisi = value;
                OnPropertyChanged();

                 }
        }

        public int _ilkCocukSenesi;
        public int  IlkCocukSenesi
        {
            get => _ilkCocukSenesi;
            set
            {
                _ilkCocukSenesi = value;
                OnPropertyChanged();
                    
            }          
        }

        public int _cocuklarArasiYil;
        public int CocuklarArasiYil
        {
            get => _cocuklarArasiYil;
            set
            {
                _cocuklarArasiYil = value;
                OnPropertyChanged();
            }
        }


       public int _cocukDestekCikisYasi;
      public int CocukDestekCikisYasi
        {
            get => _cocukDestekCikisYasi;
            set
            {
                _cocukDestekCikisYasi = value;
                OnPropertyChanged();
            }
        }

        private DestektenYoksunluk _destektenYoksunluk;
        public DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;
           
            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
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




        //İLERLEME
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            AktifDestek.BekarCocukEvlenmeYas = EvlenmeYasi;
         

            AktifDestek.BekarCocukDurum = true;

         
            int yilFark = DateTime.Now.Year - AktifDestek.dogumTarihi.Year;

            AktifDestek.BekarCocuk_SuAnCalisiyor = SuAnCalisiyor;

            AktifDestek.BekarCocuk_EvlenmeTarihi = this.EvlenmeTarihi;


             AktifDestek.BekarCocuk_CalismaBasYasi = IseGirisYasi;

            AktifDestek.BekarCocuk_CalismaBasTarihi = this.IseGirisTarihi;


            AktifDestek.BekarCocuk_18OncesiMaas = this.IsOncesiMaas;
             AktifDestek.BekarCocuk_GelecekCalismaUcreti = this.GelecekMaas;

             AktifDestek.BekarCocuk_SuAnkiUcret  = this.SuAnkiMaas;




            var basamak6 = new Basamak6DYView(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak6);

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
   
    
    
    }
}
