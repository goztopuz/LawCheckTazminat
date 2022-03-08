using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak8FMViewModel : ViewModelBase
    {
        FMHesaplaService fmService = new FMHesaplaService();


        List<FazlaMesaiKisiIzinGunleri> izinListeGun1 = new List<FazlaMesaiKisiIzinGunleri>();

        List<MaasFazlaMesai> ListeMaas = new List<MaasFazlaMesai>();

        public Basamak8FMViewModel(FazlaMesai _fmm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fmm;

            izinListeGun1 = FM.IzinGunleriBilgi.ToList();

            ListeMaas = FM.MaasBilgi.ToList();

            kkk();

            HaftalariAyarla();
            Toplam = FM.Toplam;
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

        public ICommand HaftalariToplaCommand => new Command(OnHaftalariTopla);

        async private void OnHaftalariTopla(object obj)
        {
            HaftalariTopla();
        }

        private void HaftalariTopla()
        {
            decimal _toplam = 0;
            foreach(var t in FM.HaftalikIzinHaftalarBilgi)
            {
                _toplam = _toplam + t.HaftaSonuUcret;


            }
            Toplam = _toplam;

            FM.ToplamHsonu = Toplam;

        }

        private void HaftalariAyarla()
        {

            DateTime tmpBasTar2 = FM.BasTarihHaftaSonu;
            FM.HaftalikIzinHaftalarBilgi.Clear();
            int sayac1 = 1;

            while (tmpBasTar2 < FM.BitTarihHaftaSonu)
            {

                FazlaMesaiHaftalikIzinHaftalar hafta = new FazlaMesaiHaftalikIzinHaftalar();

                hafta.Id = Guid.NewGuid().ToString().Substring(0, 8);
                hafta.BasTarih = tmpBasTar2;
                hafta.BitTarih = tmpBasTar2.AddDays(6);
                hafta.Sira = sayac1;
                hafta.IzinGunu = FM.TatilGunu;
                DayOfWeek _aa;
                if (hafta.IzinGunu == "Pazartersi")
                {
                    _aa = DayOfWeek.Monday;
                } else if(hafta.IzinGunu=="Salı")
                {
                    _aa = DayOfWeek.Tuesday;
                } else if(hafta.IzinGunu=="Çarşamba")
                {
                    _aa = DayOfWeek.Wednesday;
                }else if(hafta.IzinGunu=="Perşembe")
                {
                    _aa = DayOfWeek.Thursday;
                }else if(hafta.IzinGunu=="Cuma")
                {
                    _aa = DayOfWeek.Friday;
                }else if(hafta.IzinGunu=="Cumartesi")
                {
                    _aa = DayOfWeek.Saturday;
                }else if(hafta.IzinGunu== "Pazar")
                {
                    _aa = DayOfWeek.Sunday;
                }
                else
                {
                    _aa = DayOfWeek.Sunday;
                }


                
                DateTime tmpBaslangcc = hafta.BasTarih;
                
                while(tmpBaslangcc< hafta.BitTarih)
                {
                    if(tmpBaslangcc.DayOfWeek== _aa)
                    {
                        hafta.HaftalikIzinTarihi = tmpBaslangcc;
                       
                    }
                  tmpBaslangcc= tmpBaslangcc.AddDays(1);
                }
                

                var kisiMaasAylik = ListeMaas.Find(o => o.yilBas < hafta.BitTarih && o.yilBit > hafta.BitTarih);

                if (kisiMaasAylik != null)
                {
                    hafta.BrutUcret = kisiMaasAylik.brutMaas;

                }

                decimal fmMiktar = 0;
                fmMiktar = fmService.HaftalikTatilCalismaHesapla(hafta.BrutUcret);

                hafta.HaftaSonuUcret = fmMiktar;

                FM.HaftalikIzinHaftalarBilgi.Add(hafta);

                tmpBasTar2 = tmpBasTar2.AddDays(7);

                sayac1 = sayac1 + 1;

            }


            if(FM.TumHaftaSonlarindaCalisti==true)
            {
                foreach(var t2 in FM.HaftalikIzinHaftalarBilgi)
                {
                    t2.HaftaSonuIzinVar = true;
                }
            }

            foreach(var t in FM.HaftalikIzinHaftalarBilgi)
            {
                t.Aciklama1 = IzinKontrol(t);
                if (t.Aciklama1 == "İzin")
                {
                    t.HaftaSonuIzinVar = false;
                }
            }


        }


        private string IzinKontrol(FazlaMesaiHaftalikIzinHaftalar Hafta)
        {
            string yazii = "Hafta Tatilinde Çalışıldı";
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

            }



            return yazii;

        }



        //--------------
        public void kkk()
        {
            MessagingCenter.Subscribe<FazlaMesai>(this, "YenileFMHaftalikIzin", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }

        private void VerileriYenile(FazlaMesai _ffm)
        {


            //   this.FM = _ffm;

            ObservableCollection<FazlaMesaiHaftalikIzinHaftalar> Liste = new ObservableCollection<FazlaMesaiHaftalikIzinHaftalar>();

            foreach (var t in _ffm.HaftalikIzinHaftalarBilgi)
            {
                Liste.Add(t);
            }


            FM.HaftalikIzinHaftalarBilgi.Clear();

            foreach (var t in Liste)
            {
                FM.HaftalikIzinHaftalarBilgi.Add(t);
            }



        }
        //---------------------------------------------------


        //-----------------------------
        public ICommand HaftaTappedCommand => new Command<FazlaMesaiHaftalikIzinHaftalar>(OnHaftaTapped);
        async private void OnHaftaTapped(FazlaMesaiHaftalikIzinHaftalar _hafta)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            var sayfaHaftaBilgi = new Views.FazlaMesaiV.FMHaftalikIzınView(_hafta, FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaHaftaBilgi);

            IsBusy = false;
        }

        public ICommand YenidenToplaCommand => new Command(OnYenidenTopla);
        async private void OnYenidenTopla(object obj)
        {
            decimal toplam2 = 0;

            foreach (var t in FM.HaftalikIzinHaftalarBilgi)
            {
                if (t.HaftaSonuIzinVar == true)
                {
                    toplam2 = toplam2 + t.HaftaSonuUcret;
                }
            }

            FM.Toplam = toplam2;
            Toplam = toplam2;
        }


        // İlerle
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            FM.Toplam = Toplam;

            var sayfa = new Basamak9FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }


        // ----------------------------
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
