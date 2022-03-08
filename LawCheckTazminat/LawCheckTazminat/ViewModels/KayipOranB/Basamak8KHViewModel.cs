using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
    public class Basamak8KHViewModel : ViewModelBase
    {

        public Basamak8KHViewModel(KayipOranHesap _kh)
        {
            this.KH= new KayipOranHesap();
            this.KH = _kh;

            this.Baslik1 = _kh.AnaKategoriAciklama;
            this.Baslik2 = _kh.AltKategori1Aciklama;
            this.Baslik3 = _kh.AltKategori2Aciklama;
            this.Baslik4 = _kh.AltKategori3Aciklama;
            this.BaslikMeslek = _kh.IsKoluAnaAciklama;
            this.BaslikMeslekAlt1 = _kh.IsKoluAltDetay1Aciklama;
            this.BaslikMeslekAlt2 = _kh.IsKoluAltDetay2Aciklama;

        }

        private int _yas;
        public int Yas
        {
            get => _yas;
            set
            {
                _yas = value;
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

        private string _baslikMeslekAlt1;
        public string BaslikMeslekAlt1
        {
            get => _baslikMeslekAlt1;
            set
            {
                _baslikMeslekAlt1 = value;
                OnPropertyChanged();
            }
        }

        public string _baslikMeslekAlt2;
        public string BaslikMeslekAlt2
        {
            get => _baslikMeslekAlt2;
            set
            {
                _baslikMeslekAlt2 = value;
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
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            KH.KazaTarihindekiYasi = Yas;
            //Son Hesapalam Sayfası...



            this.HataMesaji = "";
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
