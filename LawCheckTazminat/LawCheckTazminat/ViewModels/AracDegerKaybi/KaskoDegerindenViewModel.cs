using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using Syncfusion.Data.Extensions;
using Xamarin.Essentials;
namespace LawCheckTazminat.ViewModels.AracDegerKaybi
{
    public class KaskoDegerindenViewModel : ViewModelBase
    {

        KaskoDegerService islem = new KaskoDegerService();
        public KaskoDegerindenViewModel()
        {


            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                ListeMarka = new ObservableCollection<AracMarka>();

                MarkalariCek();

            }
            else
            {
                App.Current.MainPage.DisplayAlert("Hata", "İnternet Bağlantı Hatası", "Tamam"); 


            }




            
        }

        async   private void MarkalariCek()
        {
            var aa = await islem.MarkalariCek();
            ListeMarka = aa.ToObservableCollection();

        }

       

        private ObservableCollection<AracMarka> _listeMarka;
        public ObservableCollection<AracMarka> ListeMarka
        {
                get=> _listeMarka;
                set
                {
                _listeMarka = value;
                OnPropertyChanged();
                }

        }

        private AracMarka _marka;
        public AracMarka Marka
        {
            get => _marka;
            set
            {
                _marka = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<AracModel> _listeModel;
        public ObservableCollection<AracModel> ListeModel
        {
            get => _listeModel;
            set
            {
                _listeModel = value;
                OnPropertyChanged();
            }
        }

        private AracModel _model;
        public AracModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();                  
            }
        }

        private AracDeger _aracdegeri;
        public AracDeger AracDegeri
        {
            get => _aracdegeri;
            set
            {
                _aracdegeri = value;
                OnPropertyChanged();
            }
        }


        private decimal _deger;
        public decimal Deger
        {
            get => _deger;
            set
            {
                _deger = value;
                OnPropertyChanged();
            }
        }

        //public decimal Deger { get; set; }



        private int _yil;
        public int Yil
        {
            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }
        AracDeger mm = new AracDeger();
        public ICommand KullanCommand => new Command(OnKullan);
        
        private async void OnKullan(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            mm.Deger = Deger;
            MessagingCenter.Send<AracDeger>(mm, "AracDegeriCek");
            await Application.Current.MainPage.Navigation.PopModalAsync();


            IsBusy = false;
        }


        public ICommand MarkaSecimCommand => new Command(OnMarkaSecim);
        private async void OnMarkaSecim()
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            if(Connectivity.NetworkAccess== NetworkAccess.Internet)
            {
                var bb = await islem.ModelleriCek(Marka);
                ListeModel = bb.ToObservableCollection();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hata", "İnternet Bağlantı Hatası", "Tamam");
            }


            IsBusy = false;
        }

        public ICommand ModelSecimCommand => new Command(OnModelSecim);
        private async void OnModelSecim()
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            

            if(Marka ==null  || Model == null || Yil<1950)
            {
              
                mm.Yil = Yil;
                await App.Current.MainPage.DisplayAlert("Hata", "Seçim Yapılmadı veya Hata Oluştu", "Tamam");
                IsBusy= false;
                return;
            }
            mm.MarkaId = Marka.MarkaId;
            mm.ModelId = Model.ModelId;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                try
                {
                    var aa = await islem.AracDegeriCek(mm);
                    Deger = aa.Deger;
                    mm.Deger = Deger;
                }catch(Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Hata", "Seçim Yapılmadı veya Hata Oluştu", "Tamam");
                }
      

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Internet Bağlantı Hatası", "Tamam");
            }

        

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
