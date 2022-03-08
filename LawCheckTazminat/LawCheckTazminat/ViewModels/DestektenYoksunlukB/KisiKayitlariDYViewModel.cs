using JetBrains.Annotations;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
   public class KisiKayitlariDYViewModel :ViewModelBase
    {
        string _id = "";

        DestektenYoksunlukService islem = new DestektenYoksunlukService();
        public KisiKayitlariDYViewModel(string kisi)
        {
            _id = kisi;
            KayitlariAl();
        }
       
        async  private void KayitlariAl()
        {   
            var sonuc = await islem.GetItemsByKisiAsync(_id);

            Kayitlar = sonuc.ToObservableCollection();

         
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


        private ObservableCollection<DestektenYoksunluk> _kayitlar;

        public ObservableCollection<DestektenYoksunluk> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }

        }

        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if (Kayitlar.Count == 0)
            {

                var sayfa = new Wiz1DYView(_id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
        }

        public ICommand DestektenYoksunlukSilCommand => new Command<string>(OnDestektenYoksunlukSil);

        private async void OnDestektenYoksunlukSil(string idd)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var tmp = Kayitlar.ToList();
            var sonucc = tmp.Find(o => o.Id == idd);
            Kayitlar.Remove(sonucc);

            islem.DeleteDestektenYoksunluk(idd);

            IsBusy = false;
        }



        public ICommand YeniKayitCommand=> new Command(OnYeniKayitCommand);
        private async void OnYeniKayitCommand(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //---------------------------------------------------------------
            var sayfa = new Wiz1DYView(_id);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            //-----------------------------------------------------------

            IsBusy = false;

        }
      
        public ICommand IptalCommand => new Command(OnIptalCommand);
        private async void OnIptalCommand(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }


        public ICommand KayitTappedCommand => new Command<DestektenYoksunluk>(OnKayitTapped);


        async private void OnKayitTapped(DestektenYoksunluk _destek)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sonn = new DestekSon(_destek);
            await Application.Current.MainPage.Navigation.PushModalAsync(sonn);

            IsBusy = false;
            
            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }


    }

}

