using System;
using System.Collections.Generic;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.KidemIhbarV;
using Xamarin.Forms;
using System.Linq;

namespace LawCheckTazminat.ViewModels.KidemIhbarB
{
    public class KisiKayitlariKIViewModel : ViewModelBase
    {
        string _id = "";
        Services.KidemIhbarService islem = new Services.KidemIhbarService();

        public KisiKayitlariKIViewModel(string _kisiId)
        {

            this.Kayitlar = new List<KidemIhbar>();
            _id = _kisiId;
            this.Kayitlar = islem.KidemListeByKisi(_id);

        }

        private List<KidemIhbar> _kayitlar;
        public List<KidemIhbar> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }
        }

        public ICommand KidemIhbarSilCommand => new Command<string>(OnKidemIhbarSil);

        private async void OnKidemIhbarSil(string idd)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var tmp = Kayitlar.ToList();
            var sonucc = tmp.Find(o => o.Id == idd);
            Kayitlar.Remove(sonucc);

            islem.Sil(idd);

            IsBusy = false;
        }




        public ICommand KayitTappedCommand => new Command<KidemIhbar>(OnKayitTapped);

        async private void OnKayitTapped(KidemIhbar _yy)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var kayit = islem.GetItem(_yy.Id);

         var sonn = new BasamakSonIKView(kayit);
          await Application.Current.MainPage.Navigation.PushModalAsync(sonn);

            IsBusy = false;

        }

        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if (Kayitlar.Count == 0)
            {

                KidemIhbar kidem = new KidemIhbar();
                kidem.Id = "";
                kidem.kisiId = _id;

                var sayfa = new Basamak1IKView(kidem);

                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);




            }
        }

        public ICommand YeniKayitCommand => new Command(OnYeniKayit);

        private async void OnYeniKayit(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //-----
            // IK Wiz1

            //YillikIzin _yillikIzin = new YillikIzin();
            //_yillikIzin.kisiId = _id;

            KidemIhbar kidem = new KidemIhbar();
            kidem.Id = "";
            kidem.kisiId = _id;

            var sayfa = new Basamak1IKView(kidem);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            //------

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
