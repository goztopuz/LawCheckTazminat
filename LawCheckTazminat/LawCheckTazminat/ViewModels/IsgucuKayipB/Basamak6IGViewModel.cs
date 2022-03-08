using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using LawCheckTazminat.Views.IsgucuKayipV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class Basamak6IGViewModel:ViewModelBase
    {
        public Basamak6IGViewModel(IsgucuKayip _kayip)
        {

            this.IsGucu = _kayip;

            KisiListe = _kayip.IsGucuKayipYakinlar;

            kkk();

            
        }



        private int _yukseklik;
        public int Yukseklik
        {
            get => _yukseklik;
            set
            {
                _yukseklik = value;
                OnPropertyChanged();
            }
        }


        ObservableCollection<YakinIsgucu> _kisiListe = new ObservableCollection<YakinIsgucu>();

        public ObservableCollection<YakinIsgucu> KisiListe
        {
            get => _kisiListe;
            set
            {
                _kisiListe = value;
                OnPropertyChanged();
                Yukseklik = KisiListe.Count * 90;
            }
        }



        private IsgucuKayip _isgucukayip;
        public IsgucuKayip IsGucu
        {
            get => _isgucukayip;
            set
            {
                _isgucukayip = value;
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

        public void kkk()
        {
            MessagingCenter.Subscribe<ObservableCollection<YakinIsgucu>>(this, "YenileYakin", async (aa) =>
            {
                VerileriAl(aa);
            });

        }

        private void VerileriAl(ObservableCollection<YakinIsgucu> aa)
        {

            //  ObservableCollection<YakinIsgucu> Liste2 = new ObservableCollection<YakinIsgucu>();
            var liste2 = aa.ToList();
            //foreach (var t2 in aa)
            //{
            //    Liste2.Add(t2);
            //}

            KisiListe.Clear();
            foreach (var t in liste2)
            {
                KisiListe.Add(t);
            }

            Yukseklik = KisiListe.Count * 95;


        }



        public ICommand YakinSilCommand => new Command<YakinIsgucu>(OnYakinSil);
        async private void OnYakinSil(YakinIsgucu yy)
        {
            // AktifDestek.DestekYoksunlukYakinlar.Remove(yy);

            //KisiListe.Remove(yy);

            KisiListe.Remove(yy);

            Yukseklik = KisiListe.Count * 95;



        }

        public ICommand KisiTappedCommand => new Command<YakinIsgucu>(OnKisiTapped);
        async private void OnKisiTapped(YakinIsgucu _yakin)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


           var yakinBilgi = new LawCheckTazminat.Views.IsgucuKayipV.YakinKisiEkle(IsGucu, _yakin,KisiListe);
           await Application.Current.MainPage.Navigation.PushModalAsync(yakinBilgi);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }



        public ICommand YeniKisiCommand => new Command(OnYeniKisi);
        async private void OnYeniKisi(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            Models.YakinIsgucu yy = new  YakinIsgucu();
            yy.yakinlik = "Çocuk";

            var yakinBilgi = new LawCheckTazminat.Views.IsgucuKayipV.YakinKisiEkle(IsGucu, yy, KisiListe);
           await Application.Current.MainPage.Navigation.PushModalAsync(yakinBilgi);

            IsBusy = false;

        }

        //********************************

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

        //***************************

      
     
        //*********************************************
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var basamak= new Basamak7IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak);

            IsBusy = false;

        }



    }
}
