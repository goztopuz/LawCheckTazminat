using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Ayarlar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
   public class AyarlarListeViewModel : ViewModelBase
    {
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

        public ICommand KidemTavanCommand => new Command(OnKidemTavan);
        async private void OnKidemTavan(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new KidemTavanView();
            await App.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;

        }

        public ICommand ResmiTatillerCommand => new Command(OnResmiTatiller);
        async private void OnResmiTatiller(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new ResmiTatillerView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }



        public ICommand AyarlarCommand => new Command(OnAyarlar);
        async private void OnAyarlar(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new AsgariUcretDuzenle();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }


        public ICommand VergiDilimleriCommand => new Command(OnVergiDilimleri);
        async private void OnVergiDilimleri(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            VergiDilimleri vdd = new VergiDilimleri();
            vdd.Id = 0;


            var sayfa = new VergiDilimleriView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }


        public ICommand YenidenYukleCommand => new Command(OnYenidenYukle);
        async private void OnYenidenYukle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            var sayfa = new Yedek_GeriYukle();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand YedekleCommand => new Command(OnYedekle);
        async private void OnYedekle(object obj)
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            var sayfa = new YedekleView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }

        public ICommand HakkimizdaCommand => new Command(OnHakkimizda);
        async private void OnHakkimizda(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new HakkimizdaView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


        //public ICommand YeniAsgariCommand => new Command(OnYeniAsgari);
        //async private void OnYeniAsgari(object obj)
        //{
        //    if (IsBusy == true)
        //    {
        //        return;
        //    }
        //    IsBusy = true;


        //    AsgariUcretTablosu aa = new AsgariUcretTablosu();

        //    var sayfa = new AsgariUcretYeni( , aa);
        //    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

        //    IsBusy = false;

        //}


    }
}
