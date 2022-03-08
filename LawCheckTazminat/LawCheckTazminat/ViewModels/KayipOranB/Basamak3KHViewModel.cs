using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
    public class Basamak3KHViewModel : ViewModelBase
    {

        public Basamak3KHViewModel(KayipOranHesap kh, AltKategori1 _altkategori1)
        {
            Liste.Clear();
            this.KH = new KayipOranHesap();
            this.KH = kh;

            this.Baslik1 = kh.AnaKategoriAciklama;
            this.Baslik2 = kh.AltKategori1Aciklama;

            var aa = new KayipOranAltKategori2();
            Liste = aa.ListeAltKategori2.Where(o => o.AnaKategoriId == kh.AnaKategoriId 
            && o.AltKategori1Id== _altkategori1.Id).ToList();

            if(Liste.Count==0)
            {
                ListeVar = false;
                ButtonVar = true;

            }
            else
            {
                ListeVar = true;
                ButtonVar = false;
            }

        }

        private bool _listeVar;
        public bool ListeVar
        {
            get => _listeVar;
            set
            {
                _listeVar = value;
                OnPropertyChanged();
            }
           }

        private bool _buttonVar;
        public bool ButtonVar
        {
            get => _buttonVar;
            set
            {
                _buttonVar = value;
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

        private List<AltKategori2> _liste = new List<AltKategori2>();
        public List<AltKategori2> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }


        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;



            KH.AltKategori2Id = "";
            KH.AltKategori2Aciklama = "";

            AltKategori2 alt2 = new AltKategori2();
            alt2.Id = "";
            alt2.Kategori = "";

            // 3. Sayfaya Geçme Bölümü
            var sayfa = new Views.KayipOranV.Basamak4KH(KH, alt2);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;

        }



        public ICommand AltKategori2TappedCommand => new Command<AltKategori2>(OnAltKategori2Tapped);

        private async void OnAltKategori2Tapped(AltKategori2 _altKategori2)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            KayipOranHesap KH = new KayipOranHesap();

    
      
            KH.AltKategori2Id = _altKategori2.Id;
            KH.AltKategori2Aciklama = _altKategori2.Kategori;


            // 4. Sayfaya Geçme Bölümü
            var sayfa = new Views.KayipOranV.Basamak4KH(KH, _altKategori2);
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
