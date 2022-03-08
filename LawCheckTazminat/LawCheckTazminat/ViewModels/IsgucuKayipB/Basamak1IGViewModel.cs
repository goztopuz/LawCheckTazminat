using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.IsgucuKayipV;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
   public class Basamak1IGViewModel:ViewModelBase
    {

        public Basamak1IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = _kayip;

            //if (IsGucu.duzenlemede == true)
            //{
            //    KazaGun = IsGucu.kazaTarihi.Day;
            //    KazaAy = IsGucu.kazaTarihi.Month;
            //    KazaYil = IsGucu.kazaTarihi.Year;

            //    DogumGun = IsGucu.dogumTarihi.Day;
            //    DogumAy = IsGucu.dogumTarihi.Month;
            //    DogumYil = IsGucu.dogumTarihi.Year;

            //}

        }

        private IsgucuKayip _isGucu;

        public IsgucuKayip IsGucu
        {
            get => _isGucu;
            set
            {
                _isGucu = value;
                OnPropertyChanged();
            }
        }


        //----------------------------------------

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
                this.IsGucu.askereGidisAy = _dogumAy;
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
                this.IsGucu.askereGidisYil = _dogumYil + 22;

            }
        }


        //-----------------------------------------------

        int _kazaYil;
        public int KazaYil
        {

            get => _kazaYil;
            set
            {
                _kazaYil = value;
                OnPropertyChanged();

            }
        }

        int _kazaAy;
        public int KazaAy
        {
            get => _kazaAy;
            set
            {
                _kazaAy = value;
                OnPropertyChanged();
            }
        }

        int _kazaGun;
        public int KazaGun
        {
            get => _kazaGun;
            set
            {
                _kazaGun = value;
                OnPropertyChanged();
            }
        }

        //-------------------------------------------------

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

        //---------------------------------------------------

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


        private bool SayfaKontrol()
        {
            bool durum = true;
            if(IsGucu.dogumTarihi> IsGucu.kazaTarihi)
            {
                this.HataMesaji = "Doğum Tarihi - Kaza Tarihinden Sonra Olamaz.";
                durum = false;
            }

            if(IsGucu.ad=="")
            {
                this.HataMesaji = "Ad Girilmelidir.";
                durum = false;
            }

            if(IsGucu.soyad=="")
            {
                this.HataMesaji = "Soyad Girilmelidir.";
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
            //try
            //{
            //    var kTar = new DateTime(KazaYil, KazaAy, KazaGun);

            //    IsGucu.kazaTarihi = kTar;
            //}
            //catch
            //{
            //    this.HataMesaji = "Kaza Tarihi Hatası";
            //    IsBusy = false;
            //    return;
            //}
            //try
            //{
            //    var dTar = new DateTime(DogumYil, DogumAy, DogumGun);
            //    IsGucu.dogumTarihi = dTar;
            //}
            //catch
            //{
            //    this.HataMesaji = "Doğum Tarihi Hatası";
            //    IsBusy = false;
            //    return;
            //}
            if (IsGucu.cinsiyet == "")
            {
                this.HataMesaji = "Cinsiyet Seçilmelidir";
                IsBusy = false;
                return;
            }

            if(SayfaKontrol()==false)
            {
                IsBusy = false;
                return;
            }

            this.HataMesaji = "";
            var basamak2 = new Basamak2IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak2);

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



    }
}
