using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class KidemTavanViewModel: ViewModelBase
    {

        Services.KidemTavanService islem = new Services.KidemTavanService();
        
        public KidemTavanViewModel()
        {
            this.Liste = new ObservableCollection<KidemTavan>();

            this.Liste = islem.Listele();
            kkk();
        }


        private ObservableCollection<KidemTavan> _liste;
        public ObservableCollection<KidemTavan> Liste
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
            MessagingCenter.Subscribe<KidemTavan>(this, "YenileKidemTavan", async (aa) =>
            {
                VerileriYenile();
            });

        }

        private async void VerileriYenile()
        {
            var liste2 = islem.Listele();

            Liste.Clear();
            foreach (var t in liste2)
            {
                Liste.Add(t);
            }



        }

        public ICommand KayitTappedCommand => new Command<KidemTavan>(OnKayitTapped);
        private async void OnKayitTapped(KidemTavan _kidemTavan)
        {

            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            //---------------

            var sayfa = new KidemTavanDuzenleView(_kidemTavan);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }


        public ICommand SilCommand => new Command<KidemTavan>(OnSil);
        private  void OnSil(KidemTavan kayit)
        {
            islem.Sil(kayit.Id);
            Liste.Clear();
            var sonuc1 = islem.Listele();

            foreach(var t in sonuc1)
            {
                Liste.Add(t);
            }
              
        }

        public ICommand YeniKayitCommand => new Command(OnYeniKayit);
        private async void OnYeniKayit(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new KidemTavanYeni(this.Liste.ToList());

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
