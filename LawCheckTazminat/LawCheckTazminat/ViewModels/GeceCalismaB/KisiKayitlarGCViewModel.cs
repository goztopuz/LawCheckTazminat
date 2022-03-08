using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.GeceCalisma;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;
using System.Linq;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class KisiKayitlarGCViewModel : ViewModelBase
    {

        string _id = "";

        Services.GeceMesaiService islem = new Services.GeceMesaiService();
        public KisiKayitlarGCViewModel(string _kisiId)
        {
            _id = _kisiId;
            KayitlariAl();
        }


        private void KayitlariAl()
        {

            Kayitlar = islem.GetItemsbyKisiId(_id).ToObservableCollection();

        }
        private ObservableCollection<GeceCalisma> _kayitlar;
        public ObservableCollection<GeceCalisma> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }
        }


        public ICommand GCSilCommand => new Command<string>(OnGCSil);

        private async void OnGCSil(string idd)
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


        public ICommand KayitTappedCommand => new Command<GeceCalisma>(OnKayitTapped);

        async private void OnKayitTapped(GeceCalisma _gc)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var kayit =  islem.TekAl(_gc.Id);

            var sonn = new BasamakSonGCView(kayit);
            await Application.Current.MainPage.Navigation.PushModalAsync(sonn);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if (Kayitlar.Count == 0)
            {


                GeceCalisma _geceCalisma = new GeceCalisma();
                _geceCalisma.KisiId = _id;
                var sayfa = new Basamak1GCview(_geceCalisma);
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

            GeceCalisma _geceCalisma = new GeceCalisma();
            _geceCalisma.KisiId = _id;
            var sayfa = new Basamak1GCview(_geceCalisma);
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
