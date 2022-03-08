using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
     public  class AsgariUcretYeniViewModel :ViewModelBase
    {
        bool _editMode = false;
        AsgariUcretTablosu _asgariUcret;
        ObservableCollection<AsgariUcretTablosu> _liste;
        public AsgariUcretYeniViewModel(ObservableCollection<AsgariUcretTablosu> aL, AsgariUcretTablosu aa )
        {
            this.Liste = aL;
            _asgariUcret = aa;

            Gorunum = false;

            if(aa.Id != "")
            {
                Yil = aa.yil2;

                if(aa.donem==1)
                {
                    Donemi = "Ocak-Haziran";
                }
                else if( aa.donem==2)
                {
                    Donemi = "Temmuz-Aralık";
                }

                BrutMiktar = aa.brut;

                NetMiktar = aa.net;
                _editMode = true;
                Gorunum = true;
            }
       
        }

        bool _gorunum;
        public bool Gorunum
        {
            get => _gorunum;
            set
            {
                _gorunum = value;
                OnPropertyChanged();
            }
        }

        int _yil;
        public int Yil
        {
            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }
        
        public string _donemi;
        public  string Donemi
        {
            get  => _donemi;
            set
            {
                _donemi = value;
                OnPropertyChanged();
            }
        }

        public decimal _brutMiktar;
        public decimal BrutMiktar
        {
            get => _brutMiktar;
            set
            {
                _brutMiktar = value;
                OnPropertyChanged();
            }
        }

        public decimal _netMiktar;
        public decimal NetMiktar
        {
            get => _netMiktar;
            set
            {
                _netMiktar = value;
                OnPropertyChanged();
            }
        }
            

        public string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }
      
        public ObservableCollection<AsgariUcretTablosu> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }
        public ICommand KaydetCommand => new Command(OnKaydet);

        AsgariUcretService islem = new AsgariUcretService();
        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            AsgariUcretTablosu aa = new AsgariUcretTablosu();
            aa.brut = BrutMiktar;
            aa.net = NetMiktar;
            aa.yil2 = Yil;

            if (Donemi == "Ocak-Haziran")
            {
                aa.donem = 1;
            }
            else if (Donemi == "Temmuz-Aralık")
            {
                aa.donem = 2;
            }

            aa.yil = aa.yil2 + "-" + aa.donem;
            if (_editMode == false)
            {
                // Yeni Kayıt Ekleme...  
                var kontrol = Liste.Where(o => o.yil == aa.yil).FirstOrDefault();
                if (kontrol == null)
                {
                    aa.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    await islem.AddItemAsync(aa);
            
                }
                else
                {
                    this.HataMesaji = "Bu Döneme Ait Kayıt Var";
                    return;
                }
            }
            else
            {
                // Kayıt Güncelleme....

                aa.Id = _asgariUcret.Id;
                await islem.UpdateItemAsync(aa);
         

            }
        

            MessagingCenter.Send<string>("A", "YenileAsgari");
            IsBusy = false;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public ICommand SilCommand => new Command(OnSil);

        async private void OnSil(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


 
            var sonuc1= await Application.Current.MainPage.DisplayActionSheet("Kayıt Silme", "Mevcut Asgari Ücret Verisi Silinecektir", "Sil", "İptal");
        
            if(sonuc1=="Sil")
            {

                await islem.DeleteItemAsync(_asgariUcret.Id);
                MessagingCenter.Send<string>("A", "YenileAsgari");
                await Application.Current.MainPage.Navigation.PopModalAsync();

            }
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
