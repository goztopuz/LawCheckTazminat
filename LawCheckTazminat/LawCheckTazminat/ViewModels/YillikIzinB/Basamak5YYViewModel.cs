using System;
using System.Collections.Generic;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class Basamak5YYViewModel :ViewModelBase
    {
        public List<YYResmiTatil> ResmiTatilListe = new List<YYResmiTatil>();
        public List<YYHaftaIzni> HaftaIzniListe = new List<YYHaftaIzni>();


        List<YYYilBilgi> CalisilanYilListesi = new List<YYYilBilgi>();
        List<YYYilBilgi> HesapYilListesi = new List<YYYilBilgi>();


        Services.YillikIzinService islem = new Services.YillikIzinService();

        public Basamak5YYViewModel(YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;

            if(this.YY.duzenlemede==false)
            {
                this.YY.VergiYil = YY.HesapBitisTar.Year;
            }
            

        }

        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }


        public ICommand KaydetCommand => new Command(OnKaydet);
        private async void OnKaydet(object obj)
        {
            if(IsBusy==true)
            {
                return;

            }
            IsBusy = true;

            // Kaydet....

            if(YY.Id=="")
            {

                YY.Id = Guid.NewGuid().ToString().Substring(0, 8);
                YY.EskiId = "";
            }
            else
            {
                YY.EskiId = YY.Id;
                YY.Id = Guid.NewGuid().ToString().Substring(0, 8);
              
            }

            foreach(var t in YY.CalisilanYillarBilgi)
            {
                t.YYId = YY.Id;
                t.yillikIzin = YY;
            }
            foreach(var t2 in YY.HesapYillariBilgi)
            {
                t2.YYId = YY.Id;
                t2.yillikIzin = YY;
            }
            foreach(var t3 in YY.IzindekiHaftaIzniBilgi)
            {
                t3.YYId = YY.Id;
                t3.yillikIzin = YY;
            }
            foreach(var t4 in YY.IzindekiResmiTatillerBilgi)
            {
                t4.YYId = YY.Id;
                t4.yillikIzin = YY;
            }
            foreach(var t5 in YY.IzinGunleriBilgi)
            {
                t5.FazlaMesaiId = YY.Id;
                t5.yillikIzin = YY;
            }
            foreach(var t6 in YY.IzinKaytilariBilgi)
            {
                t6.FazlaMesaiId = YY.Id;
                t6.yillikIzin = YY;
            }

            islem.AddItem(YY);

            // İlerle Son-Rapor...
            var sayfa = new BasamakYYSonView(YY);
             
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;

        }


        public ICommand IptalCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }


        private string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
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

    }
}
