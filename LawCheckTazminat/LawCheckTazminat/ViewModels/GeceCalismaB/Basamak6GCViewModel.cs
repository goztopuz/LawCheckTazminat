using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.GeceCalisma;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basamak6GCViewModel :ViewModelBase
    {
        public Basamak6GCViewModel(GeceCalisma _gece)
        {

            this.GC = new GeceCalisma();
            this.GC = _gece;
            kkk();

        }

        public ICommand YeniIzinCommand => new Command(OnYeniIzin);
        async private void OnYeniIzin(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            Models.GeceMesaiKisiIzinKayitlari mRapor = new GeceMesaiKisiIzinKayitlari();
            mRapor.Id = "";

            var sayfa = new IzinBilgiView(mRapor, GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;

        }




        public ICommand IzinTappedCommand => new Command<GeceMesaiKisiIzinKayitlari>(OnIzinTapped);

        async private void OnIzinTapped(GeceMesaiKisiIzinKayitlari kayit)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            var sayfaIzinBilgi = new Views.GeceCalisma.IzinBilgiView(kayit, GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaIzinBilgi);


            IsBusy = false;
        }


        private GeceCalisma _gc;
        public GeceCalisma GC
        {
            get => _gc;
            set
            {
                _gc = value;
                OnPropertyChanged();
            }
        }



        public void kkk()
        {
            
            MessagingCenter.Subscribe<GeceCalisma>(this, "YenileGC", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }


        public ICommand IzinSilCommand => new Command<GeceMesaiKisiIzinKayitlari>(OnIzinSil);
        async private void OnIzinSil(GeceMesaiKisiIzinKayitlari kayit)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            GC.IzinKaytilariBilgi.Remove(kayit);

       

            IsBusy = false;

        }


        private void VerileriYenile(GeceCalisma _fmm)
        {

            //FM = _fmm;

            ObservableCollection<GeceMesaiKisiIzinKayitlari> Liste = new ObservableCollection<GeceMesaiKisiIzinKayitlari>();

            foreach (var t in _fmm.IzinKaytilariBilgi)
            {
                Liste.Add(t);
            }

            GC.IzinKaytilariBilgi.Clear();

            foreach (var t in Liste)
            {
                GC.IzinKaytilariBilgi.Add(t);
            }



        }



        //----İlerleme 

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var basamak7 = new Basamak7GCView(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak7);

            IsBusy = false;
        }


        //-----------------------------
        public System.Windows.Input.ICommand IptalCommand => new Command(OnIptal);
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


    }
}
