using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using LawCheckTazminat.Views.IsgucuKayipV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class Basamak2IGViewModel :ViewModelBase
    {
        public Basamak2IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = new IsgucuKayip();
            this.IsGucu = _kayip;

        }

        private IsgucuKayip _isgucu;
        public IsgucuKayip IsGucu
        {
            get => _isgucu;
            set
            {
                _isgucu = value;
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

        private bool SayfaKontrol()
        {
            bool _durum = true;

            if(IsGucu.askerlikYapti== "YapMAdı")
            {
               DateTime _askereGidis = new DateTime(IsGucu.askereGidisYil, IsGucu.askereGidisAy, 1);

                if(IsGucu.dogumTarihi>= _askereGidis)
                {

                    this.HataMesaji = "Doğum Tarihi Askere Gidiş Tarihinden Sonra";
                    _durum = false;
                }
                if(IsGucu.dogumTarihi.AddYears(19)< _askereGidis)
                {
                    this.HataMesaji = "Askere Gidiş Tarihi Hatalı(20) Yaş.";
                    _durum = false;
                }
                if(IsGucu.kazaTarihi> _askereGidis)
                {
                    this.HataMesaji = "Askere Gidiş Kaza Tarihinden Önce.";
                    _durum = false;

                }

            }



            return _durum;
        }


        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return; ;
            }

            IsBusy = true;


            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }

            this.HataMesaji = "";
            var basamak3 = new Basamak3IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak3);
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
