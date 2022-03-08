using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class Basamak2YYViewModel : ViewModelBase
    {
        public Basamak2YYViewModel(YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;
            Liste = new ObservableCollection<YillikIzinIzinKayitlari>();
            VerileriYukle();
            kkk();

        }

        private void VerileriYukle()
        {
            foreach(var t in YY.IzinKaytilariBilgi)
            {
                Liste.Add(t);
            }

        }

        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<YillikIzinIzinKayitlari> _liste;

        public ObservableCollection<YillikIzinIzinKayitlari> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }


        public void kkk()
        {
            MessagingCenter.Subscribe<YillikIzin>(this, "YenileYillikIzinBilgi", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }

        private async void VerileriYenile(YillikIzin aa)
        {

            Liste.Clear();
            foreach (var t in aa.IzinKaytilariBilgi)
            {
                Liste.Add(t);
            }

          

        }



        public ICommand YeniIzinCommand => new Command(OnYeniIzin);

        private async void OnYeniIzin(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            Models.YillikIzinIzinKayitlari mRapor = new YillikIzinIzinKayitlari();
            mRapor.Id = "";

            var sayfa = new YillikIzinEkleDuzenleView(mRapor, YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;
        }

        public ICommand IzinSilCommand => new Command<YillikIzinIzinKayitlari>(OnIzinSil);

        async private void OnIzinSil(YillikIzinIzinKayitlari kayit)
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var liste1 = new ObservableCollection<YillikIzinIzinKayitlari>();
            foreach(var t in YY.IzinKaytilariBilgi)
            {
                liste1.Add(t);
            }


            liste1.Remove(kayit);

            Liste = liste1;

            YY.IzinKaytilariBilgi.Remove(kayit);


            var liste2 = new List<YillikIzinIzinGunleri>();
            liste2 = YY.IzinGunleriBilgi.ToList();

            foreach (var t in liste2)
            {
                if (t.KayitId == kayit.Id)
                {
                    YY.IzinGunleriBilgi.Remove(t);
                }
            }


            IsBusy = false;

        }

        public ICommand IzinTappedCommand => new Command<YillikIzinIzinKayitlari>(OnIzinTapped);

        async private void OnIzinTapped(YillikIzinIzinKayitlari kayit)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            var sayfaIzinBilgi = new Views.YillikIzinV.YillikIzinEkleDuzenleView(kayit, YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaIzinBilgi);


            IsBusy = false;
        }


        public ICommand IlerleCommand => new Command(OnIlerle);

        private async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Basamak3YYView(YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

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


        private string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
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
