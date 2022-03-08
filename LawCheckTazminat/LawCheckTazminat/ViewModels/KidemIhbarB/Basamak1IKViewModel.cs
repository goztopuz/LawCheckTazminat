using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.KidemIhbarV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KidemIhbarB
{
    public class Basamak1IKViewModel:ViewModelBase
    {
        public Basamak1IKViewModel(KidemIhbar _ikk)
        {
            this.IK = new KidemIhbar();
            this.IK = _ikk;


            if (IK.Id == "" || IK.Id == null)
            {
                IK.BaslangicTarihi = DateTime.Now;
                IK.BitisTarihi = DateTime.Now;

            }
        }

        private KidemIhbar _ik;

        public KidemIhbar IK
        {
            get => _ik;
            set
            {
                _ik = value;
                OnPropertyChanged();
            }
        }

        
        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            //ilerle Komutu
            if (SayfaVeriKontrol(IK.BaslangicTarihi, IK.BitisTarihi) == false)
            {
                IsBusy = false;

                return;
            }

            // İlerle Command.
            var sayfa = new Basamak2IKView(IK);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;    
        }


        private bool SayfaVeriKontrol(DateTime t1, DateTime t2)
        {
            bool durum = true;

            if (t1.Date > t2.Date)
            {
                this.HataMesaji = "İşe Giriş Tarihi - Hesap Tarihinden Sonra Olamaz";
                durum = false;
            }



            return durum;
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
