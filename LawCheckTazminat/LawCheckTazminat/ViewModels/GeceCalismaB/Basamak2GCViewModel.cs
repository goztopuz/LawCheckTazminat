using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basamak2GCViewModel : ViewModelBase
    {
        public Basamak2GCViewModel(GeceCalisma _gece)
        {
            this.GC = new GeceCalisma();

            this.GC = _gece;

            if(GC.Id =="" || GC.Id== null)
            {
                var tS1 = new TimeSpan(20, 0, 0);
                var tS2 = new TimeSpan(6, 0, 0);

                Saat1 = tS1;
                Saat2 = tS2;
                DinlenmeSure = 1;

            }
            else
            {
                var tS1 = new TimeSpan(GC.BasSaat.Hour, GC.BasSaat.Minute, 0);
                var tS2 = new TimeSpan(GC.BitSaat.Hour, GC.BitSaat.Minute, 0);

                Saat1 = tS1;
                Saat2 = tS2;
                DinlenmeSure =  GC.DinlenmeSure;

            }
            
        }


        private TimeSpan _saat1;
        public TimeSpan Saat1
        {
            get => _saat1;
            set
            {
                _saat1 = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan _saat2;
        public TimeSpan Saat2
        {
            get => _saat2;
            set
            {
                _saat2 = value;
                OnPropertyChanged();
            }
        }
        private double _dinlenmeSure;
        public double DinlenmeSure
        {
            get => _dinlenmeSure;
            set
            {
                _dinlenmeSure = value;
                OnPropertyChanged();
            }
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


        private bool Kontrol()
        {

            bool durum = true;
            int sayi = 0;

            if(GC.PPzt==true)
            {
                sayi = sayi + 1;
            }
            if(GC.PSal ==true)
            {
                sayi = sayi + 1;
            }
            if(GC.SCar== true)
            {
                sayi = sayi + 1;
            }
            if(GC.CPer==true)
            {
                sayi = sayi + 1;
            }
            if(GC.PCum==true)
            {
                sayi = sayi + 1;
            }
            if(GC.CCmt==true)
            {
                sayi = sayi + 1;
            }
            if(GC.CPzr==true)
            {
                sayi += 1;
            }

            if(sayi== 0)
            {
                this.HataMesaji = "Gece Çalışma Günleri Seçilmelidir.";
                return false;
            }

            
            return durum;

        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;

            }
            IsBusy = true;

            // Basamak3'e Geçiş....

            if(Kontrol()== false)
            {
                IsBusy = false;
                return;
            }

            DateTime mesaiSaat = new DateTime(2020, 12, 18, Saat1.Hours, Saat1.Minutes, 0);

            DateTime mesaiSaatBit = new DateTime(2020, 12, 18, Saat2.Hours, Saat2.Minutes, 0);

            GC.BasSaat = mesaiSaat;
            GC.BitSaat = mesaiSaatBit;
            GC.DinlenmeSure = DinlenmeSure;

            var sayfa = new Views.GeceCalisma.Basamak3GVView(GC);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
           




            this.HataMesaji = "";
            this.IsBusy = false;
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
