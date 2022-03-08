using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak6FMViewModel: ViewModelBase
    {
        public Basamak6FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;
            kkk();

        }

        public ICommand YeniIzinCommand => new Command(OnYeniIzin);
        async private void OnYeniIzin(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;


            Models.FazlaMesaiKisiIzinKayitlari mRapor = new FazlaMesaiKisiIzinKayitlari();
            mRapor.Id = "";

            var sayfa =  new RaporBilgiView(mRapor, FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;

        }

        private FazlaMesai _fazlamesai;
        public FazlaMesai FM
        {
            get => _fazlamesai;
            set
            {
                _fazlamesai = value;
                OnPropertyChanged();

            }
        }

        //----------------------

        public void kkk()
        {
            MessagingCenter.Subscribe<FazlaMesai>(this, "YenileFM", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }

        private void VerileriYenile(FazlaMesai _fmm)
        {

            //FM = _fmm;

            ObservableCollection<FazlaMesaiKisiIzinKayitlari> Liste = new ObservableCollection<FazlaMesaiKisiIzinKayitlari>();

            foreach (var t in _fmm.IzinKaytilariBilgi)
            {
                Liste.Add(t);
            }

            FM.IzinKaytilariBilgi.Clear();

            foreach (var t in Liste)
            {
                FM.IzinKaytilariBilgi.Add(t);
            }



        }



        public ICommand IzinSilCommand => new Command<FazlaMesaiKisiIzinKayitlari>(OnIzinSil);
        async private void OnIzinSil(FazlaMesaiKisiIzinKayitlari kayit)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;


            FM.IzinKaytilariBilgi.Remove(kayit);

            var silList = FM.IzinGunleriBilgi.ToList();

            foreach(var t in silList)
            {
                if(t.KayitId== kayit.Id)
                {
                    FM.IzinGunleriBilgi.Remove(t);
                }
            }


            IsBusy = false;

        }


        public ICommand IzinTappedCommand => new Command<FazlaMesaiKisiIzinKayitlari>(OnIzinTapped);

        async private void OnIzinTapped(FazlaMesaiKisiIzinKayitlari kayit)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;


            var sayfaIzinBilgi = new Views.FazlaMesaiV.RaporBilgiView(kayit, FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaIzinBilgi);


            IsBusy = false;
        }

        //-------

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var basamak7 = new Basamak7FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak7);

            IsBusy = false;
        }

        //-----------------------------
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
