using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Ayarlar;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class AsgariUcretDuzenleViewModel :ViewModelBase
    {
        AsgariUcretService islem = new AsgariUcretService();

        public AsgariUcretDuzenleViewModel()
        {
            _liste = new ObservableCollection<AsgariUcretTablosu>();
            VerileriCek();
            kkk();
        }

        public ICommand YeniKayitCommand => new Command(OnYeniKayit);      
        async private void OnYeniKayit(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //--------------------------------------------

            AsgariUcretTablosu aa = new AsgariUcretTablosu();
            aa.Id = "";
            var sayfa = new AsgariUcretYeni(Liste, aa);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //----------------------------------------

            IsBusy = false;

        }


        public ICommand AsgariTappedCommand => new Command<AsgariUcretTablosu>(OnAsgariTapped);
        async private void OnAsgariTapped(AsgariUcretTablosu _asgari)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new AsgariUcretYeni(Liste, _asgari);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
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



        private ObservableCollection<AsgariUcretTablosu> _liste;
        public ObservableCollection<AsgariUcretTablosu> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }
        public void kkk()
        {
            MessagingCenter.Subscribe<string>(this, "YenileAsgari", async (aa) =>
            {
                VerileriCek();
            });

        }


        private async void VerileriCek()
        {
            var sonuc = await islem.GetItemsAsync();
            Liste = sonuc.ToObservableCollection();

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
