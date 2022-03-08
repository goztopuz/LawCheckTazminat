using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LawCheckTazminat.Models;
namespace LawCheckTazminat.ViewModels.Ayarlar
{
   public class KidemTavanDuzenleViewModelYeni : ViewModelBase
    {

        List<KidemTavan> KDList = new List<KidemTavan>();
        public KidemTavanDuzenleViewModelYeni(List<KidemTavan> _ll)
        {

            KDList = _ll;
            this.Yil = DateTime.Now.Year;
            this.Donemi = "Ocak-Haziran";

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

        private string _donemi;
        public string Donemi
        {
            get => _donemi;
            set
            {
                _donemi = value;
                OnPropertyChanged();
            }
        }

        private decimal _miktar;
        public Decimal Miktar
        {
            get => _miktar;
            set
            {
                _miktar = value;
                OnPropertyChanged();
            }
        }


        private string _hataMesaji = "";
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }



        Services.KidemTavanService islem = new Services.KidemTavanService();

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {

            if(IsBusy== true)
            {
                return;
            }

            //---------------------

            KidemTavan kk = new KidemTavan();
            kk.Id = Guid.NewGuid().ToString();
            kk.miktar = Miktar;
            kk.yil = Yil;
            int _donem = 1;
            if(Donemi== "Temmuz-Aralık")
            {
                _donem = 2;
            }

            kk.donem = _donem;
            if(_donem ==1)
            {
                kk.baslangic = new DateTime(Yil, 1, 1);
                kk.bitis = new DateTime(Yil, 6, 30);
            }
            else
            { 
                kk.baslangic = new DateTime(Yil, 7, 1);
                kk.bitis = new DateTime(Yil, 12, 31);
            }
           if( Kontrol(kk)== false)
            {
                return;
            }

             islem.Ekle(kk);


            MessagingCenter.Send<KidemTavan>(kk, "YenileKidemTavan");

            await App.Current.MainPage.Navigation.PopModalAsync();
            //---------

            this.HataMesaji = "";
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


        private bool Kontrol(KidemTavan kk)
        {
            bool _durum = true;

            // O seneden farklı ileri bir tarih mi?
            if(Yil> DateTime.Now.Year)
            {
                _durum = false;
                this.HataMesaji = "Gelecek Bir Yıl Girilmiş";
            }

            // Girilen zaman dilimine ait kayıt var mı?
            var tar1 = kk.baslangic.Date;

            var _sonuc1 = KDList.Find(o => o.baslangic.Date == tar1);
            if(_sonuc1 != null)
            {
                _durum = false;
                this.HataMesaji = "Bu tarihe ait Kayıt Mevcut." +
                    " Bilgilerini Listeden Seçerek Düzenleyebilirsiniz";

            }


            return _durum;

        }

    }
}
