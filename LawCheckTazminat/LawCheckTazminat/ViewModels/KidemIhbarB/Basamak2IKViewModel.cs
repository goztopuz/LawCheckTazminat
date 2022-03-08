using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.KidemIhbarV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KidemIhbarB
{
    public class Basamak2IKViewModel: ViewModelBase
    {

        KidemIhbarService islem = new KidemIhbarService();

        public Basamak2IKViewModel(KidemIhbar _kidem)
        {
              IK = _kidem;

            if (_kidem.duzenlemede== false)
            {
                IK.VergiYili = IK.BitisTarihi.Year;
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




        // Kaydet Komutu

        public ICommand KaydetCommand => new Command(OnKaydet);
        private async void OnKaydet(object obj)
        { 
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            // Kaydetme İşlemi...
            if (IK.Id == "")
            {

                IK.Id = Guid.NewGuid().ToString().Substring(0, 8);
                IK.eskId = "";
            }
            else
            {
                IK.eskId = IK.Id;
                IK.Id = Guid.NewGuid().ToString().Substring(0, 8);

            }

            islem.AddItem(IK);
            var sayfa = new BasamakSonIKView(IK);

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
