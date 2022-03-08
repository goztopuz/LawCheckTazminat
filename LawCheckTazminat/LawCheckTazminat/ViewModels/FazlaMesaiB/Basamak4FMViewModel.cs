using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak4FMViewModel:ViewModelBase
    {
        public Basamak4FMViewModel(FazlaMesai _fm)
        {

            this.FM = new FazlaMesai();
            this.FM = _fm;

        }


        private FazlaMesai _fazlaMesai;

        public FazlaMesai FM
        {
            get => _fazlaMesai;
            set
            {
                _fazlaMesai = value;
                OnPropertyChanged();
            }
        }

        private bool Kontrol()
        {
            bool durum = true;
            if (FM.BasTarihResmiTatil > FM.BitTarihResmiTatil)
            {
                this.HataMesaji = "Başlangıç Bitiş Tarihinden Sonra";
                durum = false;
            }
            if (FM.BitTarihResmiTatil.Date.Year > DateTime.Now.Year)
            {
                this.HataMesaji = "Bitiş Tarihi Gelecek Bir Tarih";
                durum = false;
            }

            return durum;
        }

        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            if (Kontrol()== false)
            {
                this.IsBusy = false;
                return;
            }

         

            var basamak5 = new Basamak5aFMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak5);

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
