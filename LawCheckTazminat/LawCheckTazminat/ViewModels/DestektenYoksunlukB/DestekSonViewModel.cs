using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.Services.DestektenYoksunlukHesapService;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using LawCheckTazminat.Dependency;


using Xamarin.Essentials;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class DestekSonViewModel : ViewModelBase
    {

        YasamSuresiService yasamSureIslem = new YasamSuresiService();

        MaasHesaplaService _maasHesaplaIslem = new MaasHesaplaService();
        public List<KisiTazminatBilgi> kisiTazminatListesi = new List<KisiTazminatBilgi>();

        DestektenYoksunluk AktifDestek = new DestektenYoksunluk();


        DateTime _yasaydiVefatTarihi_TRH;

        DateTime _destekCikisTarihi_TRH;


        List<TRH_Trafik> trhListe = new List<TRH_Trafik>();
        TRHTablooo tt22;


        Decimal SonMaas = 0;

        AsgariUcretService islem2 = new AsgariUcretService();

        List<AsgariUcretTablosu> asgariMaasListesi = new List<AsgariUcretTablosu>();
   


        public DestekSonViewModel(DestektenYoksunluk dY)
        {

           
            this.Destek = new DestektenYoksunluk();
            this.Destek = dY;
            AktifDestek = dY;
            TrafikMi = false;
            SonMaas = dY.isleyecekMaas;

            decimal isMik = dY.isleyecekMaas;

            if(dY.trafikKazasi=="Evet")
            {
                tt22 = new TRHTablooo();

                TrafikMi = true;
                
            }
            BilgileriAl();

            Hesapla();



        }



        DestektenYoksunluk _destek;
        public DestektenYoksunluk Destek
        {
            get => _destek;
            set
            {
                _destek = value;
                OnPropertyChanged();
            }

        }



        private bool _trafikMi;

        public bool TrafikMi
        {
            get => _trafikMi;
            set
            {
                _trafikMi = value;
                OnPropertyChanged();
            }
        }



        private void BilgileriAl()
        {
            Ad = Destek.ad + " " + Destek.soyad;
            OlayTar = Destek.vefatTarihi.ToShortDateString();
            RaportTar = Destek.raporTarihi.ToShortDateString();
            KusurOran = "% " + Destek.kusurOrani;
            DogumTar = Destek.dogumTarihi.ToShortDateString();
            IsleyecekMaas = Destek.isleyecekMaas.ToString("C", CultureInfo.CurrentCulture);
            EmekliMaas = Destek.emekliMaas.ToString("C", CultureInfo.CurrentCulture);


            if (Destek.esEvlenmeElleHesapla == true)
            {
                EvlenmeIhtimali = Convert.ToDouble(Destek.esEvlenmeElle);
            }
            else
            {
                EvlenmeIhtimali = Convert.ToDouble(Destek.esEvlenmeYuzdesi);
            }



            EsinEvlenmeYuzdesi = "% " + EvlenmeIhtimali.ToString();

        }

        private string _ad;
        public string Ad
        {
            get => _ad;
            set
            {
                _ad = value;
                OnPropertyChanged();
            }
        }

        private string _soyad;
        public string Soyad
        {
            get => _soyad;
            set
            {
                _soyad = value;
                OnPropertyChanged();
            }
        }

        private string _olayTar;
        public string OlayTar
        {
            get => _olayTar;
            set
            {
                _olayTar = value;
                OnPropertyChanged();
            }
        }

        private string _raporTar;
        public string RaportTar
        {
            get => _raporTar;
            set
            {
                _raporTar = value;
                OnPropertyChanged();
            }
        }

        private string _kusuroran;
        public string KusurOran
        {
            get => _kusuroran;
            set
            {
                _kusuroran = value;
                OnPropertyChanged();

            }
        }

        private string _dogumTar;
        public string DogumTar
        {
            get => _dogumTar;
            set
            {
                _dogumTar = value;
                OnPropertyChanged();
            }
        }

        

        private string _isleyecekMaas;
        public string IsleyecekMaas
        {
            get => _isleyecekMaas;
            set
            {
                _isleyecekMaas = value;
                OnPropertyChanged();
            }
        }

        private string _emekliMaas;
        public string EmekliMaas
        {
            get => _emekliMaas;
            set
            {
                _emekliMaas = value;
                OnPropertyChanged();
            }

        }

        private decimal _evlenmeIndirimMiktar;
        public decimal EvlenmeIndirimMiktar
        {
            get => _evlenmeIndirimMiktar;
            set
            {
                _evlenmeIndirimMiktar = value;
                OnPropertyChanged();
            }
        }



        private string _yasamSuresi;
        public string YasamSuresi
        {
            get => _yasamSuresi;
            set
            {
                _yasamSuresi = value;
                OnPropertyChanged();
            }
        }

        private string _vefatTar;
        public string VefatTar
        {
            get => _vefatTar;
            set
            {
                _vefatTar = value;
                OnPropertyChanged();

            }
        }

    

        private string _esinEvlenmeYuzdesi;
        public string EsinEvlenmeYuzdesi
        {
            get => _esinEvlenmeYuzdesi;
            set
            {
                _esinEvlenmeYuzdesi = value;
                OnPropertyChanged();
            }

        }

        double _evlenmeIhtimali;
        public double EvlenmeIhtimali
        {
            get => _evlenmeIhtimali;
            set
            {
                _evlenmeIhtimali = value;
                OnPropertyChanged();
            }
        }

        private List<Yakin2> _yakinlar = new List<Yakin2>();
        public List<Yakin2> Yakinlar
        {
           
            get => _yakinlar;
            set
            {
                _yakinlar = value;

               
                OnPropertyChanged();
            }

        }



        private decimal _islemisToplam;
        public decimal IslemisToplam
        {
            get => _islemisToplam;
            set
            {

                _islemisToplam = value;
                OnPropertyChanged();
            }
        }

        private decimal _aktifToplam;
        public decimal AktifToplam
        {
            get => _aktifToplam;
            set
            {
                _aktifToplam = value;
                OnPropertyChanged();
            }
        }

        private decimal _pasifToplam;
        public decimal PasifToplam
        {
            get => _pasifToplam;
            set
            {
                _pasifToplam = value;
                OnPropertyChanged();
            }
        }


        private decimal _masrafToplam;
        public decimal MasrafToplam
        {
            get => _masrafToplam;
            set
            {
                _masrafToplam = value;
                OnPropertyChanged();
            }
        }

        private decimal _genelToplam;
        public decimal GenelToplam
        {
            get => _genelToplam;
            set
            {
                _genelToplam = value;
                OnPropertyChanged();
            }
        }



        private DateTime _destekBitis;
        public DateTime DestekBitis
        {
            get => _destekBitis;
            set
            {
                _destekBitis = value;
                OnPropertyChanged();
            }

           
        }


        private List<DestekPayBolumleri> _payBolumleri;
        List<DestekPayBolumleri> PayBolumleri
        {
            get => _payBolumleri;

            set
            {
                _payBolumleri = value;
                OnPropertyChanged();
            }

        }

        List<bolum> bolumListe = new List<bolum>();
        List<bolum> bolumListeSirali = new List<bolum>();


        async private void TrafikHesapla()
        {

        }


         async   private void Hesapla()
        {
           
            HesaplaKalanYass();

            YakinListeOlustur();

            MasrafHesapla();


            
            PayBolumleriHesapla();
            

    

             asgariMaasListesi = (await islem2.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);

            VefatTar = Destek.vefatTarihi.ToShortDateString();

            GenelHesap2();


        }


        List<MaasAy> maasListesi = new List<MaasAy>();

        List<MaasAy2> maasListesi2 = new List<MaasAy2>();


        private string DonemiBul(DateTime _tarih)
        {
            string donem = "";
            if (Destek.vefatTarihi <= _tarih && Destek.raporTarihi> _tarih)
            {
                // İşlemiş Dönem Maaşlarından seçilecek

                donem = "İşlemiş";

            }
            else if(Destek.raporTarihi<= _tarih && emeklilikTarihi> _tarih)
            {
                donem = "Aktif";

            }
            else if(_tarih >= emeklilikTarihi)
            {
                donem = "Pasif";
            }
        
            return donem;
        }


        private decimal AsgariOranla1(decimal _maas, DateTime _tarih, DateTime _olayTarihi)
        {
            decimal _sonuc = 0;
            string _yilBilgi = _olayTarihi.Year + "-1";
            if(_olayTarihi.Month>6)
            {
                _yilBilgi = _olayTarihi.Year + "-2";
            }
            string _yilBilgi2 = _tarih.Year + "-1";
            if(_tarih.Month>6)
            {
                _yilBilgi2 = _tarih.Year + "-2";
            }
           
         var   al1 = asgariMaasListesi.Find(o => o.yil ==_yilBilgi);

            if(al1 != null)
            {
                decimal ai = al1.net;

                decimal orann_ = _maas / ai;

                var al2 = asgariMaasListesi.Find(o => o.yil == _yilBilgi2);
                if(al2 != null)
                {
                    decimal ai2 = al2.net;
                    _sonuc = ai2 * orann_;
                }
            }
            return _sonuc;
        }


        private decimal OgunkuMaas3(DateTime _tarih)
        {
            decimal _ogunkuMaas = 0;

            if (Destek.BekarCocuk_SuAnCalisiyor == true)
            {
                // Eğer Rapor Tarihinden Küçükse
                // Olay Tarihi Asgariyle olay günü ücretii oranla
                // O tarihteki asgariyi bul ve Çarp  Çarp
                if (_tarih.Date < Destek.raporTarihi.Date)
                {
                    _ogunkuMaas = AsgariOranla1(Destek.BekarCocuk_SuAnkiUcret, _tarih, Destek.vefatTarihi);

                }
                else
                {

                    // Eğer rapor tarihine eşitse veya büyükse
                    // Olay günkü asgariyi maasa oranla...
                    // Rapor Tarihindei Asgari Oranla Çarp.
                    _ogunkuMaas = AsgariOranla1(Destek.BekarCocuk_SuAnkiUcret, Destek.raporTarihi, Destek.vefatTarihi);
                    // %10 Arttırım ve Azaltım uygulanması..

                    _ogunkuMaas = _ogunkuMaas * Convert.ToDecimal((1.1 * 0.9091));

                }
            }
            else
            {
                //O Günkü Tarih çalışma başlama tarihinden küçükse
                // İş öncesi ücreti al... rapor anı asgariye oranla..
                //O günkü asgariyle çarp ve gönder.
                if (_tarih < Destek.BekarCocuk_CalismaBasTarihi)
                {


                    if (_tarih < Destek.raporTarihi)
                    {
                        string yilbilgi1 = Destek.raporTarihi.Year.ToString() + "-1";

                        if (_tarih.Month > 6)
                        {
                            yilbilgi1 = Destek.raporTarihi.Year.ToString() + "-2";
                        }

                        var al1 = asgariMaasListesi.Find(o => o.yil == yilbilgi1);
                        if (al1 != null)
                        {
                            decimal ai1 = al1.net;
                            decimal oran1 = Destek.BekarCocuk_18OncesiMaas / ai1;


                            string yilbilgi2 = _tarih.Year + "-1";
                            if (_tarih.Month > 6)
                            {
                                yilbilgi2 = _tarih.Year + "-2";
                            }
                            var al2 = asgariMaasListesi.Find(o => o.yil == yilbilgi2);
                            if (al2 != null)
                            {
                                decimal ai2 = al2.net;

                                _ogunkuMaas = ai2 * oran1;

                            }


                        }




                    }

                    else
                    {
                        // %10 arttırım azaltımlı normal ücret.
                        decimal sonuc2 = Destek.BekarCocuk_18OncesiMaas;

                        _ogunkuMaas = sonuc2 * Convert.ToDecimal((1.1 * 0.9091));

                    }

                }


                else
                {
                    // O günkü Tarih başlama Tarihinden büyükse 
                    // Çalışma Maaşını %10 Arttırım ve % azaltımlı olarak hesapla ve geri gönder.

                    decimal _sonuc1 = Destek.BekarCocuk_GelecekCalismaUcreti;

                    _ogunkuMaas = _sonuc1 * Convert.ToDecimal((1.1 * 0.9091));

                }


            }
            //int gunsay = DateTime.DaysInMonth(_tarih.Year, _tarih.Month);

            //decimal gunlukMaas = _ogunkuMaas / gunsay;

            return _ogunkuMaas;
        }


        private decimal OgunkuMaas2(DateTime _tarih)
        {
            decimal _ogunkuMaas = 0;

            if(Destek.BekarCocuk_SuAnCalisiyor== true)
            {
                // Eğer Rapor Tarihinden Küçükse
                // Olay Tarihi Asgariyle olay günü ücretii oranla
                // O tarihteki asgariyi bul ve Çarp  Çarp
                if (_tarih.Date < Destek.raporTarihi.Date)
                {
                    _ogunkuMaas = AsgariOranla1(Destek.BekarCocuk_SuAnkiUcret, _tarih, Destek.vefatTarihi);

                }
                else
                {

                    // Eğer rapor tarihine eşitse veya büyükse
                    // Olay günkü asgariyi maasa oranla...
                    // Rapor Tarihindei Asgari Oranla Çarp.
                    _ogunkuMaas = AsgariOranla1(Destek.BekarCocuk_SuAnkiUcret, Destek.raporTarihi, Destek.vefatTarihi);
                    // %10 Arttırım ve Azaltım uygulanması..

                    _ogunkuMaas= _ogunkuMaas * Convert.ToDecimal((1.1 * 0.9091));

                }
            }
            else
            {
                //O Günkü Tarih çalışma başlama tarihinden küçükse
                // İş öncesi ücreti al... rapor anı asgariye oranla..
                //O günkü asgariyle çarp ve gönder.
                if(_tarih < Destek.BekarCocuk_CalismaBasTarihi)
                {
                
                        
                        if (_tarih < Destek.raporTarihi)
                        {
                        string yilbilgi1 = Destek.raporTarihi.Year.ToString() + "-1";

                        if (_tarih.Month > 6)
                        {
                            yilbilgi1 = Destek.raporTarihi.Year.ToString() + "-2";
                        }

                        var al1 = asgariMaasListesi.Find(o => o.yil == yilbilgi1);
                        if (al1 != null)
                        {
                            decimal ai1 = al1.net;
                            decimal oran1 = Destek.BekarCocuk_18OncesiMaas / ai1;


                            string yilbilgi2 = _tarih.Year + "-1";
                            if(_tarih.Month>6)
                            {
                                yilbilgi2 = _tarih.Year + "-2";
                            }
                            var al2 = asgariMaasListesi.Find(o => o.yil == yilbilgi2);
                            if(al2 != null)
                            {
                                decimal ai2 = al2.net;

                                _ogunkuMaas = ai2 * oran1;
                                
                            }


                        }




                        }

                    else
                        {
                            // %10 arttırım azaltımlı normal ücret.
                            decimal sonuc2 = Destek.BekarCocuk_18OncesiMaas;

                            _ogunkuMaas= sonuc2 * Convert.ToDecimal((1.1 * 0.9091));

                        }

                }

        
                else
                {
                    // O günkü Tarih başlama Tarihinden büyükse 
                    // Çalışma Maaşını %10 Arttırım ve % azaltımlı olarak hesapla ve geri gönder.

                    decimal _sonuc1 = Destek.BekarCocuk_GelecekCalismaUcreti;

                    _ogunkuMaas=_sonuc1 *Convert.ToDecimal((1.1 * 0.9091));

                }


            }
            int gunsay = DateTime.DaysInMonth(_tarih.Year, _tarih.Month);

         decimal   gunlukMaas = _ogunkuMaas / gunsay;

            return gunlukMaas;
        }

        private decimal OgunkuMaas(DateTime _tarih)
        {
            decimal maas = 0;
            decimal gunlukMaas = 0;

            bool agiKontrol = false;

            if(Destek.vefatTarihi<=_tarih && Destek.raporTarihi> _tarih)
            {
                // İşlemiş Dönem Maaşlarından seçilecek

                var x1 = Destek.DestektekYoksunlukMaaslar.Where(o => o.yilBas <= _tarih && o.yilBit >= _tarih).FirstOrDefault();

                if(x1 != null)
                {
                    maas = x1.netMaas;
                }
            }
            else if(Destek.raporTarihi<= _tarih && _tarih< emeklilikTarihi)
            {
                if(Destek.trafikKazasi=="Evet")                                 
                {
                    maas = Destek.isleyecekMaas;

                }
                else
                {
                    maas = Destek.isleyecekMaas * Convert.ToDecimal((1.1 * 0.9091));

                }


                if (Destek.AgiDus==true)
                {
                    agiKontrol = true;
                }
            }
            else if(emeklilikTarihi <= _tarih)
            {
                if (Destek.trafikKazasi == "Evet")
                {
                    maas = Destek.isleyecekMaas;

                }
                else
                {
                    maas = Destek.isleyecekMaas * Convert.ToDecimal((1.1 * 0.9091));

                }


            }

     

            // AGİ Durumunu Kontrol et ve Gereklise
            // Destekteki çocucuk sayısına göre Düş
            if ( maas != 0)
            { 
              
               if(agiKontrol==true )
                {
                    // Eş -Çalışma-Çocuk Sayısını  bul. -O günkü asgari Ücreti de bul...

                    // NET MAAŞ'dan düşülecek olan AGİ Miktarı

                    int a2e = Destek.raporTarihi.Year;
                    int b2e = 1;
                    if (Destek.raporTarihi.Month > 6)
                    {
                        b2e = 2;
                    }

                    string c2e = a2e.ToString() + "-" + b2e.ToString();

                    decimal asgariUcretAGie = 0;

                    AsgariUcretTablosu g103e = asgariMaasListesi.ToList().Find(o => o.yil == c2e);

                    if (g103e != null)
                    {
                        asgariUcretAGie = Convert.ToDecimal(g103e.brut);
                    }


                    var esVar2 = Destek.DestekYoksunlukYakinlar.ToList().Find(o => o.yakinlik == "Eş");

                    int esDurum2 = -1;

                    if (esVar2 != null)
                    {
                        if (esVar2.esCalisiyorMu == "Hayır")
                        {
                            esDurum2 = 0;
                        }
                        else if (esVar2.esCalisiyorMu == "Evet")
                        {
                            esDurum2 = 1;
                        }
                    }

                    int hesabaKatilacakCocukSayisi2 = 0;

                    foreach (var t in Destek.DestekYoksunlukYakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {

                            hesabaKatilacakCocukSayisi2 = hesabaKatilacakCocukSayisi2 + 1;

                        }

                    }


                    Decimal agi2e = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, hesabaKatilacakCocukSayisi2);

                    maas = maas - agi2e;

                    // Mevcut Destekteki çocuk Sayısı
                    int destekkiCocukSay = 0;

                    foreach (var t2 in Yakinlar)
                    {
                        if(t2.yakinlik=="Çocuk")
                        {
                            if (_tarih < t2.destekCikisT)
                            {
                                destekkiCocukSay = destekkiCocukSay + 1;
                            }

                        }
                    }

                    decimal agi3 = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, destekkiCocukSay);


                    maas= maas + agi3;
                }

            }



            int gunsay = DateTime.DaysInMonth(_tarih.Year, _tarih.Month);

            gunlukMaas = maas / gunsay;



            //// .....GÜNLÜK MAAŞI BUL...




            //// Hangi Bölümde olduğunu Bul
            //if(_tarih< Destek.raporTarihi)
            //{
            //    //işlemiş dönem Başlangıç ayında mı?

            //    //İşlemiş dönem Bitiş ayında mı?

            //}
            //else if(_tarih >= Destek.raporTarihi)
            //{
            //    //Emeklilik Dönemi Var mı?


            //    // Varsa

            //        // 1-

            //        // 2-

            //    // Yoksa

            //        // A-

            //        // B-


            //}



            return gunlukMaas;

        }
        DateTime emeklilikTarihi;



        List<GunlukBilgi> GunlukListe = new List<GunlukBilgi>();

        private async void GenelHesap2()
        {

            GunlukListe.Clear();
            // Emeklilik Yaşı..............
            int emeklilikYas = 60;

            if (Destek.emeklilikYasi != 0)
            {

                emeklilikYas = Convert.ToInt32(Destek.emeklilikYasi);
            }
             emeklilikTarihi = Destek.dogumTarihi.AddYears(emeklilikYas);


            // Eşin daha erken Ölme İhtimaline Karşı Son Tarih Hesaplaması....
            DateTime vefatYas3 = vefatKisi_TahminiVefati;

            var yakinEs = Yakinlar.Find(o => o.yakinlik == "Eş");

            if (yakinEs != null)
            {
                if (yakinEs.destekCikis != null)
                {
                    DateTime vefatYas1 = yakinEs.destekCikisT;


                    DateTime vefatYas2 = vefatKisi_TahminiVefati;



                    if (vefatYas1 > vefatKisi_TahminiVefati)
                    {

                        vefatYas3 = vefatKisi_TahminiVefati;

                    }
                    else
                    {
                        vefatYas3 = vefatYas1;

                    }

                }
            }


            if(Destek.BekarCocukDurum== true)
            {
                // Anne Baba Yaşlarından En büyük olanı bul.

                var anne1 = Yakinlar.Find(o => o.yakinlik == "Anne");
                DateTime dtAnne = DateTime.MinValue;
                if(anne1!= null)
                {
                    dtAnne = anne1.destekCikisT;
                }

                var baba1 = Yakinlar.Find(o => o.yakinlik == "Baba");
                DateTime dtBaba = DateTime.MinValue;
                if(baba1 != null)
                {
                    dtBaba = baba1.destekCikisT;
                }
                DateTime tmp11= DateTime.MinValue;
                if((baba1 != null) || (anne1 != null))
                {
                    if(dtAnne>= dtBaba)
                    {
                        tmp11 = dtAnne;
                    }
                    else if(dtBaba> dtAnne)
                    {
                        tmp11 = dtBaba;
                    }
                    vefatYas3 = tmp11;
                }



            }

            DestekBitis = vefatYas3;

            // Kusurn Oranı...
            decimal kusurrr = Convert.ToDecimal(Destek.kusurOrani);


            // BÖLÜMLERİN BELİRLENMESİ.................
            bolumListe.Clear();

            bolum bolumOlayTarihi = new bolum();
            bolumOlayTarihi.tur = 1;
            bolumOlayTarihi.baslangic = Convert.ToDateTime(Destek.vefatTarihi);
            bolumOlayTarihi.aciklama = "Olay Tarihi";
            bolumListe.Add(bolumOlayTarihi);

            bolum bolumRaporTarihi = new bolum();
            bolumRaporTarihi.tur = 2;
            bolumRaporTarihi.baslangic = Convert.ToDateTime(Destek.raporTarihi);
            bolumRaporTarihi.aciklama = "Rapor Tarihi";
            bolumListe.Add(bolumRaporTarihi);

            bolum bolumEmeklilikTarihi = new bolum();
            bolumEmeklilikTarihi.tur = 3;
            bolumEmeklilikTarihi.baslangic = emeklilikTarihi;
            bolumEmeklilikTarihi.aciklama = "Emeklilik Tarihi";

            if (Destek.calismaDurumu != "Ev İşleri")
            {
                bolumListe.Add(bolumEmeklilikTarihi);
            }


            bolum bolumYasamTarihi = new bolum();
            bolumYasamTarihi.tur = 4;
            bolumYasamTarihi.baslangic = vefatYas3;
            bolumYasamTarihi.aciklama = "Yasam Tarihi";
            bolumListe.Add(bolumYasamTarihi);


            // Asgari Ücretlerin  Bulunmasıı...
            AsgariUcretService islem2 = new AsgariUcretService();
            var asgariMaasListesi = (await islem2.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);



            // Bölüm Listelerinin Sıralanışı

            var bolumListe2 = bolumListe.OrderBy(o => o.baslangic).ToList();
            bolumListeSirali.Clear();
            bolumListeSirali = bolumListe2;


            //Günlük Döngü

            var tmpBas = Destek.vefatTarihi;
            int gunSira = 0;
            decimal katsayi = 1;
            while(tmpBas<= vefatYas3)
            {
                gunSira = gunSira + 1;
                GunlukBilgi gunn = new GunlukBilgi();


                //  1 - O Günlük Maaş Geliri
                //    AGi Ekle veya Çıkar..
                DateTime _tarih = tmpBas;
                decimal maas;


                if(Destek.BekarCocukDurum == false)
                {
                    maas = OgunkuMaas(_tarih);

                }
                else
                {
                    maas = OgunkuMaas2(_tarih);

                }


                // 2 - O Günlük Kusur Oranıyla Çarpılacak..

                decimal kusurCarpimi = (decimal) (kusurrr / 100);
                decimal gunlukTazminat = maas * kusurCarpimi;


                katsayi = 1;


                //  -  Günlük Bölüm Bilgisi Alınacak (işlemiş işleyecek......)

                string _donemi = "";
                _donemi = DonemiBul(_tarih);

                gunn.gunlukTazminat = gunlukTazminat;
                gunn.tarih = _tarih;
                gunn.donemi = _donemi;



                //3--  Yaşama İhtimali Çarpımı Katsayısı...

             






                // - Her Kişi içinde Gezilecek..
                // Kişi için
                decimal gunlukKisiTazminat = 0;
                var bol = payBolumleri.Find(o => o.basTar <= _tarih && o.bitTar >= _tarih);
                foreach (var t in Yakinlar)
                {
                    KisiTazminatBilgi kk = new KisiTazminatBilgi();

                    kk.kisiId = t.Id;
                    kk.donemi = _donemi;
                    if(t.destekCikisT >= _tarih)
                    {

                        var kisiPayBilgi = bol.paybolumkisi.Find(o => o.kisiId == t.Id);

                        if(kisiPayBilgi != null)
                        {
                            double pay = kisiPayBilgi.payoran;
                            double payda = kisiPayBilgi.toplamPay;


                            if( Destek.BekarCocukDurum== true)
                            {
                                gunlukKisiTazminat = gunlukTazminat * Convert.ToDecimal(PayBolumleriHesaplaBekar(_tarih));
                            }
                            else
                            {
                                gunlukKisiTazminat = gunlukTazminat * Convert.ToDecimal((pay / payda));

                            }

                        }

                        if (t.yakinlik == "Eş")
                        {
                            // Dönemi İşlemiş değilse Evlenme Yüzdesini Düş..

                            if(_donemi != "İşlemiş")
                            {
                                decimal carpimYuzde = Convert.ToDecimal((100 - Destek.esEvlenmeYuzdesi) / 100);
                                gunlukKisiTazminat = gunlukKisiTazminat * carpimYuzde;

                            }

                        }

                        if (_donemi != "İşlemiş" && Destek.trafikKazasi == "Evet" && Destek.trafikTRh==true)
                        {
                            katsayi = YasamaIhtimali(AktifDestek.cinsiyet, AktifDestek.dogumTarihi,
                                Convert.ToDateTime(AktifDestek.raporTarihi), Convert.ToDateTime(gunn.tarih)
                                , t.destekCikisT);
                        }

                        gunlukKisiTazminat = katsayi * gunlukKisiTazminat;

                        kk.alinanMiktar = gunlukKisiTazminat;
                        kk.Isim = t.ad;
                        gunn.kisiListe.Add(kk);
                    }
                                       
                }


                // 4-ExtraConditions...(Erkekse-Askerlik yapmamışsa Tazminat düşülecek)

                if (Destek.cinsiyet == "Erkek")
                {
                    if (Destek.askerlikYapti == "YapMAdı")
                    {
                        DateTime askereGidis = new DateTime(Destek.askereGidisYil, Destek.askereGidisAy, 1);
                        if (askereGidis != null)
                        {

                            DateTime askerlikBasTar = askereGidis;
                            DateTime askelikBitisTar = askerlikBasTar.AddMonths(Convert.ToInt32(Destek.askerlikSuresi));


                            if(_tarih>= askelikBitisTar && _tarih <= askelikBitisTar)
                            {
                                gunn.gunlukTazminat = 0;
                                foreach(var t2 in gunn.kisiListe)
                                {
                                    t2.alinanMiktar = 0;
                                }
                            }

                        }
                    }
                }



            // Bir Gün Arttırılarak Devam Edilecek..
                GunlukListe.Add(gunn);
                tmpBas = tmpBas.AddDays(1);


            }


            decimal _masrafToplam;

            // Genel Toplamlar

            decimal _islemisToplam = 0;
            decimal _aktifToplam = 0;
            decimal _pasifToplam = 0;
            decimal _genelToplam = 0;

            foreach(var t in GunlukListe)
            {

                foreach(var t2 in t.kisiListe)
                {
                    if(t.donemi=="İşlemiş")
                    {
                        _islemisToplam = _islemisToplam + t2.alinanMiktar;
                    }
                    else if( t.donemi=="Aktif")
                    {
                        _aktifToplam = _aktifToplam + t2.alinanMiktar;
                    }
                    else if( t.donemi=="Pasif")
                    {
                        _pasifToplam = _pasifToplam + t2.alinanMiktar;
                    }
                    
                }

            }


            AktifToplam = _aktifToplam;

            IslemisToplam = _islemisToplam;

            PasifToplam = _pasifToplam;

            _masrafToplam = MasrafToplam;
            _genelToplam = _aktifToplam + _islemisToplam + _pasifToplam + _masrafToplam;

            GenelToplam = _genelToplam;


           

        }

        


        private async void GenelHesap()
        {

            // Emeklilik Yaşı..............
            int emeklilikYas = 60;

            if (Destek.emeklilikYasi != 0)
            {

                emeklilikYas = Convert.ToInt32(Destek.emeklilikYasi);
            }

            DateTime emeklilikTarihi = Destek.dogumTarihi.AddYears(emeklilikYas);


            // Hesaplama Bitiş Tarihinin Belirlenmesi..........
            DateTime vefatYas3 = vefatKisi_TahminiVefati;

            var yakinEs = Yakinlar.Find(o => o.yakinlik == "Eş");

            if (yakinEs != null)
            {
                if (yakinEs.destekCikis != null)
                {
                    DateTime vefatYas1 = yakinEs.destekCikisT;


                    DateTime vefatYas2 = vefatKisi_TahminiVefati;



                    if (vefatYas1 > vefatKisi_TahminiVefati)
                    {

                        vefatYas3 = vefatKisi_TahminiVefati;

                    }
                    else
                    {
                        vefatYas3 = vefatYas1;

                    }

                }
            }


            //KUSUR ORANI

            decimal kusurrr = Convert.ToDecimal(Destek.kusurOrani);


            // BÖLÜMLERİN BELİRLENMESİ.................
            bolumListe.Clear();

            bolum bolumOlayTarihi = new bolum();
            bolumOlayTarihi.tur = 1;
            bolumOlayTarihi.baslangic = Convert.ToDateTime(Destek.vefatTarihi);
            bolumOlayTarihi.aciklama = "Olay Tarihi";
            bolumListe.Add(bolumOlayTarihi);

            bolum bolumRaporTarihi = new bolum();
            bolumRaporTarihi.tur = 2;
            bolumRaporTarihi.baslangic = Convert.ToDateTime(Destek.raporTarihi);
            bolumRaporTarihi.aciklama = "Rapor Tarihi";
            bolumListe.Add(bolumRaporTarihi);

            bolum bolumEmeklilikTarihi = new bolum();
            bolumEmeklilikTarihi.tur = 3;
            bolumEmeklilikTarihi.baslangic = emeklilikTarihi;
            bolumEmeklilikTarihi.aciklama = "Emeklilik Tarihi";

            if (Destek.calismaDurumu != "Ev İşleri")
            {
                bolumListe.Add(bolumEmeklilikTarihi);
            }


            bolum bolumYasamTarihi = new bolum();
            bolumYasamTarihi.tur = 4;
            bolumYasamTarihi.baslangic = vefatYas3;
            bolumYasamTarihi.aciklama = "Yasam Tarihi";
            bolumListe.Add(bolumYasamTarihi);


            int siraa = 0;
            // HESAP BAŞLANGIÇ



            AsgariUcretService islem2 = new AsgariUcretService();
            var asgariMaasListesi = (await islem2.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);

            maasListesi.Clear();

            maasListesi2.Clear();

            var bolumListe2 = bolumListe.OrderBy(o => o.baslangic).ToList();
            bolumListeSirali.Clear();
            bolumListeSirali = bolumListe2;

            int tt = 1;

            int j = bolumListe2.Count();

            int emekliDurum = 0;

            int islenmisDurum = 0;
            var gecmisMaasDegerleri = Destek.DestektekYoksunlukMaaslar.ToList();

            //--------------------------------------------------------------------------------------
            // NET MAAŞ'dan düşülecek olan AGİ Miktarı

            int a2e = Destek.raporTarihi.Year;
            int b2e = 1;
            if (Destek.raporTarihi.Month > 6)
            {
                b2e = 2;
            }

            string c2e = a2e.ToString() + "-" + b2e.ToString();

            decimal asgariUcretAGie = 0;

            AsgariUcretTablosu g103e = asgariMaasListesi.ToList().Find(o => o.yil == c2e);

            if (g103e != null)
            {
                asgariUcretAGie = Convert.ToDecimal(g103e.brut);
            }


            var esVar2 = Destek.DestekYoksunlukYakinlar.ToList().Find(o => o.yakinlik == "Eş");

            int esDurum2 = -1;

            if (esVar2 != null)
            {
                if (esVar2.esCalisiyorMu == "Hayır")
                {
                    esDurum2 = 0;
                }
                else if (esVar2.esCalisiyorMu == "Evet")
                {
                    esDurum2 = 1;
                }
            }

            int hesabaKatilacakCocukSayisi2 = 0;

            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Çocuk")
                {

                    hesabaKatilacakCocukSayisi2 = hesabaKatilacakCocukSayisi2 + 1;

                }

            }


            Decimal agi2e = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, hesabaKatilacakCocukSayisi2);


            //kisi.maas = Convert.ToDecimal(kisi.ekBilgi4);

            SonMaas = Destek.isleyecekMaas - agi2e;

            //Kişi Maaşından AGİ'nin temizlenme kodu sonu....



            // ANA FOR YAPISI...........

            for (int i = 0; i < j; i++)
            {
                if (i + 1 == j)
                {

                    break;
                }



                string bolumAciklama = "";
                DateTime bolBas = bolumListe2[i].baslangic;
                DateTime bolBit = bolumListe2[i + 1].baslangic.AddDays(-1);

                bolumAciklama = bolumListe2[i].tur.ToString();

                if (bolumListe2[i].tur == 2)
                {
                    islenmisDurum = 1;
                }

                if (bolumListe2[i].tur == 3)
                {
                    emekliDurum = 1;
                }


                //Vefat Tarihi Çıkartılacak!
                if (bolumListe2[i].tur == 4)
                {
                    break;
                }

                if (gecmisMaasDegerleri.Count == 0)
                {
                    return;
                }


                // Başlangıç GÜN Ve- AY - Son Gün Hesapları

                DateTime tar1A = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);

                int gunnX = (tar1A - bolBas).Days + 1;

                DateTime tmpTarX = tar1A;

                int ayyX = 0;


                while (tmpTarX < bolBit)
                {

                    ayyX = ayyX + 1;

                    tmpTarX = tmpTarX.AddMonths(1);

                }

                if (bolBas.Day > bolBit.Day)
                {
                    ayyX = ayyX - 2;
                }

                else if (bolBas.Day <= bolBit.Day)
                {

                    ayyX = ayyX - 2;
                }
                int gun2X = bolBit.Day;


                DateTime tazminatBas1X = Convert.ToDateTime(bolBas);

                decimal gunToplamX1 = 0;



                //  Gün AY ADet Hesaplamaları Bitiş


                //1- BAŞLANGIÇ GÜNLER
                //Gün Bölüm1
                if (bolBas.Day == 1)
                {

                    decimal oAykiKisiMaasi = 0;
                    MaasAy m = new MaasAy();

                    m.tarih = bolBas;

                    DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT;


                    var s7 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;
                    int a = bolBas.Year;
                    int b = 1;
                    if (bolBas.Month > 6)
                    {
                        b = 2;
                    }


                    string c = a.ToString() + "-" + b.ToString();
                    AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g100 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > bolBas)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }


                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);





                    if (s7 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s7.netMaas);






                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                    }







                    if (islenmisDurum == 0)
                    {
                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;

                        oAykiKisiMaasi = oAykiKisiMaasi + agi;

                        m.donemi = "İşlenmiş";

                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;

                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(Destek.emekliMaas);
                        m.donemi = "Pasif";
                    }

                    m.aciklama = bolumAciklama;


                    m.maas = oAykiKisiMaasi;


                    m.maas2 = m.maas;





                    // BolBaştaki Maaş Bulunacak 



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }

                    decimal kusur = Convert.ToDecimal(kusurrr);





                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));



                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;
                    tazminatBas1X = tazminatBas1X.AddMonths(1);
                    //AY ALINACAK
                    //  gunToplamX1 = maasIlk;

                }
                else
                {
                    MaasAy m = new MaasAy();

                    m.tarih = bolBas;

                    DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT2;

                    // AY MAAŞ Güne çevrilip Gün olarak bulunacak

                    decimal oAykiKisiMaasi = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;
                    int a = bolBas.Year;
                    int b = 1;
                    if (bolBas.Month > 6)
                    {
                        b = 2;
                    }



                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);
                    if (g100 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }

                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > bolBas)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }

                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);



                    var s8 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                    if (s8 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s8.netMaas);




                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);




                    }




                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";


                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(Destek.emekliMaas);
                        m.donemi = "Pasif";
                    }

                    m.aciklama = bolumAciklama;
                    gunnX = gunnX - 1;
                    System.Decimal gunMaas = oAykiKisiMaasi / 30;
                    gunToplamX1 = gunMaas * gunnX;

                    Decimal agi2 = (agi / 30) * gunnX;
                    m.maas = gunToplamX1;

                    m.maas2 = m.maas;


                    if (islenmisDurum != 0)
                    {

                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }


                    decimal kusur = Convert.ToDecimal(kusurrr);


                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));

                    m.aciklama = bolumAciklama;


                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;
                    tazminatBas1X = tazminatBas1X.AddMonths(1);

                }









                // AYLAR

                for (int j2 = 0; j2 < ayyX + 1; j2++)
                {


                    MaasAy m = new MaasAy();
                    decimal oAykiKisiMaasi = 0;


                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                    DateTime tmpT = m.tarih.AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT2;

                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();


                    AsgariUcretTablosu g105 = asgariMaasListesi.Find(o => o.yil == c);


                    if (g105 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g105.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > bolBas)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }



                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);

                    var s1 = Destek.DestektekYoksunlukMaaslar.ToList().Find(o => o.yilBas <= tazminatBas1X && o.yilBit >= tazminatBas1X);


                    if (s1 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s1.netMaas);


                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);






                    }



                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        m.maas = oAykiKisiMaasi;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.maas2 = oAykiKisiMaasi;


                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        m.maas = oAykiKisiMaasi;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                        m.maas2 = oAykiKisiMaasi;
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(Destek.emekliMaas);
                        m.maas = oAykiKisiMaasi;
                        m.donemi = "Pasif";
                        m.maas2 = oAykiKisiMaasi;

                    }

                    m.aciklama = bolumAciklama;



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }



                    decimal kusur = Convert.ToDecimal(kusurrr);

                    decimal kayipOran = 0;




                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));

                    m.aciklama = bolumAciklama;


                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;


                    tazminatBas1X = tazminatBas1X.AddMonths(1);
                    //ToplamX1 = ToplamX1 + maas;

                }






                // GÜN BÖLÜM2
                DateTime sonAyKontrol = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                if (bolBit.AddDays(1).Month != sonAyKontrol.Month)
                {

                    MaasAy m = new MaasAy();

                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                    m.tarih2 = bolBit;
                    decimal oAykiKisiMaasi = 0;
                    m.aciklama = bolumAciklama;

                    var s14 = Destek.DestektekYoksunlukMaaslar.ToList().Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                    Decimal oAykiAsgariUcret = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g107 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }


                    if (s14 != null)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);



                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                        //YAKIN LİSTE

                        var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;
                        if (esVar != null)
                        {
                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in Yakinlar)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.destekCikisT >= bolBit)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                    }









                    m.maas = oAykiKisiMaasi;






                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        m.maas = oAykiKisiMaasi - agi2f;
                        m.maas2 = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        m.maas = oAykiKisiMaasi;
                        m.maas2 = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(Destek.emekliMaas);
                        m.donemi = "Pasif";

                        m.maas = oAykiKisiMaasi;
                        m.maas2 = m.maas;

                    }


                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }





                    decimal kusur = Convert.ToDecimal(kusurrr);





                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));






                    m.sira = siraa + 1;
                    //m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas) * 1.1) * (0.9));
                    maasListesi.Add(m);
                    siraa = siraa + 1;


                }
                else
                {
                    // AY MAAŞ Güne çevrilip Gün olarak bulunacak


                    MaasAy m = new MaasAy();

                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                    m.tarih2 = bolBit;
                    decimal oAykiKisiMaasi = 0;
                    m.aciklama = bolumAciklama;

                    var s14 = gecmisMaasDegerleri.Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                    Decimal oAykiAsgariUcret = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g107 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }


                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;
                    if (esVar != null)
                    {
                        if (esVar.calisma == 0)
                        {
                            esDurum = 0;
                        }
                        else if (esVar.calisma == 1)
                        {
                            esDurum = 1;
                        }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= bolBit)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }


                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                    if (s14 != null)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);



                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                    }




                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(Destek.emekliMaas);
                        m.donemi = "Pasif";
                    }




                    m.aciklama = bolumAciklama;

                    int days = DateTime.DaysInMonth(tazminatBas1X.Year, tazminatBas1X.Month);
                    System.Decimal gunMaas = oAykiKisiMaasi / days;





                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);

                    m.tarih2 = bolBit;








                    int gunSay = (m.tarih2 - m.tarih).Days;
                    gunSay = gunSay + 1;








                    m.maas = gunMaas * gunSay;


                    m.maas2 = m.maas;



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }


                    decimal kusur = Convert.ToDecimal(kusurrr);



                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));


                    m.sira = siraa + 1;

                    maasListesi.Add(m);
                    siraa = siraa + 1;

                }




                // FOR BİTİŞ
            }


            int ii = 0;

            foreach (var t4 in payBolumleri)
            {
                if (ii == 0)
                {
                    t4.basTar = Destek.vefatTarihi;

                }
                else
                {
                    t4.basTar = payBolumleri[ii - 1].bitTar.AddDays(1);

                }

                ii = ii + 1;
            }


            //ASKERLİK DURUMU KONTROLÜ

            if (Destek.cinsiyet == "Erkek")
            {
                if (Destek.askerlikYapti == "YapMAdı")
                {
                    DateTime askereGidis = new DateTime(Destek.askereGidisYil, Destek.askereGidisAy, 1);
                    if (askereGidis != null)
                    {

                        DateTime askerlikBasTar = askereGidis;
                        DateTime askelikBitisTar = askerlikBasTar.AddMonths(Convert.ToInt32(Destek.askerlikSuresi));

                        //askelikBitisTar = askelikBitisTar.AddDays(-1);

                        foreach (var t2 in maasListesi)
                        {

                            bool askerde = false;

                            if (t2.tarih >= askerlikBasTar && t2.tarih < askelikBitisTar)
                            {

                                askerde = true;

                            }

                            if (askerde == true)
                            {
                                t2.donemi = "Askerde";
                                t2.maas = 0;
                                t2.maas2 = 0;
                                t2.maas3 = 0;
                                t2.maas4 = 0;
                            }



                        }

                    }

                }

            }



            //HESAPLAMA DEVAM
            foreach (var t2 in maasListesi)
            {
                MaasAy2 m2 = new MaasAy2();

                m2.aciklama = t2.aciklama;
                m2.donemi = t2.donemi;
                m2.maas = t2.maas;
                m2.maas2 = t2.maas2;
                m2.maas3 = t2.maas3;
                m2.maas4 = t2.maas4;
                m2.sira = t2.sira;
                m2.tarih = t2.tarih;
                m2.tarih2 = t2.tarih2;

                decimal gunlukMaas = 0;

                double aradakiGunSayisi = 0;

                try
                {
                    aradakiGunSayisi = (m2.tarih2 - m2.tarih).TotalDays;
                }
                catch (Exception ex)
                {

                }


                gunlukMaas = m2.maas4 / Convert.ToDecimal(aradakiGunSayisi + 1);

                DateTime tmpBas = m2.tarih;
                DateTime tmpBit = m2.tarih2;

                while (tmpBas <= m2.tarih2)
                {



                    var bol = payBolumleri.Find(o => o.basTar <= tmpBas && o.bitTar >= tmpBas);

                    decimal tazminatSon = 0;
                    if (bol != null)
                    {
                        m2.bolumBitisi = bol.bitTar;
                        double payda = bol.toplamPay;

                        foreach (var t3 in bol.paybolumkisi)
                        {

                            var kisi2 = m2.oAyAlacakKisiler.Find(o => o.Id == t3.kisiId);
                            double pay = t3.payoran;

                            if (kisi2 == null)
                            {

                                PayKisi2 k = new PayKisi2();

                                k.Id = t3.kisiId;

                                k.destekBit = t3.destekCikisTarihi;

                                k.isim = t3.isim;

                                k.payOran = (int)t3.payoran;
                                k.toplamPay = (int)payda;
                                k.alinanMiktar = (gunlukMaas * Convert.ToDecimal(pay)) / Convert.ToDecimal(payda);
                                k.yakinlik = t3.yakinlik;
                                if (t3.yakinlik == "Eş")
                                {
                                    if (t2.aciklama != "1")
                                    {
                                        decimal  evMik =  (k.alinanMiktar * (Convert.ToDecimal(EvlenmeIhtimali)) / 100);
                                        k.alinanMiktar = k.alinanMiktar - evMik;
                                        EvlenmeIndirimMiktar = EvlenmeIndirimMiktar + evMik;
                                    }
                                }
                                k.donem = t2.aciklama;
                                //tazminatSon = tazminatSon + k.alinanMiktar;
                                m2.oAyAlacakKisiler.Add(k);

                            }
                            else
                            {


                                Decimal Mikk = (gunlukMaas * Convert.ToDecimal(pay)) / Convert.ToDecimal(payda);


                                if (t3.yakinlik == "Eş")
                                {
                                    if (t2.aciklama != "1")
                                    {


                                        Mikk = Mikk * (100 - Convert.ToDecimal(EvlenmeIhtimali)) / 100;


                                    }
                                }
                                kisi2.donem = t2.aciklama;

                                kisi2.alinanMiktar = kisi2.alinanMiktar + Mikk;
                            }





                        }
                    }
                    tmpBas = tmpBas.AddDays(1);


                }
                //m2.maas5 = tazminatSon;


                maasListesi2.Add(m2);





            }


            var xx = maasListesi2;




            //HESAPLAMA DEVAM2

            kisiTazminatListesi.Clear();
            foreach (var t2 in Yakinlar)
            {
                KisiTazminatBilgi kk = new KisiTazminatBilgi();
                kk.kisiId = t2.Id;
                kk.Isim = t2.ad;
                decimal kisiToplam = 0;

                foreach (var t3 in maasListesi2)
                {
                    var kisi = t3.oAyAlacakKisiler.Find(o => o.Id == t2.Id);
                    if (kisi != null)
                    {
                        kisiToplam = kisiToplam + kisi.alinanMiktar;

                    }

                }

                kk.alinanMiktar = kisiToplam;


                kisiTazminatListesi.Add(kk);

            }



            //------------------------------------------------

            DonemKisiPay = payBolumleri;

            foreach (var t in payBolumleri)
            {
                string aciklama = "";

                foreach (var t2 in t.paybolumkisi)
                {
                    string bilgi = "";
                    bilgi = t2.payoran + "\\" + t.toplamPay + " : " + t2.isim;
                    aciklama = aciklama + bilgi + "\n";
                }

                t.kisiBilgi = aciklama;
            }



            // TOPLAMA İŞLEMLERİ......

            decimal islenmisToplam = 0;
            decimal aktifToplam = 0;
            decimal pasifToplam = 0;
            decimal genelToplam = 0;

            foreach (var t in maasListesi2)
            {
                foreach (var t2 in t.oAyAlacakKisiler)
                {
                    if (t2.donem == "1")
                    {
                        islenmisToplam = islenmisToplam + t2.alinanMiktar;

                    }
                    else if (t2.donem == "2")
                    {
                        aktifToplam = aktifToplam + t2.alinanMiktar;
                    }
                    else if (t2.donem == "3")
                    {
                        pasifToplam = pasifToplam + t2.alinanMiktar;
                    }

                }

            }

            decimal masraff = MasrafToplam;
            genelToplam = islenmisToplam + aktifToplam + pasifToplam + masraff;

            IslemisToplam = islenmisToplam;
            AktifToplam = aktifToplam;
            PasifToplam = pasifToplam;
            GenelToplam = genelToplam;


        }



        DateTime vefatKisi_TahminiVefati;

        private void HesaplaTrafik()
        {
       



        }

        private void HesaplaKalanYass()
        {

            int mevcutYas = 0;

            //


            // Sadece YIL alınacak küsür Yok
            if (Destek.yasiYuvarla == 0)
            {

                DateTime tmpDogum = Destek.dogumTarihi;
                int yil = 0;
                while (Destek.vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                mevcutYas = yil;

            }
            else if (Destek.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = Destek.dogumTarihi;
                int yil = 0;
                while (Destek.vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = Destek.dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= Destek.vefatTarihi)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                mevcutYas = yil;
            }




            string cinss = "1";
            if (Destek.cinsiyet == "Kadın")
            {
                cinss = "2";
            }

            decimal kalanOmur = 0;

            if(AktifDestek.trafikKazasi=="Evet"&& AktifDestek.trafikTRh== true)
            {
                kalanOmur = Convert.ToDecimal(yasamSureIslem.TrhSureHesapla(mevcutYas,AktifDestek.cinsiyet));

            }
            else
            {
                kalanOmur = Convert.ToDecimal(yasamSureIslem.PmfSureHesapla(mevcutYas));

            }



            decimal kalanYil = Math.Truncate(kalanOmur);
            decimal kusurat = kalanOmur - kalanYil;

            decimal kalanAy1 = kusurat * 12;

            decimal kalanAy = Math.Truncate(kalanAy1);

            decimal gun = (kalanAy1 - kalanAy) * 30;

            decimal kalanGun = Convert.ToInt32(gun);




            //gelen kalan ömür  küsüratları olay tarihine eklenir.
            DateTime vefatYas = Destek.vefatTarihi;

            vefatYas = vefatYas.AddYears(Convert.ToInt32(kalanYil));
            vefatYas = vefatYas.AddMonths(Convert.ToInt32(kalanAy));
            vefatYas = vefatYas.AddDays(Convert.ToInt32(kalanGun));

            //lblBakiyeOmur.Text = vefatYas.ToShortDateString();
            vefatKisi_TahminiVefati = vefatYas;
            VefatTar = vefatYas.ToShortDateString();


        }


        private YakinSureHesapBilgi Hesapla_YakinKalanSureler(Yakin2 kisi)
        {

            YakinSureHesapBilgi aa = new YakinSureHesapBilgi();


            DateTime vefatTarihi = Destek.vefatTarihi;
            // Sadece YIL alınacak küsür Yok


            if (Destek.yasiYuvarla == 0)
            {

                DateTime tmpDogum = kisi.dogumTarihiT;
                int yil = 0;
                while (vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                aa.mevcutYas = yil;




            }

            else if (Destek.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = kisi.dogumTarihiT;
                int yil = 0;
                while (vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = kisi.dogumTarihiT.AddYears(yil);

                while (tmpDogum2 <= vefatTarihi)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                aa.mevcutYas = yil;
            }

            double kalanOmur = 0;


            if(AktifDestek.trafikKazasi=="Evet" && AktifDestek.trafikTRh== true)
            {
                kalanOmur = Convert.ToDouble(yasamSureIslem.TrhSureHesapla(aa.mevcutYas, kisi.cinsiyet));

            }
            else
            {
                kalanOmur = Convert.ToDouble(yasamSureIslem.PmfSureHesapla(aa.mevcutYas));

            }



            double kalanYil = Math.Truncate(kalanOmur);
            aa.destekSuresi = (int)kalanYil;

            double kusurat = kalanOmur - kalanYil;

            double kalanAy1 = kusurat * 12;

            double kalanAy = Math.Truncate(kalanAy1);

            double gun = (kalanAy1 - kalanAy) * 30;

            decimal kalanGun = Convert.ToInt32(gun);



            //gelen kalan ömür  küsüratları olay tarihine eklenir.
            DateTime vefatYas = vefatTarihi;

            vefatYas = vefatYas.AddYears(Convert.ToInt32(kalanYil));
            vefatYas = vefatYas.AddMonths(Convert.ToInt32(kalanAy));
            vefatYas = vefatYas.AddDays(Convert.ToInt32(kalanGun));

            aa.destekBitis = vefatYas;

            return aa;

        }

        private void YakinListeOlustur()
        {
            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Eş")
                {
                    Yakin2 y2 = new Yakin2();
                    y2.Id = t.Id;
                    y2.yakinlik = t.yakinlik;
                    y2.cinsiyet = t.cinsiyet;
                    y2.ad = t.ad + " " + t.soyad;
                    y2.dogumTarihi = t.dogumTarihi.ToShortDateString();
                    y2.dogumTarihiT = t.dogumTarihi;

                    y2.calisma = 0;
                    if (t.esCalisiyorMu == "Evet")
                    {
                        y2.calisma = 1;
                    }

                    Yakinlar.Add(y2);

                }
            }

            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Çocuk")
                {
                    Yakin2 y2 = new Yakin2();
                    y2.Id = t.Id;
                    y2.yakinlik = t.yakinlik;
                    y2.cinsiyet = t.cinsiyet;
                    y2.ad = t.ad + " " + t.soyad;
                    y2.dogumTarihi = t.dogumTarihi.ToShortDateString();
                    y2.dogumTarihiT = t.dogumTarihi;

                    y2.okulBitirmeYas = t.okulBitisYas;
                    Yakinlar.Add(y2);
                }

            }

            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Anne")
                {
                    Yakin2 y2 = new Yakin2();
                    y2.Id = t.Id;
                    y2.yakinlik = t.yakinlik;
                    y2.cinsiyet = t.cinsiyet;
                    y2.ad = t.ad + " " + t.soyad;
                    y2.dogumTarihi = t.dogumTarihi.ToShortDateString();
                    y2.dogumTarihiT = t.dogumTarihi;
                    Yakinlar.Add(y2);
                }

            }

            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Baba")
                {
                    Yakin2 y2 = new Yakin2();
                    y2.Id = t.Id;
                    y2.yakinlik = t.yakinlik;
                    y2.cinsiyet = t.cinsiyet;
                    y2.ad = t.ad + " " + t.soyad;
                    y2.dogumTarihi = t.dogumTarihi.ToShortDateString();
                    y2.dogumTarihiT = t.dogumTarihi;
                    Yakinlar.Add(y2);
                }

            }

            foreach (var t in Destek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Kardeş")
                {
                    Yakin2 y2 = new Yakin2();
                    y2.Id = t.Id;
                    y2.yakinlik = t.yakinlik;
                    y2.cinsiyet = t.cinsiyet;
                    y2.ad = t.ad + " " + t.soyad;
                    y2.dogumTarihi = t.dogumTarihi.ToShortDateString();
                    y2.dogumTarihiT = t.dogumTarihi;
                    Yakinlar.Add(y2);
                }

            }


            foreach (var t in Yakinlar)
            {

                var yasBilgi = Hesapla_YakinKalanSureler(t);
                int KalanYill;

                if (t.yakinlik == "Çocuk")
                {
                    int yil2 = t.okulBitirmeYas;
                    KalanYill = yil2 - yasBilgi.mevcutYas;

                    t.destekSuresiT = Convert.ToInt32(KalanYill);
                    t.destekSuresi = t.destekSuresiT.ToString();

                    t.destekCikisT = t.dogumTarihiT.AddYears(Convert.ToInt16(t.okulBitirmeYas));
                    t.destekCikis = t.destekCikisT.ToShortDateString();

                }
                else
                {
                    t.destekCikisT = yasBilgi.destekBitis;
                    t.destekCikis = yasBilgi.destekBitis.ToShortDateString();

                    KalanYill = yasBilgi.destekSuresi;
                    t.destekSuresiT = KalanYill;
                    t.destekSuresi = KalanYill.ToString();

                }


            }

        }

        private void MasrafHesapla()
        {

            foreach (var t in Destek.DosyaDestektenYoksunlukMasraf)
            {
                MasrafToplam = MasrafToplam + Convert.ToDecimal(t.miktar);

            }


        }

        List<DestekPayBolumleri> payBolumleri = new List<DestekPayBolumleri>();

       private decimal PayBolumleriHesaplaBekar(DateTime _tarih)
        {

            double _sonuc = 0;

            int _anneBabaSay = 0; ;
            bool _anneVar = false;
            bool _babaVar = false;

            if(Yakinlar.Count>0)
            {

                //List<DestekPayBolumleri> payBolumleri2 = new List<DestekPayBolumleri>();

                //var kisiListesSirali = Yakinlar.OrderBy(o => o.destekSuresiT).ToList();

                //double tazminatYilSayisi = kisiListesSirali[kisiListesSirali.Count - 1].destekSuresiT;


                foreach (var t in Yakinlar)
                {
                    if (t.yakinlik.ToLower() == "anne" && _tarih <= t.destekCikisT)
                    {
                        _anneBabaSay = _anneBabaSay + 1;
                        _anneVar = true;
                    }
                    if (t.yakinlik.ToLower() == "baba" && _tarih <= t.destekCikisT)
                    {
                        _anneBabaSay = _anneBabaSay + 1;
                        _babaVar = true;
                    }

                } 
                    //Evlenmeden Önce

                    if (_tarih.Date < Destek.BekarCocuk_EvlenmeTarihi.Date )
                    {
                        if(_anneBabaSay==2)
                        {
                            _sonuc = 0.25;
                        }
                        else if(_anneBabaSay==1)
                        {
                            _sonuc = 0.5;
                        }
                    }
                    else
                    { // Evlendikten Sonra
                        
                        if(_anneBabaSay==2)
                        {
                            _sonuc = 0.125;
                        }
                        else if( _anneBabaSay==1)
                        {
                            _sonuc = 0.25;
                        }

                    }
                           

           }


            return (decimal) _sonuc;
        }

        private void PayBolumleriHesapla()
        {
          
            if (Yakinlar.Count > 0)
            {

                payBolumleri.Clear();

                List<DestekPayBolumleri> payBolumleri2 = new List<DestekPayBolumleri>();

                var kisiListesSirali = Yakinlar.OrderBy(o => o.destekSuresiT).ToList();

                double tazminatYilSayisi = kisiListesSirali[kisiListesSirali.Count - 1].destekSuresiT;

                //Pay Bölümlerinin Oluşturulması..

                int AnneBabaSay = 0;

                foreach (var t in kisiListesSirali)
                {


                    DestekPayBolumleri bol = new DestekPayBolumleri();

                    if(t.yakinlik.ToLower()=="anne")
                    {
                        AnneBabaSay = AnneBabaSay + 1;
                    }
                    if(t.yakinlik.ToLower()=="baba")
                    {
                        AnneBabaSay = AnneBabaSay + 1;
                    }


                    bol.bitTar = t.destekCikisT;

                    payBolumleri2.Add(bol);



                }
                payBolumleri = payBolumleri2.OrderBy(o => o.bitTar).ToList();


                // Her Paydaki Kişilerin Oluşturulması
                foreach (var t2 in payBolumleri)
                {
                    foreach (var t3 in kisiListesSirali)
                    {

                        if (t2.bitTar <= t3.destekCikisT)
                        {
                            PayBolumleriKisi k = new PayBolumleriKisi();
                            k.isim = t3.ad;
                            k.sure = t3.destekSuresiT;
                            k.kisiId = t3.Id;
                            k.yakinlik = t3.yakinlik;
                            k.tamOran = t3.tamOran;
                            k.destekCikisTarihi = t3.destekCikisT;

                            t2.paybolumkisi.Add(k);

                        }
                    }
                }

                // Her Paydaki Kişilerin Oranları
                foreach (var t3 in payBolumleri)
                {
                    double t1 = 2;
                    foreach (var t4 in t3.paybolumkisi)
                    {
                        double t2 = 1;
                        if (t4.yakinlik == "Eş")
                        {
                            t2 = 2;

                        }
                        //if (t4.yakinlik == "Anne")
                        //{
                        //    if (t4.tamOran == 0.5)
                        //    {
                        //        t2 = 0.5;
                        //    }
                        //}

                        //if (t4.yakinlik == "Baba")
                        //{
                        //    if (t4.tamOran == 0.5)
                        //    {
                        //        t2 = 0.5;
                        //    }
                        //}
                        t4.payoran = t2;
                        t1 = t1 + t2;

                    }

                    t3.toplamPay = t1;

                    // EĞER ANNE ve BABA PAYORANLARI TOPLAMI %25'debn Fazlaysa
                    // 12.5 12.5 yap veya %25 Yap

                }
                //Toplam PAy Oranının Eklenmesi
                foreach (var t3 in payBolumleri)
                {
                    double toplamP = t3.toplamPay;
                    foreach (var t4 in t3.paybolumkisi)
                    {
                        t4.toplamPay = toplamP;
                        
                    }
                }

                // Anne Baba Sayılarını Kontrol Et..
                if(AnneBabaSay >0)
                {
                    // Her Pay Bölümünde Kişilerde Anne ve Baba Toplamı 
                    // % 25'i geçmişse Oranları Tekarar Hesapla
                    // Anne Babayı %25'e sabitle diğerlerini Dağıt..

                    
                    foreach(var t4 in payBolumleri)
                    {
                        double toplamAB = 0;
                        var aa = t4.paybolumkisi.Find(o => o.yakinlik.ToLower() == "anne");
                        if(aa!= null)
                        {
                            toplamAB = toplamAB + (aa.payoran / aa.toplamPay);
                        }

                        var bb = t4.paybolumkisi.Find(o => o.yakinlik.ToLower() == "baba");
                        if(bb != null)
                        {
                            toplamAB = toplamAB + (bb.payoran / bb.toplamPay);
                        }

                        if(toplamAB> 0.25)
                        {
                            // O Bölüm için yeniden hesaplama.....

                            //Anne ve Baba 
                            //aa.payoran = 1;
                            //aa.toplamPay = 8;
                            //bb.payoran = 1;
                            //bb.toplamPay = 8;
                    
                            foreach (var t5 in t4.paybolumkisi)                        
                            {
                                double pPayda2 = 2;
                                double pp2=0; 
                                if (t5.yakinlik == "Eş")
                                {
                                    pp2 = 2;
                                    pPayda2 = pPayda2 + pp2;
                                }
                                else if(t5.yakinlik== "Çocuk")
                                {
                                    pp2 = 1;
                                    pPayda2 = pPayda2 + pp2;

                                }
                                t5.payoran = pp2 *3;
                                t5.toplamPay = pPayda2 *4;

                                if((t5.yakinlik.ToLower()=="anne") || t5.yakinlik.ToLower()== "baba")
                                {
                                    t5.payoran = 1;
                                    t5.toplamPay = 8;

                                }
                               

                            }


                        }

                    }
                }



            }


        }


        List<DestekPayBolumleri> _donemKisiPay;

        public List<DestekPayBolumleri> DonemKisiPay
        {
            get => _donemKisiPay;
            set
            {
                _donemKisiPay = value;
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
        //COMMAND BÖLÜMLERİ...

        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        async private void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            Destek.Duzenlemede = true;

            var sayfa = new BasamakDY1View(Destek);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }

        public ICommand RaporBitisCommand => new Command(OnRaporBitis);

        async private void OnRaporBitis(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new Views.AnaSayfaV.Anasayfa();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }

        public ICommand WordRaporOlusturCommand => new Command(OnWordRaporOlustur);

        async private void OnWordRaporOlustur(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
           // WordRapor();
            WordRapor2();
            IsBusy = false;

        }

         async   private void WordRapor()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);


            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;


            //Appends paragraph
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("BİLİRKİŞİ RAPORU") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("______________ MAHKEMESİ'NE") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("\t\t\t______________ ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            // Dosya Bilgileri......

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DOSYA NO \t\t\t:") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph.ParagraphFormat.LineSpacing = 18f;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVACI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVALI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVA KONUSU \t\t: Destekten Yoksunluk") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TALEP") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("SAVUNMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TESPİT") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DEĞERLENDİRME VE HESAPLAMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Olay Tarihi \t\t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string olayTarihi = "4.03.2019";
            textRange = paragraph.AppendText(olayTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            //RaporTarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Rapor Tarihi \t\t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string raporTarihi = "4.03.2019";
            textRange = paragraph.AppendText(raporTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //Paylaştırma Türü	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Paylaştırma Türü \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string paylastirmaTuru = "Progresif Rant";
            textRange = paragraph.AppendText(paylastirmaTuru) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            //Yaşam Tablosu	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Yaşam Tablosu \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string yasamTablosu = "PMF---";
            textRange = paragraph.AppendText(yasamTablosu) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //Davalı Kusur Oranı
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Davalı Kusur Oranı \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string kusurOrani = "%" + "100";
            textRange = paragraph.AppendText(kusurOrani) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            // PAY TABLOLARI OLUŞTURMA--------------------------------------------

            IWTable tablooo = section.AddTable();
            tablooo.TableFormat.Borders.LineWidth = 2f;
            tablooo.ApplyStyle(BuiltinTableStyle.LightGrid);
            //tablooo.TableFormat.CellSpacing = 10;
            //tablooo.TableFormat.IsAutoResized = true;
            //tablooo.TableFormat.Borders.Horizontal.Color = Syncfusion.Drawing.Color.Black;
            //tablooo.TableFormat.Borders.Vertical.Color = Syncfusion.Drawing.Color.Black;

            int ii2 = Yakinlar.Count + 1;
            int jj2 = payBolumleri.Count + 1;


            tablooo.ResetCells(ii2, jj2);

            tablooo[0, 0].AddParagraph().AppendText("    ");

           

            int jj = 1;
            foreach(var t1 in payBolumleri)
            {
                tablooo[0, jj].AddParagraph().AppendText("Bitiş: " + t1.bitTar.ToShortDateString());
                jj = jj + 1;
            }

            int ii = 1;
            foreach(var t2 in Yakinlar)
            {
                string yakinId = "";
                yakinId = t2.Id;
                string isimm = t2.ad + " (" + t2.yakinlik + ")";
                tablooo[ii, 0].AddParagraph().AppendText(isimm);

                int j22=1;
                foreach(var t3 in payBolumleri)
                {
                    string yazi = "";
                    var payyBol2 = t3.toplamPay;

                    var payyKisisi = t3.paybolumkisi.Find(o => o.kisiId == yakinId);

                    if (payyKisisi != null)
                    {
                        yazi = payyKisisi.payoran + "/" + payyBol2;
                    }else
                    {
                        yazi = "   -     ";
                    }

                    tablooo[ii, j22].AddParagraph().AppendText(yazi);

                    j22 = j22 + 1;
                }

                ii = ii + 1;
            }


            //for (int i = 0; i < ii2; i++)
            //{
            //    WTableRow row =  tablooo.AddRow();
            //    row.Height = 35;



            //    string kkIdd = "";
            //    if (i > 0)
            //    {
            //        kkIdd = Yakinlar[i - 1].Id;
            //    }

            //    for (int j = 0; j < jj2; j++)
            //    {
            //         cell = row.AddCell();
                    
                    
            //        if (i > 0 && j == 0)
            //        {
            //            string isimm = Yakinlar[i - 1].ad + " (" + Yakinlar[i - 1].yakinlik + ")";
            //            cell.Width = 200;
            //            cell.AddParagraph().AppendText(isimm);
                        
            //        }
            //        else if (i == 0 && j > 0)
            //        {
            //            string trr = "Bitiş :" + payBolumleri[j - 1].bitTar.ToShortDateString();
            //            cell.AddParagraph().AppendText(trr);

            //        }
            //        else if (i > 0 && j > 0)
            //        {

            //            cell.Width = 50;
            //            string yazi = "";

            //            var payyBol2 = payBolumleri[j - 1].toplamPay;

            //            var payyKisisi = payBolumleri[j - 1].paybolumkisi.Find(o => o.kisiId == kkIdd);

            //            if (payyKisisi != null)
            //            {
            //                yazi = payyKisisi.payoran + "/" + payyBol2;
            //            }
            //            cell.AddParagraph().AppendText(yazi);
            //        }

            //    }

            //}


            //Pay tablosu bitişi...................................................


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DESTEKTEN YOKSUNLUK HESABI") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";


            paragraph = section.AddParagraph();
         

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //Vefat Eden Kişi
            string vefatKisi = AktifDestek.ad + " " + AktifDestek.soyad;
            textRange = paragraph.AppendText("Vefat Eden \t\t\t: " + vefatKisi) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
           
            //Doğum Tarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;        
            textRange = paragraph.AppendText("Doğum Tarihi \t\t\t: " ) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            string dogumTar = AktifDestek.dogumTarihi.ToShortDateString();
            textRange = paragraph.AppendText(paylastirmaTuru) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //Vefat Tarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Doğum Tarihi \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            string vefatTar = AktifDestek.vefatTarihi.ToShortDateString();
            textRange = paragraph.AppendText(vefatTar) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            // Gelir
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Gelir \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            string gelir = SonMaas.ToString("C", CultureInfo.CurrentCulture);
            textRange = paragraph.AppendText(gelir) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            // Emeklimaas
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Gelir \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            string emekliMaas = AktifDestek.emekliMaas.ToString("C", CultureInfo.CurrentCulture);
            textRange = paragraph.AppendText(emekliMaas) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            // Askerlik Durum.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Askerlik Durumu \t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            string askerlikDurum = AktifDestek.askerlikYapti;
            textRange = paragraph.AppendText(askerlikDurum) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Yakınlar") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";


            // YAKINLAR
            var MaaslarDagitilmis = maasListesi2;

            foreach (var t in Yakinlar)
            {

                Decimal miktarGecmiş = 0;
                Decimal miktarAktif = 0;
                Decimal miktarPasif = 0;
                foreach (var aa in MaaslarDagitilmis)
                {
                    var kisii = aa.oAyAlacakKisiler.Find(o => o.Id == t.Id);
                    if (kisii != null)
                    {
                        if (aa.aciklama == "1")
                        {
                            miktarGecmiş = miktarGecmiş + kisii.alinanMiktar;
                        }
                        else if (aa.aciklama == "2")
                        {

                            miktarAktif = miktarAktif + kisii.alinanMiktar;


                        }
                        else if (aa.aciklama == "3")
                        {
                            miktarPasif = miktarPasif + kisii.alinanMiktar;
                        }
                    }

                }


                // Yakınlık
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Yakınlık \t\t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string yakinlik = t.yakinlik;
                textRange = paragraph.AppendText(yakinlik) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;


                // Ad-Soyad
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Ad Soyad \t\t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string isimm = t.ad;
                textRange = paragraph.AppendText(isimm) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;

                // Doğum Tarihi
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Ad Soyad \t\t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string dogumtar = t.dogumTarihiT.ToShortDateString();
                textRange = paragraph.AppendText(dogumtar) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;

                // Destek Tarihi
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Destek Çıkış Tarihi \t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string destekTar = t.destekCikisT.ToShortDateString();
                textRange = paragraph.AppendText(destekTar) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;


                //Evlenme Olasılığı
                if (t.yakinlik == "Eş")
                {
                    paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.LineSpacing = 18f;
                    paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                    textRange = paragraph.AppendText("Evlenme Olasılığı \t\t: %") as WTextRange;
                    textRange.CharacterFormat.FontSize = 12f;
                    textRange.CharacterFormat.Bold = true;
                    textRange.CharacterFormat.FontName = "Times New Roman";

                    double evlenmeYuzdesi;
                    if (AktifDestek.esEvlenmeElleHesapla == false)
                    {
                        evlenmeYuzdesi = Convert.ToDouble(AktifDestek.esEvlenmeYuzdesi);
                    }
                    else
                    {
                        evlenmeYuzdesi = Convert.ToDouble(AktifDestek.esEvlenmeElle);
                    }

                    textRange = paragraph.AppendText(evlenmeYuzdesi.ToString()) as WTextRange;
                    textRange.CharacterFormat.FontName = "Times New Roman";

                }

                // İşlemiş Dönem
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("İşlemiş Dönem  \t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string bolumIslemisYazi = "";

                var _kisiDonemToplamlari = KisiDonemToplam(t.Id);

            

                DateTime tarihBolumIslemisBaslangic = AktifDestek.vefatTarihi;
                DateTime tarihBolumIslemisBitis = AktifDestek.raporTarihi.AddDays(-1);

                bolumIslemisYazi = tarihBolumIslemisBaslangic.ToShortDateString() + "-" + tarihBolumIslemisBitis.ToShortDateString() +
                    "\t\t" + String.Format("{0:C2}", _kisiDonemToplamlari.islemisKisiToplam);


                textRange = paragraph.AppendText(bolumIslemisYazi) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;


                //
                // İşlemiş Dönem
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Aktif Dönem\t\t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string bolumAktifYazisi = "";

                var bolumEmekli = bolumListe.Find(o => o.aciklama == "Emeklilik Tarihi");

                DateTime tarihBolumAktifBaslangic = AktifDestek.raporTarihi;
                DateTime tarihBolumAktisBitis = bolumEmekli.baslangic.AddDays(-1);

                bolumAktifYazisi = tarihBolumAktifBaslangic.ToShortDateString() + " - " +
                    tarihBolumAktisBitis.ToShortDateString() + "\t\t" + String.Format("{0:C2}", _kisiDonemToplamlari.AktifKisiToplam);

                textRange = paragraph.AppendText(bolumAktifYazisi) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;

                //EMEKLİ TOPLAMI.....................
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("Emekli Dönem\t\t: ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                string bolumEmekliYazisi = "";
                var bolumYasam = bolumListe.Find(o => o.aciklama == "Yasam Tarihi");
                DateTime tarihBolumEmekliBitis = bolumYasam.baslangic;
                DateTime tarihBolumEmekliBaslangic = tarihBolumAktisBitis.AddDays(1);

                bolumEmekliYazisi = tarihBolumEmekliBaslangic.ToShortDateString() + " - " +
                    tarihBolumEmekliBitis.ToShortDateString() + "\t\t" + String.Format("{0:C2}", _kisiDonemToplamlari.EmekliKisiToplam);
                textRange = paragraph.AppendText(bolumEmekliYazisi) as WTextRange;
                textRange.CharacterFormat.FontName = "Times New Roman";
                textRange.CharacterFormat.FontSize = 12f;

                //-----
               
                if(t.yakinlik=="Eş")
                {
                    paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.LineSpacing = 18f;
                    paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                    textRange = paragraph.AppendText("Evlenme İndirimi\t\t:\t\t\t\t\t ") as WTextRange;
                    textRange.CharacterFormat.FontSize = 12f;
                    textRange.CharacterFormat.Bold = true;
                    textRange.CharacterFormat.FontName = "Times New Roman";

                    string evlenmeIndirimYazisi = String.Format("{0:C2}", EvlenmeIndirimMiktar);
                    textRange = paragraph.AppendText(evlenmeIndirimYazisi) as WTextRange;
                    textRange.CharacterFormat.FontName = "Times New Roman";
                    textRange.CharacterFormat.FontSize = 12f;

                }
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("_________________________________________________________________________________ ") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                decimal kisiToplamMiktar = _kisiDonemToplamlari.islemisKisiToplam + _kisiDonemToplamlari.AktifKisiToplam
                    + _kisiDonemToplamlari.EmekliKisiToplam;
                string kisiToplamMiktarYazi = String.Format("{0:C2}", kisiToplamMiktar);
                textRange = paragraph.AppendText("Toplam" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t"
                    + kisiToplamMiktarYazi) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";

                if (AktifDestek.cinsiyet == "Erkek")
                {
                    if (AktifDestek.askerlikYapti !="Yaptı")
                    {

                        DateTime askereGidisTar = new DateTime(AktifDestek.askereGidisYil, AktifDestek.askereGidisAy, 1);

                        string askerlikYazi = "(" + askereGidisTar.ToShortDateString() + " - " 
                            + askereGidisTar.AddMonths(Convert.ToInt32(AktifDestek.askerlikSuresi)).ToShortDateString() +
                        " Tarihleri arasında askerlik hizmeti olarak hesaplanarak toplam tutardan düşülmüştür.)";

                        paragraph = section.AddParagraph();
                        paragraph.ParagraphFormat.LineSpacing = 18f;
                        paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                        textRange = paragraph.AppendText(askerlikYazi) as WTextRange;
                        textRange.CharacterFormat.FontSize = 12f;
                        textRange.CharacterFormat.Bold = true;
                        textRange.CharacterFormat.FontName = "Times New Roman";

                    }
                }

                paragraph = section.AddParagraph();
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                textRange = paragraph.AppendText("Masraflar") as WTextRange;
                textRange.CharacterFormat.FontSize = 14f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";


                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("....") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;     
                textRange.CharacterFormat.FontName = "Times New Roman";

                foreach(var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.LineSpacing = 18f;
                    paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                    string masrafYazi = "/t"+ String.Format("{0:C2}", t2.miktar) + "\t:" + t2.masraftur2;
                    textRange = paragraph.AppendText(masrafYazi) as WTextRange;
                    textRange.CharacterFormat.FontSize = 12f;
                    textRange.CharacterFormat.FontName = "Times New Roman";
                }


            }



            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);

            //Save the stream as a file in the device and invoke it for viewing
            var yoll=  await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Sample.docx", "application/msword", stream);

            //await Launcher.OpenAsync(new OpenFileRequest
            //{
            //    File = new ReadOnlyFile(file)
            //});



            //await Browser.OpenAsync(yoll, new BrowserLaunchOptions
            //{
            //    LaunchMode = BrowserLaunchMode.SystemPreferred,
            //    TitleMode = BrowserTitleMode.Show,
            //});





            //Load the Word document as stream

            //Stream docStream = stream;
            //// Loads the stream into Word Document.
            //WordDocument wordDocument = new WordDocument(docStream, Syncfusion.DocIO.FormatType.Automatic);
            ////Instantiation of DocIORenderer for Word to PDF conversion
            //DocIORenderer render = new DocIORenderer();
            ////Sets true to preserve the Word document form field as editable PDF form field in PDF document
            //render.Settings.PreserveFormFields = true;

            //render.Settings.EmbedFonts = true;

            //render.Settings.EmbedCompleteFonts = true;


            ////Converts Word document into PDF document
            //PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);


            ////Releases all resources used by the Word document and DocIO Renderer objects
            //render.Dispose();
            //wordDocument.Dispose();
            ////Saves the PDF file
            //MemoryStream outputStream = new MemoryStream();
            //pdfDocument.Save(outputStream);

            //await Xamarin.Forms.DependencyService.Get<ISave>().PdfMemoryToFileStream(outputStream);
            ////Closes the instance of PDF document object
            //pdfDocument.Close();

            //var sayfa1 = new PdfView1();
            //await Application.Current.MainPage.Navigation.PushModalAsync(sayfa1);

        }



        async private void WordRapor2()
        {

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);


            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;


            //Appends paragraph
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DESTEKTEN YOKSUNLUK BİLİRKİŞİ RAPORU") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("______________ MAHKEMESİ'NE") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("\t\t\t______________ ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            // Dosya Bilgileri......

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DOSYA NO \t\t:") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph.ParagraphFormat.LineSpacing = 18f;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVACI \t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVALI \t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVA KONUSU \t : Destekten Yoksunluk".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TALEP") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("SAVUNMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TESPİT") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DEĞERLENDİRME VE HESAPLAMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Olay Tarihi \t \t:  ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string olayTarihi = AktifDestek.vefatTarihi.ToShortDateString();
            textRange = paragraph.AppendText(olayTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //RaporTarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Rapor Tarihi \t\t:  ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string raporTarihi = AktifDestek.raporTarihi.ToShortDateString();
            textRange = paragraph.AppendText(raporTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontSize = 12f;

            //Paylaştırma Türü	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Paylaştırma Türü \t:  ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string paylastirmaTuru = "Progresif Rant".ToUpper();
            textRange = paragraph.AppendText(paylastirmaTuru) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;

            //Yaşam Tablosu	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Yaşam Tablosu\t\t:  ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string yasamTablosu = AktifDestek.yasamTablosu;
            textRange = paragraph.AppendText(yasamTablosu.ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;

            //Davalı Kusur Oranı
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Davalı Kusur Oranı \t:  ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string kusurOrani = "%" + AktifDestek.kusurOrani;
            textRange = paragraph.AppendText(kusurOrani) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            // PAY TABLOLARI OLUŞTURMA--------------------------------------------
            paragraph.ParagraphFormat.LineSpacing = 16f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("PAYLAŞTIRMA ORANLARI") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";




            if (AktifDestek.BekarCocukDurum == true)
            {
                //int ii2 = Yakinlar.Count + 1;





                //List<DestekPayBolumleri> ListePayBolumleri2 = new List<DestekPayBolumleri>();
                //DestekPayBolumleri pB = new DestekPayBolumleri();
                //pB.bitTar = AktifDestek.BekarCocuk_EvlenmeTarihi;
                //ListePayBolumleri2.Add(pB);
                //int annebabaSay = 0;
                //if (Yakinlar.Count== 1)
                //{

                //    var kisi = Yakinlar.Find(o => o.yakinlik.ToLower() == "anne" || o.yakinlik == "baba");
                //    if(kisi != null)
                //    {
                //        DestekPayBolumleri pB2 = new DestekPayBolumleri();
                //        pB2.bitTar = kisi.destekCikisT;
                //        ListePayBolumleri2.Add(pB2);
                //        annebabaSay = 1;
                //    }


                //}
                //else
                //{

                //    var kisi = Yakinlar.Find(o => o.yakinlik.ToLower() == "anne" );
                //    if(kisi != null)
                //    {
                //        DestekPayBolumleri pB2 = new DestekPayBolumleri();
                //        pB2.bitTar = kisi.destekCikisT;
                //        ListePayBolumleri2.Add(pB2);
                //        annebabaSay += 1;
                //    }


                //    var kisi2 = Yakinlar.Find(o => o.yakinlik.ToLower()  == "baba");

                //    if(kisi2 != null)
                //    {
                //        DestekPayBolumleri pB3 = new DestekPayBolumleri();
                //        pB3.bitTar = kisi2.destekCikisT;
                //        ListePayBolumleri2.Add(pB3);
                //        annebabaSay += 1;
                //    }


                //}


                //int jj2 = ListePayBolumleri2.Count + 1;

                //tablooo.ResetCells(ii2, jj2);

                //tablooo[0, 0].AddParagraph().AppendText("    ");
                //tablooo[0, 0].Width = 120;
                //tablooo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //tablooo.Rows[0].Height = 41;


                //int j = 1;

                //var ListeSirali = ListePayBolumleri2.OrderBy(o => o.bitTar).ToList();
                //foreach(var t in ListeSirali)
                //{
                //    tablooo[0, j].AddParagraph().AppendText("  Bitiş: ".ToUpper() + t.bitTar.ToShortDateString() + "   "));
                //    tablooo[0, j].Width = 85;

                //    j = j + 1;
                //}

                //if(annebabaSay == 1)
                //{
                //    var kisi = Yakinlar.Find(o => o.yakinlik.ToLower() == "anne" || o.yakinlik == "baba");
                //    if(kisi != null)
                //    {
                //        tablooo[1, 0].AddParagraph().AppendText("  " + kisi.yakinlik.ToUpper() + "   ");
                //        tablooo[1, 0].Width = 85;

                //        tablooo[1, 1].AddParagraph().AppendText(" 0.5   ");
                //        tablooo[1, 1].Width = 85;

                //        tablooo[1, 1].AddParagraph().AppendText(" 0.25   ");
                //        tablooo[1, 1].Width = 85;

                //    }

                //}
                //else
                //{


                //    //Anne İçin Tarihler
                //    var kisiA = Yakinlar.Find(o => o.yakinlik.ToLower() == "anne" );
                //    var kisiB = Yakinlar.Find(o => o.yakinlik.ToLower() == "baba");

                //    // Anne 
                //    var tar1 = ListeSirali[0].;


                //    string aO1 = "-";
                //    if(tar1<AktifDestek.BekarCocuk_EvlenmeTarihi)
                //    {
                //        aO1 = "1/4";
                //        if(tar1>kisiB.destekCikisT )
                //        {
                //            aO1 = "1/2";
                //        }
                //    }else
                //    {
                //        aO1 = "1/8";
                //        if (tar1 > kisiB.destekCikisT)
                //        {
                //            aO1 = "1/4";
                //        }

                //    }

                //    var tar2 = ListeSirali[1].bitTar.AddDays(-1);
                //    var tar3 = ListeSirali[2].bitTar.AddDays(-1);
                //    var oranA1=   PayBolumleriHesaplaBekar(tar1);




                //}

                IWTable tabloooB = section.AddTable();
                tabloooB.TableFormat.Borders.LineWidth = 0.2f;
                tabloooB.ApplyStyle(BuiltinTableStyle.LightGrid);
                tabloooB.ResetCells(2, 3);

                tabloooB[0, 0].AddParagraph().AppendText("    ");
                tabloooB[0, 0].Width = 120;
                tabloooB.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloooB.Rows[0].Height = 60;

                tabloooB[0, 1].AddParagraph().AppendText("Evlenme Önce");
                tabloooB[0, 1].Width = 120;
                tabloooB.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloooB[0, 2].AddParagraph().AppendText("Evlenme Sonra");
                tabloooB[0, 2].Width = 120;
                tabloooB.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloooB[1, 0].AddParagraph().AppendText("Anne +Baba   ");
                tabloooB[1, 0].Width = 120;
                tabloooB.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloooB.Rows[1].Height = 60;

                tabloooB[1, 1].AddParagraph().AppendText("1/2");
                tabloooB[1, 1].Width = 120;
                tabloooB.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloooB[1, 2].AddParagraph().AppendText("1/4");
                tabloooB[1, 2].Width = 120;
                tabloooB.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



                // Else başlangıcı
            }
            else
            {

         
            int ii2 = Yakinlar.Count + 1;
            int jj2 = payBolumleri.Count + 1;


                IWTable tablooo = section.AddTable();
                tablooo.TableFormat.Borders.LineWidth = 0.2f;
                tablooo.ApplyStyle(BuiltinTableStyle.LightGrid);

                tablooo.ResetCells(ii2, jj2);
                tablooo[0, 0].AddParagraph().AppendText("    ");
            tablooo[0, 0].Width = 120;
            tablooo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooo.Rows[0].Height = 41;

            int jj = 1;
            foreach (var t1 in payBolumleri)
            {
                tablooo[0, jj].AddParagraph().AppendText("    Bitiş: ".ToUpper() + t1.bitTar.ToShortDateString() + "   ");
                tablooo[0, jj].Width = 85;

                jj = jj + 1;
            }

            int ii = 1;

            foreach (var t2 in Yakinlar)
            {

                tablooo.Rows[ii].Height = 38;
                string yakinId = "";
                yakinId = t2.Id;

                string isimm = t2.ad + " (" + t2.yakinlik + ")";
                tablooo[ii, 0].AddParagraph().AppendText("  " + isimm.ToUpper() + "  ");
                tablooo[ii, 0].Width = 120;
                tablooo.Rows[ii].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                int j22 = 1;
                foreach (var t3 in payBolumleri)
                {
                    string yazi = "";
                    var payyBol2 = t3.toplamPay;

                    var payyKisisi = t3.paybolumkisi.Find(o => o.kisiId == yakinId);

                    if (payyKisisi != null)
                    {
                        yazi = payyKisisi.payoran + "/" + payyKisisi.toplamPay;
                    }
                    else
                    {
                        yazi = "   -     ";
                    }

                    tablooo[ii, j22].AddParagraph().AppendText("\t" + yazi + "   ");
                    tablooo[ii, j22].Width = 85;
                    tablooo.Rows[ii].Cells[j22].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    j22 = j22 + 1;
                }

                ii = ii + 1;
            }

            }


            //Pay tablosu bitişi...................................................


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 16f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DESTEKTEN YOKSUNLUK HESABI") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();


            IWTable tablooo2 = section.AddTable();

            tablooo2.TableFormat.Borders.LineWidth = 0.2f;
            tablooo2.TableFormat.CellSpacing = 0;

            tablooo2.ApplyStyle(BuiltinTableStyle.LightGrid);


          


            WCharacterFormat cf1 = new WCharacterFormat(document);

            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf2 = new WCharacterFormat(document);
            cf2.FontSize = 12;
            cf2.FontName = "Times New Roman";

            if (AktifDestek.BekarCocukDurum == true)
            {

                IWTable tablooo2BB = section.AddTable();

                tablooo2BB.TableFormat.Borders.LineWidth = 0.2f;
                tablooo2BB.TableFormat.CellSpacing = 0;

                tablooo2BB.ApplyStyle(BuiltinTableStyle.LightGrid);


                tablooo2BB.ResetCells(7, 2);
                tablooo2BB.IndentFromLeft = 0;

                tablooo2BB[0, 0].AddParagraph().AppendText(" Vefat Eden :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[0, 0].Width = 180;
                tablooo2BB.Rows[0].Height = 45;


                string vefatKisi = AktifDestek.ad + " " + AktifDestek.soyad;
                tablooo2BB[0, 1].AddParagraph().AppendText("   " + vefatKisi.ToUpper() + "").ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[0, 1].Width = 270;


                tablooo2BB[1, 0].AddParagraph().AppendText(" Doğum Tarihi : \t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[1, 0].Width = 180;
                tablooo2BB.Rows[1].Height = 45;


                string dogumTar = AktifDestek.dogumTarihi.ToShortDateString();
                tablooo2BB[1, 1].AddParagraph().AppendText("   " + dogumTar + "   ").ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[1, 1].Width = 270;


                tablooo2BB[2, 0].AddParagraph().AppendText(" Evlenme Tarihi : \t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[2, 0].Width = 180;
                tablooo2BB.Rows[2].Height = 45;

                string evlenmeTar = AktifDestek.BekarCocuk_EvlenmeTarihi.ToShortDateString() + " (" +
                    AktifDestek.BekarCocukEvlenmeYas + " Yaş)";
                    
                tablooo2BB[2, 1].AddParagraph().AppendText("   " + evlenmeTar + "   ").ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[2, 1].Width = 270;
                
                tablooo2BB[3, 0].AddParagraph().AppendText("Olay Tarihinde Çalışıyor Mu?".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[3, 0].Width = 180;
                tablooo2BB.Rows[3].Height = 45;

                string calismaDurum = "HAYIR";
                if(AktifDestek.BekarCocuk_SuAnCalisiyor==true)
                {
                     calismaDurum = "EVET";
                }
                tablooo2BB[3, 1].AddParagraph().AppendText("   " + calismaDurum + "   ").ApplyCharacterFormat(cf1);
                tablooo2BB.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2BB[3, 1].Width = 270;

                if(calismaDurum == "EVET")
                {
                    tablooo2BB[4, 0].AddParagraph().AppendText("Olay Tarihindeki Maaş".ToUpper()).ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[4, 0].Width = 180;
                    tablooo2BB.Rows[4].Height = 45;

                    string gelir = AktifDestek.BekarCocuk_SuAnkiUcret.ToString("C", CultureInfo.CurrentCulture);
                    tablooo2BB[4, 1].AddParagraph().AppendText("  " + gelir + "   ");
                    tablooo2BB.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[4, 1].Width = 270;

                }
                else
                {
                    tablooo2BB[4, 0].AddParagraph().AppendText("İş Öncesi Miktar:".ToUpper()).ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[4, 0].Width = 180;
                    tablooo2BB.Rows[4].Height = 45;

                    string gelir = AktifDestek.BekarCocuk_18OncesiMaas.ToString("C", CultureInfo.CurrentCulture);
                    tablooo2BB[4, 1].AddParagraph().AppendText("  " + gelir + "   ").ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[4, 1].Width = 270;

                    tablooo2BB[5, 0].AddParagraph().AppendText("İşe Giriş Tar:".ToUpper()).ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[5, 0].Width = 180;
                    tablooo2BB.Rows[5].Height = 45;

                    string _iseGirisTar = AktifDestek.BekarCocuk_CalismaBasTarihi.ToShortDateString() +
                         " (" + AktifDestek.BekarCocuk_CalismaBasYasi + " Yaş)";

                    tablooo2BB[5, 1].AddParagraph().AppendText("   " + _iseGirisTar + "   ").ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[5, 1].Width = 270;

                    tablooo2BB[6, 0].AddParagraph().AppendText("Çalışma Yaşında Olsaydı Ücret:".ToUpper()).ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[6, 0].Width = 180;
                    tablooo2BB.Rows[6].Height = 45;


                    string gelir2 = AktifDestek.BekarCocuk_GelecekCalismaUcreti.ToString("C", CultureInfo.CurrentCulture);
                    tablooo2BB[6, 1].AddParagraph().AppendText("  " + gelir2 + "   ").ApplyCharacterFormat(cf1);
                    tablooo2BB.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo2BB[6, 1].Width = 270;

                }



            }
            else
            {
                tablooo2.ResetCells(5, 2);
                tablooo2.IndentFromLeft = 0;

                tablooo2[0, 0].AddParagraph().AppendText(" Vefat Eden :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[0, 0].Width = 180;
                tablooo2.Rows[0].Height = 45;


                string vefatKisi = AktifDestek.ad + " " + AktifDestek.soyad;
                tablooo2[0, 1].AddParagraph().AppendText("   " + vefatKisi.ToUpper() + "").ApplyCharacterFormat(cf1);
                tablooo2.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[0, 1].Width = 270;

                tablooo2[1, 0].AddParagraph().AppendText(" Doğum Tarihi : \t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[1, 0].Width = 180;
                tablooo2.Rows[1].Height = 45;


                string dogumTar = AktifDestek.dogumTarihi.ToShortDateString();
                tablooo2[1, 1].AddParagraph().AppendText("   " + dogumTar + "   ").ApplyCharacterFormat(cf1);
                tablooo2.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[1, 1].Width = 270;

                //   tablooo2[2, 0].AddParagraph().AppendText("").ApplyCharacterFormat(cf1);




                tablooo2[2, 0].AddParagraph().AppendText(" Net Maaş :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[2, 0].Width = 180;
                tablooo2.Rows[2].Height = 45;

                string gelir = SonMaas.ToString("C", CultureInfo.CurrentCulture);
                tablooo2[2, 1].AddParagraph().AppendText("  " + gelir + "   ");
                tablooo2.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[2, 1].Width = 270;

                tablooo2[3, 0].AddParagraph().AppendText(" Emekli Maaş :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[3, 0].Width = 180;
                tablooo2.Rows[3].Height = 45;



                string emekliMaas = AktifDestek.emekliMaas.ToString("C", CultureInfo.CurrentCulture);
                tablooo2[3, 1].AddParagraph().AppendText("   " + emekliMaas + "   ");
                tablooo2.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[3, 1].Width = 270;


                tablooo2[4, 0].AddParagraph().AppendText(" Askerlik :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[4, 0].Width = 180;
                tablooo2.Rows[4].Height = 45;

                string askerlikDurum = AktifDestek.askerlikYapti;
                tablooo2[4, 1].AddParagraph().AppendText("  " + askerlikDurum + "   ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo2.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo2[4, 1].Width = 270;
            }
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("%10 Arttırım %10 Azaltım Çarpımı Kullanılarak Gelecek" +
                " Dönem Maaş Hesaplaması Yapılmıştır.".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;


            paragraph = section.AddParagraph();






            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Yakınlar".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            // YAKINLAR
            var MaaslarDagitilmis = maasListesi2;

            foreach (var t in Yakinlar)
            {
                paragraph.ParagraphFormat.LineSpacing = 14f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText(t.ad) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.Bold = true;
                textRange.CharacterFormat.FontName = "Times New Roman";



                Decimal miktarGecmiş = 0;
                Decimal miktarAktif = 0;
                Decimal miktarPasif = 0;

                foreach (var gg in GunlukListe)
                {

                    var kisii2 = gg.kisiListe.Find(o => o.kisiId == t.Id);
                    if (kisii2 != null)
                    {
                        if (kisii2.donemi == "İşlemiş")
                        {
                            miktarGecmiş = miktarGecmiş + kisii2.alinanMiktar;
                        }
                        else if (kisii2.donemi == "Aktif")
                        {
                            miktarAktif = miktarAktif = kisii2.alinanMiktar;
                        }
                        else if (kisii2.donemi == "Pasif")
                        {
                            miktarPasif = miktarPasif + kisii2.alinanMiktar;
                        }
                    }

                }



                paragraph = section.AddParagraph();


                IWTable tablooo3 = section.AddTable();

                tablooo3.TableFormat.Borders.LineWidth = 0.2f;
                tablooo3.TableFormat.CellSpacing = 0;
                tablooo3.IndentFromLeft = 30;

                tablooo3.ResetCells(11, 3);



                tablooo3.Rows[0].Height = 40;

                tablooo3.ApplyStyle(BuiltinTableStyle.LightGrid);




                tablooo3.Rows[0].Height = 40;

                tablooo3[0, 0].AddParagraph().AppendText("\t       ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[0, 0].Width = 120;

                tablooo3[0, 1].AddParagraph().AppendText("\t       ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[0, 1].Width = 200;

                tablooo3[0, 2].AddParagraph().AppendText(" Miktar \t".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[0, 2].Width = 120;

                tablooo3.Rows[1].Height = 40;
                tablooo3[1, 0].AddParagraph().AppendText(" Yakınlık :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[1, 0].Width = 120;

                string yakinlik = t.yakinlik;
                tablooo3[1, 1].AddParagraph().AppendText("   " + yakinlik.ToUpper() + "   ");
                tablooo3.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[1, 1].Width = 200;

                tablooo3[1, 2].AddParagraph().AppendText(" \t ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[1, 2].Width = 120;


                tablooo3.Rows[2].Height = 40;
                tablooo3[2, 0].AddParagraph().AppendText(" İsim :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[2, 0].Width = 120;

                string isimm = t.ad;
                tablooo3[2, 1].AddParagraph().AppendText("   " + isimm.ToUpper() + "   ");
                tablooo3.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[2, 1].Width = 200;

                tablooo3[2, 2].AddParagraph().AppendText(" \t ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[2, 2].Width = 120;

                tablooo3.Rows[3].Height = 40;
                tablooo3[3, 0].AddParagraph().AppendText(" Doğum Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[3, 0].Width = 120;

                string dogumtarrr = t.dogumTarihiT.ToShortDateString();
                tablooo3[3, 1].AddParagraph().AppendText("   " + dogumtarrr + "   ");
                tablooo3.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[3, 1].Width = 200;

                tablooo3[3, 2].AddParagraph().AppendText("  \t ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[3].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[3, 2].Width = 120;

                tablooo3.Rows[4].Height = 40;
                tablooo3[4, 0].AddParagraph().AppendText("Destek Çıkış : ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[4, 0].Width = 120;

                string destekTar = t.destekCikisT.ToShortDateString();
                tablooo3[4, 1].AddParagraph().AppendText("   " + destekTar + "   ");
                tablooo3.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[4, 1].Width = 200;

                tablooo3[4, 2].AddParagraph().AppendText("  \t ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[4].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[4, 2].Width = 120;


                tablooo3.Rows[5].Height = 40;
                tablooo3[5, 0].AddParagraph().AppendText("Evlenme Olasılığı : ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[5, 0].Width = 120;

                if (t.yakinlik == "Eş")
                {
                    double evlenmeYuzdesi;
                    if (AktifDestek.esEvlenmeElleHesapla == false)
                    {
                        evlenmeYuzdesi = Convert.ToDouble(AktifDestek.esEvlenmeYuzdesi);
                    }
                    else
                    {
                        evlenmeYuzdesi = Convert.ToDouble(AktifDestek.esEvlenmeElle);
                    }

                    string yuzdeYazi = " \t %" + evlenmeYuzdesi.ToString() + "\t";

                    tablooo3[5, 1].AddParagraph().AppendText(yuzdeYazi);
                }
                else
                {
                    tablooo3[5, 1].AddParagraph().AppendText("  -  ");
                }
                tablooo3.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[5, 1].Width = 200;

                tablooo3[5, 2].AddParagraph().AppendText("   ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[5].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[5, 2].Width = 120;



                tablooo3.Rows[6].Height = 40;

                tablooo3[6, 0].AddParagraph().AppendText(" İşlemiş Dönem : ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[6, 0].Width = 120;


                string bolumIslemisYazi = "";
                DateTime tarihBolumIslemisBaslangic = AktifDestek.vefatTarihi;
                DateTime tarihBolumIslemisBitis = AktifDestek.raporTarihi.AddDays(-1);
                bolumIslemisYazi = tarihBolumIslemisBaslangic.ToShortDateString() + " - " + tarihBolumIslemisBitis.ToShortDateString();
                tablooo3[6, 1].AddParagraph().AppendText("\n   " + bolumIslemisYazi + "  ");
                tablooo3.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[6, 1].Width = 200;

                var _kisiDonemToplamlari = KisiDonemToplam(t.Id);
                string kisiIslemisToplam = String.Format("{0:C2}", _kisiDonemToplamlari.islemisKisiToplam);
                tablooo3[6, 2].AddParagraph().AppendText("   " + kisiIslemisToplam + "  ");
                tablooo3.Rows[6].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[6, 2].Width = 120;




                tablooo3.Rows[7].Height = 40;

                tablooo3[7, 0].AddParagraph().AppendText(" Aktif Dönem :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[7].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[7, 0].Width = 120;

                string bolumAktifYazisi = "";
                var bolumEmekli = bolumListe.Find(o => o.aciklama == "Emeklilik Tarihi");

                DateTime tarihBolumAktifBaslangic = AktifDestek.raporTarihi;
                DateTime tarihBolumAktisBitis = bolumEmekli.baslangic.AddDays(-1);

                bolumAktifYazisi = tarihBolumAktifBaslangic.ToShortDateString() + " - " +
                    tarihBolumAktisBitis.ToShortDateString();
                tablooo3[7, 1].AddParagraph().AppendText("   " + bolumAktifYazisi + "  ");
                tablooo3.Rows[7].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[7, 1].Width = 200;


                String kisiAktifToplam = String.Format("{0:C2}", _kisiDonemToplamlari.AktifKisiToplam);
                tablooo3[7, 2].AddParagraph().AppendText("  " + kisiAktifToplam + " ");
                tablooo3.Rows[7].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[7, 2].Width = 120;


                tablooo3.Rows[8].Height = 40;
                tablooo3[8, 0].AddParagraph().AppendText("Emekli Dönemi : ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[8].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[8, 0].Width = 120;

                string bolumEmekliYazisi = "";
                var bolumYasam = bolumListe.Find(o => o.aciklama == "Yasam Tarihi");
                DateTime tarihBolumEmekliBitis = bolumYasam.baslangic;
                DateTime tarihBolumEmekliBaslangic = tarihBolumAktisBitis.AddDays(1);
                bolumEmekliYazisi = tarihBolumEmekliBaslangic.ToShortDateString() + " - " +
                    tarihBolumEmekliBitis.ToShortDateString();

                tablooo3[8, 1].AddParagraph().AppendText("   " + bolumEmekliYazisi + "  ");
                tablooo3.Rows[8].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[8, 1].Width = 200;

                string kisiEmekliToplam = String.Format("{0:C2}", _kisiDonemToplamlari.EmekliKisiToplam);
                tablooo3[8, 2].AddParagraph().AppendText("   " + kisiEmekliToplam + " ");
                tablooo3.Rows[8].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[8, 2].Width = 120;



                tablooo3.Rows[9].Height = 40;

                tablooo3[9, 0].AddParagraph().AppendText(" Düşülen Evlenme İndirimi : ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[9].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[9, 0].Width = 120;

                tablooo3[9, 1].AddParagraph().AppendText("       ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[9].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[9, 1].Width = 200;


                if (t.yakinlik == "Eş")
                {
                    decimal mikk1 = 0;
                    mikk1 = _kisiDonemToplamlari.EmekliKisiToplam + _kisiDonemToplamlari.AktifKisiToplam;
                    decimal mikk2 = (mikk1 / Convert.ToDecimal(100)) * Convert.ToDecimal(EvlenmeIhtimali);
                    EvlenmeIndirimMiktar = mikk2;
                    string evlenmeIndirimYazisi = String.Format("{0:C2}", EvlenmeIndirimMiktar);

                    tablooo3[9, 2].AddParagraph().AppendText("   " + evlenmeIndirimYazisi + " ");


                }
                else
                {

                    tablooo3[9, 2].AddParagraph().AppendText("  -  ");

                }

                tablooo3.Rows[9].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[9, 2].Width = 120;




                tablooo3.Rows[10].Height = 40;
                tablooo3[10, 0].AddParagraph().AppendText(" Toplam :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo3.Rows[10].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[10, 0].Width = 120;


                tablooo3[10, 1].AddParagraph().AppendText("   ").ApplyCharacterFormat(cf1);
                tablooo3.Rows[10].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[10, 1].Width = 200;


                decimal kisiToplamMiktar = _kisiDonemToplamlari.islemisKisiToplam + _kisiDonemToplamlari.AktifKisiToplam
                 + _kisiDonemToplamlari.EmekliKisiToplam;

                string kisiToplamYazi = String.Format("{0:C2}", kisiToplamMiktar);
                tablooo3[10, 2].AddParagraph().AppendText("\n   " + kisiToplamYazi + "   ");
                tablooo3.Rows[10].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo3[10, 2].Width = 120;














                if (AktifDestek.cinsiyet == "Erkek")
                {
                    if (AktifDestek.askerlikYapti != "Yaptı")
                    {

                        DateTime askereGidisTar = new DateTime(AktifDestek.askereGidisYil, AktifDestek.askereGidisAy, 1);

                        string askerlikYazi = "(" + askereGidisTar.ToShortDateString() + " - "
                            + askereGidisTar.AddMonths(Convert.ToInt32(AktifDestek.askerlikSuresi)).ToShortDateString() +
                        " Tarihleri arasında askerlik hizmeti olarak hesaplanarak toplam tutardan düşülmüştür.)";

                        paragraph = section.AddParagraph();
                        paragraph.ParagraphFormat.LineSpacing = 18f;
                        paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                        textRange = paragraph.AppendText(askerlikYazi) as WTextRange;
                        textRange.CharacterFormat.FontSize = 12f;
                        textRange.CharacterFormat.Bold = true;
                        textRange.CharacterFormat.FontName = "Times New Roman";

                    }
                }
                paragraph = section.AddParagraph();
                paragraph = section.AddParagraph();
            }
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Masraflar".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();


            if (AktifDestek.DosyaDestektenYoksunlukMasraf.Count == 0)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("....") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.FontName = "Times New Roman";
            } else
            {
                IWTable tablooo4 = section.AddTable();

                tablooo4.TableFormat.Borders.LineWidth = 0.3f;
                tablooo4.TableFormat.CellSpacing = 0;

                tablooo4.ApplyStyle(BuiltinTableStyle.LightGrid);
                int satirSay = AktifDestek.DosyaDestektenYoksunlukMasraf.Count + 2;

                tablooo4.ResetCells(satirSay, 2);

                tablooo4.Rows[0].Height = 40;
                tablooo4[0, 0].AddParagraph().AppendText("\t Masraf :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo4.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo4[0, 0].Width = 250;


                tablooo4[0, 1].AddParagraph().AppendText("\t Miktar :\t ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo4.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo4[0, 1].Width = 150;

                int ii4 = 1;
                decimal toplamMasrafMiktar = 0;
                foreach (var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    tablooo4.Rows[ii4].Height = 36;

                    tablooo4[ii4, 0].AddParagraph().AppendText("  " + t2.masraftur2 + "   ");
                    tablooo4.Rows[ii4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo4[ii4, 0].Width = 250;

                    string masrafMiktar = String.Format("{0:C2}", t2.miktar);
                    tablooo4[ii4, 1].AddParagraph().AppendText("   " + masrafMiktar + "   ");
                    tablooo4.Rows[ii4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tablooo4[ii4, 1].Width = 150;

                    toplamMasrafMiktar = toplamMasrafMiktar + t2.miktar;
                    ii4 = ii4 + 1;

                }

                tablooo4.Rows[ii4].Height = 37;

                tablooo4[ii4, 0].AddParagraph().AppendText("  Toplam : \t".ToUpper());
                tablooo4.Rows[ii4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo4[ii4, 0].Width = 250;

                tablooo4[ii4, 1].AddParagraph().AppendText("   " + String.Format("{0:C2}", toplamMasrafMiktar));
                tablooo4.Rows[ii4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooo4[ii4, 1].Width = 150;

            }


            //İŞLEMİŞ DÖNEM YILLARA GÖRE TAZMİNAT TOPLAMLARI:



            List<GunlukBilgi> listeMaasIslemis = new List<GunlukBilgi>();
            List<GunlukBilgi> lsteMaasIsleyeecekAktif = new List<GunlukBilgi>();
            List<GunlukBilgi> listeMaasIsleyecekPasif = new List<GunlukBilgi>();

            List<YilSayiYapi> yillarIslemis = new List<YilSayiYapi>();
            List<YilSayiYapi> yillarIsleyecekAktif = new List<YilSayiYapi>();
            List<YilSayiYapi> yillarIsleyecekPasif = new List<YilSayiYapi>();


            foreach (var t in GunlukListe)
            {
                if (t.donemi == "İşlemiş")
                {

                    listeMaasIslemis.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIslemis.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIslemis.Add(ys);
                    }

                }
                else if (t.donemi == "Aktif")
                {
                    lsteMaasIsleyeecekAktif.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIsleyecekAktif.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIsleyecekAktif.Add(ys);
                    }
                }
                else if (t.donemi == "Pasif")
                {

                    listeMaasIsleyecekPasif.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIsleyecekPasif.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIsleyecekPasif.Add(ys);
                    }


                }

            }





            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("İşlemiş Dönem".ToUpper()).ApplyCharacterFormat(cf1);
            paragraph = section.AddParagraph();

            IWTable tabloIslemis = section.AddTable();

            tabloIslemis.TableFormat.Borders.LineWidth = 02f;
            tabloIslemis.TableFormat.CellSpacing = 0;

            tabloIslemis.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay2 = yillarIslemis.Count + 1;

            tabloIslemis.ResetCells(satirSay2, 5);

            decimal genelToplam1 = 0;

            tabloIslemis.IndentFromLeft = 10;
            tabloIslemis.Rows[0].Height = 40;

            tabloIslemis[0, 0].AddParagraph().AppendText("Başlangıç  \n   -    Bitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloIslemis[0, 0].Width = 105;
            tabloIslemis[0, 1].AddParagraph().AppendText(" Süre  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloIslemis[0, 1].Width = 105;
            tabloIslemis[0, 2].AddParagraph().AppendText(" Maaş  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloIslemis[0, 2].Width = 105;
            tabloIslemis[0, 3].AddParagraph().AppendText("Dönem \nTazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloIslemis[0, 3].Width = 105;
            tabloIslemis[0, 4].AddParagraph().AppendText("Genel \nToplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloIslemis[0, 4].Width = 105;

            var gecmisMaasDegerleri = AktifDestek.DestektekYoksunlukMaaslar.ToList();

            if (yillarIslemis.Count == 1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = listeMaasIslemis[0].tarih.ToShortDateString();
                int bitisIndex = listeMaasIslemis.Count - 1;
                donemBitis = listeMaasIslemis[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;
                DateTime donemBitisTarihi = listeMaasIslemis[bitisIndex].tarih;

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);
                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (AktifDestek.BekarCocukDurum == true)
                {
                    DateTime d1 = new DateTime(donemYil, 6, 1);
                    DateTime d2 = new DateTime(donemYil, 12, 1);

                    // 1 - Çalışıyor mu?
                    if (AktifDestek.BekarCocuk_SuAnCalisiyor == true)
                    {
                        var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1, AktifDestek.vefatTarihi);
                        yilMaas1.netMaas = oran1 ;

                        var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2, AktifDestek.vefatTarihi);
                        yilMaas2.netMaas = oran2 ;

                    }
                    else
                    {
                        // Çalışmıyorsa.....

                        if (d1 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1;
                        }

                        // Yılın 2. Yarısı için
                        if (d2 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2;
                        }

                    }


                }


                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {
                    foreach (var t3 in t2.kisiListe)
                    {
                        donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[1].Height = 40;

                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 0].Width = 105;

                tabloIslemis[1, 1].AddParagraph().AppendText("" + donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 1].Width = 105;

                if (donemBaslangicTarihi.Month > 6)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);

                }
                else if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);
                }
                else if (donemBaslangicTarihi.Month < 7 && donemBitisTarihi.Month > 6)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
              "\n -" + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 2].Width = 105;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 3].Width = 105;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 4].Width = 105;

            }
            else if (yillarIslemis.Count == 2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);
                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (AktifDestek.BekarCocukDurum == true)
                {
                    DateTime d1 = new DateTime(donemYil, 6, 1);
                    DateTime d2 = new DateTime(donemYil, 12, 1);

                    // 1 - Çalışıyor mu?
                    if (AktifDestek.BekarCocuk_SuAnCalisiyor == true)
                    {
                        var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1, AktifDestek.vefatTarihi);
                        yilMaas1.netMaas = oran1 ;

                        var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2, AktifDestek.vefatTarihi);
                        yilMaas2.netMaas = oran2 ;

                    }
                    else
                    {
                        // Çalışmıyorsa.....

                        if (d1 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1;
                        }

                        // Yılın 2. Yarısı için
                        if (d2 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2;
                        }

                    }


                }




                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }


                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }


                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;


                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[1].Height = 40;

                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 0].Width = 105;

                tabloIslemis[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 1].Width = 105;

                if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText("" + String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);

                }
                else
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
                                     "\n -" + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 2].Width = 105;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 3].Width = 105;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 4].Width = 105;


                //....... TABLO 2.SATIR...........
                //--------------------------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIslemis[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;

                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                //-------------------------
                int donemYilB = yillarIslemis[1].yill;
                string donem1B = yillarIslemis[1].yill.ToString() + "-1";
                string donem2B = yillarIslemis[1].yill.ToString() + "-2";

                var yilMaas1B = gecmisMaasDegerleri.Find(o => o.yil == donem1B);
                var yilMaas2B = gecmisMaasDegerleri.Find(o => o.yil == donem2B);


                if (AktifDestek.BekarCocukDurum == true)
                {
                    DateTime d1B = new DateTime(donemYilB, 6, 1);
                    DateTime d2B = new DateTime(donemYilB, 12, 1);

                    // 1 - Çalışıyor mu?
                    if (AktifDestek.BekarCocuk_SuAnCalisiyor == true)
                    {
                        var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1B, AktifDestek.vefatTarihi);
                        yilMaas1B.netMaas = oran1 ;

                        var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2B, AktifDestek.vefatTarihi);
                        yilMaas2B.netMaas = oran2 ;

                    }
                    else
                    {
                        // Çalışmıyorsa.....

                        if (d1B < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1B, AktifDestek.raporTarihi);
                            yilMaas1B.netMaas = oran1 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1B, AktifDestek.raporTarihi);
                            yilMaas1B.netMaas = oran1;
                        }

                        // Yılın 2. Yarısı için
                        if (d2B < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2B, AktifDestek.raporTarihi);
                            yilMaas2B.netMaas = oran2 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2B, AktifDestek.raporTarihi);
                            yilMaas2B.netMaas = oran2;
                        }

                    }


                }







                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", yilMaas1B.netMaas);
                }

                if (yilMaas2B != null)
                {
                    donemYilMaas22 = String.Format("{0:C2}", yilMaas2B.netMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;

                        }

                    }
                }

                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[2].Height = 40;
                tabloIslemis[2, 0].AddParagraph().AppendText(donemBaslangic2 + "\n -" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[2, 0].Width = 105;

                tabloIslemis[2, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[2, 1].Width = 105;

                if (donemYilMaas12 == donemYilMaas22)
                {
                    tabloIslemis[2, 2].AddParagraph().AppendText("" + String.Format("{0:C2}", donemYilMaas12) +
                        "").ApplyCharacterFormat(cf2);
                }
                else
                {
                    tabloIslemis[2, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas12) +
                     "\n -" + String.Format("{0:C2}", donemYilMaas22)).ApplyCharacterFormat(cf2);

                }

                tabloIslemis.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[2, 2].Width = 105;

                tabloIslemis[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[2, 3].Width = 105;

                tabloIslemis[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[2, 4].Width = 105;


            }
            else if (yillarIslemis.Count > 2)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);

                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (AktifDestek.BekarCocukDurum== true)
                {
                    DateTime d1 = new DateTime(donemYil, 6, 1);
                    DateTime d2 = new DateTime(donemYil, 12, 1);

                    // 1 - Çalışıyor mu?
                    if (AktifDestek.BekarCocuk_SuAnCalisiyor==true)
                    {
                        var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1, AktifDestek.vefatTarihi);
                        yilMaas1.netMaas = oran1 ;

                        var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2, AktifDestek.vefatTarihi);
                        yilMaas2.netMaas = oran2  ;

                    }
                    else
                    {
                        // Çalışmıyorsa.....

                        if(d1<AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1, AktifDestek.raporTarihi);
                            yilMaas1.netMaas = oran1 ;
                        }

                        // Yılın 2. Yarısı için
                        if (d2 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2, AktifDestek.raporTarihi);
                            yilMaas2.netMaas = oran2 ;
                        }

                    }


                }

                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }


                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }





                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 0].Width = 105;

                tabloIslemis[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 1].Width = 105;

                if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText("" + String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);

                }
                else
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
                                     "\n - " + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 2].Width = 105;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 3].Width = 105;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[1, 4].Width = 105;

                //-----------------------------------------------
                // YILLAR BÖLÜMÜ.............


                int yilSayac = 1;

                int yilSayisi = yillarIslemis.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIslemis[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    string donem1X = donemX.ToString() + "-1";
                    string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = gecmisMaasDegerleri.Find(o => o.yil == donem1X);

                    var yilMaas2X = gecmisMaasDegerleri.Find(o => o.yil == donem2X);


                    if (AktifDestek.BekarCocukDurum == true)
                    {
                        DateTime d1 = new DateTime(donemX, 6, 1);
                        DateTime d2 = new DateTime(donemX, 12, 1);

                        // 1 - Çalışıyor mu?
                        if (AktifDestek.BekarCocuk_SuAnCalisiyor == true)
                        {
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1, AktifDestek.vefatTarihi);
                            yilMaas1X.netMaas = oran1 ;

                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2, AktifDestek.vefatTarihi);
                            yilMaas2X.netMaas = oran2 ;

                        }
                        else
                        {
                            // Çalışmıyorsa.....

                            if (d1 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                            {
                                //İşten Önce
                                var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1, AktifDestek.raporTarihi);
                                yilMaas1X.netMaas = oran1 ;

                            }
                            else
                            {
                                //İşten Sonra....
                                var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1, AktifDestek.raporTarihi);
                                yilMaas1X.netMaas = oran1 ;
                            }

                            // Yılın 2. Yarısı için
                            if (d2 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                            {
                                //İşten Önce
                                var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2, AktifDestek.raporTarihi);
                                yilMaas2X.netMaas = oran2 ;

                            }
                            else
                            {
                                //İşten Sonra....
                                var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2, AktifDestek.raporTarihi);
                                yilMaas2X.netMaas = oran2 ;
                            }

                        }

                    }







                    if (yilMaas1X != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", yilMaas1X.netMaas);
                    }

                    if (yilMaas2X != null)
                    {
                        donemYilMaas2X = String.Format("{0:C2}", yilMaas2X.netMaas);
                    }


                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in listeMaasIslemis)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            foreach (var t3 in t2.kisiListe)
                            {
                                donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            }

                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);


                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);



                    tabloIslemis.Rows[yilSayac].Height = 40;
                    // tabloya yazma
                    tabloIslemis[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "\n - " + donemBitisX).ApplyCharacterFormat(cf2);
                    tabloIslemis.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloIslemis[yilSayac, 0].Width = 105;

                    tabloIslemis[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloIslemis.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloIslemis[yilSayac, 1].Width = 105;

                    if (donemBitisTarihi.Month < 7)
                    {
                        tabloIslemis[yilSayac, 2].AddParagraph().AppendText("" + String.Format("{0:C2}", donemYilMaas1X)).ApplyCharacterFormat(cf2);

                    }
                    else
                    {
                        tabloIslemis[yilSayac, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1X) +
                                         "\n - " + String.Format("{0:C2}", donemYilMaas2X)).ApplyCharacterFormat(cf2);
                    }
                    tabloIslemis.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloIslemis[yilSayac, 2].Width = 105;

                    tabloIslemis[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloIslemis.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloIslemis[yilSayac, 3].Width = 105;

                    tabloIslemis[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX)).ApplyCharacterFormat(cf2);
                    tabloIslemis.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloIslemis[yilSayac, 4].Width = 105;




                }





                //....... SON BÖLÜM............
                //--------------------------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;


                DateTime donemBaslangicTarih2 = new DateTime(donemBitisTarihi22.Year, 1, 1);

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;

                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }

                int yilSon1 = yillarIslemis.Count - 1;
                //-------------------------
                int donemYilB = yillarIslemis[yilSon1].yill;
                string donem1B = yillarIslemis[yilSon1].yill.ToString() + "-1";
                string donem2B = yillarIslemis[yilSon1].yill.ToString() + "-2";

                var yilMaas1B = gecmisMaasDegerleri.Find(o => o.yil == donem1B);
                var yilMaas2B = gecmisMaasDegerleri.Find(o => o.yil == donem2B);
                if (AktifDestek.BekarCocukDurum == true)
                {
                    DateTime d1 = new DateTime(donemYilB, 6, 1);
                    DateTime d2 = new DateTime(donemYilB, 12, 1);

                    // 1 - Çalışıyor mu?
                    if (AktifDestek.BekarCocuk_SuAnCalisiyor == true)
                    {
                        var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d1, AktifDestek.vefatTarihi);
                        yilMaas1B.netMaas = oran1;

                        var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, d2, AktifDestek.vefatTarihi);
                        yilMaas2B.netMaas = oran2 ;

                    }
                    else
                    {
                        // Çalışmıyorsa.....

                        if (d1 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d1, AktifDestek.raporTarihi);
                            yilMaas1B.netMaas = oran1 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran1 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d1, AktifDestek.raporTarihi);
                            yilMaas1B.netMaas = oran1 ;
                        }

                        // Yılın 2. Yarısı için
                        if (d2 < AktifDestek.BekarCocuk_CalismaBasTarihi)
                        {
                            //İşten Önce
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_18OncesiMaas, d2, AktifDestek.raporTarihi);
                            yilMaas2B.netMaas = oran2 ;

                        }
                        else
                        {
                            //İşten Sonra....
                            var oran2 = AsgariOranla1(AktifDestek.BekarCocuk_GelecekCalismaUcreti, d2, AktifDestek.raporTarihi);
                            yilMaas2B.netMaas = oran2 ;
                        }

                    }

                }



                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", yilMaas1B.netMaas);
                }

                if (yilMaas2B != null)
                {
                    donemYilMaas22 = String.Format("{0:C2}", yilMaas2B.netMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;

                        }

                    }
                }

                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[yilSayac + 1].Height = 40;

                tabloIslemis[yilSayac + 1, 0].AddParagraph().AppendText(donemBaslangic2 + "\n - " + donemBitis2).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[yilSayac + 1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[yilSayac + 1, 0].Width = 105;

                tabloIslemis[yilSayac + 1, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[yilSayac + 1, 1].Width = 105;

                if (donemYilMaas12 == donemYilMaas22)
                {
                    tabloIslemis[yilSayac + 1, 2].AddParagraph().AppendText("" + String.Format("{0:C2}", donemYilMaas12) +
                        "").ApplyCharacterFormat(cf2);
                }
                else
                {
                    tabloIslemis[yilSayac + 1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas12) +
                     "\n -" + String.Format("{0:C2}", donemYilMaas22)).ApplyCharacterFormat(cf2);

                }
                tabloIslemis.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[yilSayac + 1, 2].Width = 105;

                tabloIslemis[yilSayac + 1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[yilSayac + 1, 3].Width = 105;

                tabloIslemis[yilSayac + 1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis.Rows[yilSayac + 1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloIslemis[yilSayac + 1, 4].Width = 105;





            }



            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("İşleyecek Aktif Dönem Dönem".ToUpper()).ApplyCharacterFormat(cf1);

            section.AddParagraph().AppendText("(%10 Arttrım %10 Azaltım Uygulanmış Maaş Değerleridir.)");

            IWTable tabloAktif = section.AddTable();

            tabloAktif.TableFormat.Borders.LineWidth = 0.2f;
            tabloAktif.TableFormat.CellSpacing = 0;

            tabloAktif.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay2A = yillarIsleyecekAktif.Count + 1;

            tabloAktif.ResetCells(satirSay2A, 5);

            decimal genelToplam1A = 0;
            tabloAktif.Rows[0].Height = 40;

            tabloAktif[0, 0].AddParagraph().AppendText("Başlangıç  \n - Bitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloAktif[0, 0].Width = 105;

            tabloAktif[0, 1].AddParagraph().AppendText("Süre  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloAktif[0, 1].Width = 105;

            tabloAktif[0, 2].AddParagraph().AppendText("Maaş  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloAktif[0, 2].Width = 105;

            tabloAktif[0, 3].AddParagraph().AppendText("Dönem \nTazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloAktif[0, 3].Width = 105;

            tabloAktif[0, 4].AddParagraph().AppendText("Genel \nToplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloAktif[0, 4].Width = 105;


            if(AktifDestek.BekarCocukDurum== true)
            {
                if(AktifDestek.BekarCocuk_SuAnCalisiyor== true)
                {
                    //Oranla

                    decimal oran1 = AsgariOranla1(AktifDestek.BekarCocuk_SuAnkiUcret, AktifDestek.raporTarihi, AktifDestek.vefatTarihi);
                    SonMaas = oran1 ;
                }
                else
                {
                    SonMaas = AktifDestek.BekarCocuk_18OncesiMaas;
                }
            }

            if (yillarIsleyecekAktif.Count==1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = lsteMaasIsleyeecekAktif[0].tarih.ToShortDateString();
                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;
                donemBitis = lsteMaasIsleyeecekAktif[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;
                DateTime donemBitisTarihi = lsteMaasIsleyeecekAktif[bitisIndex].tarih;

                int donemYil = yillarIsleyecekAktif[0].yill;



                var yilMaas1 = SonMaas;
          
         


                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }



                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {
                    foreach (var t3 in t2.kisiListe)
                    {
                        donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    }
                }


                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n - " + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 0].Width = 105;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 1].Width = 105;


                tabloAktif[1,2].AddParagraph().AppendText("" +  donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 2].Width = 105;


                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 3].Width = 105;

                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 4].Width = 105;




            }
            else if(yillarIsleyecekAktif.Count==2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;
                //    DateTime donemBitisTarihi = listeMaasIslemis[bitisIndex].tarih2;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekAktif[0].yill;

                var yilMaas1 = SonMaas;
                if (AktifDestek.BekarCocukDurum == true)
                {
                    SonMaas = OgunkuMaas3(donemBitisTarihi.AddDays(-1));

                }

                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloAktif.Rows[1].Height = 40;
                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n - " + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 0].Width = 105;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 1].Width = 105;


                tabloAktif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 2].Width = 105;

                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 3].Width = 105;

                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 4].Width = 105;

                // 2.Satır....

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIslemis[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();

                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;

                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                var yilMaas1B = SonMaas;

                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", SonMaas);
                }

                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);
                
                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloAktif.Rows[2].Height = 40;

                tabloAktif[2, 0].AddParagraph().AppendText(donemBaslangic2 + "\n - " + donemBitis2).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[2, 0].Width = 105;

                tabloAktif[2, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[2, 1].Width = 105;

                tabloAktif[2, 2].AddParagraph().AppendText("" + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[2, 2].Width = 105;

                tabloAktif[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2));
                tabloAktif.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[2, 3].Width = 105;

                tabloAktif[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami));
                tabloAktif.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[2, 4].Width = 105;



            }
            else if(yillarIsleyecekAktif.Count>2)
            {
                // ----------------------------İLK BÖLÜM------------------------------------

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = yillarIsleyecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekAktif[0].yill;

                var yilMaas1 = SonMaas;

                if (AktifDestek.BekarCocukDurum == true)
                {
                    if (AktifDestek.BekarCocuk_CalismaBasTarihi < donemBitisTarihi)
                    {
                        SonMaas = AktifDestek.BekarCocuk_GelecekCalismaUcreti;
                    }

                }

                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }

                int aySay1 = 0;
                
                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }


                decimal donemTazminatToplami = 0;
                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {
                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;
                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloAktif.Rows[1].Height = 40;

                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 0].Width = 105;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 1].Width = 105;

                tabloAktif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 2].Width = 105;

                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 3].Width = 105;
                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[1, 4].Width = 105;


                //Ara yıllar..
                int yilSayac = 1;

                int yilSayisi = yillarIsleyecekAktif.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIsleyecekAktif[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    //string donem1X = donemX.ToString() + "-1";
                    //string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = SonMaas;
                    if (AktifDestek.BekarCocukDurum == true)
                    {
                        if(AktifDestek.BekarCocuk_CalismaBasTarihi< donemBtisTarihiX)
                        {
                            SonMaas = AktifDestek.BekarCocuk_GelecekCalismaUcreti;
                        }

                    }


                    if (SonMaas != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", SonMaas);
                    }



                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in lsteMaasIsleyeecekAktif)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            foreach (var t3 in t2.kisiListe)
                            {
                                donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            }

                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);

                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);


                    tabloAktif.Rows[yilSayac].Height = 40;
                    tabloAktif[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "\n -" + donemBitisX).ApplyCharacterFormat(cf2);
                    tabloAktif.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloAktif[yilSayac, 0].Width = 105;

                    tabloAktif[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloAktif.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloAktif[yilSayac, 1].Width = 105;

                    tabloAktif[yilSayac, 2].AddParagraph().AppendText("" + donemYilMaas1X).ApplyCharacterFormat(cf2);
                    tabloAktif.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloAktif[yilSayac, 2].Width = 105;

                    tabloAktif[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloAktif.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloAktif[yilSayac, 3].Width = 105;

                    tabloAktif[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX)).ApplyCharacterFormat(cf2);
                    tabloAktif.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloAktif[yilSayac, 4].Width = 105;
                }


                //---- Son Bölüm


                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";

                int sonIndex = yillarIsleyecekAktif.Count() - 1;

                int sonYil = yillarIsleyecekAktif[sonIndex].yill;
                DateTime donemBaslangicTarih2 = new DateTime(sonYil, 1, 1);
                int bitisIndex2 = lsteMaasIsleyeecekAktif.Count - 1;
                DateTime donemBitisTarihi22 = lsteMaasIsleyeecekAktif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();

                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }


                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 ;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }



                var yilMaas1B =SonMaas;

                if (AktifDestek.BekarCocukDurum == true)
                {
                    if (AktifDestek.BekarCocuk_CalismaBasTarihi < donemBitisTarihi22)
                    {
                        SonMaas = AktifDestek.BekarCocuk_GelecekCalismaUcreti;
                    }

                }

                donemYilMaas12 = String.Format("{0:C2}", SonMaas);
                

                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;

                        }

                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);





                tabloAktif.Rows[yilSayac +1].Height = 40;

                tabloAktif[yilSayac +1, 0].AddParagraph().AppendText(donemBaslangic2 + "\n -" + donemBitis2);
                tabloAktif.Rows[yilSayac +1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[yilSayac+1, 0].Width = 105;

                tabloAktif[yilSayac+1, 1].AddParagraph().AppendText(donemSure2);
                tabloAktif.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[yilSayac + 1, 1].Width = 105;

                tabloAktif[yilSayac+1, 2].AddParagraph().AppendText("" + donemYilMaas12);
                tabloAktif.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[yilSayac + 1, 2].Width = 105;

                tabloAktif[yilSayac+1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2));
                tabloAktif.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[yilSayac + 1, 3].Width = 105;


                tabloAktif[yilSayac +1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami));
                tabloAktif.Rows[yilSayac + 1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloAktif[yilSayac + 1, 4].Width = 105;


            }



            // PASİF DÖNEM TABLO İŞLEMLERİ.............
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("Pasif Dönem".ToUpper()).ApplyCharacterFormat(cf1);
            paragraph = section.AddParagraph();

            IWTable tabloPasif = section.AddTable();

            tabloPasif.TableFormat.Borders.LineWidth =0.3f;
            tabloPasif.TableFormat.CellSpacing = 0;

            tabloPasif.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay3A = yillarIsleyecekPasif.Count + 1;

            tabloPasif.ResetCells(satirSay3A, 5);

            decimal genelToplam2A = 0;

            tabloPasif.Rows[0].Height = 40;
            tabloPasif[0, 0].AddParagraph().AppendText("Başlangıç  \n -Bitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloPasif[0, 0].Width = 105;

            tabloPasif[0, 1].AddParagraph().AppendText("Süre  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloPasif[0, 1].Width = 105;         

            tabloPasif[0, 2].AddParagraph().AppendText("Maaş  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloPasif[0, 2].Width = 105;

            tabloPasif[0, 3].AddParagraph().AppendText("Dönem  \n Tazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloPasif[0, 3].Width = 105;

            tabloPasif[0, 4].AddParagraph().AppendText("Genel  \n Toplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloPasif[0, 4].Width = 105;



            if (yillarIsleyecekPasif.Count==1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = listeMaasIsleyecekPasif[0].tarih.ToShortDateString();
                int bitisIndex = listeMaasIsleyecekPasif.Count - 1;
                donemBitis = listeMaasIsleyecekPasif[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;
                DateTime donemBitisTarihi = listeMaasIsleyecekPasif[bitisIndex].tarih;

                int donemYil = yillarIsleyecekAktif[0].yill;


                if (AktifDestek.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", AktifDestek.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {
                    foreach (var t3 in t2.kisiListe)
                    {
                        donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[0].Height = 40;
                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 0].Width = 105;

                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 1].Width = 105;

                tabloPasif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 2].Width = 105;

                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 3].Width = 105;

                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 4].Width = 105;



            }
            else if( yillarIsleyecekPasif.Count==2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = listeMaasIsleyecekPasif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekPasif[0].yill;

                var yilMaas1 = AktifDestek.emekliMaas;

                if (AktifDestek.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", AktifDestek.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;


                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[1].Height = 40;

                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 0].Width = 105;

                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 1].Width = 105;

                tabloPasif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 2].Width = 105;

                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 3].Width = 105;

                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 4].Width = 105;

                // 2.bölüm.....

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIsleyecekPasif[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIsleyecekPasif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;

                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                int donemYil2 = yillarIsleyecekPasif[1].yill;



                var yilMaas1B = AktifDestek.emekliMaas;



                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", AktifDestek.emekliMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[2].Height = 40; ;

                tabloPasif[2, 0].AddParagraph().AppendText(donemBaslangic2 + "\n -" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[2, 0].Width = 105;

                tabloPasif[2, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[2, 1].Width = 105;

                tabloPasif[2, 2].AddParagraph().AppendText("" + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[2, 2].Width = 105;

                tabloPasif[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[2, 3].Width = 105;

                tabloPasif[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami2)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[2, 4].Width = 105;
            }
            else if (yillarIsleyecekPasif.Count>2)
            {

                // ----------------------------İLK BÖLÜM------------------------------------

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = yillarIsleyecekPasif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekPasif[0].yill;
           
                var yilMaas1 = AktifDestek.emekliMaas;

                if (AktifDestek.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", AktifDestek.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);


                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloPasif.Rows[1].Height = 40;

                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + "\n -" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 0].Width = 105;

                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 1].Width = 105;

                tabloPasif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 2].Width = 105;

                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 3].Width = 105;

                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[1, 4].Width = 105;




                // ----------------------YILLAR-------------------------------------------

                int yilSayac = 1;

                int yilSayisi = yillarIsleyecekPasif.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIsleyecekPasif[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    //string donem1X = donemX.ToString() + "-1";
                    //string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = AktifDestek.emekliMaas;
                    if (AktifDestek.emekliMaas != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", AktifDestek.emekliMaas);
                    }



                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in listeMaasIsleyecekPasif)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            foreach (var t3 in t2.kisiListe)
                            {
                                donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            }

                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);

                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);

                    tabloPasif.Rows[yilSayac].Height = 40;
                    tabloPasif.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloPasif[yilSayac, 0].Width = 105;
                    tabloPasif[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "\n -" + donemBitisX).ApplyCharacterFormat(cf2);

                    tabloPasif[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloPasif.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloPasif[yilSayac, 1].Width = 105;

                    tabloPasif[yilSayac, 2].AddParagraph().AppendText("" + donemYilMaas1X).ApplyCharacterFormat(cf2);
                    tabloPasif.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloPasif[yilSayac, 2].Width = 105;

                    tabloPasif[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloPasif.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloPasif[yilSayac, 3].Width = 105;

                    tabloPasif[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX)).ApplyCharacterFormat(cf2);
                    tabloPasif.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    tabloPasif[yilSayac, 4].Width = 105;



                }


                //****************************************************************

                //---------------------------- SON BÖLÜM--------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";

                int sonIndex = yillarIsleyecekPasif.Count() - 1;

                int sonYil = yillarIsleyecekPasif[sonIndex].yill;
                DateTime donemBaslangicTarih2 = new DateTime(sonYil, 1, 1);
                int bitisIndex2 = listeMaasIsleyecekPasif.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIsleyecekPasif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;

                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;



                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 ;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }

                int donemYil2 = yillarIsleyecekPasif[1].yill;
                //string donem12 = yillarIslemis[1].yill.ToString() + "-1";
                //string donem22 = yillarIslemis[1].yill.ToString() + "-2";



                var yilMaas1B = AktifDestek.emekliMaas;



                if (AktifDestek.emekliMaas != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", AktifDestek.emekliMaas);
                }

                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        foreach (var t3 in t2.kisiListe)
                        {
                            donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        }

                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);



                tabloPasif.Rows[yilSayac + 1].Height = 40;
                tabloPasif[yilSayac+1, 0].AddParagraph().AppendText(donemBaslangic2 + "\n -" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[yilSayac+1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[yilSayac+1, 0].Width = 105;

                tabloPasif[yilSayac+1, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[yilSayac + 1, 1].Width = 105;

                tabloPasif[yilSayac +1, 2].AddParagraph().AppendText("" + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[yilSayac + 1, 2].Width = 105;

                tabloPasif[yilSayac+1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[yilSayac + 1, 3].Width = 105;

                tabloPasif[yilSayac+1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami2)).ApplyCharacterFormat(cf2);
                tabloPasif.Rows[yilSayac + 1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloPasif[yilSayac + 1, 4].Width = 105;

            }

         


      






            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "DestektenYoksunlukRapor", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            //Save the stream as a file in the device and invoke it for viewing
  //          var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("DestektenYoksunlukRapor.docx", "application/msword", stream);

            //});





         

         
        }






        private KisiToplamlariDonemsel KisiDonemToplam(string kisiId)
        {
            KisiToplamlariDonemsel aa = new KisiToplamlariDonemsel();

            foreach (var t in GunlukListe)
            {

                foreach(var t2 in t.kisiListe)
                {
             
                        if(t2.kisiId== kisiId)
                        {

                        if (t2.donemi == "İşlemiş")
                        {
                            aa.islemisKisiToplam = aa.islemisKisiToplam + t2.alinanMiktar;
                       
                        }else if(t2.donemi=="Aktif")
                        {
                            aa.AktifKisiToplam = aa.AktifKisiToplam + t2.alinanMiktar;

                        }else if(t2.donemi=="Pasif")
                        {
                            aa.EmekliKisiToplam = aa.EmekliKisiToplam + t2.alinanMiktar;

                        }

                    }

                 
                }
            }

            return aa;

        }
        private class YilSayiYapi
        {
            public int yill { get; set; }
        }
        //  -------------------------------------------------------------------------------------------
        //AKTÜERYAL HESAPLAMA


        async   private void TrafikHesaplama2()
        {
            var gecmisMaasDegerleri = Destek.DestektekYoksunlukMaaslar.ToList();
            trhListe = tt22.Liste;

            int emeklilikYas = 60;

            if (AktifDestek.emeklilikYasi != null)
            {

                if (AktifDestek.emeklilikYasi != 0)
                {

                    emeklilikYas = Convert.ToInt32(AktifDestek.emeklilikYasi);

                }
            }


            DateTime emeklilikTarihi = AktifDestek.dogumTarihi.AddYears(emeklilikYas);

            //  var yakinEs = yakinlar.Find(o => o.yakinlik == "Eş");


            Yakin2 yakinEs = null;


            try
            {
                yakinEs = Yakinlar.OrderByDescending(o => o.destekCikisT).First();

            }
            catch (Exception ex)
            {

            }






            int mevcutYas = 0;
            //KİŞİNİN TAHMİNİ YAŞAM SÜRESİ BULUNACAK!!!!!!
            // Sadece YIL alınacak küsür Yok
            if (AktifDestek.yasiYuvarla == 0)
            {

                DateTime tmpDogum = AktifDestek.dogumTarihi;
                int yil = 0;
                while (AktifDestek.vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                mevcutYas = yil;

            }
            else if (AktifDestek.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = AktifDestek.dogumTarihi;
                int yil = 0;
                while (AktifDestek.vefatTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = AktifDestek.dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= AktifDestek.vefatTarihi)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                mevcutYas = yil;
            }
          
            string cinss = "1";
            if (AktifDestek.cinsiyet == "Kadın")
            {
                cinss = "2";
            }
            decimal kalanOmur = 0;

            //if (drpYasamTablosu.SelectedIndex == 0)
            //{
            //    kalanOmur = islem.yasamTablosuKalanYil_Al_PMF(mevcutYas);

            //}
            //else if (drpYasamTablosu.SelectedIndex == 1)
            //{
            //    kalanOmur = islem.yasamTablosuKalanYil_Al_CSO1980(mevcutYas, cinss);
            //}
            //else if (drpYasamTablosu.SelectedIndex == 2)
            //{
            //    kalanOmur = islem.yasamTablosuKalanYil_Al_TRH2010(mevcutYas, cinss);
            //}


            kalanOmur =Convert.ToDecimal( yasamSureIslem.TrhSureHesapla(mevcutYas, cinss));

            decimal kalanYil = Math.Truncate(kalanOmur);
            decimal kusurat = kalanOmur - kalanYil;

            decimal kalanAy1 = kusurat * 12;

            decimal kalanAy = Math.Truncate(kalanAy1);

            decimal gun = (kalanAy1 - kalanAy) * 30;

            decimal kalanGun = Convert.ToInt32(gun);

            decimal kusurrr = Convert.ToDecimal(AktifDestek.kusurOrani);




            //gelen kalan ömür  küsüratları olay tarihine eklenir.
            DateTime vefatYas = AktifDestek.vefatTarihi;

            vefatYas = vefatYas.AddYears(Convert.ToInt32(kalanYil));
            vefatYas = vefatYas.AddMonths(Convert.ToInt32(kalanAy));
            vefatYas = vefatYas.AddDays(Convert.ToInt32(kalanGun));
            DateTime vefatYas3 = vefatYas; ;

            _yasaydiVefatTarihi_TRH = vefatYas;

            if (yakinEs != null)
            {


                if (yakinEs.destekCikisT != null)
                {
                    DateTime vefatYas1 = yakinEs.destekCikisT;


                    DateTime vefatYas2 = vefatYas;



                    if (vefatYas1 > vefatYas)
                    {

                        vefatYas3 = vefatYas;

                    }
                    else
                    {
                        vefatYas3 = vefatYas1;

                    }

                }
            }
            // VEFAT EDEN KİŞİ İÇİN HESAPLAMA BİTİŞİ

            //bolum Listesi Oluşturma



            _destekCikisTarihi_TRH = vefatYas3;

            bolumListe.Clear();

            bolum bolumOlayTarihi = new bolum();
            bolumOlayTarihi.tur = 1;
            bolumOlayTarihi.baslangic = Convert.ToDateTime(AktifDestek.vefatTarihi);
            bolumOlayTarihi.aciklama = "Olay Tarihi";
            bolumListe.Add(bolumOlayTarihi);

            bolum bolumRaporTarihi = new bolum();
            bolumRaporTarihi.tur = 2;
            bolumRaporTarihi.baslangic = Convert.ToDateTime(AktifDestek.raporTarihi);
            bolumRaporTarihi.aciklama = "Rapor Tarihi";
            bolumListe.Add(bolumRaporTarihi);

            bolum bolumEmeklilikTarihi = new bolum();
            bolumEmeklilikTarihi.tur = 3;
            bolumEmeklilikTarihi.baslangic = emeklilikTarihi;
            bolumEmeklilikTarihi.aciklama = "Emeklilik Tarihi";

            if (AktifDestek.calismaDurumu != "Ev İşleri")
            {
                bolumListe.Add(bolumEmeklilikTarihi);
            }


            bolum bolumYasamTarihi = new bolum();
            bolumYasamTarihi.tur = 4;
            bolumYasamTarihi.baslangic = vefatYas3;
            bolumYasamTarihi.aciklama = "Yasam Tarihi";
            bolumListe.Add(bolumYasamTarihi);


            //lblTazminatBitis.Text = vefatYas3.ToShortDateString();

            int siraa = 0;
            // HESAP BAŞLANGIÇ

            //BilirikisiHesap islem2 = new BilirikisiHesap();


            AsgariUcretService islem2b = new AsgariUcretService();

         
            var asgariMaasListesi = (await islem2b.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);





            asgariMaasListesi.OrderByDescending(o => o.brut);

            maasListesi.Clear();
            
            maasListesi2.Clear();

            var bolumListe2 = bolumListe.OrderBy(o => o.baslangic).ToList();
            bolumListeSirali.Clear();
            bolumListeSirali = bolumListe2;

            int tt = 1;

            int j = bolumListe2.Count();

            int emekliDurum = 0;

            int islenmisDurum = 0;



            ////foreach (var t3 in maasListesiB)
            ////{
            ////    {

            ////        t3.maas3 = t3.maas2;
            ////        t3.maas4= Convert.ToDecimal((t3.maas3) * (bilgi.kendiKusuru / 100));
            ////        maasListesi.Add(t3);tx

            ////    }

            ////}




            var esVar2 = Yakinlar.Find(o => o.yakinlik == "Eş");

            int esDurum2 = -1;

            if (esVar2 != null)
            {
                if (esVar2.calisma == 0)
                {
                    esDurum2 = 0;
                }
                else if (esVar2.calisma == 1)
                {
                    esDurum2 = 1;
                }
            }

            int hesabaKatilacakCocukSayisi2 = 0;

            foreach (var t in Yakinlar)
            {
                if (t.yakinlik == "Çocuk")
                {

                    hesabaKatilacakCocukSayisi2 = hesabaKatilacakCocukSayisi2 + 1;

                }

            }


            // ANA FOR YAPISI...........


            // ANA FOR YAPISI...........

            for (int i = 0; i < j; i++)
            {
                if (i + 1 == j)
                {

                    break;
                }



                string bolumAciklama = "";
                DateTime bolBas = bolumListe2[i].baslangic;
                DateTime bolBit = bolumListe2[i + 1].baslangic.AddDays(-1);

                bolumAciklama = bolumListe2[i].tur.ToString();

                if (bolumListe2[i].tur == 2)
                {
                    islenmisDurum = 1;
                }

                if (bolumListe2[i].tur == 3)
                {
                    emekliDurum = 1;
                }


                //Vefat Tarihi Çıkartılacak!
                if (bolumListe2[i].tur == 4)
                {
                    break;
                }

                if (gecmisMaasDegerleri.Count == 0)
                {
                    return;
                }


                // Başlangıç GÜN Ve- AY - Son Gün Hesapları

                DateTime tar1A = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);

                int gunnX = (tar1A - bolBas).Days + 1;

                DateTime tmpTarX = tar1A;

                int ayyX = 0;


                while (tmpTarX < bolBit)
                {

                    ayyX = ayyX + 1;

                    tmpTarX = tmpTarX.AddMonths(1);

                }
                
                if (bolBas.Day > bolBit.Day)
                {
                    ayyX = ayyX - 2;
                }

                else if (bolBas.Day <= bolBit.Day)
                {

                    ayyX = ayyX - 2;
                }
                int gun2X = bolBit.Day;


                DateTime tazminatBas1X = Convert.ToDateTime(bolBas);

                decimal gunToplamX1 = 0;



                //  Gün AY ADet Hesaplamaları Bitiş


                // BAŞLANGIÇ GÜNLER
                //Gün Bölüm1


                if (bolBas.Day == 1)
                {

                    decimal oAykiKisiMaasi = 0;
                    MaasAy m = new MaasAy();


                    m.tarih = bolBas;

                    DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT;


                    var s7 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;
                    int a = bolBas.Year;
                    int b = 1;
                    if (bolBas.Month > 6)
                    {
                        b = 2;
                    }


                    string c = a.ToString() + "-" + b.ToString();
                    AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g100 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > m.tarih2)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= bolBit)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }


                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);





                    if (s7 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s7.netMaas);






                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                    }







                    if (islenmisDurum == 0)
                    {
                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;

                        oAykiKisiMaasi = oAykiKisiMaasi + agi;

                        m.donemi = "İşlenmiş";

                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;

                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(AktifDestek.emekliMaas);
                        m.donemi = "Pasif";
                    }

                    m.aciklama = bolumAciklama;


                    m.maas = oAykiKisiMaasi;


                    m.maas2 = m.maas;





                    // BolBaştaki Maaş Bulunacak 



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }

                    decimal kusur = Convert.ToDecimal(AktifDestek.kusurOrani);





                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));



                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;
                    tazminatBas1X = tazminatBas1X.AddMonths(1);
                    //AY ALINACAK
                    //  gunToplamX1 = maasIlk;

                }
                else
                {
                    MaasAy m = new MaasAy();

                    m.tarih = bolBas;

                    DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT2;

                    // AY MAAŞ Güne çevrilip Gün olarak bulunacak

                    decimal oAykiKisiMaasi = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;
                    int a = bolBas.Year;
                    int b = 1;
                    if (bolBas.Month > 6)
                    {
                        b = 2;
                    }



                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);
                    if (g100 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }

                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > bolBas)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }

                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);



                    var s8 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                    if (s8 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s8.netMaas);




                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);




                    }




                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";


                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(AktifDestek.emekliMaas);
                        m.donemi = "Pasif";
                    }

                    m.aciklama = bolumAciklama;
                    gunnX = gunnX - 1;
                    System.Decimal gunMaas = oAykiKisiMaasi / 30;
                    gunToplamX1 = gunMaas * gunnX;

                    Decimal agi2 = (agi / 30) * gunnX;
                    m.maas = gunToplamX1;

                    m.maas2 = m.maas;


                    if (islenmisDurum != 0)
                    {

                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }


                    decimal kusur = Convert.ToDecimal(AktifDestek.kusurOrani);


                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));

                    m.aciklama = bolumAciklama;


                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;
                    tazminatBas1X = tazminatBas1X.AddMonths(1);

                }









                // AYLAR

                for (int j2 = 0; j2 < ayyX + 1; j2++)
                {


                    MaasAy m = new MaasAy();
                    decimal oAykiKisiMaasi = 0;

                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                    DateTime tmpT = m.tarih.AddMonths(1);
                    DateTime tmpT2 = tmpT.AddDays(-1);
                    m.tarih2 = tmpT2;


                    Decimal agi2f = 0;

                    Decimal agi = 0;
                    Decimal oAykiAsgariUcret = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();


                    AsgariUcretTablosu g105 = asgariMaasListesi.Find(o => o.yil == c);


                    if (g105 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g105.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }



                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;

                    if (esVar != null)
                    {
                        if (esVar.destekCikisT > bolBas)

                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }



                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);

                    var s1 = gecmisMaasDegerleri.Find(o => o.yilBas <= tazminatBas1X && o.yilBit >= tazminatBas1X);


                    if (s1 != null)
                    {
                        oAykiKisiMaasi = Convert.ToDecimal(s1.netMaas);


                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);






                    }



                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";
                        m.maas = oAykiKisiMaasi;
                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.maas2 = oAykiKisiMaasi;


                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        m.maas = oAykiKisiMaasi;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                        m.maas2 = oAykiKisiMaasi;
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(AktifDestek.emekliMaas);
                        m.maas = oAykiKisiMaasi;
                        m.donemi = "Pasif";
                        m.maas2 = oAykiKisiMaasi;

                    }

                    m.aciklama = bolumAciklama;



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }



                    decimal kusur = Convert.ToDecimal(AktifDestek.kusurOrani);

                    decimal kayipOran = 0;




                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));

                    m.aciklama = bolumAciklama;


                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;


                    tazminatBas1X = tazminatBas1X.AddMonths(1);
                    //ToplamX1 = ToplamX1 + maas;

                }






                // GÜN BÖLÜM2
                DateTime sonAyKontrol = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                if (bolBit.AddDays(1).Month != sonAyKontrol.Month)
                {

                    MaasAy m = new MaasAy();

                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                    m.tarih2 = bolBit;
                    decimal oAykiKisiMaasi = 0;
                    m.aciklama = bolumAciklama;

                    var s14 = gecmisMaasDegerleri.Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                    Decimal oAykiAsgariUcret = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g107 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }


                    if (s14 != null)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);



                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                        //YAKIN LİSTE

                        var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;
                        if (esVar != null)
                        {
                            if (esVar.calisma == 0)
                            {
                                esDurum = 0;
                            }
                            else if (esVar.calisma == 1)
                            {
                                esDurum = 1;
                            }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in Yakinlar)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.destekCikisT >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                    }









                    m.maas = oAykiKisiMaasi;






                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        m.maas = oAykiKisiMaasi - agi2f;
                        m.maas2 = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        m.maas = oAykiKisiMaasi;
                        m.maas2 = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(AktifDestek.emekliMaas);
                        m.donemi = "Pasif";
                    }


                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }





                    decimal kusur = Convert.ToDecimal(AktifDestek.kusurOrani);





                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));






                    m.sira = siraa + 1;
                    //m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas) * 1.1) * (0.9));
                    maasListesi.Add(m);
                    siraa = siraa + 1;


                }
                else
                {
                    // AY MAAŞ Güne çevrilip Gün olarak bulunacak


                    MaasAy m = new MaasAy();

                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                    m.tarih2 = bolBit;
                    decimal oAykiKisiMaasi = 0;
                    m.aciklama = bolumAciklama;

                    var s14 = gecmisMaasDegerleri.Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                    Decimal oAykiAsgariUcret = 0;

                    Decimal agi2f = 0;

                    Decimal agi = 0;


                    int a = tazminatBas1X.Year;
                    int b = 1;
                    if (tazminatBas1X.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();

                    AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                    if (g107 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                    }
                    else
                    {
                        if (a >= DateTime.Now.Year)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                        }


                    }


                    //YAKIN LİSTE

                    var esVar = Yakinlar.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;
                    if (esVar != null)
                    {
                        if (esVar.calisma == 0)
                        {
                            esDurum = 0;
                        }
                        else if (esVar.calisma == 1)
                        {
                            esDurum = 1;
                        }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in Yakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.destekCikisT >= m.tarih2)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }


                    agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                    if (s14 != null)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);



                        agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                    }




                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;



                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(SonMaas);
                        oAykiKisiMaasi = oAykiKisiMaasi + agi;
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(AktifDestek.emekliMaas);
                        m.donemi = "Pasif";
                    }




                    m.aciklama = bolumAciklama;

                    int days = DateTime.DaysInMonth(tazminatBas1X.Year, tazminatBas1X.Month);
                    System.Decimal gunMaas = oAykiKisiMaasi / days;





                    m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);

                    m.tarih2 = bolBit;








                    int gunSay = (m.tarih2 - m.tarih).Days;
                    gunSay = gunSay + 1;








                    m.maas = gunMaas * gunSay;


                    m.maas2 = m.maas;



                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                    }
                    else
                    {
                        m.maas3 = m.maas2;
                    }


                    decimal kusur = Convert.ToDecimal(AktifDestek.kusurOrani);



                    m.maas4 = Convert.ToDecimal((m.maas3) * (kusur / 100));


                    m.sira = siraa + 1;

                    maasListesi.Add(m);
                    siraa = siraa + 1;

                }







                // FOR BİTİŞ

            }




            int ii = 0;

            foreach (var t4 in payBolumleri)
            {
                if (ii == 0)
                {
                    t4.basTar =AktifDestek.vefatTarihi;

                }
                else
                {
                    t4.basTar = payBolumleri[ii - 1].bitTar.AddDays(-1);

                }

                ii = ii + 1;
            }


            //ASKERLİK DURUMU KONTROLÜ

            if (AktifDestek.cinsiyet == "Erkek")
            {
                if (AktifDestek.askerlikYapti == "YapMAdı")
                {
                
                        DateTime askerlikBasTar = new DateTime(AktifDestek.askereGidisYil, AktifDestek.askereGidisAy,1);
                        DateTime askelikBitisTar = askerlikBasTar.AddMonths(Convert.ToInt32(AktifDestek.askerlikSuresi));

                        //askelikBitisTar = askelikBitisTar.AddDays(-1);

                        foreach (var t2 in maasListesi)
                        {

                            bool askerde = false;

                            if (t2.tarih >= askerlikBasTar && t2.tarih < askelikBitisTar)
                            {

                                askerde = true;

                            }

                            if (askerde == true)
                            {
                                t2.donemi = "Askerde";
                                t2.maas = 0;
                                t2.maas2 = 0;
                                t2.maas3 = 0;
                                t2.maas4 = 0;
                            }



                        }

                }

            }




            foreach (var t2 in maasListesi)
            {
                MaasAy2 m2 = new MaasAy2();

                m2.aciklama = t2.aciklama;
                m2.donemi = t2.donemi;
                m2.maas = t2.maas;
                m2.maas2 = t2.maas2;
                m2.maas3 = t2.maas3;
                m2.maas4 = t2.maas4;
                m2.sira = t2.sira;
                m2.tarih = t2.tarih;
                m2.tarih2 = t2.tarih2;

                decimal gunlukMaas = 0;

                double aradakiGunSayisi = (m2.tarih2 - m2.tarih).TotalDays;

                gunlukMaas = m2.maas4 / Convert.ToDecimal(aradakiGunSayisi + 1);

                DateTime tmpBas = m2.tarih;
                DateTime tmpBit = m2.tarih2;

                // Bolum Tam Olarak Aysa...
                if ((m2.tarih.Day == 1) && (m2.tarih.AddDays(1).Month != m2.tarih.Month))
                {

                    var bol = payBolumleri.Find(o => o.basTar <= tmpBas && o.bitTar >= tmpBas);


                }
                else
                {
                    while (tmpBas <= m2.tarih2)
                    {



                        var bol = payBolumleri.Find(o => o.basTar <= tmpBas && o.bitTar >= tmpBas);

                        decimal tazminatSon = 0;
                        if (bol != null)
                        {
                            m2.bolumBitisi = bol.bitTar;
                            double payda = bol.toplamPay;

                            foreach (var t3 in bol.paybolumkisi)
                            {

                                var kisi2 = m2.oAyAlacakKisiler.Find(o => o.Id == t3.kisiId);
                                double pay = t3.payoran;
                                var kisii = Yakinlar.Find(o => o.Id == t3.kisiId);
                                string cinsiyet = kisii.cinsiyet;
                                DateTime dogTar = kisii.dogumTarihiT;
                                DateTime cikisTar = kisii.destekCikisT  ;

                                if (kisi2 == null)
                                {

                                    PayKisi2 k = new PayKisi2();

                                    k.Id = t3.kisiId;

                                    k.destekBit = t3.destekCikisTarihi;

                                    k.isim = t3.isim;

                                    k.payOran = (int)t3.payoran;
                                    k.toplamPay = (int)payda;

                                    decimal katsayi = 1;


                                    if (t2.donemi != "İşlenmiş")
                                    {
                                        katsayi = YasamaIhtimali(cinsiyet, dogTar, Convert.ToDateTime(AktifDestek.raporTarihi), Convert.ToDateTime(t2.tarih), cikisTar);


                                    }

                                    k.alinanMiktar = (gunlukMaas * Convert.ToDecimal(pay)) / Convert.ToDecimal(payda);
                                    k.yakinlik = t3.yakinlik;
                                    if (t3.yakinlik == "Eş")
                                    {
                                        if (t2.aciklama != "1")
                                        {

                                            //      k.alinanMiktar = k.alinanMiktar * (100 - Convert.ToDecimal(EvlenmeIhtimali)) / 100;

                                        }
                                    }

                                    k.alinanMiktar = k.alinanMiktar * katsayi;
                                    k.donem = t2.aciklama;
                                    m2.tazminatMiktar = m2.tazminatMiktar + k.alinanMiktar;
                                    //tazminatSon = tazminatSon + k.alinanMiktar;
                                    m2.oAyAlacakKisiler.Add(k);

                                }
                                else
                                {


                                    decimal katsayi = 1;


                                    if (t2.donemi != "İşlenmiş")
                                    {
                                        katsayi = YasamaIhtimali(cinsiyet, dogTar, Convert.ToDateTime(AktifDestek.raporTarihi), Convert.ToDateTime(t2.tarih), cikisTar);


                                    }
                                    Decimal Mikk = (gunlukMaas * Convert.ToDecimal(pay)) / Convert.ToDecimal(payda);


                                    if (t3.yakinlik == "Eş")
                                    {
                                        if (t2.aciklama != "1")
                                        {


                                            //          Mikk = Mikk * (100 - Convert.ToDecimal(EvlenmeIhtimali)) / 100;


                                        }
                                    }

                                    Mikk = Mikk * katsayi;
                                    m2.tazminatMiktar = m2.tazminatMiktar;
                                    kisi2.donem = t2.aciklama;

                                    kisi2.yasamaSansi = katsayi;

                                    kisi2.alinanMiktar = kisi2.alinanMiktar + Mikk;
                                }





                            }
                        }
                        tmpBas = tmpBas.AddDays(1);


                    }
                }



                //m2.maas5 = tazminatSon;


                maasListesi2.Add(m2);

            }


            var xx = maasListesi2;



            kisiTazminatListesi.Clear();
            foreach (var t2 in Yakinlar)
            {
                KisiTazminatBilgi kk = new KisiTazminatBilgi();
                kk.kisiId = t2.Id;
                kk.Isim = t2.ad;
                decimal kisiToplam = 0;

                decimal kisiIslemisToplam = 0;
                decimal kisiAktifToplam = 0;
                decimal kisiPasifToplam = 0;

                decimal indirimToplam = 0;

                foreach (var t3 in maasListesi2)
                {
                    var kisi = t3.oAyAlacakKisiler.Find(o => o.Id == t2.Id);
                    if (kisi != null)
                    {
                        kisiToplam = kisiToplam + kisi.alinanMiktar;

                        if (t3.aciklama == "1")
                        {
                            kisiIslemisToplam = kisiIslemisToplam + kisi.alinanMiktar;
                        }
                        else if (t3.aciklama == "2")
                        {
                            kisiAktifToplam = kisiAktifToplam + kisi.alinanMiktar;
                        }
                        else if (t3.aciklama == "3")
                        {
                            kisiPasifToplam = kisiPasifToplam + kisi.alinanMiktar;
                        }

                    }

                }

                kk.alinanMiktar = kisiToplam;

                kk.islemisToplam = kisiIslemisToplam;
                kk.aktifToplam = kisiAktifToplam;
                kk.pasifToplam = kisiPasifToplam;

                decimal indirimMiktari = 0;


                if (t2.yakinlik == "Eş")
                {

                    decimal katsayiEvlenme = 0;

                    katsayiEvlenme = Convert.ToDecimal(EvlenmeIhtimali) / 100;

                    indirimMiktari = (kk.aktifToplam + kk.pasifToplam) * katsayiEvlenme;

                    //txtIndirim.Text = String.Format("{0:C2}", indirimMiktari);


                }

                kk.indrim = indirimMiktari;
                kk.alinanMiktar = kk.alinanMiktar - indirimMiktari;

                kisiTazminatListesi.Add(kk);




            }



            //foreach (var jj in grdKisiPayOran.Rows)
            //{

            //    string idd2 = jj.Cells["Id"].Value.ToString();

            //    var kisii = kisiTazminatListesi.Find(o => o.kisiId == idd2);

            //    jj.Cells["Miktar"].Value = kisii.alinanMiktar;

            //}


            decimal islenmisToplam = 0;
            decimal aktifToplam = 0;
            decimal pasifToplam = 0;
            decimal genelToplam = 0;

            foreach (var t in maasListesi2)
            {
                foreach (var t2 in t.oAyAlacakKisiler)
                {
                    if (t2.donem == "1")
                    {
                        islenmisToplam = islenmisToplam + t2.alinanMiktar;

                    }
                    else if (t2.donem == "2")
                    {
                        aktifToplam = aktifToplam + t2.alinanMiktar;
                    }
                    else if (t2.donem == "3")
                    {
                        pasifToplam = pasifToplam + t2.alinanMiktar;
                    }

                }

            }
            genelToplam = islenmisToplam + aktifToplam + pasifToplam;

            //lblGenelToplam.Text = String.Format("{0:C2}", genelToplam);
            //lblAktifToplam.Text = String.Format("{0:C2}", aktifToplam);
            //lblIslenmisToplam.Text = String.Format("{0:C2}", islenmisToplam);
            //lblPasifToplam.Text = String.Format("{0:C2}", pasifToplam);

            //lblMasrafToplam.Text = String.Format("{0:C2}", txtTopMasraf.Text);

            //txtTazminat1.Text = String.Format("{0:C2}", islenmisToplam);
            //txtTazminatAktif.Text = String.Format("{0:C2}", aktifToplam);
            //txtTazminatPasif.Text = String.Format("{0:C2}", pasifToplam);





        }





        private int kisininOTarihtekiYasi(DateTime dogumTarihi, DateTime tarih, bool yuvarla)
        {
            int yass1 = 0;



            if (yuvarla == false)
            {

                DateTime tmpDogum = dogumTarihi;
                int yil = 0;
                while (tarih > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                yass1 = yil;

            }
            else if (yuvarla == true)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = dogumTarihi;
                int yil = 0;
                while (tarih > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= tarih)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                yass1 = yil;
            }

            return yass1;


        }


        private decimal YasamaIhtimali(string cinsiyet, DateTime dogumTar, DateTime raporTarihi, DateTime hesapTarihi, DateTime cikisTarihi)
        {


            int raporTarihindekiYas = kisininOTarihtekiYasi(dogumTar, raporTarihi, false);


            int cikisTarihindekiYas = kisininOTarihtekiYasi(dogumTar, cikisTarihi.AddDays(1), false);

            int _hesapTarihindekiYas = kisininOTarihtekiYasi(dogumTar, hesapTarihi, false);



            List<AktuerYasamaSans> listeYasamaSansi = new List<AktuerYasamaSans>();

            int yasAktifBas1 = Convert.ToInt32(raporTarihindekiYas);
            int yasAktifYasBitis1 = Convert.ToInt32(cikisTarihindekiYas + 2);


            int tmpAktifBas1 = yasAktifBas1;
            int tmpAktifBit1 = yasAktifYasBitis1;

            int sayacc = 0;
            listeYasamaSansi.Clear();
            for (int i = 0; tmpAktifBas1 < tmpAktifBit1; tmpAktifBas1++)
            {

                sayacc = sayacc + 1;
                if (cinsiyet == "Kadın")
                {
                    

                    AktuerYasamaSans aa = new AktuerYasamaSans();
                    var kayit1A = trhListe.Find(o => o.Id == yasAktifBas1);
                    var kayit2A = trhListe.Find(o => o.Id == tmpAktifBas1);
                    
                    decimal nx1A = Convert.ToDecimal(kayit1A.NxKadin);
                    decimal dx1A = Convert.ToDecimal(kayit1A.DxKadin);
                    decimal nx2A = Convert.ToDecimal(kayit2A.NxKadin);

                    decimal hes1A = ((nx1A - nx2A) / dx1A);

                    AktuerYasamaSans aa2 = new AktuerYasamaSans();

                    aa2.oran = hes1A;
                    aa2.sira = sayacc;
                    aa2.yas = tmpAktifBas1;
                    listeYasamaSansi.Add(aa2);





                }

                else
                {

                    var kayit1A = trhListe.Find(o => o.Id == yasAktifBas1);
                    var kayit2A = trhListe.Find(o => o.Id == tmpAktifBas1);
                    decimal nx1A = Convert.ToDecimal(kayit1A.NxErkek);
                    decimal dx1A = Convert.ToDecimal(kayit1A.DXErkek);
                    decimal nx2A = Convert.ToDecimal(kayit2A.NxErkek);

                    decimal hes1A = ((nx1A - nx2A) / dx1A);

                    AktuerYasamaSans aa = new AktuerYasamaSans();

                    aa.oran = hes1A;
                    aa.sira = sayacc;
                    aa.yas = tmpAktifBas1;
                    listeYasamaSansi.Add(aa);
                }





            }


            decimal deger1 = 0;
            decimal deger2 = 0;

            var ilkYas = listeYasamaSansi.Find(o => o.yas == _hesapTarihindekiYas);
            if (ilkYas != null)
            {
                deger1 = ilkYas.oran;
            }

            var ilkYas2 = listeYasamaSansi.Find(o => o.yas == _hesapTarihindekiYas + 1);
            if (ilkYas2 != null)
            {
                deger2 = ilkYas2.oran;
            }

            return deger2 - deger1;

        }

        private class AktuerYasamaSans
        {
            public int sira { get; set; }
            public int yas { get; set; }
            public decimal oran { get; set; }

        }
        private class KisiToplamlariDonemsel
        {
            public decimal islemisKisiToplam;
            public decimal AktifKisiToplam;
            public decimal EmekliKisiToplam;
        }
    }
    //----------------------------------------------------------------------------

        public class YakinSureHesapBilgi
    {
        public int destekSuresi { get; set; }
        public DateTime destekBitis;
        public int olayTarihiYas;
        public int mevcutYas;
    }

    public class Yakin2
    {
        public string Id { get; set; }
        public string yakinlik { get; set; }
        public string ad { get; set; }
        public string cinsiyet { get; set; }
        public string dogumTarihi { get; set; }
        public string destekSuresi { get; set; }
        public string destekCikis { get; set; }
        public DateTime dogumTarihiT { get; set; }
        public DateTime destekCikisT { get; set; }
        public int destekSuresiT { get; set; }
        public double tamOran { get; set; }
        public int okulBitirmeYas { get; set; }

        public int calisma { get; set; }


    }

    public class DestekPayBolumleri
    {
        public List<PayBolumleriKisi> paybolumkisi { get; set; }

        public DestekPayBolumleri()
        {
            paybolumkisi = new List<PayBolumleriKisi>();

        }

        public int sure { get; set; }
        public int bas { get; set; }
        public int bitis { get; set; }
        public double toplamPay { get; set; }

        public DateTime basTar { get; set; }
        public DateTime bitTar { get; set; }

        public string kisiBilgi { get; set; }

    }


    public class PayBolumleriKisi

    {
        public string isim { get; set; }
        public string yakinlik { get; set; }
        public double payoran { get; set; }
        public double sure { get; set; }
        public int bas { get; set; }
        public int bitis { get; set; }
        public string kisiId { get; set; }
        public double tamOran { get; set; }
        public double toplamPay { get; set; }
        public DateTime destekCikisTarihi { get; set; }

        public decimal bolumToplami { get; set; }

        public DateTime dogumTarihi;
    }

    public class bolum

    {
        public string aciklama { get; set; }
        public DateTime baslangic { get; set; }
        public int tur { get; set; }
    }


    public class GunlukBilgi
    {

        public GunlukBilgi()
        {
            kisiListe = new List<KisiTazminatBilgi>();
        }
        public int sira { get; set; }

        public DateTime tarih { get; set; }

        public decimal gunlukUcret { get; set; }

        public decimal gunlukTazminat { get; set; }

        public string donemi { get; set; }

        public string aciklama { get; set; }

        public decimal trafikYasamKatsayi { get; set; }

        public decimal miktar { get; set; }

        public List<KisiTazminatBilgi> kisiListe { get; set; }


    }

    public class MaasAy2
    {
        public MaasAy2()
        {

            oAyAlacakKisiler = new List<PayKisi2>();
        }

        public decimal maas { get; set; }

        public decimal maas2 { get; set; }

        public decimal maas3 { get; set; }

        public decimal maas4 { get; set; }

        public decimal maas5 { get; set; }

        public DateTime tarih { get; set; }

        public DateTime tarih2 { get; set; }

        public string donemi { get; set; }

        public string aciklama { get; set; }

        public List<PayKisi2> oAyAlacakKisiler { get; set; }

        public DateTime bolumBitisi { get; set; }

        public int sira { get; set; }


        public decimal tazminatMiktar { get; set; }


    }
  
    public class PayKisi2
    {
        public string isim { get; set; }
        public string Id { get; set; }
        public DateTime destekBit { get; set; }
        public Decimal alinanMiktar { get; set; }
        public int payOran { get; set; }
        public int toplamPay { get; set; }
        public string donem { get; set; }
        public string yakinlik { get; set; }

        public decimal yasamaSansi { get; set; }


    }

    public class MaasAy
    {

        public decimal maas { get; set; }

        public decimal maas2 { get; set; }

        public decimal maas3 { get; set; }

        public decimal maas4 { get; set; }

        public decimal kayipOran { get; set; }

        public DateTime tarih { get; set; }

        public DateTime tarih2 { get; set; }

        public string donemi { get; set; }

        public string aciklama { get; set; }
        public int sira { get; set; }

    }
   
    public class KisiTazminatBilgi
    {

        public string kisiId { get; set; }
        public string Isim { get; set; }
        public decimal alinanMiktar { get; set; }


        public decimal islemisToplam { get; set; }
        public decimal aktifToplam { get; set; }
        public decimal pasifToplam { get; set; }

        public decimal indrim { get; set; }

        public decimal destekBit { get; set; }
        public string donemi { get; set; }
        public string aciklama { get; set; }
        public int sira { get; set; }

    }
}


