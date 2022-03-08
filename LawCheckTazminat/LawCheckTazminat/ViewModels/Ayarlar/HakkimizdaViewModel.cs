using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    class HakkimizdaViewModel : ViewModelBase
    {

        public HakkimizdaViewModel()
        {

        }



        public ICommand AdreseGitCommand => new Command(OnAdreseGit);
        private async void OnAdreseGit(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            Uri urii = new Uri("www.bulutservisleri.com");
            await Browser.OpenAsync(urii, BrowserLaunchMode.SystemPreferred);

            IsBusy = false;
        }


        public ICommand YardimaGitCommand => new Command(OnYardimaGit);
        private async   void OnYardimaGit(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            Uri urii = new Uri("www.bulutservisleri.com/tazminappyardim");
            await Browser.OpenAsync(urii, BrowserLaunchMode.SystemPreferred);

            IsBusy = false;
        }



        public ICommand IptalCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {

            if(IsBusy== true)
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
