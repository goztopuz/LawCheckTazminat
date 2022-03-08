using System;
using System.Windows.Input;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.NetBrut
{
    public class NetBrutSecimViewModel: ViewModelBase
    {
        public NetBrutSecimViewModel()
        {

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

        public ICommand NettenBrutCommand => new Command(OnNetBrut);
        async private void OnNetBrut(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.NetBrutV.NetBrut1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;
        }

        public ICommand BrutNetCommand => new Command(OnBrutNet);
        async private void OnBrutNet(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            //-------

            var sayfa = new Views.NetBrutV.BrutNet1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }
    }
}
