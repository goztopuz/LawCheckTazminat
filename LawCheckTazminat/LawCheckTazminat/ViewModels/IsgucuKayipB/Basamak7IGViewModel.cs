using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using System.Windows.Input;
using LawCheckTazminat.Views.IsgucuKayipV;
using System.Collections.ObjectModel;
using System.Linq;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class Basamak7IGViewModel:ViewModelBase
    {
        public Basamak7IGViewModel(Models.IsgucuKayip _kayip)
        {
            this.IsGucu = new IsgucuKayip();
            this.IsGucu = _kayip;

            MasrafListe = _kayip.IsGucuKayipMasraflar;

            Yukseklik = MasrafListe.Count * 90;

            kkk();
        }


        private IsgucuKayip _isgucuKayip;

        public IsgucuKayip IsGucu
        {
            get => _isgucuKayip;
            set
            {
                _isgucuKayip = value;
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

        private int _yukseklik;
        public int Yukseklik
        {
            get => _yukseklik;
            set
            {
                _yukseklik = value;
                OnPropertyChanged();
            }
        }





        public ICommand MasrafTappedCommand => new Command<MasrafIsgucu>(OnMasrafTapped);

        async private void OnMasrafTapped(MasrafIsgucu _masraf)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var masrafBilgi = new MasrafEkleIsgucu(IsGucu, _masraf, MasrafListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(masrafBilgi);

            IsBusy = false;
        }


        public void kkk()
        {
            MessagingCenter.Subscribe<ObservableCollection<MasrafIsgucu>>(this, "YenileMasraf", async (aa) =>
            {
               
                VerileriYukle(aa);
            });

        }
        private ObservableCollection<MasrafIsgucu> _masrafListe;
        public ObservableCollection<MasrafIsgucu> MasrafListe
        {
            get => _masrafListe;
            set
            {
                _masrafListe = value;
                OnPropertyChanged();
            }
        }

        private void VerileriYukle(ObservableCollection<MasrafIsgucu> aa)
        {
            //ObservableCollection<MasrafIsgucu> Liste = new ObservableCollection<MasrafIsgucu>();

            //foreach (var t in aa)
            //{
            //    Liste.Add(t);

            //}

            var aa2 = aa.ToList();
            MasrafListe.Clear();
            foreach (var t in aa2)
            {
               MasrafListe.Add(t);
            }

            Yukseklik = MasrafListe.Count * 90;
            
        }


        public ICommand YeniMasrafCommand => new Command(OnYeniMasraf);
        async private void OnYeniMasraf(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            MasrafIsgucu mm = new MasrafIsgucu();
            var masrafBilgi = new MasrafEkleIsgucu(IsGucu, mm, MasrafListe );
            await Application.Current.MainPage.Navigation.PushModalAsync(masrafBilgi);

            IsBusy = false;
        }

        public ICommand MasrafSilCommand => new Command<MasrafIsgucu>(OnMasrafSil);
        async private void OnMasrafSil(MasrafIsgucu mm)
        {
            MasrafListe.Remove(mm);

            Yukseklik = MasrafListe.Count * 95;

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

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            
            //---------------------------------------------------------
            var sayfa = new Basamak8IGView(this.IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


    }
}
