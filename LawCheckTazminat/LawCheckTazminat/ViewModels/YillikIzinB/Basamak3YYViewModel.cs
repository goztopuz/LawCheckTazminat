using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class Basamak3YYViewModel: ViewModelBase
    {
        public Basamak3YYViewModel(YillikIzin _yillik)
        {

            this.YY = new YillikIzin();
            this.YY = _yillik;
            if( _yillik.Id != null )
            {
                if(_yillik.Id != "")
                {
                    SayfaYukle();
                }
            }
            YY.Kullanilan = YY.IzinGunleriBilgi.Count;
            kkk();

        }

        private void SayfaYukle()
        {
            BrutUcret = this.YY.BrutUcret;
        } 

        private decimal _brutUcret;
        public decimal BrutUcret
        {
            get => _brutUcret;
            set
            {
                _brutUcret = value;
                OnPropertyChanged();
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
    
        
        
        public void kkk()
        {
            MessagingCenter.Subscribe<YillikIzin>(this, "YenileYillikBrutUcret", async (aa) =>
            {
                BrutUcret = aa.BrutUcret;
            });

        }

        public ICommand NetBrutCommand => new Command(OnNetBrut);
        private async void OnNetBrut(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new YillikIzinNetBrutView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            // İlerle İşlemi

            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }


            YY.BrutUcret = BrutUcret;
            var sayfa = new Basamak3BYYView(YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;
        }

        private bool SayfaKontrol()
        {
            bool durum = true;

            if(YY.BrutUcret<0)
            {
                this.HataMesaji = "Brüt Ücret Negatif Olamaz";
                durum = false;
            }
            if(YY.izindeAlinanRaporSayisi<0)
            {
                this.HataMesaji = "Rapor Sayısı Negatif Olamaz.";
                durum = false;
            }

            return durum;
        }

        //--------

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
