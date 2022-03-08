using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;
using LawCheckTazminat.Views.GeceCalisma;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basamak8GCViewModel :ViewModelBase
    {

        ResmiTatilService islem1 = new ResmiTatilService();
        GeceMesaiService islem2 = new GeceMesaiService();

        List<ResmiTatiller> resmiTatilListe = new List<ResmiTatiller>();
        public Basamak8GCViewModel(GeceCalisma _gece)
        {

            this.GC = new GeceCalisma();
            this.GC = _gece;

            ListeResmi = new ObservableCollection<GeceCakisanGun>();
            ListeIzin = new ObservableCollection<GeceCakisanGun>();

            resmiTatilListe = islem1.GetItemss();

            ResmiListeOlustur();
            IzinListeOlustur();

            var listeSecili = GC.GeceDusecekTarihler.ToList();
            if(GC.Id !="" && GC.Id!= null)
            {
                foreach(var t in ListeResmi)
                {
                    var kayit1 = listeSecili.Find(o => o.Tarih.Date == t.Tarih.Date);
                    if(kayit1 == null)
                    {

                        t.Secili = true;

                        //if (kayit1.Secili==true)
                        //{
                        //    t.Secili = true;
                        //}
                    }
                }

                foreach(var t2 in ListeIzin)
                {
                    var kayit2 = listeSecili.Find(o => o.Tarih.Date == t2.Tarih.Date);
                    if(kayit2 == null)
                    {
                        t2.Secili = true;
                        //if (kayit2.Secili== true)
                        //{
                        //    t2.Secili = true;
                        //}
                    }

                }
            }

        }

        private GeceCalisma _gc;
        public GeceCalisma GC
        {
            get => _gc;
            set
            {
                _gc = value;
                OnPropertyChanged();
            }
        }


        List<GeceCalisilanGunler> gunlerListe = new List<GeceCalisilanGunler>();

        private void ResmiListeOlustur()
        {
            ListeResmi.Clear();
            foreach (var t in GC.HaftalarBilgi)
            {
                // 1.Gün..
                if(t.PPzt== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarPPzt;
                    g1.KayitId = t.Id;
                    gunlerListe.Add(g1);

                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    g2.KayitId = t.Id;

                    g2.Tarih = g1.Tarih.AddDays(1);
                    //gunlerListe.Add(g2);
                    
                }

                //2. Gün
                if(t.PSal== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarPSal;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    g2.KayitId = t.Id;

                    g2.Tarih = g1.Tarih.AddDays(1);
                    //gunlerListe.Add(g2);

                }

                // 3.Gün
                if(t.SCar== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarSCar;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);

                    g2.Tarih = g1.Tarih.AddDays(1);

                    g2.KayitId = t.Id;

                    //gunlerListe.Add(g2);
                }

                // 4.Gün
                if(t.CPer== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarCPer;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    g1.KayitId = t.Id;

                    g2.Tarih = g1.Tarih.AddDays(1);
                    //gunlerListe.Add(g2);
                }

                //5.Gün
                if(t.PCum== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarPCum;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    g2.Tarih = g1.Tarih.AddDays(1);

                    g1.KayitId = t.Id;

                    //gunlerListe.Add(g2);

                }

                //6. Gün
                if(t.CCmt== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarCCmt;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);

                    g2.Tarih = g1.Tarih.AddDays(1);
                    g2.KayitId = t.Id;

                    //gunlerListe.Add(g2);
                }

                //7.Gün
                if(t.CPzr== true)
                {
                    GeceCalisilanGunler g1 = new GeceCalisilanGunler();
                    g1.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    g1.Tarih = t.TarCPzr;
                    g1.KayitId = t.Id;

                    gunlerListe.Add(g1);
                    GeceCalisilanGunler g2 = new GeceCalisilanGunler();
                    g2.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    g1.KayitId = t.Id;
                    g2.Tarih = g1.Tarih.AddDays(1);
                    //gunlerListe.Add(g2);
                }

            }

            List<GeceCakisanGun> liste2 = new List<GeceCakisanGun>();
            //foreach (var t2 in gunlerListe)
            //{    
            //    var mevcut = resmiTatilListe.Find(o => o.tarih.Date == t2.Tarih.Date);
            //    if(mevcut != null)
            //    {
            //        // Çakışan Günler......
            //        GeceCakisanGun gg = new GeceCakisanGun();
            //        gg.Id = Guid.NewGuid().ToString();
            //        gg.kayitId = t2.KayitId;
            //        gg.tur = "Resmi";
            //        gg.Tarih = mevcut.tarih;
            //        gg.Aciklama = mevcut.aciklama;
            //        liste2.Add(gg);           
            //    }
            //}

            var LR = resmiTatilListe.ToList().OrderByDescending(o => o.tarih).ToList();
            liste2.Clear();
            foreach(var t2b in LR)
            {
                var mevcut = gunlerListe.Find(o => o.Tarih.Date == t2b.tarih.Date);

                if (mevcut != null)
                {
                    // Çakışan Günler......
                    GeceCakisanGun gg = new GeceCakisanGun();
                    gg.Id = Guid.NewGuid().ToString();
                    gg.kayitId = mevcut.KayitId;
                    gg.tur = "Resmi";
                    gg.Tarih = t2b.tarih;
                    gg.GunAdi = gg.Tarih.DayOfWeek.ToString();
                    gg.Aciklama = t2b.aciklama;
                    liste2.Add(gg);
                }
            }



            foreach(var t3 in liste2)
            {
                ListeResmi.Add(t3);
            }

        }


        private void IzinListeOlustur()
        {
            ListeIzin.Clear();
            List<GeceCakisanGun> liste4 = new List<GeceCakisanGun>();


            foreach(var t in GC.IzinKaytilariBilgi)
            {
                DateTime tarBas = t.BaslangicTar;
                while(tarBas.Date <= t.BitisTar.Date)
                {
                    var kayit1 = gunlerListe.Find(o => o.Tarih.Date == tarBas.Date);

                    if(kayit1 != null)
                    {
                        GeceCakisanGun gg = new GeceCakisanGun();
                        gg.Id = Guid.NewGuid().ToString();
                        gg.kayitId = t.Id;
                        gg.tur = "Izin";
                        gg.GunAdi = kayit1.Tarih.DayOfWeek.ToString();
                        gg.Tarih = kayit1.Tarih;
                        gg.Aciklama ="İzin";
                        

                        liste4.Add(gg);
                    }
                    tarBas = tarBas.AddDays(1);
                }
            }


            //foreach(var t in gunlerListe)
            //{
            //    foreach(var t2 in GC.IzinKaytilariBilgi)
            //    {
            //        if(t2.BaslangicTar >= t.Tarih && t2.BitisTar<= t2.BitisTar)
            //        {
            //            // Çakışan Günler......

            //            GeceCakisanGun gg = new GeceCakisanGun();
            //            gg.Id = Guid.NewGuid().ToString();
            //            gg.kayitId = t.KayitId;
            //            gg.tur = "Resmi";
            //            gg.GunAdi = t.Tarih.DayOfWeek.ToString();
            //            gg.Tarih = t.Tarih;
            //            gg.Aciklama = t2.Aciklama;

            //            liste4.Add(gg);
                      

            //        }
            //    }
            //}


            var liste4b = liste4.OrderByDescending(o => o.Tarih);
            foreach(var tt in liste4b)
            {
                ListeIzin.Add(tt);
            }
           
        }

        private ObservableCollection<GeceCakisanGun> _listeResmi;
        public ObservableCollection<GeceCakisanGun> ListeResmi
        {
            get => _listeResmi;
            set
            {
                _listeResmi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GeceCakisanGun> _listeIzin;
        public ObservableCollection<GeceCakisanGun> ListeIzin
        {
            get => _listeIzin;
            set
            {
                _listeIzin = value;
                OnPropertyChanged();
            }
        }




        private void GCCikarma()
        {
            // Çıkarma ve Hesaplama
            GC.GeceDusecekTarihler.Clear();

            foreach(var tt in ListeResmi)
            {
              if(tt.Secili== false)
                    {
                    HesaptanDusecekTarihler dT = new HesaptanDusecekTarihler();
                    dT.Id = Guid.NewGuid().ToString().Substring(0, 20);
                    dT.IzinTur = "Resmi";
                    dT.KisiId = GC.KisiId;
                    dT.Tarih = tt.Tarih;
                    dT.Aciklama = tt.Aciklama;
                    dT.Secili = tt.Secili;
                    GC.GeceDusecekTarihler.Add(dT);
                }
            }

            foreach(var tt2 in ListeIzin)
            {

                if(tt2.Secili== false)
                { 
          
                    HesaptanDusecekTarihler dT = new HesaptanDusecekTarihler();
                    dT.Id = Guid.NewGuid().ToString().Substring(0, 20);
                    dT.IzinTur = "Izin";
                    dT.KisiId = GC.KisiId;
                    dT.Tarih = tt2.Tarih;
                    dT.Aciklama = tt2.Aciklama;
                    dT.Secili = tt2.Secili;
                    GC.GeceDusecekTarihler.Add(dT);
                }
            }



        }

        //--- Kaydet Command-----
        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }


            GCCikarma();

            IsBusy = true;
            // Kaydet Bilgisi...

            if(GC.Id==""|| GC.Id== null)
            {
                GC.Id=Guid.NewGuid().ToString().Substring(0, 8);
                GC.eskiId = "";
            }
            else
            {
                GC.eskiId = GC.Id;
                GC.Id = Guid.NewGuid().ToString().Substring(0, 8);


            }


            foreach(var t in GC.HaftalarBilgi)
            {
                t.GeceMesaiId = GC.Id;
                t.geceCalisma = GC;
            }
            foreach (var t in GC.IzinKaytilariBilgi)
            {
                t.GeceMesaiId = GC.Id;
                t.geceCalisma = GC;
            }

            foreach (var t in GC.MaasBilgi)
            {
                t.GeceMesaiId = GC.Id;
                t.geceCalisma = GC;
            }

            foreach (var t in GC.GeceDusecekTarihler)
            {
                t.GeceMesaiId = GC.Id;
                t.geceCalisma = GC;
            }


            islem2.Ekle(GC);

            // İlerle Son-Rapor...
            var sayfa = new BasamakSonGCView(GC);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            HataMesaji = "";
            IsBusy = false;
        }



        //-----------------------------
        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            HataMesaji = "";
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

    public class GeceCakisanGun
    {

        public string Id { get; set; }
        public string GeceId { get; set; }
        public string tur { get; set; }
        public string kayitId { get; set; }

        public bool Secili { get; set; }
        public string TarihBilgi { get; set; }
        public string Aciklama { get; set; }
        public string GunAdi { get; set; }

        public DateTime Tarih { get; set; }


    }

    public class GeceCalisilanGunler
    {
        public string Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
        public string KayitId { get; set; }
        public string GunBilgi { get; set; }

    }

}
