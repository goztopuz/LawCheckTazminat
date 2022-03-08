using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class YeniKayipViewModel:ViewModelBase
    {

        ObservableCollection<KayipOran> listee = new ObservableCollection<KayipOran>();
        public YeniKayipViewModel(Models.IsgucuKayip _isgucu, KayipOran _oran, ObservableCollection<KayipOran> liste1)
        {
            this.Isgucu = new IsgucuKayip ();
            this.Isgucu = _isgucu;

            this.Oran = new KayipOran();
            this.Oran = _oran;

            listee = liste1;
            if(_oran.id=="")
            {
                // Yeni Kayit

                this.OranTarih = DateTime.Now;
            }
            else
            {

                this.OranTarih = _oran.baslangicTarihi;
                this.OranYuzde = _oran.kayipOrani;
            }
        }

        private double _oranYuzde;
        public double OranYuzde
        {
            get => _oranYuzde;
            set
            {
                _oranYuzde = value;
                OnPropertyChanged();
            }
        }
        private DateTime _oranTarih;
        public DateTime OranTarih
        {
            get => _oranTarih;
            set
            {
                _oranTarih = value;
            }
        }

       private IsgucuKayip _isgucuKayip;
       public IsgucuKayip Isgucu
        {
            get => _isgucuKayip;

            set
            {
                _isgucuKayip = value;
                OnPropertyChanged();
            }
        }

        private KayipOran _kayipOran;
        public KayipOran Oran
        {
            get => _kayipOran;
            set
            {
                _kayipOran = value;
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


            //if(OranYuzde==0)
            //{
            //  this.HataMesaji = "Kayıp Yüzdesi Girilmelidir.";
            //    _durum = false;

            //}
            if(OranYuzde<0)
            {
                this.HataMesaji = "Kayıp Yüzdesi Eksi Değer Olamaz";
                _durum = false;
            }
            if(OranYuzde>=100)
            {
                this.HataMesaji = "Kayıp Yüzdesi 100 ve Üstü Olamaz";
                _durum = false;
            }


            if(OranTarih< Isgucu.kazaTarihi)
            {
                this.HataMesaji = "Yeni Kayıp Yüzdesi Olay Tarihinden Önce Olamaz";
                _durum = false;

            }
            foreach(var t in  listee)
            {
                if(Oran.id=="")
                {
                    if (t.baslangicTarihi == OranTarih)
                    {
                        this.HataMesaji = "Girilen Tarihli Oran Bilgisi Mevcut";
                        _durum = false;
                    }

                }
            
            }

            return _durum;
        }
        

        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            Oran.baslangicTarihi = OranTarih;
            Oran.kayipOrani = OranYuzde;

            if (SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }


            // Ekleme KodlarıEklenecek...
            if (Oran.id == null || Oran.id == "")
            {
                Oran.id = Guid.NewGuid().ToString().Substring(0, 8).ToString();
                Oran.aciklama = "Ek";

                this.Isgucu.IsGucuKayipOranlar.Add(Oran);

            }
            else
            {

                KayipOran orannn = this.Isgucu.IsGucuKayipOranlar.Where(o => o.id == Oran.id).FirstOrDefault();
                orannn = Oran;

            }

            MessagingCenter.Send<ObservableCollection<KayipOran>>(Isgucu.IsGucuKayipOranlar, "YenileKayip");

            this.HataMesaji = "";
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;


        }

    }
}
