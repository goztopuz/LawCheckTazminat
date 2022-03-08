using System;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class IzinBilgiViewModel : ViewModelBase
    {
        public IzinBilgiViewModel(GeceMesaiKisiIzinKayitlari _rapor, Models.GeceCalisma _gece)
        {
            this.GC = new GeceCalisma();
            this.GC = _gece;

            this.RP = new GeceMesaiKisiIzinKayitlari();
            this.RP = _rapor;

            if (_rapor.Id != "")
            {
                this.IzinBas = _rapor.BaslangicTar;
                this.IzinBit = _rapor.BitisTar;
                this.Aciklama = _rapor.Aciklama;

            }
            else
            {
                this.IzinBas = GC.BasTarih;
                this.IzinBit = GC.BasTarih;
            }

        }

        private GeceMesaiKisiIzinKayitlari _rp;
        public GeceMesaiKisiIzinKayitlari RP
        {
            get => _rp;
            set
            {
                _rp = value;
                OnPropertyChanged();
            }
        }



        private GeceCalisma _gc;
        public GeceCalisma GC
        {
            get => _gc;
            set
            {
                _gc = value;
                OnPropertyChanged();
            }
              
        }

        private DateTime _izinBaslangic;
        public DateTime IzinBas
        {
            get => _izinBaslangic;
            set
            {
                _izinBaslangic = value;
                OnPropertyChanged();
            }
        }


        private DateTime _izinBitis;
        public DateTime IzinBit
        {
            get => _izinBitis;
            set
            {
                _izinBitis = value;
                OnPropertyChanged();
            }

        }


        public string _aciklama;
        public string Aciklama
        {
            get => _aciklama;
            set
            {
                _aciklama = value;
                OnPropertyChanged();
            }

        }


        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            // KaydetCommand...


            if (RP.Id == null || RP.Id == "")
            {
                // Yeni Kayıt

                GeceMesaiKisiIzinKayitlari kayitYeni = new GeceMesaiKisiIzinKayitlari();
                kayitYeni.Id = Guid.NewGuid().ToString().Substring(0, 8);
                kayitYeni.BaslangicTar = IzinBas;
                kayitYeni.BitisTar = IzinBit;
                kayitYeni.Aciklama = Aciklama;

                GC.IzinKaytilariBilgi.Add(kayitYeni);

      

            }
            else
            {
                // Güncelleme

                var kayit = GC.IzinKaytilariBilgi.ToList().Find(o => o.Id == RP.Id);

                kayit.BaslangicTar = IzinBas;
                kayit.BitisTar = IzinBit;
                kayit.Aciklama = Aciklama;


            }


            MessagingCenter.Send<GeceCalisma>(GC, "YenileGC");

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
