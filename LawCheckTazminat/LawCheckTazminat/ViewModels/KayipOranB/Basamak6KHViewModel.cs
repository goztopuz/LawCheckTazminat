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
    public class Basamak6KHViewModel : ViewModelBase
    {
        public Basamak6KHViewModel(KayipOranHesap kH)
        {
            this.KH = new KayipOranHesap();
            KH = kH;


            this.Baslik1 = kH.AnaKategoriAciklama;
            this.Baslik2 = kH.AltKategori1Aciklama;
            this.Baslik3 = kH.AltKategori2Aciklama;
            this.Baslik4 = kH.AltKategori3Aciklama;
            this.BaslikMeslek = kH.IsKoluAnaAciklama;


            var aa = new KayipOranAltIsKolu1();

            Liste.Clear();
            Liste = aa.Liste.Where(o => o.AnaIsKolu == kH.IsKoluAltDetay1Id).ToList();

            if (Liste.Count == 0)
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

        private List<IsKoluAltDetay1> _liste = new List<IsKoluAltDetay1>();
        public List<IsKoluAltDetay1> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
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

        private string _baslikMeslek;
        public string BaslikMeslek
        {
            get => _baslikMeslek;
            set
            {
                _baslikMeslek = value;
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


        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            KH.IsKoluAltDetay1Id = "";
            KH.IsKoluAltDetay1Aciklama = "";

            //Sonraki Sayfa

            IsBusy = false;

        }

        public ICommand ItemTappedCommand => new Command<IsKoluAltDetay1>(OnItemTapped);
        private async void OnItemTapped(IsKoluAltDetay1 isAlt1)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            KH.IsKoluAltDetay1Id = isAlt1.Id;
            KH.IsKoluAltDetay1Aciklama = isAlt1.Meslek;
            // Sonraki Sayfa
            

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
