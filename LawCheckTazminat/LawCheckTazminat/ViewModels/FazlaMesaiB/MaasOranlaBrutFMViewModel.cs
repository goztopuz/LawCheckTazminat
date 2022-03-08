using System;
using System.Collections.Generic;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class MaasOranlaBrutFMViewModel : ViewModelBase
    {
        List<AsgariUcretTablosu> aL2 = new List<AsgariUcretTablosu>();
        public MaasOranlaBrutFMViewModel(FazlaMesai _fm, List<AsgariUcretTablosu> aL)
        {
            aL2 = aL;
            this.UcretYil = DateTime.Now.Year;
            this.UcretAy = DateTime.Now.Month;
        }

        private decimal _brutMaas;
        public decimal BrutMaas
        {
            get => _brutMaas;
            set
            {
                _brutMaas = value;
                OnPropertyChanged();
            }
        }

        private decimal _oAykiAsgari;
        public decimal OAykiAsgari
        {
            get => _oAykiAsgari;
            set
            {
                _oAykiAsgari = value;
                OnPropertyChanged();
            }
        }


        private int _ucretYil;
        public int UcretYil
        {
            get => _ucretYil;
            set
            {
                _ucretYil = value;
                OnPropertyChanged();
            }
        }

        private int _ucretAy;
        public int UcretAy
        {
            get => _ucretAy;
            set
            {
                _ucretAy = value;
            }
        }


        public decimal _oran;
        public Decimal Oran
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
                return; 
            }


           

            IsBusy = true;
            decimal _oran2;
            int _donem = 1;
            if (UcretAy > 6)
            {
                _donem = 2;
            }
            OAykiAsgari = aL2.Find(o => o.yil2 == UcretYil && o.donem == _donem).brut;
            _oran2 = BrutMaas / OAykiAsgari;
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
            ao.maas = BrutMaas;
            ao.oran = Oran;
            MessagingCenter.Send<AsgariOran>(ao, "OranlaBrutAsgariFM");
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
