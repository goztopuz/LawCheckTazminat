using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class MasrafEkleViewModel :ViewModelBase 
    {

        public MasrafEkleViewModel(DestektenYoksunluk dyy, Masraf _mm)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;

            this.MasrafBilgi = new Masraf();
            this.MasrafBilgi = _mm;


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
        private Masraf _masraf;
        public Masraf MasrafBilgi
        {
            get => _masraf;
            set
            {
                _masraf = value;
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
        async private void OnIptal(Object obj)
        {
            
            await Application.Current.MainPage.Navigation.PopModalAsync();

       
        }

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
          if(  this.MasrafBilgi.miktar==0)
            {
                this.HataMesaji = "Miktar Boş Kalamaz";
                return;

            }
            
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            // Ekleme Kodları...
            if (MasrafBilgi.Id == null || MasrafBilgi.Id == "")
            {
                MasrafBilgi.Id = Guid.NewGuid().ToString().Substring(0, 10).ToString();
                this.AktifDestek.DosyaDestektenYoksunlukMasraf.Add(MasrafBilgi);

            }
            else
            {
                Masraf masraff = this.AktifDestek.DosyaDestektenYoksunlukMasraf.Where(o => o.Id == MasrafBilgi.Id).FirstOrDefault();

                masraff.ekBilgi1 = MasrafBilgi.ekBilgi1;
                masraff.ekBilgi2 = MasrafBilgi.ekBilgi2;
                masraff.masrafTur1 = MasrafBilgi.masrafTur1;
                masraff.masraftur2 = MasrafBilgi.masraftur2;
                masraff.miktar = MasrafBilgi.miktar;
                masraff.odemeTur = MasrafBilgi.odemeTur;
                masraff.tarihBas = MasrafBilgi.tarihBas;
                masraff.tarihBit = MasrafBilgi.tarihBit;

            }
            MessagingCenter.Send<ObservableCollection<Masraf>>(AktifDestek.DosyaDestektenYoksunlukMasraf, "MasrafYenile");

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
        
        }

        }
}
