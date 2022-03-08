using LawCheckTazminat.Contracts.Services;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Contacts
{
     public  class AdresDefteriViewModel :ViewModelBase
    {
        private Contact _kisi;

        //public IDataStore<Contact> islem = App.Repository;

        ContactsService islem = new ContactsService();
        public AdresDefteriViewModel(Contact _kk)
        {
            this.Kisi = new Contact();
            this.Kisi = _kk;


            if (this.Kisi.DogumTarihi != null)
            {
                this.DogumGun = this.Kisi.DogumTarihi.Day;
                this.DogumAy = this.Kisi.DogumTarihi.Month;
                this.DogumYil = this.Kisi.DogumTarihi.Year;

            }

        }

        public Contact Kisi
        {
            get => _kisi;

            set
            {
                _kisi = value;
                OnPropertyChanged();
            }
        }
        int _dogumGun;
        public int DogumGun
        {
            get => _dogumGun;
            set
            {
                _dogumGun = value;
                OnPropertyChanged();
            }
        }
        int _dogumAy;
        public int DogumAy
        {
            get => _dogumAy;
            set
            {
                _dogumAy = value;
                OnPropertyChanged();
            }
        }

        int _dogumYil;
        public int DogumYil
        {

            get => _dogumYil;
            set
            {
                _dogumYil = value;

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


        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            bool tarihVar = false;

            if( this.DogumYil !=0)
            {
                tarihVar = true;
            }
            try
            {
                if(tarihVar==true)
                {
                    var dTar = new DateTime(DogumYil, DogumAy, DogumGun);
                    Kisi.DogumTarihi = dTar;
                }
               
            }
            catch
            {
              //  this.HataMesaji = "Doğum Tarihi Hatası";
            }

            
            if(Kisi.Ad =="")
            {
                this.HataMesaji = "Ad Bilgisi Girilmelidir.";
            }

            if (this.Kisi.Id == "" || this.Kisi.Id == null)
            {
                Kisi.Id = Guid.NewGuid().ToString().Substring(0, 10);

                Kisi.BackgroundColor = RenkVer();

                await islem.AddItemAsync(Kisi);
            }
            else
            {
                if (Kisi.BackgroundColor == "")
                {
                    Kisi.BackgroundColor = RenkVer();

                }
                await islem.UpdateItemAsync(Kisi);
            }

            MessagingCenter.Send<string>("A", "Yenile");
            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;
        }


        private string RenkVer()
        {
            Random rnd = new Random();
            int i = rnd.Next(1, 10);

            string renk = "#837bff";
            switch (i)
            {
                case 1:
                    renk = "#837bff";
                    break;
                case 2:
                    renk = "#678cc8";
                    break;
                case 3:
                    renk = "#29aaa0";
                    break;
                case 4:
                    renk = "#db7faa";
                    break;
                case 5:
                    renk = "#dc9737";
                    break;
                case 6:
                    renk = "#a8d35f";
                    break;
                case 7:
                    renk = "#ec5b76";
                    break;
                case 8:
                    renk = "#6c88ff";
                    break;
                case 9:
                    renk = "#cf62e2";
                    break;
                case 10:
                    renk = "#f57780";
                    break;


            }


            return renk;

        }

    }


}

