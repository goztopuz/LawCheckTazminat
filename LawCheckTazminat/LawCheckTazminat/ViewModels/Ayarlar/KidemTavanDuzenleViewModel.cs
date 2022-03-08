using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class KidemTavanDuzenleViewModel : ViewModelBase
    {

        KidemTavanService islem = new KidemTavanService();

        List<KidemTavan> liste = new List<KidemTavan>();
        public KidemTavanDuzenleViewModel()
        {

            liste = islem.Listele().ToList();
        }

        private DateTime _baslangic;
        public DateTime Baslangic
        {
            get => _baslangic;
            set
            {
                _baslangic = value;
                OnPropertyChanged();
            }
        }

        private DateTime _bitis;
        public DateTime Bitis
        {
            get => _bitis;
            set
            {
                _bitis = value;
                OnPropertyChanged();
            }
        }

        private decimal _miktar;
        public decimal Miktar
        {
            get => _miktar;
            set
            {
                _miktar = value;
                OnPropertyChanged();
            }
        }

        bool Kontrol()
        {
            bool _durum = true;

            //Kayıtlar Aynı Yılda Olmalıdır.

            if (Baslangic.Year!= Bitis.Year)
            {
                Hata = "Başlangıç ve Bitiş Aynı Yıl İçerisinde Olmalıdır.";
                return false;
            }

                //Önce- Sonra
            if(Bitis<Baslangic)
            {
                Hata = "Başlangıç - Bitiş Tarihinden Sonra";
                return false;
            }

                // Miktar Önceki  Yıldan Düşük- Yüksek Ayarıı

                // Kontrol.


                return _durum;
        }

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            //-----

            if(Kontrol()== false)
            {
                IsBusy = false;

                return;
            }
            KidemTavan _kayit = new KidemTavan();

            _kayit.Id = Guid.NewGuid().ToString().Substring(0, 8);
            _kayit.baslangic = Baslangic;
            _kayit.bitis = Bitis;
            _kayit.yil = Baslangic.Year;
            _kayit.miktar = Miktar;

            islem.Ekle(_kayit);

            MessagingCenter.Send<KidemTavan>(_kayit, "YenileKidemTavan");

            await Application.Current.MainPage.Navigation.PopModalAsync();


            this.Hata = "";
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

        private string _hata;
        public string Hata
        {
            get => _hata;
            set
            {
                _hata = value;
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
