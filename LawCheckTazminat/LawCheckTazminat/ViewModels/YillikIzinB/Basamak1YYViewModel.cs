using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class Basamak1YYViewModel : ViewModelBase
    {
        public Basamak1YYViewModel(YillikIzin _yillik)
        {

            this.YY = new YillikIzin();
            this.YY = _yillik;

            HataMesaji = "";

            if (YY.Id == "" || YY.Id== null)
            {
                YY.HesapBaslangicTar = DateTime.Now.AddYears(-5);
                YY.HesapBitisTar = DateTime.Now;
                YY.IseGirisTarihi = DateTime.Now.AddYears(-5);
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


        private bool SayfaVeriKontrol(DateTime t1, DateTime t2)
        {
            bool durum = true;

            if(t1.Date>t2.Date)
            {
                this.HataMesaji = "İşe Giriş Tarihi - Hesap Tarihinden Sonra Olamaz";
                 durum = false;
            }
       


            return durum;
        }

        public ICommand IlerleCommand => new Command(OnIlerle);
        
        private async void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            // Sayfa Kontrolü....

            //if(SayfaVeriKontrol(YY.IseGirisTarihi,YY.HesapBitisTar)== false)
            //{
            //    IsBusy = false;

            //    return;
            //}

            if(YY.HesapBaslangicTar< YY.IseGirisTarihi)
            {
                HataMesaji = "Hesap Başlangıç Tarihi Hesap Tarihinden Önce. ";
                IsBusy = false;
                return;
            }

            if(YY.HesapBaslangicTar> YY.HesapBitisTar)
            {
                HataMesaji = "Hesap Başlangıç Tarihi- Hesap Bitiş Tarihinden Sonra.";
                IsBusy = false;
                return;
            }

            if(YY.DogumTarihi> YY.IseGirisTarihi)
            {
                HataMesaji = "Doğum Tarihi İşe Giriş Tarihinden Sonra Olamaz";
                IsBusy = false;
                return;

            }

            DateTime test1 = YY.HesapBitisTar.AddYears(-5);
            test1 = test1.AddDays(-1);

            if (test1> YY.HesapBaslangicTar)
            {
                var durum1 = await App.Current.MainPage.DisplayAlert("Tarih Uyarısı", "Hesap Başlangıcı Bitiş Tarihinden 5 Yıldan Fazla." +
                    "Devam Etmek İstiyor musunuz?", "Evet", "Hayır");

                if (durum1 == false)
                {
                    IsBusy = false;
                    return;
              }

            }

            // İlerle Command.
            var sayfa = new Basamak2YYView(YY);

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
