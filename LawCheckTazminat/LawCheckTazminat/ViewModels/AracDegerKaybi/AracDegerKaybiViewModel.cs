using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.AracDegerKaybi
{
    public class AracDegerKaybiViewModel : ViewModelBase
    {

        public   AracDegerKaybiViewModel()
        {
            kkk();
        }

        public void kkk()
        {
            MessagingCenter.Subscribe<AracDeger>(this, "AracDegeriCek", async (aa) =>
            {
                AracDeger = aa.Deger;

            });

        }

        private decimal _aracDeger;
        public decimal AracDeger
        {
            get => _aracDeger;
            set
            {
                _aracDeger = value;
                OnPropertyChanged();
            }
        }
      
        private double _aracKm;
        public double AracKm
        {
            get => _aracKm;
            set
            {
                _aracKm = value;
                OnPropertyChanged();
            }
        }

        private decimal _masrafMiktar;
        public decimal MasrafMiktar
        {
            get => _masrafMiktar;
            set
            {
                _masrafMiktar = value;
                OnPropertyChanged();
            }
        }
     
        private decimal _sonuc;
        public decimal Sonuc
        {
            get => _sonuc;
            set
            {
                _sonuc = value;
                OnPropertyChanged();
            }
        }

        decimal bazDegerKaybi;
        double hasarBoyutKatsayi;
        double kullanilmislikKatsayi;


        public ICommand HesaplaCommand => new Command(OnHesapla);
        private  void OnHesapla(object obj)
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //Baz Değer Kaybı
            bazDegerKaybi = AracDeger * Convert.ToDecimal(0.19);

            // KullanilmişlikKatsayi - kullanilmislikKatsayi;
            if(AracKm>=0 && AracKm<=14999)
            {
                kullanilmislikKatsayi = 0.9;
            }
            else if(AracKm>=15000 && AracKm<=29999)
            {
                kullanilmislikKatsayi = 0.8;
            }
            else if(AracKm >= 30000 && AracKm <= 44999)
            {
                kullanilmislikKatsayi = 0.6;
            }
            else if(AracKm >= 45000 && AracKm<=59999)
            {
                kullanilmislikKatsayi = 0.4;
            }
            else if(AracKm >= 60000 && AracKm<= 74999)
            {
                kullanilmislikKatsayi = 0.3;
            }
            else if(AracKm >= 75000 && AracKm <= 149999)
            {
                kullanilmislikKatsayi = 0.2;
            }
            else if(AracKm>= 150000)
            {
                kullanilmislikKatsayi = 0.1;
            }

            string durum = "";
         
            //Hasar Boyut Katsayi- hasarBoyutKatsayi
            if(AracDeger>=0 && AracDeger <= 75000 )
            {
                decimal a1Miktar = AracDeger * (decimal)0.2500;
                decimal a2AltMiktar = AracDeger * (decimal)0.1500;
                decimal a2UstMiktar = AracDeger*(decimal) 0.2500;
                decimal a3AltMiktar = AracDeger * (decimal)0.050 ;
                decimal a3UstMiktar = AracDeger *(decimal)0.1500;
                decimal a4Miktar = AracDeger *(decimal) 0.050;

                if(MasrafMiktar>a1Miktar)
                {
                    durum = "A1";
                }
                else if(MasrafMiktar> a2AltMiktar && MasrafMiktar<= a2UstMiktar)
                {
                    durum = "A2";
                }
                else if(MasrafMiktar>a3AltMiktar && MasrafMiktar<= a3UstMiktar)
                {
                    durum = "A3";
                }
                else if(MasrafMiktar<= a4Miktar)
                {
                    durum = "A4";
                }


            }
            else if(AracDeger > 75000 && AracDeger <= 150000)
            {
                decimal a1Miktar = AracDeger * (decimal)0.2000;
                decimal a2AltMiktar = AracDeger * (decimal)0.1200;
                decimal a2UstMiktar = AracDeger * (decimal)0.2000;
                decimal a3AltMiktar = AracDeger * (decimal)0.040;
                decimal a3UstMiktar = AracDeger * (decimal)0.1200;
                decimal a4Miktar = AracDeger * (decimal)0.040;

                if (MasrafMiktar > a1Miktar)
                {
                    durum = "A1";
                }
                else if (MasrafMiktar > a2AltMiktar && MasrafMiktar <= a2UstMiktar)
                {
                    durum = "A2";
                }
                else if (MasrafMiktar > a3AltMiktar && MasrafMiktar <= a3UstMiktar)
                {
                    durum = "A3";
                }
                else if (MasrafMiktar <= a4Miktar)
                {
                    durum = "A4";
                }


            }
            else if(AracDeger > 150000 && AracDeger <= 300000)
            {
                decimal a1Miktar = AracDeger * (decimal)0.2000;
                decimal a2AltMiktar = AracDeger * (decimal)0.1000;
                decimal a2UstMiktar = AracDeger * (decimal)0.2000;
                decimal a3AltMiktar = AracDeger * (decimal)0.030;
                decimal a3UstMiktar = AracDeger * (decimal)0.1000;
                decimal a4Miktar = AracDeger * (decimal)0.030;

                if (MasrafMiktar > a1Miktar)
                {
                    durum = "A1";
                }
                else if (MasrafMiktar > a2AltMiktar && MasrafMiktar <= a2UstMiktar)
                {
                    durum = "A2";
                }
                else if (MasrafMiktar > a3AltMiktar && MasrafMiktar <= a3UstMiktar)
                {
                    durum = "A3";
                }
                else if (MasrafMiktar <= a4Miktar)
                {
                    durum = "A4";
                }

            }
            else if(AracDeger >300000)
            {
                decimal a1Miktar = AracDeger * (decimal)0.2000;
                decimal a2AltMiktar = AracDeger * (decimal)0.0800;
                decimal a2UstMiktar = AracDeger * (decimal)0.2000;
                decimal a3AltMiktar = AracDeger * (decimal)0.020;
                decimal a3UstMiktar = AracDeger * (decimal)0.0800;
                decimal a4Miktar = AracDeger * (decimal)0.020;

                if (MasrafMiktar > a1Miktar)
                {
                    durum = "A1";
                }
                else if (MasrafMiktar > a2AltMiktar && MasrafMiktar <= a2UstMiktar)
                {
                    durum = "A2";
                }
                else if (MasrafMiktar > a3AltMiktar && MasrafMiktar <= a3UstMiktar)
                {
                    durum = "A3";
                }
                else if (MasrafMiktar <= a4Miktar)
                {
                    durum = "A4";
                }

            }

           
            if( durum=="A1")
            {
                hasarBoyutKatsayi = 0.9;
            }
            else if(durum=="A2")
            {
                hasarBoyutKatsayi = 0.75;
            }
            else if(durum=="A3")
            {
                hasarBoyutKatsayi = 0.5;
            }
            else if(durum=="A4")
            {
                hasarBoyutKatsayi = 0.25;
            }



            decimal deger = 0;
            // Hesaplama İşlemleri

            deger = bazDegerKaybi * (decimal)hasarBoyutKatsayi * (decimal)kullanilmislikKatsayi;

            Sonuc = deger;
            IsBusy = false;
        }

        public ICommand FormulSayfaCommand => new Command(OnFormulSayfa);
        private async void OnFormulSayfa(object obj)
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new Views.AracDegerKaybi.AracDegerKaybiFormul() ;
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand KaskoDegerindenCommand => new Command(OnKaskoDegerinden);
        private async void OnKaskoDegerinden()
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            //-------
            var sayfa = new Views.AracDegerKaybi.KaskoDegerinden();
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
