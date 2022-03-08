using System;
using System.Collections.Generic;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Xamarin.Forms;
using System.Linq;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class KisiKayitlariYYViewModel : ViewModelBase
    {
        string _id = "";
        Services.YillikIzinService islem = new Services.YillikIzinService();

        public KisiKayitlariYYViewModel(string _kisiId)
        {
            this.Kayitlar = new List<YillikIzin>();
            _id = _kisiId;
            this.Kayitlar = islem.GetItemsByKisi(_kisiId);

        }

        private List<YillikIzin> _kayitlar;
        public List<YillikIzin> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }
        }


        public ICommand KayitTappedCommand => new Command<YillikIzin>(OnKayitTapped);

        async private void OnKayitTapped(YillikIzin _yy)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var kayit =  islem.GetItem(_yy.Id);

            var sonn = new BasamakYYSonView(kayit);
            await Application.Current.MainPage.Navigation.PushModalAsync(sonn);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }


        public ICommand YYSilCommand => new Command<string>(OnYYSil);

        private async void OnYYSil(string idd)
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

        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if (Kayitlar.Count == 0)
            {

                YillikIzin _yillikIzin = new YillikIzin();
                _yillikIzin.kisiId = _id;

                var sayfa = new Basamak1YYView(_yillikIzin);

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
            // FM Wiz1

            YillikIzin _yillikIzin = new YillikIzin();
          _yillikIzin.kisiId = _id;

            var sayfa = new Basamak1YYView(_yillikIzin);

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
