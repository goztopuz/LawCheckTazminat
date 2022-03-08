using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Ayarlar;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class VerhgiDilimleriViewModel : ViewModelBase
    {


        Services.VergiDilimlerService islem = new Services.VergiDilimlerService();
        List<VergiDilimleri> liste1 = new List<VergiDilimleri>();

        public VerhgiDilimleriViewModel()
        {

            this.Yil = DateTime.Now.Year.ToString();


            this.YillarListe = new ObservableCollection<Yillar>();


            VerileriYenile();
        }

        private void VerileriYenile()
        {
            Liste =  islem.GetItems().ToObservableCollection();
            List<Yillar> tmpYillar = new List<Yillar>();
            tmpYillar.Clear();


          

            foreach (var tt in Liste)
            {

                Yillar yy = new Yillar();
                yy.Yill = tt.Id.ToString();
                yy.Yil2 = tt.Id;
                tmpYillar.Add(yy);

            }
            var tmpYil2 = tmpYillar.OrderByDescending(o => o.Yil2);
            foreach (var t2 in tmpYil2)
            {
                var k1 = YillarListe.ToList().Find(o => o.Yil2 == t2.Yil2);
                if (k1 == null)
                {
                    YillarListe.Add(t2);
                }
            }

         

        }

        private ObservableCollection<VergiDilimleri> _liste;
        public ObservableCollection<VergiDilimleri> Liste
        {
            get=> _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }

        }

        private ObservableCollection<Yillar> _yillarListe;
        public ObservableCollection<Yillar> YillarListe
        {
            get => _yillarListe;
            set
            {
                _yillarListe = value;
                OnPropertyChanged();
            }
        }


        private decimal _mik1;
        public decimal Mik1
        {
            get => _mik1;
            set
            {
                _mik1 = value;
                OnPropertyChanged();
            }
        }

        private decimal _mik2;
        public decimal Mik2
        {
            get => _mik2;
            set
            {
                _mik2 = value;
                OnPropertyChanged();
            }
        }

        private decimal _mik3;
        public decimal Mik3
        {
            get => _mik3;

            set
            {
                _mik3 = value;
                OnPropertyChanged();
            }
        }
        private VergiDilimleri _secili;
        public VergiDilimleri Secili
        {
            get => _secili;
            set
            {
                _secili = value;
                OnPropertyChanged();
            }
        }
        private string _yil;
        public string Yil
        {

            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }

        private Yillar _yil2;
        public Yillar Yil2
        {
            get => _yil2;
            set
            {
                _yil2 = value;
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


        public ICommand SecCommand => new Command(OnSec);
        async private void OnSec(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }

            if(Yil2== null)
            {
                return;
            }
            IsBusy = true;

            int yill2 = Convert.ToInt16(Yil2.Yil2);

            var abc = Liste.ToList().Find(o => o.Id == yill2);


            Mik1 = (decimal) abc.Vd1Miktar;
            Mik2 = (decimal) abc.Vd2Miktar;
            Mik3 =  (decimal) abc.Vd3Miktar;


            IsBusy = false;
        }

        public ICommand VergiDilimiSilCommand => new Command(OnVergiDilimiSil);
        async private void OnVergiDilimiSil()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            // Silme İşlemi .....

            IsBusy = false;
        }

        public ICommand YeniVergiBilgiCommand => new Command(OnYeniVergiBilgi);
        async private void OnYeniVergiBilgi()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;


            VergiDilimleri _vd = new VergiDilimleri();
            _vd.Id = 0;
            var sayfa = new VergiDilimiDetayView(_vd,Liste);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


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
    }
}
