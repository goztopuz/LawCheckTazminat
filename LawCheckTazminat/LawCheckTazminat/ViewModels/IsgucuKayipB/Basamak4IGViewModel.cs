using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public   class Basamak4IGViewModel :ViewModelBase
    {
        public Basamak4IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = _kayip;

            if(this.IsGucu.duzenlemede == false)
            {
                this.IsGucu.hastaneYatisTarihi = _kayip.kazaTarihi;
                this.IsGucu.hastaneCikisTarihi = this.IsGucu.hastaneYatisTarihi.AddDays(3);
            }
        }

        private IsgucuKayip _isgucukayip;

        public IsgucuKayip IsGucu
        {
            get => _isgucukayip;
            set
            {
                _isgucukayip = value;
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


        private bool SayfaKontrol()
        {
            bool _durum = true;
            // Kayip 0 dan büyük 100 den Küçük olmalıdır.

          if(  IsGucu.kayipOrani==0)
            {
                this.HataMesaji = "Kayıp Oranı Girilmelidir.";
                _durum = false;

           }
          if(IsGucu.kayipOrani<0)
            {
                this.HataMesaji = "Kayıp Yüzdesi Eksi Değer Olamaz.";
                _durum = false;
            }
          if(IsGucu.kayipOrani >=100)
            {
                this.HataMesaji = "Kayip Yüzdesi 100 ve Üstü Olamaz";
                _durum = false;
            }

          if( IsGucu.hastaneYatisi== "Var")
            {
                if(IsGucu.hastaneYatisTarihi<IsGucu.kazaTarihi)
                {
                    this.HataMesaji = "Hastane Yatışı Kaza Tarihinden Önce Olamaz";
                    _durum = false;
                }
                if(IsGucu.hastaneYatisTarihi>IsGucu.hastaneCikisTarihi)
                {
                    this.HataMesaji = "Yatış Tarihinden Çıkış Tarihinden Önce Olamaz";
                    _durum = false;
                }
            }

            return _durum;

        }
        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //var silKontrol1 = this.IsGucu.IsGucuKayipOranlar.ToList().Find(o => o.aciklama == "Gerçek");
            //if(silKontrol1 != null)
            //{
            //    this.IsGucu.IsGucuKayipOranlar.Remove(silKontrol1);
            //}

            //var silKontrol2 = this.IsGucu.IsGucuKayipOranlar.ToList().Find(o => o.aciklama == "Gerçek2");
            //if(silKontrol2 != null)
            //{
            //    this.IsGucu.IsGucuKayipOranlar.Remove(silKontrol2);
            //}
            //// Hastane
            //var silKontrol3 = this.IsGucu.IsGucuKayipOranlar.ToList().Find(o => o.aciklama == "Hastane");
            //if(silKontrol3 != null)
            //{
            //    this.IsGucu.IsGucuKayipOranlar.Remove(silKontrol3);
            //}





            // KAYIP ORANLARINI DÜZENLE....

            //KayipOran kayip1 = new KayipOran();
            //kayip1.id = Guid.NewGuid().ToString().Substring(0, 10);
            //kayip1.kayipOrani = IsGucu.kayipOrani;
            //kayip1.baslangicTarihi = IsGucu.kazaTarihi;
            //kayip1.aciklama = "Gerçek";
            //kayip1.aciklama2 = "Gerçek";


            //this.IsGucu.IsGucuKayipOranlar.Add(kayip1);


            //    if(IsGucu.hastaneYatisi=="Var")
            //    {
            //        KayipOran kayip2 = new KayipOran();
            //        kayip2.id = Guid.NewGuid().ToString().Substring(0, 10);
            //        kayip2.kayipOrani = 100;
            //        kayip2.baslangicTarihi = IsGucu.hastaneYatisTarihi;
            //        kayip2.cikisTarihi = IsGucu.hastaneCikisTarihi;
            //        kayip2.aciklama = "Hastane";
            //        kayip2.aciklama2 = "Hastane";
            //        this.IsGucu.IsGucuKayipOranlar.Add(kayip2);


            //        if (kayip2.baslangicTarihi> kayip1.baslangicTarihi)
            //        {
            //            KayipOran kayip3 = new KayipOran();
            //            kayip3.id = Guid.NewGuid().ToString().Substring(0, 10);
            //            kayip3.kayipOrani = IsGucu.kayipOrani;
            //            kayip3.baslangicTarihi = kayip2.cikisTarihi.AddDays(1);
            //            kayip3.aciklama = "Gerçek2";
            //            kayip3.aciklama2 = "Gerçek2";
            //            this.IsGucu.IsGucuKayipOranlar.Add(kayip3);

            //        }


            //}


            if(SayfaKontrol()== false)
            {
                IsBusy = false;
                return;
            }


            //İLERLE COMMAND
            //-----------------------------------


            this.HataMesaji = "";
            var sayfa = new Views.IsgucuKayipV.Basamak5IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }





        }
    }
