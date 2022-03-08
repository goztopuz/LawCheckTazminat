using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
   public class Basamak1DYViewModel :ViewModelBase
    {

        public Basamak1DYViewModel(DestektenYoksunluk dY)

        {
         //   this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dY;

            if(dY.Duzenlemede==true)
            {


                //VefatGun = dY.vefatTarihi.Day;
                //VefatAy = dY.vefatTarihi.Month;
                //VefatYil = dY.vefatTarihi.Year;
                OlayTarihi = dY.vefatTarihi;


                //DogumGun = dY.dogumTarihi.Day;
                //DogumAy = dY.dogumTarihi.Month;
                //DogumYil = dY.dogumTarihi.Year;

                DogumTarihi = dY.dogumTarihi;

            }
             _cinsiyet = dY.cinsiyet;


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


        private DateTime _olayTarihi;
        public DateTime OlayTarihi
        {
            get => _olayTarihi;
            set
            {
                _olayTarihi = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dogumTarihi;
        public DateTime DogumTarihi
        {
            get => _dogumTarihi;
            set
            {
                _dogumTarihi = value;
                OnPropertyChanged();
            }
        }

        int _dogumGun;
        public int DogumGun
        {
            get => _dogumGun;
            set
            {
                _dogumGun = value;
                OnPropertyChanged();
            }
        }
        int _dogumAy;
        public int DogumAy
        {
            get => _dogumAy;
            set
            {
                _dogumAy = value;
                this.AktifDestek.askereGidisAy = _dogumAy;
                OnPropertyChanged();
            }
        }

        int _dogumYil;
        public int DogumYil
        {

            get => _dogumYil;
            set
            {
                _dogumYil = value;
                this.AktifDestek.askereGidisYil = _dogumYil + 22;

            }
        }

        int _vefatYil;
        public int VefatYil
        {

            get => _vefatYil;
            set
            {
                _vefatYil = value;
                OnPropertyChanged();

            }
        }

        int _vefatAy;
        public int VefatAy
        {
            get => _vefatAy;
              set
            {
                _vefatAy = value;
                OnPropertyChanged();
            }
            }

        int _vefatGun;
        public int VefatGun
        {
            get => _vefatGun;
             set
            {
                _vefatGun = value;
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

        string _cinsiyet;
        public string Cinsiyet
        {
            get => _cinsiyet;
            set
            {
                _cinsiyet = value;
            }
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


        private bool SayfaKontrol()
        {
            bool durum = true;
            if(AktifDestek.dogumTarihi>= AktifDestek.vefatTarihi)
            {
                durum = false;
                this.HataMesaji = "Vefat Tarihi Doğum Tarihinden Önce";
            }
            if(this.AktifDestek.ad=="")
            {
                durum = false;
                this.HataMesaji = "Kişi Adı Girilmelidir";
            }

            if(AktifDestek.vefatTarihi> DateTime.Now)
            {
                durum = false;
                this.HataMesaji = "Vefat Tarihi Gelecek Bir Tarih";
            }

            return durum;
        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        
        async private void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            //try
            //{
            //    var vftTar = new DateTime(VefatYil, VefatAy, VefatGun);

            //    AktifDestek.vefatTarihi = vftTar;
            //}
            //catch
            //{
            //    this.HataMesaji = "Vefat Tarihi Hatası";
            //    IsBusy = false;
            //    return;
            //}
            //try
            //{
            //    var dTar = new DateTime(DogumYil, DogumAy, DogumGun);
            //    AktifDestek.dogumTarihi = dTar;
            //}catch
            //{
            //    this.HataMesaji = "Doğum Tarihi Hatası";
            //    IsBusy = false;
            //    return;
            //}
            if(AktifDestek.cinsiyet=="")
            {
                this.HataMesaji = "Cinsiyet Seçilmelidir";
                IsBusy = false;
                return;
            }

            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }


            this.HataMesaji = "";
            var basamak2 = new Basamak2DYView(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak2);
            
            IsBusy = false;
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
   
    }
}
