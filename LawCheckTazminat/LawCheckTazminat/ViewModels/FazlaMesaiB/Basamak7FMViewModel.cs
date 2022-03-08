using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak7FMViewModel:ViewModelBase
    {

        FMHesaplaService fmService =  new FMHesaplaService();

        List<FazlaMesaiKisiIzinGunleri> izinListeGun1 = new List<FazlaMesaiKisiIzinGunleri>();

        List<MaasFazlaMesai> ListeMaas = new List<MaasFazlaMesai>();

        public Basamak7FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;
            kkk();
            izinListeGun1 = FM.IzinGunleriBilgi.ToList();
            ListeMaas = FM.MaasBilgi.ToList();
            HaftalariAyarla();
            MesaiUcretleriTopla();

        }

        private void MesaiUcretleriTopla()
        {
            decimal toplamMesaiUcret = 0;

            foreach(var t2 in FM.FMHaftalarBilgi)
            {
                toplamMesaiUcret = toplamMesaiUcret + t2.MesaiUcret;
            }

            this.FM.Toplam = toplamMesaiUcret;
           
        }


        private void HaftalariAyarla()
        {

            DateTime tmpBasTar2 = FM.BasTarihMesai;
            FM.FMHaftalarBilgi.Clear();
            int sayac1 = 1;
            while(tmpBasTar2 < FM.BitTarihMesai)
            {

                FazlaMesaiHaftalar hafta = new FazlaMesaiHaftalar();

                hafta.Id = Guid.NewGuid().ToString().Substring(0, 10);
                hafta.BasTarih = tmpBasTar2;
                hafta.BitTarih = tmpBasTar2.AddDays(6);
                hafta.Sira = sayac1;
                hafta.FazlaMesaiVar = true;
                hafta.FazlaMesaiOHaftadaki = FM.FazlaMesaiMiktar2;
                hafta.FazlaMesaiSozlesme = FM.SozlesmeCalismaSaat;

                
                var kisiMaasAylik = ListeMaas.Find(o => o.yilBas < hafta.BitTarih && o.yilBit > hafta.BitTarih);
                if(kisiMaasAylik != null)
                {
                    hafta.BrutUcret = kisiMaasAylik.brutMaas;

                }

                decimal fmMiktar = 0;
                 fmMiktar = fmService.FMHesapla(hafta.FazlaMesaiSozlesme,
                 hafta.FazlaMesaiOHaftadaki, hafta.BrutUcret);


                
                hafta.MesaiUcret = fmMiktar;


                FM.FMHaftalarBilgi.Add(hafta);
                              
                tmpBasTar2 = tmpBasTar2.AddDays(7);
                sayac1 = sayac1 + 1;

            }


            foreach(var t in FM.FMHaftalarBilgi)
            {
                t.Aciklama1 = IzinKontrol(t);
                if(t.Aciklama1=="İzin")
                {
                    t.FazlaMesaiVar = false;
                }
              }

             }

        private string IzinKontrol(FazlaMesaiHaftalar Hafta)
        {
            string yazii = "Fazla Mesai";
            var izinListe = izinListeGun1;

            string izinId = "";
            var tmpBas = Hafta.BasTarih;
            bool izinvar = false;
            while (tmpBas <= Hafta.BitTarih)
            {
                var kontrol = izinListe.Find(o => o.Tarih == tmpBas);
                if (kontrol != null)
                {
                    izinvar = true;
                    izinId = kontrol.KayitId;

                }
                tmpBas = tmpBas.AddDays(1);

            }

            if (izinvar == true)
            {
                yazii = "İzin";

             //   var izin1 = FM.IzinKaytilariBilgi.ToList().Find(o => o.Id == izinId);

             //   IzinYazi = "Çalışma Haftası İçerisinde İzin Mevcut.\n"
             //+ izin1.BaslangicTar.ToShortDateString() + izin1.BitisTar.ToShortDateString();
            }



            return yazii;

        }




        private FazlaMesai _fazlamesai;
        public FazlaMesai FM
        {
            get => _fazlamesai;
            set
            {
                _fazlamesai = value;
                OnPropertyChanged();

            }
        }

        private decimal _toplam;
        public decimal Toplam
        {
            get => _toplam;
            set
            {
                _toplam = value;
                OnPropertyChanged();
            }
        }
        

        //public void kkk()
        //{
        //    MessagingCenter.Subscribe<FazlaMesai>(this, "YenileFMHaftalar", async (aa) =>
        //    {
        //        VerileriYenile(aa);
        //    });

        //}
        public void kkk()
        {
            MessagingCenter.Subscribe<FazlaMesai>(this, "YenileFMHaftalar", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }
        private void VerileriYenile2(FazlaMesaiHaftalar _hafta)
        {
            var kayit = FM.FMHaftalarBilgi.ToList().Find(o => o.Id == _hafta.Id);
            if(kayit != null)
            {
                FM.FMHaftalarBilgi.Remove(kayit);
            }

            FM.FMHaftalarBilgi.Add(_hafta);
            FM.FMHaftalarBilgi.OrderBy(o => o.Sira);
            MesaiUcretleriTopla();
        }

        private void VerileriYenile(FazlaMesai _ffm)
        {
      //      this.FM = _ffm;
            ObservableCollection<FazlaMesaiHaftalar> Liste = new ObservableCollection<FazlaMesaiHaftalar>();

            foreach (var t in _ffm.FMHaftalarBilgi)
            {
                Liste.Add(t);
            }

            FM.FMHaftalarBilgi.Clear();

            foreach (var t in Liste)
            {
                FM.FMHaftalarBilgi.Add(t);
            }


        }

        public ICommand YenidenToplaCommand => new Command(OnYenidenTopla);
        async private void OnYenidenTopla(object obj)
        {
            decimal toplam2 = 0;

            foreach(var t in FM.FMHaftalarBilgi)
            {
                if(t.FazlaMesaiVar ==true)
                {
                    toplam2 = toplam2 + t.MesaiUcret;
                }
            }

            FM.Toplam = toplam2;
            Toplam = toplam2;
        }

        //-----------------------------

        //------
        public ICommand HaftaTappedCommand => new Command<FazlaMesaiHaftalar>(OnHaftaTapped);
        async private void OnHaftaTapped(FazlaMesaiHaftalar _hafta)
        {
            var sayfaHaftaBilgi = new Views.FazlaMesaiV.FMHaftaDuzenle(_hafta, FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaHaftaBilgi);


        }
        // İlerle
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            FM.Toplam = Toplam;

            var sayfa = new Basamak8FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        //--------------------
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



    }
}
