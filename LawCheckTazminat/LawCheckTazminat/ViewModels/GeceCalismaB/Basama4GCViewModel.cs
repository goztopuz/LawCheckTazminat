using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.GeceCalisma;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basama4GCViewModel: ViewModelBase
    {
        public Basama4GCViewModel(GeceCalisma _gece)
        {

            this.GC = new GeceCalisma();
            this.GC = _gece;

            HaftalikHesap();

            if(GC.Id !="" && GC.Id !=  null)
            {
                VergiYil = GC.VergiYili;
                Matrah = GC.VergiMatrah;
            }
            else
            {
                VergiYil = GC.BitTarih.Year;
            }
        }

            //   GeceFazlaSaat
            //  GeceFazlaSaatElle
        private void HaftalikHesap()
        {

            DinlenmeSaat = GC.DinlenmeSure;

            int saat1 = GC.BasSaat.Hour;
            int dk1 = GC.BasSaat.Minute;
            int saat2 = GC.BitSaat.Hour;
            int dk2 = GC.BitSaat.Minute;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            CalisilanSaat = saat;

            GeceFazlaSaat= saat - (7.5 +DinlenmeSaat);
            GeceFazlaSaatElle = saat - (7.5 + DinlenmeSaat);
            if (GeceFazlaSaat<0)
            {
                GeceFazlaSaat = 0;
            }

            if(GeceFazlaSaatElle<0)
            {
                GeceFazlaSaatElle = 0;
            }

        }
        private double _a1=55.8;
        public double A1
        {
            get => _a1;
            set
            {
                _a1 = value;
                OnPropertyChanged();
            }
        }

        private double _calisilanSaat;
        public double CalisilanSaat
        {
            get => _calisilanSaat;
            set
            {
                _calisilanSaat = value;
                OnPropertyChanged();
            }
        }

        private double _dinlenmeSaat;
        public double DinlenmeSaat
        {
            get => _dinlenmeSaat;
            set
            {
                _dinlenmeSaat = value;
                OnPropertyChanged();
            }
        }

        private double _geceFazlaSaatElle;
        public double GeceFazlaSaatElle
        {
            get => _geceFazlaSaatElle;
            set
            {
                _geceFazlaSaatElle = value;
                OnPropertyChanged();

            }
        }
        private double _geceFazlaSaat;
        public double GeceFazlaSaat
        {
            get => _geceFazlaSaat;
            set
            {
                _geceFazlaSaat = value;
                OnPropertyChanged();
            }
        }


        private int _vergiYil;
        public int VergiYil
        {
            get => _vergiYil;
            set
            {
                _vergiYil = value;
                OnPropertyChanged();
            }
        }

        private decimal _matrah;
        public decimal Matrah
        {
            get => _matrah;
            set
            {
                _matrah = value;
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

            if(GC.GeceFazlaSaatElle<0)
            {
                this.HataMesaji = "Değer (0)Sıfırdan Küçü Olamaz.";
                return false;
            }

            return durum;
        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy==true)
            {
                return;
           }
            IsBusy = true;

            //İlerle...........
            if(Kontrol()==false)
            {
                IsBusy = false;
                return;
            }
            GC.GeceFazlaSaatElle = GeceFazlaSaatElle;
            GC.GeceFazlaSaat = GeceFazlaSaat;
            GC.VergiYili = VergiYil; ;
            GC.VergiMatrah = Matrah;


            //var sayfa = new Basamak5aGCView(GC);

            var sayfa = new Basamak5GCView(GC);
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
