using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Destek;
using Xamarin.Forms;

using Xamarin.Essentials;

namespace LawCheckTazminat.ViewModels.Destek
{
    public class DestekListeleViewModel : ViewModelBase
    {

        DestekTalepService islem = new DestekTalepService();

        public DestekListeleViewModel()
        {

                    kkk();
                    VerileriCek();

        }

     async   private void VerileriCek()
        {

            if(Connectivity.NetworkAccess== NetworkAccess.Internet)
            {
                Sorularim = islem.SorulariAl();
                List<IdBilgi> liste1 = new List<IdBilgi>();
                foreach (var t in Sorularim)
                {
                    IdBilgi ii = new IdBilgi();
                    ii.Id = t.Id;
                    ii.Id2 = "LawCheck1.0";
                    liste1.Add(ii);
                }
                Yanitlar = await islem.CevaplariAl(liste1);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hata", "İnternet Bağlantı Hatası", "Tamam");
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        

        }

        public void kkk()
        {
            MessagingCenter.Subscribe<DestekTalep>(this, "YenileDestekListele", async (aa) =>
            {
              VerileriCek();
            });

        }

        private ObservableCollection<DestekTalep> _sorularim;
        public  ObservableCollection<DestekTalep> Sorularim
        {
            get => _sorularim;
            set
            {
                _sorularim = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DestekYanit> _yanitlar;
        public ObservableCollection<DestekYanit> Yanitlar
        {
            get => _yanitlar;
            set
            {
                _yanitlar = value;
                OnPropertyChanged();
            }

        }

        public ICommand SoruTappedCommand => new Command<DestekTalep>(OnSoruTapped);

        async private void OnSoruTapped(DestekTalep dd)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new SoruGoruntule(dd);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand YanitTappedCommand => new Command<DestekYanit>(OnYanitTapped);

        async private void OnYanitTapped(DestekYanit obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new YanitGoruntuleView(obj);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


        public ICommand YeniDestekCommand => new Command(OnYeniDestek);
        async private void OnYeniDestek(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new YeniDestekView();
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
