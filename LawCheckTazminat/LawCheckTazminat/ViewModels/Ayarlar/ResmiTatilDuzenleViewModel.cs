using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class ResmiTatilDuzenleViewModel: ViewModelBase
    {

        LawCheckTazminat.Services.ResmiTatilService islem = new Services.ResmiTatilService();

        public ResmiTatilDuzenleViewModel(ResmiTatiller _tatil, ObservableCollection<ResmiTatiller> Liste)
        {
            this.TatilGunu = new ResmiTatiller();
            this.TatilGunu = _tatil;

            Tarih = DateTime.Now;
            this._liste = Liste;

        }

    

        private ResmiTatiller _tatilGunu;
        public ResmiTatiller TatilGunu
        {
            get => _tatilGunu;
            set
            {
                _tatilGunu = value;
                OnPropertyChanged();
            }
        }


        private DateTime _tarih;

        public DateTime Tarih
        {
            get => _tarih;
            set
            {
                _tarih = value;
                OnPropertyChanged();

            }
        }

        private string _aciklama;
        public string Aciklama
        {
            get => _aciklama;
            set
            {
                _aciklama = value;
                OnPropertyChanged();

            }
        }



        public ObservableCollection<ResmiTatiller> _liste;

        // Kaydet---------
        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            // İşlemler

            // Kayıtlı Bir Tarih Olup Olmadığını Kontrol Et.

            var kayitVar = _liste.ToList().Find(o => o.tarih.Date == Tarih.Date);

            if(kayitVar!= null)
            {
                string yazii = Tarih.Date.Date.ToShortDateString() + " Tarih'li Tatil Kayıtlı";
                await Application.Current.MainPage.DisplayAlert("İşlem İptal", yazii, "İptal");
                IsBusy = false;
                return;
            }


                ResmiTatiller tt = new ResmiTatiller();
                tt.Id = Guid.NewGuid().ToString().Substring(0, 8);
                tt.tarih = Tarih;
                tt.aciklama = Aciklama;
                tt.yil = Tarih.Year ;

                await  islem.AddItemAsync(tt);

            // YenileResmiTatil
           MessagingCenter.Send<ResmiTatiller>(tt, "ResmiTatilYenile");

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
        }

        public ICommand ListeleCommand => new Command(OnListele);

        async private void OnListele()
        {

        }


        // ----------------------------
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
