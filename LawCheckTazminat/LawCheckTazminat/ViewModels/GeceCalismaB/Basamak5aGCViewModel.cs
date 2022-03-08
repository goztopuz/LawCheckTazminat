using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.GeceCalisma;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    class Basamak5aGCViewModel : ViewModelBase
    {
        public Basamak5aGCViewModel(GeceCalisma _gece)
        {
            this.GC = new GeceCalisma();
            this.GC = _gece;


            Bekar = GC.Bekar;
            EsCalisiyor = GC.EsCalisiyor;
            CocukSayisi = GC.CocukSayisi;

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



        private string _bekar;
        public string Bekar
        {
            get => _bekar;
            set
            {
                _bekar = value;
                OnPropertyChanged();
            }
        }

        private string _esCalisiyor;
        public string EsCalisiyor
        {
            get => _esCalisiyor;
            set
            {
                _esCalisiyor = value;
                OnPropertyChanged();
            }
        }

        private int _cocukSayisi;
        public int CocukSayisi
        {
            get => _cocukSayisi;
            set
            {
                _cocukSayisi = value;
                OnPropertyChanged();
            }
        }


        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            // İLERLEME İŞLEMİ
            GC.Bekar = Bekar;
            GC.EsCalisiyor = EsCalisiyor;
            GC.CocukSayisi = CocukSayisi;

            var basamak5 = new Basamak5GCView(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak5);

            IsBusy = false;
        }

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
