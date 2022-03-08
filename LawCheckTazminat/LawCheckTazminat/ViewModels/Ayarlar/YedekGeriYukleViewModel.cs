using System;
using System.Windows.Input;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class YedekGeriYukleViewModel: ViewModelBase
    {
        public YedekGeriYukleViewModel()
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
