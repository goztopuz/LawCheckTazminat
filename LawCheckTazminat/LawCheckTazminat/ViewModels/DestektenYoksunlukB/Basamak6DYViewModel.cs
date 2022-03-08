using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    public class Basamak6DYViewModel :ViewModelBase
    {
        public Basamak6DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.MasrafListe = new ObservableCollection<Masraf>();
            this.AktifDestek = dyy;

            VerileriYukle(this.AktifDestek.DosyaDestektenYoksunlukMasraf);

            kkk();
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


        public ICommand MasrafTappedCommand => new Command<Masraf>(OnMasrafTapped);

        async private void OnMasrafTapped(Masraf _masraf)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var masrafBilgi = new MasrafEkle(AktifDestek, _masraf);
            await Application.Current.MainPage.Navigation.PushModalAsync(masrafBilgi);

            IsBusy = false;
        }





        private Models.DestektenYoksunluk _destektenYoksunluk;

        public Models.DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;

            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
                Yukseklik = AktifDestek.DosyaDestektenYoksunlukMasraf.Count * 80;
            }
        }

        public void kkk()
        {
            MessagingCenter.Subscribe<ObservableCollection<Masraf>>(this, "MasrafYenile", async (aa) =>
            {
                VerileriYukle(aa);
            });

        }

        private int _yuksekik;
        public int Yukseklik
        {
            get => _yuksekik;
            set
            {
                _yuksekik = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Masraf> _masrafListe;
        public ObservableCollection<Masraf> MasrafListe
        {
            get => _masrafListe;
            set
            {
                _masrafListe = value;
                OnPropertyChanged();
            }
        }

        private void VerileriYukle(ObservableCollection<Masraf> aa)
        {
            ObservableCollection<Masraf> Liste = new ObservableCollection<Masraf>();
         var   Liste2 = aa.ToList();

            MasrafListe.Clear();

            foreach(var t in Liste2)
            {
                MasrafListe.Add(t);

            }

            Yukseklik = MasrafListe.Count * 80;
          

        }


        public ICommand YeniMasrafCommand => new Command(OnYeniMasraf);
        async private void OnYeniMasraf(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            Masraf mm = new Masraf();
            var masrafBilgi = new MasrafEkle(AktifDestek, mm);
            await Application.Current.MainPage.Navigation.PushModalAsync(masrafBilgi);

            IsBusy = false;
        }

        public ICommand MasrafSilCommand => new Command<Masraf>(OnMasrafSil);

        async private void OnMasrafSil(Masraf mm)
        {
            MasrafListe.Remove(mm);

            Yukseklik = MasrafListe.Count * 80;

        }



        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await  Application.Current.MainPage.Navigation.PopModalAsync();
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
            this.AktifDestek.DosyaDestektenYoksunlukMasraf.Clear();

            foreach(var t in MasrafListe)
            {
                this.AktifDestek.DosyaDestektenYoksunlukMasraf.Add(t);
            }

            var sayfa = new Basamak7DYView(this.AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }

        }
}
