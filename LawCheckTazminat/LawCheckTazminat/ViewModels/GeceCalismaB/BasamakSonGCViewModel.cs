using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class BasamakSonGCViewModel:ViewModelBase
    {

        Services.HesaplaVergi islemVergi = new Services.HesaplaVergi();

        List<GeceGunBilgi> ListeGunler = new List<GeceGunBilgi>();

        List<GCGunBilgi> CalisilanGunListe = new List<GCGunBilgi>();

        public BasamakSonGCViewModel(GeceCalisma _gece)
        {

            this.GC = new GeceCalisma();
            this.GC = _gece;

        //    Hesapla2();

            Hesapla();
           
        }




        private void Hesapla2()
        {

            decimal _brutToplam2 = 0;


            Double damgaVergi = 0.00759;

            /// GC- TOPLAM Hesaplanacak!!!
            // --

            CalisilanGunListe.Clear();
            var ll = GC.GeceDusecekTarihler.ToList();


            foreach(var t in GC.HaftalarBilgi )
            {
                if(t.GeceMesaiVar==true)
                {

                    //1
                    if(t.PPzt==true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarPPzt.AddDays(-1);
                        g1.BitTar = t.BitTarPPzt;
                        g1.GunlukUcret = t.UcretPPzt;
                        g1.SaatSayi =(decimal) t.NetSaatEllePPzt;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;

                        CalisilanGunListe.Add(g1);
                    }


                    //2
                    if(t.PSal== true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarPSal.AddDays(-1);
                        g1.BitTar = t.BitTarPSal;
                        g1.GunlukUcret = t.UcretPSal;
                        g1.SaatSayi = (decimal)t.NetSaatEllePSal;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);

                    }

                    //3
                    if(t.SCar == true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarSCar.AddDays(-1);
                        g1.BitTar = t.BitTarSCar;
                        g1.GunlukUcret = t.UcretSCar;
                        g1.SaatSayi = (decimal)t.NetSaatElleSCar;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);
                    }


                    //4
                    if(t.CPer== true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarCPer.AddDays(-1);
                        g1.BitTar = t.BitTarCPer;
                        g1.GunlukUcret = t.UcretCPer;
                        g1.SaatSayi = (decimal)t.NetSaatElleCPer;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);
                    }


                    //5
                    if(t.PCum== true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarPCum.AddDays(-1);
                        g1.BitTar = t.BitTarPCum;
                        g1.GunlukUcret = t.UcretPCum;
                        g1.SaatSayi = (decimal)t.NetSaatEllePCum;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);
                    }


                    //6
                    if(t.CCmt == true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarCCmt.AddDays(-1);
                        g1.BitTar = t.BitTarCCmt;
                        g1.GunlukUcret = t.UcretCCmt;
                        g1.SaatSayi = (decimal)t.NetSaatElleCCmt;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);
                    }

                    //7
                    if(t.CPzr == true)
                    {
                        GCGunBilgi g1 = new GCGunBilgi();
                        g1.BasTar = t.BitTarCPzr.AddDays(-1);
                        g1.BitTar = t.BitTarCPzr;
                        g1.GunlukUcret = t.UcretCPzr;
                        g1.SaatSayi = (decimal)t.NetSaatelleCPzr;
                        g1.GunToplam = g1.GunlukUcret * g1.SaatSayi;
                        CalisilanGunListe.Add(g1);

                    }

                }
            }

            foreach(var t2 in CalisilanGunListe)
            {
                _brutToplam2 = _brutToplam2 + t2.GunToplam;
            }

        }


        private void Hesapla()
        {

            decimal _brutToplam = 0;


            Double damgaVergi = 0.00759;

            /// GC- TOPLAM Hesaplanacak!!!
            // --

            ListeGunler.Clear();
            var ll = GC.GeceDusecekTarihler.ToList();

            foreach(var t2 in GC.HaftalarBilgi)
            {
                t2.GerceklesenFazlaMesaiSaati = 0;
                double saatTop1 = 0;
                if(t2.GeceMesaiVar == true)
                {
                    //Pazartesi
                    if(t2.PSal==true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarPSal.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }                      
                        }

                        if(GC.TatilVardiyaiDus==true)
                        {
                            if(GC.TatilGunu=="Pazartesi")
                            {
                                durumEkle = false;
                            }
                        }
                      
                        if(durumEkle== true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatEllePSal;
                        }

                        t2.PSal = durumEkle;
                    }
                    //Salı
                    if(t2.SCar==true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarSCar.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }
                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Salı")
                            {
                                durumEkle = false;
                            }
                        }
                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatElleSCar;
                        }

                        t2.SCar = durumEkle;
                    }
                    //Çarşamba
                    if(t2.CPer== true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarCPer.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }

                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Çarşamba")
                            {
                                durumEkle = false;
                            }
                        }

                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatElleCPer;
                        }

                        t2.CPer = durumEkle;
                    }
                   
                    //Perşembe
                    if(t2.PCum== true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarPCum.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }

                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Perşembe")
                            {
                                durumEkle = false;
                            }
                        }

                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatEllePCum;
                        }

                        t2.PCum = durumEkle;
                    }
                  
                    // Cuma
                    if(t2.CCmt== true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarCCmt.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }

                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Cuma")
                            {
                                durumEkle = false;
                            }
                        }

                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatElleCCmt;
                        }

                        t2.CCmt = durumEkle;
                    }
                  
                    //Cumartesi
                    if(t2.CPzr== true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarCPzr.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }

                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Cumartesi")
                            {
                                durumEkle = false;
                            }
                        }

                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatelleCPzr;
                        }

                        t2.CPzr = durumEkle;
                    }
                  
                    
                    // Pazar
                    if(t2.PPzt== true)
                    {
                        var k1 = ll.Find(o => o.Tarih.Date == t2.TarPPzt.Date);
                        bool durumEkle = true;
                        if (k1 != null)
                        {
                            if (k1.Secili == false)
                            {
                                durumEkle = false;
                            }
                        }

                        if (GC.TatilVardiyaiDus == true)
                        {
                            if (GC.TatilGunu == "Pazar")
                            {
                                durumEkle = false;
                            }
                        }

                        if (durumEkle == true)
                        {
                            saatTop1 = saatTop1 + t2.NetSaatEllePPzt;
                        }

                        t2.PPzt = durumEkle;
                    }
                }
                t2.GerceklesenFazlaMesaiSaati = saatTop1;
             }
            decimal _toplam = 0;
        
            foreach (var t3 in GC.HaftalarBilgi)
            {
                decimal haftaToplam = 0;
                t3.GeceUcret = 0;
                if (t3.GeceMesaiVar == true)
                {

                    if (t3.PSal == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatEllePSal * t3.SaatlikPSal;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.SCar == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatElleSCar * t3.SaatlikSCar;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.CPer == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatElleCPer * t3.SaatlikCPer;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.PCum == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatEllePCum * t3.SaatlikPCum;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.CCmt == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatElleCCmt * t3.SaatlikCCmt;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.CPzr == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatelleCPzr * t3.SaatlikCPzr;
                        haftaToplam = haftaToplam + miktar;
                    }
                    if (t3.PPzt == true)
                    {
                        decimal miktar = (decimal)t3.NetSaatEllePPzt * t3.SaatlikPPzt;
                        haftaToplam = haftaToplam + miktar;
                    }

                    t3.GeceUcret = haftaToplam;
                }
                 _toplam = _toplam + haftaToplam;
            }
            

            //foreach(var t in GC.HaftalarBilgi)
            //{
            //    // Pazartesi
            //    if(GC.PSal== true)
            //    {
            //        GeceGunBilgi g1 = new GeceGunBilgi();
            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatPSal;
            //        g1.BasTar = t.TarPSal;
            //        g1.BitTar = t.BitTarPSal;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretPSal;
            //        g1.NetSaat = t.NetSaatEllePSal;
            //        g1.SaatlikUcret = t.SaatlikPSal;

            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if(k1 != null)
            //        {
            //            if(k1.Secili ==false)
            //            {
            //                durumEkle = false;

            //            }
            //        }
            //        if(k2 != null)
            //        {

            //            if (k2.Secili == false)
            //            {
            //                durumEkle = false;

            //            }
            //        }

            //        if(durumEkle== true)
            //        {
            //            ListeGunler.Add(g1);
            //        }
               
            //    }


            //    //  Salı
            //    if(GC.SCar== true)
            //    {
            //        GeceGunBilgi g1 = new GeceGunBilgi();
            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatSCar;
            //        g1.BasTar = t.TarSCar;
            //        g1.BitTar = t.BitTarSCar;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretSCar;
            //        g1.NetSaat = t.NetSaatElleSCar;
            //        g1.SaatlikUcret = t.SaatlikSCar;

            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }


            //    }


            //    // Çarşamba
            //    if(GC.CPer==true)
            //    {


            //        GeceGunBilgi g1 = new GeceGunBilgi();
            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatCPer;
            //        g1.BasTar = t.TarCPer;
            //        g1.BitTar = t.BitTarCPer;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretCPer;
            //        g1.NetSaat = t.NetSaatElleCPer;
            //        g1.SaatlikUcret = t.SaatlikCPer;


            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }
            //    }


            //    // Perşembe
            //    if(GC.PCum==true)
            //    {
            //        GeceGunBilgi g1 = new GeceGunBilgi();

            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatPCum;
            //        g1.BasTar = t.TarPCum;
            //        g1.BitTar = t.BitTarPCum;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretPCum;
            //        g1.NetSaat = t.NetSaatEllePCum;
            //        g1.SaatlikUcret = t.SaatlikPCum;

            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }

            //    }



            //    // Cuma
            //    if(GC.CCmt==true)
            //    {
            //        GeceGunBilgi g1 = new GeceGunBilgi();


            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatCCmt;
            //        g1.BasTar = t.TarCCmt;
            //        g1.BitTar = t.BitTarCCmt;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretCCmt;
            //        g1.NetSaat = t.NetSaatElleCCmt;
            //        g1.SaatlikUcret = t.SaatlikCCmt;


            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }

            //    }


            //    // Cumartesi
            //    if(GC.CPzr==true)
            //    {

            //        GeceGunBilgi g1 = new GeceGunBilgi();

            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatCPzr;
            //        g1.BasTar = t.TarCPzr;
            //        g1.BitTar = t.BitTarCPzr;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretCPzr;
            //        g1.NetSaat = t.NetSaatelleCPzr;
            //        g1.SaatlikUcret = t.SaatlikCPzr;


            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }


            //    }


            //    // Pazar
            //    if(GC.PPzt==true)
            //    {

            //        GeceGunBilgi g1 = new GeceGunBilgi();


            //        g1.Id = Guid.NewGuid().ToString().Substring(0, 6);
            //        g1.BasSaat = t.BasSaatPPzt;
            //        g1.BasTar = t.TarPPzt;
            //        g1.BitTar = t.BitTarPPzt;
            //        g1.KayitId = t.Id;
            //        g1.Miktar = t.UcretPPzt;
            //        g1.NetSaat = t.NetSaatEllePPzt;
            //        g1.SaatlikUcret = t.SaatlikPPzt;


            //        var k1 = ll.Find(o => o.Tarih.Date == g1.BasTar.Date);
            //        var k2 = ll.Find(o => o.Tarih.Date == g1.BitTar.Date);

            //        bool durumEkle = true;
            //        if (k1 != null)
            //        {
            //            durumEkle = false;
            //        }
            //        if (k2 != null)
            //        {
            //            durumEkle = false;
            //        }

            //        if (durumEkle == true)
            //        {
            //            ListeGunler.Add(g1);
            //        }
            //    }            
          
            //}




            //ListeGunler = ListeGunler.OrderBy(o => o.BasTar).ToList();
            //int siraa = 0;
            //foreach (var tt in ListeGunler)
            //{
            //    siraa = siraa + 1;
            //    tt.Sira = siraa;
            //    tt.Miktar = (decimal)tt.NetSaat * tt.SaatlikUcret;

            //    _toplam = _toplam + tt.Miktar;
            //}

            GC.Toplam = _toplam;

            GC.SGK = GC.Toplam * Convert.ToDecimal(0.14);
            GC.DamgaV = GC.Toplam * Convert.ToDecimal(damgaVergi);
            GC.Issizlik = GC.Toplam * (Convert.ToDecimal(0.01));

            decimal dusecekMiktar = GC.SGK + GC.DamgaV + GC.Issizlik;

            decimal mik1 = GC.Toplam - dusecekMiktar;
            decimal gelirVergi = islemVergi.VergiHesapla(
                Convert.ToDecimal(GC.VergiMatrah), mik1, Convert.ToInt32(GC.VergiYili));

            GC.Vergi = gelirVergi;

            GC.Net = GC.Toplam - GC.Vergi - dusecekMiktar;

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


        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        private async void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

             GC.duzenlemede= true;

            //----
            var sayfa = new LawCheckTazminat.Views.GeceCalisma.Basamak1GCview(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



            this.HataMesaji = "";
            IsBusy = false;
        }

        public ICommand BitisCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.AnaSayfaV.Anasayfa();

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;

        }

        public ICommand RaporAlCommand => new Command(OnGCRapor);


        private async void OnGCRapor(object obj)
        {

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();
            PdfDocument documentP = new PdfDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;

            //Set Margin of the section

            section.PageSetup.Margins.Top = 72;
            section.PageSetup.Margins.Left = 25;
            section.PageSetup.Margins.Right = 10;
            section.PageSetup.Margins.Bottom = 72;

            section.PageSetup.PageSize = PageSize.A4;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Gece Çalışma Mesai Ücret Hesaplama") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            //Ad Soyad , BaşTarih, Bit Tarih,  Çalışılan Günler, Baş Saat - Bit Saat... İzin Günü--
            // İzin Günü Çakışması düşülecek(E/H)
            IWTable tablooBaslangic = section.AddTable();
            //tablooBaslangic.TableFormat.Borders.LineWidth = 2f;
            tablooBaslangic.TableFormat.CellSpacing = 0;
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 12;
            cf3.FontName = "Times New Roman";

            tablooBaslangic.ApplyStyle(BuiltinTableStyle.LightGrid);
            tablooBaslangic.ResetCells(7, 2);
            tablooBaslangic.IndentFromLeft = 80;
            WTableRow row = tablooBaslangic.Rows[0];
            //Specifies the row height
            row.Height = 25;

            tablooBaslangic[0, 0].AddParagraph().AppendText(" Ad Soyad :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[0, 0].Width = 200;
            tablooBaslangic[0, 1].AddParagraph().AppendText("   " + "-------------- + ").ApplyCharacterFormat(cf3);
            tablooBaslangic[0, 1].Width = 200;

            tablooBaslangic[1, 0].AddParagraph().AppendText(" Başlangıç Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[1, 1].AddParagraph().AppendText(" " + GC.BasTarih.ToShortDateString() + "\t ").ApplyCharacterFormat(cf3);
            tablooBaslangic[1, 0].Width = 200;
            tablooBaslangic[1, 1].Width = 200;

            tablooBaslangic[2, 0].AddParagraph().AppendText(" Bitiş Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[2, 1].AddParagraph().AppendText( GC.BitTarih.ToShortDateString()).ApplyCharacterFormat(cf3);
            tablooBaslangic[2, 0].Width = 200;
            tablooBaslangic[2, 1].Width = 200;

            tablooBaslangic[3, 0].AddParagraph().AppendText(" Başlangıç Saati :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[3, 1].AddParagraph().AppendText( GC.BasSaat.ToShortTimeString() ).ApplyCharacterFormat(cf3);
            tablooBaslangic[3, 0].Width = 200;
            tablooBaslangic[3, 1].Width = 200;

            tablooBaslangic[4, 0].AddParagraph().AppendText(" Bitiş Saati :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[4, 1].AddParagraph().AppendText( GC.BitSaat.ToShortTimeString() + "\t ").ApplyCharacterFormat(cf3);
            tablooBaslangic[4, 0].Width = 200;
            tablooBaslangic[4, 1].Width = 200;

            tablooBaslangic[5, 0].AddParagraph().AppendText(" Hafta İzin Günü :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[5, 1].AddParagraph().AppendText( GC.TatilGunu + "\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[5, 0].Width = 200;
            tablooBaslangic[5, 1].Width = 200;

            string ekleYazi = "Hayır";
            if (GC.TatilVardiyaEkle == true)
            {
                ekleYazi = "Evet";
            }
            tablooBaslangic[6, 0].AddParagraph().AppendText(" İzni Çakışması Eklenmesi :".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[6, 1].AddParagraph().AppendText( ekleYazi + "\t").ApplyCharacterFormat(cf3);

            tablooBaslangic[6, 0].Width = 200;
            tablooBaslangic[6, 1].Width = 200;

            tablooBaslangic.Rows[0].Height = 35;
            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic.Rows[1].Height = 35;
            tablooBaslangic.Rows[2].Height = 35;
            tablooBaslangic.Rows[3].Height = 35;
            tablooBaslangic.Rows[4].Height = 35;
            tablooBaslangic.Rows[5].Height = 35;
            tablooBaslangic.Rows[6].Height = 35;
          

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            textRange = paragraph.AppendText(" Gece Çalışılan Günler :\t ".ToUpper()) as WTextRange; ;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();

            string gunAdlari = "";
            if(GC.PSal==true)
            {
                gunAdlari = gunAdlari +  "Pazartesi-Salı,  ";
            }
            if(GC.SCar==true)
            {
                gunAdlari = gunAdlari + "Salı-Çarşamba, ";
            }
            if(GC.CPer==true)
            {
                gunAdlari = gunAdlari + "Çarşamba- Perşembe, ";

            }

            if(GC.PCum == true)
            {
                gunAdlari = gunAdlari + "Perşembe-Cuma, ";
            }
            if(GC.CCmt== true)
            {
                gunAdlari = gunAdlari + "Cuma-Cumartesi, ";
            }
            if(GC.CPzr== true)
            {
                gunAdlari = gunAdlari + "Cumartesi-Pazar, ";
            }
            if(GC.PPzt== true)
            {
                gunAdlari = gunAdlari + "Pazar-Pazartesi, ";
            }

            textRange = paragraph.AppendText("  " +gunAdlari.ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
          
            textRange.CharacterFormat.FontName = "Times New Roman";

            //------ Hesplama Yazıları
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.5f;
            tabloo.TableFormat.CellSpacing = 0;

            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.ResetCells(6, 2);
            tabloo.IndentFromLeft = 80;

            tabloo[0, 0].AddParagraph().AppendText(" Toplam Brüt :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamBrut = String.Format("{0:C2}", GC.Toplam);
            tabloo[0, 1].AddParagraph().AppendText(toplamBrut + "\t").ApplyCharacterFormat(cf1);
      

            tabloo[1, 0].AddParagraph().AppendText(" Sgk :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamSgk = String.Format("{0:C2}", GC.SGK);
            tabloo[1, 1].AddParagraph().AppendText( toplamSgk + "\t").ApplyCharacterFormat(cf1);

            tabloo[2, 0].AddParagraph().AppendText("Vergi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamVergi = String.Format("{0:C2}", GC.Vergi);
            tabloo[2, 1].AddParagraph().AppendText("   " + toplamVergi ).ApplyCharacterFormat(cf1);

            tabloo[3, 0].AddParagraph().AppendText(" Damga Vergisi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamDamgaV = String.Format("{0:C2}", GC.DamgaV);
            tabloo[3, 1].AddParagraph().AppendText(toplamDamgaV + "\t").ApplyCharacterFormat(cf1);

            tabloo[4, 0].AddParagraph().AppendText(" İşsizlik Sigortası :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamIssizlik = String.Format("{0:C2}", GC.Issizlik);
            tabloo[4, 1].AddParagraph().AppendText("   " + toplamIssizlik + "\t").ApplyCharacterFormat(cf1);

            tabloo[5, 0].AddParagraph().AppendText(" Net :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamNet = String.Format("{0:C2}", GC.Net);
            tabloo[5, 1].AddParagraph().AppendText( toplamNet + "\t").ApplyCharacterFormat(cf1);

            tabloo[0, 0].Width = 200;
            tabloo[0, 1].Width = 200;
            tabloo[1, 0].Width = 200;
            tabloo[1, 1].Width = 200;
            tabloo[2, 0].Width = 200;
            tabloo[2, 1].Width = 200;
            tabloo[3, 0].Width = 200;
            tabloo[3, 1].Width = 200;
            tabloo[4, 0].Width = 200;
            tabloo[4, 1].Width = 200;
            tabloo[5, 0].Width = 200;
            tabloo[5, 1].Width = 200;

            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            tabloo.Rows[0].Height = 45;
            tabloo.Rows[1].Height = 45;
            tabloo.Rows[2].Height = 45;
            tabloo.Rows[3].Height = 45;
            tabloo.Rows[4].Height = 45;
            tabloo.Rows[5].Height = 45;
            // Maaş Listesi

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Maaş Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 13f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();
            // Dönem İçindeki Maaşların Tablosu.....
            int maasSat = GC.MaasBilgi.Count + 1;
            IWTable tablooMaas = section.AddTable();

            tablooMaas.TableFormat.Borders.LineWidth = 0.5f;
            tablooMaas.TableFormat.CellSpacing = 0;
            tablooMaas.IndentFromLeft = 50;
            tablooMaas.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooMaas.ResetCells(maasSat, 3);

            tablooMaas[0, 0].AddParagraph().AppendText("\n Dönemi :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas[0, 1].AddParagraph().AppendText("\n Brüt :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas[0, 2].AddParagraph().AppendText("\n Net :\t \n".ToUpper()).ApplyCharacterFormat(cf1);

            tablooMaas[0, 0].Width = 150;
            tablooMaas[0, 1].Width = 150;
            tablooMaas[0, 2].Width = 150;

            var listeMaas = GC.MaasBilgi.OrderBy(o => o.yilBas).ToList();
            int s1 = 0;
            foreach (var t in listeMaas)
            {
                s1 = s1 + 1;
                tablooMaas.Rows[s1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas.Rows[s1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas.Rows[s1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tablooMaas[s1, 0].Width = 150;
                tablooMaas[s1, 1].Width = 150;
                tablooMaas[s1, 2].Width = 150;
                tablooMaas.Rows[s1].Height = 50;

                tablooMaas[s1, 0].AddParagraph().AppendText( t.yil + "\t");

                string brutMaasYazi = String.Format("{0:C2}", t.brutMaas);
                tablooMaas[s1, 1].AddParagraph().AppendText( brutMaasYazi + "\t").ApplyCharacterFormat(cf1);

                string netMaasYazi = String.Format("{0:C2}", t.netMaas);
                tablooMaas[s1, 2].AddParagraph().AppendText( netMaasYazi + "\t").ApplyCharacterFormat(cf1);
            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            //İzin Listesi

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzin Tablosu") as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();

            //-------------------
            // Dönem İçindeki İzinlerin Tablosu........
            //--------
            int izinSat = GC.IzinKaytilariBilgi.Count + 1;
            IWTable tablooIzin = section.AddTable();
            tablooIzin.IndentFromLeft = 50;
            tablooIzin.TableFormat.Borders.LineWidth = 0.5f;
            tablooIzin.TableFormat.CellSpacing = 0;

            tablooIzin.ApplyStyle(BuiltinTableStyle.LightGrid);
            tablooIzin.ResetCells(izinSat, 3);
            int s2 = 0;
            tablooIzin[0, 0].AddParagraph().AppendText("\n Başlangıç :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin[0, 1].AddParagraph().AppendText("\n Bitiş  :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin[0, 2].AddParagraph().AppendText("\n Açıklama :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin[0, 0].Width = 150;
            tablooIzin[0, 1].Width = 150;
            tablooIzin[0, 2].Width = 150;
            var izinListe = GC.IzinKaytilariBilgi.OrderBy(o => o.BaslangicTar).ToList();
            foreach (var t in izinListe)
            {
                s2 = s2 + 1;

                tablooIzin.Rows[s2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin.Rows[s2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin.Rows[s2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tablooIzin.Rows[s2].Height = 50;
                tablooIzin[s2, 0].Width = 150;
                tablooIzin[s2, 1].Width = 150;
                tablooIzin[s2, 2].Width = 150;

                tablooIzin[s2, 0].AddParagraph().AppendText( t.BaslangicTar.ToShortDateString() + "\t");
                tablooIzin[s2, 1].AddParagraph().AppendText( t.BitisTar.ToShortDateString() + "\t");
                tablooIzin[s2, 2].AddParagraph().AppendText( t.Aciklama + "\t");
            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            // GECE ÇALIŞMA HAFTALAR.--- Günler...
            //---------------------------------------------------------------
            //Sira- BasTar-BitTar- Bas-saat Bit Saat - Toplam Saat: Toplam Gelir.

            //*-----------------------------------------
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Gece Çalışma Hafta Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            int satSay = GC.HaftalarBilgi.Count + 1;
            // Sira- Baş-Bit-MaaşBrüt- Haftalık Saat -Mesai Ücreti -Genel Toplam...
            int sutSay = 5;
            paragraph = section.AddParagraph();
            IWTable tabloo2 = section.AddTable();

            tabloo2.TableFormat.Borders.LineWidth = 0.5f;
    

            tabloo2.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo2.ResetCells(satSay, sutSay);
            tabloo2[0, 0].AddParagraph().AppendText("\n Sıra :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 1].AddParagraph().AppendText("\n Tarih :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            //tabloo2[0, 2].AddParagraph().AppendText("\n Saatler :\t \n").ApplyCharacterFormat(cf1);
            //tabloo2[0, 3].AddParagraph().AppendText("\n Saatlik Ücret :\t \n").ApplyCharacterFormat(cf1);
            tabloo2[0, 2].AddParagraph().AppendText("\n Mesai Saati :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 3].AddParagraph().AppendText("\n Mesai Ücreti :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 4].AddParagraph().AppendText("\n Genel Toplam :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 0].Width = 100;
            tabloo2[0, 1].Width = 100;
            tabloo2[0, 2].Width = 100;
            tabloo2[0, 3].Width = 100;
            tabloo2[0, 4].Width = 110;
            //-----------------------------------------------------------------
            int i = 0;
            decimal genelToplamGC= 0;
            //ListeGunler

            var listee = GC.HaftalarBilgi.OrderBy(o => o.BasTarih);
            foreach (var t in listee)
            {
                i = i + 1;

                tabloo2[i, 0].AddParagraph().AppendText(i.ToString());

                tabloo2.Rows[i].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2.Rows[i].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2.Rows[i].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2.Rows[i].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                string tarihYazi = "";
                tabloo2[i, 0].Width = 100;
                tabloo2[i, 1].Width = 100;
                tabloo2[i, 2].Width = 100;
                tabloo2[i, 3].Width = 100;
                tabloo2[i, 4].Width = 110;
                tabloo2.Rows[i].Height = 50;

                tabloo2[i, 1].AddParagraph().AppendText( t.BasTarih.ToShortDateString() + "\t \n" + t.BitTarih.ToShortDateString());

                //tabloo2[0, 2].AddParagraph().AppendText (t.bas.ToShortTimeString()  + "\t \n" + t.BitSaat.ToShortTimeString());

                //string saatUcretYazi = String.Format("{0:C2}", t.SaatlikUcret);
                //tabloo2[0, 2].AddParagraph().AppendText("\n" + saatUcretYazi + "\t \n");

                tabloo2[i, 2].AddParagraph().AppendText( t.GerceklesenFazlaMesaiSaati.ToString() );

                //Saatlik Miktar
                string miktarYazi = String.Format("{0:C2}", t.GeceUcret);
                tabloo2[i, 3].AddParagraph().AppendText( miktarYazi );


                genelToplamGC= genelToplamGC + t.GeceUcret;
                string genelToplamYazi = String.Format("{0:C2}", genelToplamGC);
                tabloo2[i, 4].AddParagraph().AppendText( genelToplamYazi );


            }

            //----------------------------------------------------------------
            // Kaydetme Bölümü....
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);

            var status =await  Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            var sayfa = new Views.PdfV.PdfView(stream, "GeceCalismaRapor", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            //Save the stream as a file in the device and invoke it for viewing
            //var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("GC_GC.docx", "application/msword", stream);
      //  var yoll=      await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("Output.pdf", "application / pdf", stream);


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

    public class GeceGunBilgi
    {
        public string Id { get; set; }
        public int Sira { get; set; }
        public string KayitId { get; set; }
        public DateTime BasTar { get; set; }
        public DateTime BitTar { get; set; }
        public DateTime BasSaat { get; set; }
        public DateTime BitSaat { get; set; }
        public double NetSaat { get; set; }
        public decimal SaatlikUcret { get; set; }
        public decimal Miktar { get; set; }

    }
}
