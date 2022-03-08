using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basmak1GCViewModel :ViewModelBase
    {

        public Basmak1GCViewModel(GeceCalisma _gece )
        {
            this._gc = new GeceCalisma();
            this._gc = _gece;

            if (GC.duzenlemede == false)
            {
                GC.BasTarih = DateTime.Now;
                GC.BitTarih = DateTime.Now;
            }
        }

        private bool Kontrol()
        {
            bool durum = true;
            if (GC.BasTarih > GC.BitTarih)
            {
                this.HataMesaji = "Başlangıç Bitiş Tarihinden Sonra";
                durum = false;
            }
            if (GC.BitTarih.Date.Year > DateTime.Now.Year)
            {
                this.HataMesaji = "Bitiş Tarihi Gelecek Bir Tarih";
                durum = false;
            }


            return durum;
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


        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            if (Kontrol() == false)
            {
                IsBusy = false;
                return;

            }

            this.HataMesaji = "";

            var basamak2 = new LawCheckTazminat.Views.GeceCalisma.Basama2GCView(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak2);

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

        //---------------------------------------------------

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
