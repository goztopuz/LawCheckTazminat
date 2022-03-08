using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basamak7GCViewModel: ViewModelBase
    {

        List<MaasGeceMesai> ListeMaas = new List<MaasGeceMesai>();

        public Basamak7GCViewModel(Models.GeceCalisma _gece)
        {

            this.GC = new GeceCalisma();
            this.GC = _gece;



            ListeMaas = GC.MaasBilgi.ToList();

            ListeResmi = new ObservableCollection<GeceCakisanGun>();

            HaftalariTarihleriOlustur();

            if(GC.duzenlemede== true)
            {
                _uyariVisible = true;
            }

            kkk();

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


        private void HaftalariTarihleriOlustur()
        {


            DateTime tmpBasTar2 = GC.BasTarih;
           GC.HaftalarBilgi.Clear();
            int sayac1 = 1;
            while (tmpBasTar2 < GC.BitTarih)
            {
                GeceMesaiHaftalar hafta = new GeceMesaiHaftalar();

                hafta.Id = Guid.NewGuid().ToString().Substring(0, 10);
                hafta.BasTarih = tmpBasTar2;
                hafta.BitTarih = tmpBasTar2.AddDays(6);
                hafta.Sira = sayac1;
                hafta.GeceMesaiVar = true;

                // Hesaplama Bölümü Başlangıç......
                //------------------------------------------------------------------

                // 1- Haftanın Tüm Günlerini Gez...
                //Tüm Tarihleri Mesai Varmış gibi işle..(Sadece seçili olanları bool true yap)
                //   Her günüün saatlik ücretini bul(Başlangıçtan)

                //1- Pazartesi-Salı
                string _gunisimleri = "";

                if(GC.PSal== true)
                {
                    hafta.PSal = true;
                    _gunisimleri = _gunisimleri + "Pzt-Sal, ";
                }else
                 {
                    hafta.PSal = false;
                }
                // 2- Salı-Çar
                if(GC.SCar==true)
                {
                    hafta.SCar = true;
                    _gunisimleri = _gunisimleri + "Sal-Çar, ";

                }
                else
                {
                    hafta.SCar = false;
                }
                   // 3- Car-Per
                if(GC.CPer==true)
                {
                    hafta.CPer = true;
                    _gunisimleri = _gunisimleri + "ÇarPer, ";


                }
                else { hafta.CPer = false; }

                // 4- Per-Cuma
                if(GC.PCum==true)
                {
                    hafta.PCum = true;
                    _gunisimleri = _gunisimleri + "Per-Cum, ";

                }
                else { hafta.PCum = false; }

                //5- Cuma-Cmtsi
                if(GC.CCmt==true)
                {
                    hafta.CCmt = true;
                    _gunisimleri = _gunisimleri + "Cum-Cmt, ";

                }
                else { hafta.CCmt = false; }

                // 6- Cmt-PAz
                if(GC.CPzr== true)
                {
                    hafta.CPzr = true;
                    _gunisimleri = _gunisimleri + "Cmt-Paz, ";

                }
                else { hafta.CPzr = false; }

                // 7- Pz-Pztesi
                if(GC.PPzt==true)
                {
                    hafta.PPzt = true;
                    _gunisimleri = _gunisimleri + "Paz-Pzt";
                }
                else { hafta.PPzt = false; }

                int isimUzunluk = _gunisimleri.Length;
                if(isimUzunluk>2)
                {
                    _gunisimleri = _gunisimleri.Substring(0, isimUzunluk - 2);
                }

                hafta.GunIsimleri = _gunisimleri;

                

                DateTime tmp3 = hafta.BasTarih;
                while(tmp3<= hafta.BitTarih)
                {

                    // Hangi Gün??
                    DayOfWeek gunn = tmp3.DayOfWeek;

                    if(gunn== DayOfWeek.Monday)
                    {
                      
                            //Pazartesi- Salı
                            hafta.TarPSal = tmp3.Date;
                            hafta.BitTarPSal = tmp3.Date.AddDays(1);
                            hafta.BasSaatPSal = GC.BasSaat;
                            hafta.BitSaatPSal = GC.BitSaat;

                            var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarPSal && o.yilBit >= hafta.TarPSal);
                            decimal saatlik = maas.brutMaas / (decimal)225;
                            hafta.SaatlikPSal = saatlik;
                            hafta.NetSaatEllePSal = GC.GeceFazlaSaatElle;
                        
                    }
                    if(gunn== DayOfWeek.Tuesday)
                    {
                      
                        //Salı-Çarşamba
                        hafta.TarSCar = tmp3.Date;
                        hafta.BitTarSCar = tmp3.Date.AddDays(1);
                        hafta.BasSaatSCar = GC.BasSaat;
                        hafta.BitSaatSCar = GC.BitSaat;


                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarSCar && o.yilBit >= hafta.TarSCar);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikSCar = saatlik;
                        hafta.NetSaatElleSCar = GC.GeceFazlaSaatElle;

                        
                    }
                    if(gunn== DayOfWeek.Wednesday)
                    {
                        //Çar-Per
                    
                        hafta.TarCPer = tmp3.Date;
                        hafta.BitTarCPer = tmp3.Date.AddDays(1);
                        hafta.BasSaatCPer = GC.BasSaat;
                        hafta.BitSaatCPer = GC.BitSaat;


                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarCPer && o.yilBit >= hafta.TarCPer);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikCPer = saatlik;
                        hafta.NetSaatElleCPer = GC.GeceFazlaSaatElle;

                        

                    }
                    if(gunn == DayOfWeek.Thursday)
                    {
                        // Per-Cuma
                     
                        hafta.TarPCum = tmp3.Date;
                        hafta.BitTarPCum = tmp3.Date.AddDays(1);
                        hafta.BasSaatPCum = GC.BasSaat;
                        hafta.BitSaatPCum = GC.BitSaat;


                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarPCum && o.yilBit >= hafta.TarPCum);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikPCum = saatlik;

                        hafta.NetSaatEllePCum = GC.GeceFazlaSaatElle;

                    


                    }
                    if(gunn== DayOfWeek.Friday)
                    {
                    
                        //cuma-cmtsi
                        hafta.TarCCmt = tmp3.Date;
                        hafta.BitTarCCmt = tmp3.Date.AddDays(1);
                        hafta.BasSaatCCmt = GC.BasSaat;
                        hafta.BitSaatCCmt = GC.BitSaat;


                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarCCmt && o.yilBit >= hafta.TarCCmt);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikCCmt = saatlik;

                        hafta.NetSaatElleCCmt = GC.GeceFazlaSaatElle;

                        
                    }
                    if(gunn == DayOfWeek.Saturday)
                    {
                        
                        // Cmts-Pazar
                        hafta.TarCPzr = tmp3.Date;
                        hafta.BitTarCPzr = tmp3.Date.AddDays(1);
                        hafta.BasSaatCPzr = GC.BasSaat;
                        hafta.BitSaatCPzr = GC.BitSaat;


                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarCPzr && o.yilBit >= hafta.TarCPzr);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikCPzr = saatlik;

                        hafta.NetSaatelleCPzr = GC.GeceFazlaSaatElle;

                        
                    }
                    if (gunn== DayOfWeek.Sunday)
                    {

                     
                        //Pazar-Pazartesi

                        hafta.TarPPzt = tmp3.Date;
                        hafta.BitTarPPzt = tmp3.Date.AddDays(1);
                        hafta.BasSaatPPzt = GC.BasSaat;
                        hafta.BitSaatPPzt = GC.BitSaat;

                        //Maaş
                        var maas = ListeMaas.Find(o => o.yilBas.Date <= hafta.TarPPzt && o.yilBit >= hafta.TarPPzt);
                        decimal saatlik = maas.brutMaas / (decimal)225;
                        hafta.SaatlikPPzt = saatlik;

                        // Fazla Mesai Ucret sayısı

                        hafta.NetSaatEllePPzt = GC.GeceFazlaSaatElle;

                        
                    }



                    tmp3 = tmp3.AddDays(1);
                }


          

                //2- 2. Döngü olabilir. bool true ise ucreti hesapla....
                // Son haftayasa Tarih aşımını kontrol et... Her Günün Saatlerin Bu
                //Saatlik Maaşla çarp



                //hafta.ToplamNetSaat = hafta.NetSaatPPzt + hafta.NetSaatPSal + hafta.NetSaatSCar +
                //    hafta.NetSaatCPer + hafta.NetSaatPCum + hafta.NetSaatCCmt + hafta.NetSaatCPzr;

                hafta.ToplamSaatElle = 0;
                if(GC.PSal== true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatEllePSal);
                    if(GC.TatilVardiyaiDus==true)
                    {
                        if(GC.TatilGunu=="Pazartesi")
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle  -(double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.SCar== true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatElleSCar);

                    if (GC.TatilVardiyaiDus == true)
                    {
                        if (GC.TatilGunu == "Salı" )
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.CPer== true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatElleCPer);

                    if (GC.TatilVardiyaiDus == true)
                    {

                        if (GC.TatilGunu == "Çarşamba" )
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.PCum)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatEllePCum);

                    if (GC.TatilVardiyaiDus == true)
                    {

                        if (GC.TatilGunu == "Perşembe" )
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.CCmt==true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatElleCCmt);

                    if (GC.TatilVardiyaiDus == true)
                    {

                        if (GC.TatilGunu == "Cuma")
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.CPzr == true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatelleCPzr);

                    if (GC.TatilVardiyaiDus == true)
                    {

                        if (GC.TatilGunu == "Cumartesi")
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);

                        }
                    }

                }
                if(GC.PPzt == true)
                {
                    hafta.ToplamSaatElle = hafta.ToplamSaatElle + (double)(hafta.NetSaatEllePPzt);

                    if (GC.TatilVardiyaiDus == true)
                    {
                        if (GC.TatilGunu == "Pazar")
                        {
                            hafta.ToplamSaatElle = hafta.ToplamSaatElle - (double)(hafta.NetSaatEllePSal);
                        }
                    }

                }

            


                //hafta.ToplamSaatElle = (double) (hafta.NetSaatEllePPzt + hafta.NetSaatEllePSal + hafta.NetSaatElleSCar +
                //    hafta.NetSaatElleCPer + hafta.NetSaatEllePCum + hafta.NetSaatElleCCmt + hafta.NetSaatelleCPzr);

                //// Hesaplama Bölümü Bitiş......
                //------------------------------------------------------------------

                GC.HaftalarBilgi.Add(hafta);

                tmpBasTar2 = tmpBasTar2.AddDays(7);
                sayac1 = sayac1 + 1;
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




        public void kkk()
        {
            MessagingCenter.Subscribe<GeceMesaiHaftalar>(this, "YenileGCHaftalar", async (aa) =>
            {
                VerileriYenile(aa);
            });

        }
    
        private void VerileriYenile(GeceMesaiHaftalar _gcc)
        {
            //      this.FM = _ffm;
            ObservableCollection<GeceMesaiHaftalar> Liste = new ObservableCollection<GeceMesaiHaftalar>();

            var kayit = GC.HaftalarBilgi.Where(o => o.Id == _gcc.Id).FirstOrDefault();
            kayit = _gcc;


            //1- Pazartesi-Salı
            string _gunisimleri = "";

            if (GC.PSal == true)
            {
                kayit.PSal = true;
                _gunisimleri = _gunisimleri + "Pzt-Sal, ";
            }
            else
            {
                kayit.PSal = false;
            }
            // 2- Salı-Çar
            if (GC.SCar == true)
            {
               kayit.SCar = true;
                _gunisimleri = _gunisimleri + "Sal-Çar, ";

            }
            else
            {
                kayit.SCar = false;
            }
            // 3- Car-Per
            if (GC.CPer == true)
            {
                kayit.CPer = true;
                _gunisimleri = _gunisimleri + "ÇarPer, ";


            }
            else { kayit.CPer = false; }

            // 4- Per-Cuma
            if (GC.PCum == true)
            {
                kayit.PCum = true;
                _gunisimleri = _gunisimleri + "Per-Cum, ";

            }
            else { kayit.PCum = false; }

            //5- Cuma-Cmtsi
            if (GC.CCmt == true)
            {
                kayit.CCmt = true;
                _gunisimleri = _gunisimleri + "Cum-Cmt, ";

            }
            else { kayit.CCmt = false; }

            // 6- Cmt-PAz
            if (GC.CPzr == true)
            {
                kayit.CPzr = true;
                _gunisimleri = _gunisimleri + "Cmt-Paz, ";

            }
            else { kayit.CPzr = false; }

            // 7- Pz-Pztesi
            if (GC.PPzt == true)
            {
                kayit.PPzt = true;
                _gunisimleri = _gunisimleri + "Paz-Pzt";
            }
            else { kayit.PPzt = false; }

            int isimUzunluk = _gunisimleri.Length;
            if (isimUzunluk > 2)
            {
                _gunisimleri = _gunisimleri.Substring(0, isimUzunluk - 2);
            }

            kayit.GunIsimleri = _gunisimleri;

            foreach (var t in GC.HaftalarBilgi)
            {
                Liste.Add(t);
            }
            


            GC.HaftalarBilgi.Clear();

            foreach (var t in Liste)
            {
                GC.HaftalarBilgi.Add(t);
            }

            
        }


        //------
        public ICommand HaftaTappedCommand => new Command<GeceMesaiHaftalar>(OnHaftaTapped);
        async private void OnHaftaTapped(GeceMesaiHaftalar _hafta)
        {
            var sayfaHaftaBilgi = new Views.GeceCalisma.HaftaBilgiView(GC,_hafta);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfaHaftaBilgi);


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

            var sayfa = new Views.GeceCalisma.Basamak8GCView(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            this.IsBusy = false;
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
            IsBusy = false;

        }

        public bool _uyariVisible = false;

        public bool UyariVisible
        {
            get => _uyariVisible;
            set
            {
                _uyariVisible = value;
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
