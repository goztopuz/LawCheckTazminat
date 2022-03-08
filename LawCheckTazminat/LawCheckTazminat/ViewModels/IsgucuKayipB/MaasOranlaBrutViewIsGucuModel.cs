using System;
using System.Collections.Generic;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class MaasOranlaBrutViewIsGucuModel:ViewModelBase
    {
        public MaasOranlaBrutViewIsGucuModel(IsgucuKayip _kayip, List<AsgariUcretTablosu> aL)
        {
            Ay = _kayip.kazaTarihi.Month;
            Yil = _kayip.kazaTarihi.Year;

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
            if (IsBusy == true)
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
                return; ;
            }

            IsBusy = true;

            //-------------------------------


            var ao = new AsgariOran();
            ao.maas = OlayTarihiMaas;
            ao.oran = Oran;
            MessagingCenter.Send<AsgariOran>(ao, "OranlaBrutAsgariIsGucu");
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
