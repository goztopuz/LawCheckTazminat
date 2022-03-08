using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using System.Collections.ObjectModel;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;
using System.Windows.Input;
using LawCheckTazminat.Views.IsgucuKayipV;
using System.Linq;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class KisiKayitlariIGViewModel :ViewModelBase
    {

        Services.IsGucuKayipService islem = new Services.IsGucuKayipService();

        string _id = "";
        public KisiKayitlariIGViewModel(string  kisi)
        {
            _id = kisi;

            KayitlariAl();
        }

        async private void KayitlariAl()
        {
            var sonuc = await islem.KisiKayitlari(_id);
            Kayitlar = sonuc.ToObservableCollection();
          
        }



        private ObservableCollection<IsgucuKayip> _kayitlar;

        public ObservableCollection<IsgucuKayip> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }

        }


        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if(Kayitlar.Count == 0)
            {

                _isgucu.musteriId = _id;
                var sayfa = new Views.IsgucuKayipV.Basamak1IGView(_isgucu);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
        }
        public ICommand YeniKayitCommand => new Command(OnYeniKayit);
        IsgucuKayip _isgucu = new IsgucuKayip();

        private async void OnYeniKayit(object obj)
        {


            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //---------------------------------------------------------------


            //var sayfa = new Wiz1DYView(_id);


            _isgucu.musteriId = _id;
            var sayfa = new Views.IsgucuKayipV.Basamak1IGView(_isgucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            //-----------------------------------------------------------

            IsBusy = false;

        }
     

        public ICommand IsgucuSilCommand => new Command<string>(OnIsgucuSil);
        private async void OnIsgucuSil(string  idd)
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


        public ICommand IptalCommand => new Command(OnIptalCommand);
        private async void OnIptalCommand(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }


        public ICommand KayitTappedCommand => new Command<IsgucuKayip>(OnKayitTapped);
        async private void OnKayitTapped(IsgucuKayip _isgucuKayipp)
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;



            var sonn = new DestekSonIsgucu(_isgucuKayipp);

            await Application.Current.MainPage.Navigation.PushModalAsync(sonn);


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
