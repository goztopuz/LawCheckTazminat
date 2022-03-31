using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.IsgucuKayipV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
  public  class Basamak5IGViewModel: ViewModelBase
    {

        public Basamak5IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = _kayip;

            if(_kayip.hastaneYatisi == "Var")
            {
                HastaneVisible = true;
            }

            if(IsGucu.IsGucuKayipOranlar.Count>0)
            {
                KayipOranlar = IsGucu.IsGucuKayipOranlar;

            }

            kkk();

        }

        ObservableCollection<KayipOran> _kayipOranlar = new ObservableCollection<KayipOran>();

        public ObservableCollection<KayipOran> KayipOranlar
        {
            get => _kayipOranlar;
            set
            {
                _kayipOranlar = value;
                OnPropertyChanged();
                Yukseklik = KayipOranlar.Count * 90;
            }
        }

        private IsgucuKayip _isGucu;
        public IsgucuKayip IsGucu
        {
            get => _isGucu;

            set
            {
                _isGucu = value;
                OnPropertyChanged();

            }
        }

        

        bool _hastaneVisible = false;
        public bool HastaneVisible
        {
            get => _hastaneVisible;

            set
            {
                _hastaneVisible = value;
                OnPropertyChanged();
            }
        }

        private int _yukseklik=100;
        public int Yukseklik
        {
            get => _yukseklik;
            set
            {
                _yukseklik = value;
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



        // -------KayıpOran Tapped----------
        public ICommand KayipTappedCommand => new Command<KayipOran>(OnKayipTapped);

        async private void OnKayipTapped(KayipOran _kayip)
        {
            if (IsBusy == true)
            {
                return;
            }

            if(_kayip.aciklama !="Ek")
            {
                return;
            }
            

            IsBusy = true;

            var kayipSayfa = new Views.IsgucuKayipV.YeniKayip(IsGucu, _kayip, KayipOranlar);
            await Application.Current.MainPage.Navigation.PushModalAsync(kayipSayfa);

            IsBusy = false;
        }

        public void kkk()
        {
            MessagingCenter.Subscribe<ObservableCollection<KayipOran>>(this, "YenileKayip", async (aa) =>
            {
                VerileriYukle(aa);
            });

        }

        private  void VerileriYukle(ObservableCollection<KayipOran> aa)
        {

            KayipOranlar.Clear();

            var aa2 = aa.ToList().OrderBy(o=> o.baslangicTarihi);

            foreach(var t2 in aa2)
            {
                KayipOranlar.Add(t2);
            }

            Yukseklik = KayipOranlar.Count * 90;

            //ObservableCollection<KayipOran> Liste = new ObservableCollection<KayipOran>();

            //foreach(var t in aa)
            //{
            //    Liste.Add(t);
            //}
            //IsGucu.IsGucuKayipOranlar.Clear();


            //foreach(var t2 in Liste)
            //{
            //    IsGucu.IsGucuKayipOranlar.Add(t2);
            //}
           
        }

        //------------------------------------

        public ICommand SilKayipCommand => new Command<KayipOran>(OnKayipOranSil);

        async private void OnKayipOranSil(KayipOran kk)
        {


            KayipOranlar.Remove(kk);

            Yukseklik = KayipOranlar.Count * 90;

            //if (kk.aciklama =="Ek")
            //{
            //    IsGucu.IsGucuKayipOranlar.Remove(kk);

            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Silme Uyarı", "Bu Veri Silinemez. " +
            //        "Bir Önceki sayfadan düzenlebilir", "Tamam");

            //    return;
            //}

        }
          

        //-------------------------
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


        //------------------------------
        public ICommand YeniKayipCommand => new Command(OnYeniKayip);

        async private void OnYeniKayip(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            // YENİ KAYIP EKRAN.....................

            KayipOran orann = new KayipOran();
            orann.id = "";
            var yakinBilgi = new Views.IsgucuKayipV.YeniKayip(IsGucu, orann, KayipOranlar);
            await Application.Current.MainPage.Navigation.PushModalAsync(yakinBilgi);

            IsBusy = false;


        }


        //-----------------------------------
        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;



            // İLERLE COMMAND...............................
            this.IsGucu.IsGucuKayipOranlar.Clear();
            foreach(var t in KayipOranlar)
            {
                this.IsGucu.IsGucuKayipOranlar.Add(t);
            }

            this.HataMesaji = "";
            var sayfa = new Basamak7IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            
            IsBusy = false;


        }



        }
}
