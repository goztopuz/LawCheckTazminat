using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LawCheckTazminat.Models;
using System.Threading.Tasks;
using LawCheckTazminat.Views;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{

public class Wiz1DYViewModel: ViewModelBase
    {

        string _kisiId = "";
        public Wiz1DYViewModel(string kisiId)
        {
            _kisiId = kisiId;
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek.musteriId = _kisiId;
        }

        private Models.DestektenYoksunluk _destektenYoksunluk;    
        
        public Models.DestektenYoksunluk AktifDestek
            {
                    get => _destektenYoksunluk;

                set
                 {
                        _destektenYoksunluk = value;
                         OnPropertyChanged();
                   }
    }

        public ICommand BaslaCommand => new Command(OnIleri);

        async   private void OnIleri(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            var basamak1 = new BasamakDY1View( AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak1);
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



    }
}
