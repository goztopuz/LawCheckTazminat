using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
   public class Basamak2DYViewModel : ViewModelBase
    {

        public Basamak2DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;
            _calisma = dyy.calismaDurumu;
            _askerlik = dyy.askerlikYapti;

        }


        private Models.DestektenYoksunluk _destektenYoksunluk;
        public Models.DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;

            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
        }

        string _calisma;
        public string Calisma
        {
            get => _calisma;
            set
            {
                _calisma = value;
                OnPropertyChanged();
            }
        }
        string _askerlik;
        public string Askerlik
        {
            get => _askerlik;
            set
            {
                _askerlik = value;
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

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy==true)
            {
                return; ;
            }
            
            IsBusy = true;
            var basamak3 = new Basamak3DYView(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak3);
            
            IsBusy = false;


        }

        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
        }

    }


}
