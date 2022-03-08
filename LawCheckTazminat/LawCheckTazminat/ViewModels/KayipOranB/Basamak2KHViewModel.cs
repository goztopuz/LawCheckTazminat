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
    public class Basamak2KHViewModel : ViewModelBase
    {
            public Basamak2KHViewModel(AnaKategori _anaKategori)
                {
            this.ANA = new AnaKategori();

            this.ANA = _anaKategori;
            var aa = new KayipOranAltKategori1();
            Liste = aa.ListeAltKategori1.Where(o=> o.AnaKategoriId == _anaKategori.Id).ToList();

            Baslik1 = _anaKategori.Kategori;

        }

        private AnaKategori _ana;
        public AnaKategori ANA
        {
            get => _ana;
            set
            {
                _ana = value;
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

        private List<AltKategori1> _liste = new List<AltKategori1>();
        public List<AltKategori1> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();


            }
        }
        public ICommand AltKategori1TappedCommand => new Command<AltKategori1>(OnAltKategori1Tapped);

        private async void OnAltKategori1Tapped(AltKategori1 _altKategori1)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            KayipOranHesap KH = new KayipOranHesap();

            KH.AnaKategoriAciklama = ANA.Kategori;
            KH.AnaKategoriId = ANA.Id;

            KH.AltKategori1Id = _altKategori1.Id;
            KH.AltKategori1Aciklama = _altKategori1.Kategori;


            // 3. Sayfaya Geçme Bölümü
            var sayfa = new Views.KayipOranV.Basamak3KH(KH,_altKategori1);
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
