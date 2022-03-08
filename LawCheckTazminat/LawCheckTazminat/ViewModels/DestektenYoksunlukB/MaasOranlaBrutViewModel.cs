using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
     public   class MaasOranlaBrutViewModel : ViewModelBase
    {


        public MaasOranlaBrutViewModel(DestektenYoksunluk dY, List<AsgariUcretTablosu> aL)
        {
            Ay = dY.vefatTarihi.Month;
            Yil = dY.vefatTarihi.Year;

            int _donem = 1;
            if (Ay > 6)
            {
                _donem = 2;
            }
            OAykiAsgari = aL.Find(o => o.yil2 == Yil && o.donem == _donem).brut;
        }

        int _yil;
        public int Yil
        {
            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }

        int _ay;
        public int Ay
        {
            get => _ay;
            set
            {
                _ay = value;
                OnPropertyChanged();
            }
        }

        decimal _oAykiAsgari;
        public decimal OAykiAsgari
        {
            get => _oAykiAsgari;
            set
            {
                _oAykiAsgari = value;
                OnPropertyChanged();
            }
        }

        decimal _olayTarihiMaas;
        public decimal OlayTarihiMaas
        {
            get => _olayTarihiMaas;
            set
            {
                _olayTarihiMaas = value;
                OnPropertyChanged();
            }
        }

        decimal _oran;
        public decimal Oran
        {
            get => _oran;
            set
            {
                _oran = value;
                OnPropertyChanged();
            }
        }

        public ICommand OranlaCommand => new Command(OnOranla);
        async private void OnOranla(object obj)
        {
            if (IsBusy == true)
            {
                return; ;
            }

            IsBusy = true;
            decimal _oran2;
            _oran2 = OlayTarihiMaas / OAykiAsgari;
            Oran = _oran2;
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
 
        public ICommand KullanCommand => new Command(OnKullan);

        async private void OnKullan(object obj)
        {
            if (IsBusy == true)
            {
                return; 
            }

            IsBusy = true;

            var ao = new AsgariOran();
            ao.maas = OlayTarihiMaas;
            ao.oran = Oran;
            MessagingCenter.Send<AsgariOran>(ao, "OranlaBrutAsgari");
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;


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
