using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
   public  class Basamak5KHViewModel :ViewModelBase
    {

        public Basamak5KHViewModel(KayipOranHesap kh2)
        {

            this.KH = new KayipOranHesap();
            KH = kh2;


            this.Baslik1 = kh2.AnaKategoriAciklama;
            this.Baslik2 = kh2.AltKategori1Aciklama;
            this.Baslik3 = kh2.AltKategori2Aciklama;
            this.Baslik4 = kh2.AltKategori3Aciklama;

            var aa = new KayipOranIsKolu();
            Liste= aa.Liste;


        }



        private string _baslik1;
        public string Baslik1
        {
            get => _baslik1;
            set
            {
                _baslik1 = value;
                OnPropertyChanged();
            }
        }

        private string _baslik2;
        public string Baslik2
        {
            get => _baslik2;
            set
            {
                _baslik2 = value;
                OnPropertyChanged();
            }
        }

        private string _baslik3;
        public string Baslik3
        {
            get => _baslik3;
            set
            {
                _baslik3 = value;
                OnPropertyChanged();
            }
        }

        private string _baslik4;
        public string Baslik4
        {
            get => _baslik4;
            set
            {
                _baslik4 = value;
                OnPropertyChanged();
            }
        }

        private KayipOranHesap _kh;
        public KayipOranHesap KH
        {
            get => _kh;
            set
            {
                _kh = value;
                OnPropertyChanged();
            }
        }


        private List<AnaIskolu> _liste = new List<AnaIskolu>();
        public List<AnaIskolu> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }

        public ICommand ItemTappedCommand => new Command<AnaIskolu>(OnItemTapped);
        private async void OnItemTapped(AnaIskolu iK)
        {
            if(IsBusy== true)
            {
                return ;
            }
            IsBusy = true;
            // Sonraki Basamağa Geçiş Kodu;
            KH.IsKoluAnaAciklama = iK.Meslek;
            KH.IsKoluAnaId = iK.Id;

            var sayfa =  new Views.KayipOranV.Basamak6KH(KH);
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

            this.HataMesaji = "";
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
