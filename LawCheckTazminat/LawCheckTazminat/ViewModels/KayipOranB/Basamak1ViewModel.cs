using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
   public class Basamak1ViewModel : ViewModelBase
    {

      //  List<AnaKategori> Liste = new List<AnaKategori>();


        public Basamak1ViewModel()
        {
            var aa = new KayipOranAnaKategori();
            Liste = aa.Liste;

        }

        private List<AnaKategori> _liste = new List<AnaKategori>();
        public List<AnaKategori> Liste
        {
            get => _liste;

            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }
        



        //------
        public ICommand AnaKategoriTappedCommand => new Command<AnaKategori>(AnaKategoriTapped);
        async private void AnaKategoriTapped(AnaKategori _kategori)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            
            var sayfa = new Views.KayipOranV.Basamak2KH(_kategori);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

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

            this.HataMesaji = "";
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
