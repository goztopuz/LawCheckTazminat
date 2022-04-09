using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class Basamak4DYViewModel : ViewModelBase
    {

        private Models.DestektenYoksunluk _destektenYoksunluk;

        

        public Basamak4DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;
            //this.Yakinlar = this.AktifDestek.DestekYoksunlukYakinlar;

            KisiListe = AktifDestek.DestekYoksunlukYakinlar;

            kkk();
            Yukseklik = (KisiListe.Count * 90) +30;

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


        ObservableCollection<Yakin> _kisiListe = new ObservableCollection<Yakin>();

        public ObservableCollection<Yakin> KisiListe
        {
            get => _kisiListe;
            set
            {
                _kisiListe = value;
                OnPropertyChanged();
                Yukseklik = (KisiListe.Count * 90) +30;
            }
        }


        public Models.DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;

            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
        }

        private List<Yakin> _yakinlar;
        public List<Yakin> Yakinlar
        {
            get => _yakinlar;
            set
            {
                _yakinlar = value;
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

        public void kkk()
        {
            MessagingCenter.Subscribe<ObservableCollection<Yakin>>(this, "Yenile", async (aa) =>
            {
                VerileriAl(aa);
            });

        }

        private void VerileriAl(ObservableCollection<Yakin> aa)
        {

            ObservableCollection<Yakin> Liste2 = new ObservableCollection<Yakin>();

            foreach(var t2 in aa)
            {
                Liste2.Add(t2);
            }

            KisiListe.Clear();
            foreach(var t in Liste2)
            {
                KisiListe.Add(t);
            }

            Yukseklik = (KisiListe.Count * 90) + 30;


        }

        public ICommand YakinSilCommand => new Command<Yakin>(OnYakinSil);
        async private void OnYakinSil(Yakin yy)
        {
            // AktifDestek.DestekYoksunlukYakinlar.Remove(yy);

            KisiListe.Remove(yy);

        }

        public ICommand KisiTappedCommand => new Command<Yakin>(OnKisiTapped);
        async  private void OnKisiTapped(Yakin _yakin)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            

            var yakinBilgi = new YakinKisiEkle(KisiListe, _yakin,AktifDestek);
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


            Yakin yy = new Yakin();
            
            var yakinBilgi = new YakinKisiEkle(KisiListe, yy,AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(yakinBilgi);

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

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            if(AktifDestek.DestekYoksunlukYakinlar.Count== 0)

            {
                await App.Current.MainPage.DisplayAlert("Uyarı", "Yakın Eklenmelidir", "Tamam");
                IsBusy = false;
                return;
            }

            bool bekarOlmaDurumu = true;

            foreach(var t in AktifDestek.DestekYoksunlukYakinlar)
            {
                if(t.yakinlik== "Eş")
                {
                    bekarOlmaDurumu = false;
                }
                if(t.yakinlik == "Çocuk")
                {
                    bekarOlmaDurumu = false;
                }
            }

            if(bekarOlmaDurumu== true)
            {
                AktifDestek.BekarCocukDurum = true;
                var basamak4b = new Basamak4BekarCocukDY(AktifDestek);
                await Application.Current.MainPage.Navigation.PushModalAsync(basamak4b);
            }
            else
            {
                AktifDestek.BekarCocukDurum = false;
                var basamak5 = new Basamak5DYView(AktifDestek);
                await Application.Current.MainPage.Navigation.PushModalAsync(basamak5);
            }
         

            IsBusy = false;

        }


    }
}
