using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class Basamak4YYiewModel: ViewModelBase
    {
        public List<YYResmiTatil> ResmiTatilListe = new List<YYResmiTatil>();
        public List<YYHaftaIzni> HaftaIzniListe = new List<YYHaftaIzni>();

        List<ResmiTatiller> TumResmiTatiller = new List<ResmiTatiller>();

        List<YYYilBilgi> CalisilanYilListesi = new List<YYYilBilgi>();

        List<YYYilBilgi2> HesapYilListesi = new List<YYYilBilgi2>();

        public Services.ResmiTatilService islemResmiTatil = new Services.ResmiTatilService();

        public Basamak4YYiewModel(YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;

           TumResmiTatiller = islemResmiTatil.GetItemss();
            YYCalisilanYillariHesapla();

            YYRemiTatilleriBul();

            YYHaftaIzniBul();

            CakismaKontrol();

        //    NetYillariHesapla();

            NetYillariHesapla2();

        }

        private double _hakEdilen;
        public double HakEdilen
        {
            get => _hakEdilen;
            set
            {
                _hakEdilen = value;
                OnPropertyChanged();
            }
        }

        private double _brutToplam;
        public double BrutToplam
        {
            get => _brutToplam;
            set
            {
                _brutToplam = value;
                OnPropertyChanged();

            }
        }

        private double _resmiTatilToplam;
        public double ResmiTatilToplam
        {
            get => _resmiTatilToplam;
            set
            {
                _resmiTatilToplam = value;
                OnPropertyChanged();

            }
        }

        private double _cakisanGunToplam;
        public double CakisanGunToplam
        {
            get => _cakisanGunToplam;
            set
            {
                _cakisanGunToplam = value;
                OnPropertyChanged();
            }
        }

        private double _haftalikIzinToplam;
        public double HaftalikIzinToplam
        {
            get => _haftalikIzinToplam;
            set
            {
                _haftalikIzinToplam = value;
                OnPropertyChanged();

            }
        }

        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }



        //--------


        private void YYCalisilanYillariHesapla()
        {

            int calisilanYillar = -1;
            int mevcutYil = 0;
            int yilIzinSay;

            DateTime tmpBasTar = YY.IseGirisTarihi;


            
            while(tmpBasTar<= YY.HesapBitisTar)
            {

                calisilanYillar = calisilanYillar + 1;
                YYYilBilgi yyb = new YYYilBilgi();
                yyb.Id = Guid.NewGuid().ToString().Substring(0, 8);
                yyb.BasTarih = tmpBasTar;
                yyb.BitTarih = tmpBasTar.AddYears(1);
                yyb.BitTarih = yyb.BitTarih.AddDays(-1);
                yyb.YilSay = calisilanYillar;
                yyb.GunSay = TatilGunuBul(calisilanYillar);

                CalisilanYilListesi.Add(yyb);

                tmpBasTar = tmpBasTar.AddYears(1);

            }



        }

        private int TatilGunuBul(int yill)
        {
            int izinHak = 0;
            if (yill >= 1 && yill <= 6)
            {
                izinHak = 14;
            }
            if (yill > 6 && yill <= 16)
            {
                izinHak = 20;
            }
            if (yill > 16)
            {
                izinHak = 26;
            }

            return izinHak;
        }

        private void YYRemiTatilleriBul()
        {
            foreach (var t in YY.IzinGunleriBilgi)
            {
                var durum = TumResmiTatiller.Find(o => o.tarih.Date == t.Tarih.Date);
                if (durum != null)
                {
                    YYResmiTatil yyr = new YYResmiTatil();
                    yyr.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    yyr.Aciklama = durum.aciklama;
                    yyr.Tarih = durum.tarih;
                    if (t.Tarih >= YY.HesapBaslangicTar && t.Tarih <= YY.HesapBitisTar)
                    {
                        ResmiTatilListe.Add(yyr);
                    }
                }
            }

            ResmiTatilToplam = ResmiTatilListe.Count;

            YY.IzindekiResmiTatillerBilgi = ResmiTatilListe.ToObservableCollection();

        }

        private void YYHaftaIzniBul()
        {
            DayOfWeek _aa;
            if (YY.izinGunu == "Pazartersi")
            {
                _aa = DayOfWeek.Monday;
            }
            else if (YY.izinGunu == "Salı")
            {
                _aa = DayOfWeek.Tuesday;
            }
            else if (YY.izinGunu == "Çarşamba")
            {
                _aa = DayOfWeek.Wednesday;
            }
            else if (YY.izinGunu == "Perşembe")
            {
                _aa = DayOfWeek.Thursday;
            }
            else if (YY.izinGunu == "Cuma")
            {
                _aa = DayOfWeek.Friday;
            }
            else if (YY.izinGunu == "Cumartesi")
            {
                _aa = DayOfWeek.Saturday;
            }
            else if (YY.izinGunu == "Pazar")
            {
                _aa = DayOfWeek.Sunday;
            }
            else
            {
                _aa = DayOfWeek.Sunday;
            }


            foreach (var t in YY.IzinGunleriBilgi)
            {
                if (t.Tarih.DayOfWeek == _aa)
                {

                    YYHaftaIzni yyh = new YYHaftaIzni();
                    yyh.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    yyh.Tarih = t.Tarih;
                    yyh.Aciklama = YY.izinGunu;
                    if (t.Tarih >= YY.HesapBaslangicTar && t.Tarih <= YY.HesapBitisTar)
                    {

                        HaftaIzniListe.Add(yyh);

                    }
                }

            }

            HaftalikIzinToplam = HaftaIzniListe.Count;

            YY.IzindekiHaftaIzniBilgi = HaftaIzniListe.ToObservableCollection();

        }



        private void CakismaKontrol()
        {
            int _adet = 0;
            foreach( var t in ResmiTatilListe)
            {
                foreach( var t2 in  HaftaIzniListe)
                {

                    if(t.Tarih.Date == t2.Tarih.Date)
                    {
                        _adet += 1;
                    }
                }
            }

            CakisanGunToplam = (-1) * _adet;
        }

        private void NetYillariHesapla()
        {
            DateTime tmpBasTar2 = YY.HesapBitisTar.AddYears(-5);

            DateTime tmpBastar = new DateTime(tmpBasTar2.Year, YY.IseGirisTarihi.Month, YY.IseGirisTarihi.Day);


            List<YYYilBilgi> Liste2 = new List<YYYilBilgi>();


            if (CalisilanYilListesi.Count < 6)
            {
                foreach (var t in CalisilanYilListesi)
                {
                    Liste2.Add(t);
                }
            }
            else
            {

                var sayi = CalisilanYilListesi.Count;
                Liste2.Add(CalisilanYilListesi[sayi - 6]);
                Liste2.Add(CalisilanYilListesi[sayi - 5]);
                Liste2.Add(CalisilanYilListesi[sayi - 4]);
                Liste2.Add(CalisilanYilListesi[sayi - 3]);
                Liste2.Add(CalisilanYilListesi[sayi - 2]);
                Liste2.Add(CalisilanYilListesi[sayi - 1]);


            }


            foreach (var t2 in Liste2)
            {
                int tatilSay = TatilGunuBul(t2.YilSay);
                t2.GunSay = tatilSay;
            }


            DateTime tmpHesapBaslangici;

            if (Liste2.Count > 0)
            {
                tmpHesapBaslangici = Liste2[0].BasTarih;
                this.YY.HesapBaslangicTar = tmpHesapBaslangici;
            }


            int sayy = 0;

            foreach (var t5 in Liste2)
            {
                YYYilBilgi2 y2 = new YYYilBilgi2();
                y2.BasTarih = t5.BasTarih;
                y2.BitTarih = t5.BitTarih;
                y2.GunSay = t5.GunSay;
                y2.Id = t5.Id;
                y2.YilSay = t5.YilSay;
                y2.YYId = t5.YYId;
                sayy = sayy + t5.YilSay;
                HesapYilListesi.Add(y2);

            }


            //this.HakEdilen = tmpHakEdilen;
            //this.YY.Gun = this.HakEdilen;

            this.YY.CalisilanYillarBilgi = CalisilanYilListesi.ToObservableCollection();

            this.YY.HesapYillariBilgi = HesapYilListesi.ToObservableCollection();

            var tmpsay2 = 0;
            foreach (var tt in YY.HesapYillariBilgi)
            {
                tmpsay2 = tmpsay2 + tt.GunSay;
            }

            // Eğer İzin kullanacağı Zamanı kalmışsa kontrolü Yapılacak Ara-Sürümde..
            tmpsay2 =tmpsay2 - (YY.HesapYillariBilgi[YY.HesapYillariBilgi.Count - 1].GunSay);

            YY.Gun = tmpsay2;
            double dusulecekGunlerToplami = 0;
            YY.ResmiTatilSayi = 0;
            foreach(var t in YY.IzindekiResmiTatillerBilgi)
            {
                if(t.Tarih>=YY.HesapBaslangicTar)
                {
                    YY.ResmiTatilSayi = YY.ResmiTatilSayi + 1;
                }
            }
            YY.HaftalikIzinSayi = 0;
            foreach(var t2 in YY.IzindekiHaftaIzniBilgi)
            {
                if(t2.Tarih>= YY.HesapBaslangicTar)
                {
                    YY.HaftalikIzinSayi = YY.HaftalikIzinSayi + 1;
                }
            }

            if(YY.raporlarinYillikIzindenDusme==true)
            {
                dusulecekGunlerToplami = dusulecekGunlerToplami + YY.izindeAlinanRaporSayisi;
            }

            if(YY.haftalikIizinYillikIzindenDusme==true)
            {
                dusulecekGunlerToplami = dusulecekGunlerToplami + YY.HaftalikIzinSayi;
             }
            HaftalikIzinToplam = YY.IzindekiHaftaIzniBilgi.Count;

            if (YY.resmiTatilYillikIzindenDusme==true)
            {
                dusulecekGunlerToplami = dusulecekGunlerToplami + YY.ResmiTatilSayi;
            }
            ResmiTatilToplam = YY.IzindekiResmiTatillerBilgi.Count;

            int kullanilanIzinSayisi = 0;
            foreach(var t in YY.IzinGunleriBilgi)
            {
                if(t.Tarih>= YY.HesapBaslangicTar)
                {
                    kullanilanIzinSayisi = kullanilanIzinSayisi + 1;
                }
            }
            this.YY.Kullanilan = kullanilanIzinSayisi;


            double sonDusme = YY.Kullanilan - dusulecekGunlerToplami;


            if(sonDusme < 0)
            {
                sonDusme = 0;
            }

            YY.Gun2 = YY.Gun - sonDusme;


        }



        private void NetYillariHesapla2()
        {

            HesapYilListesi.Clear();
            DateTime tmpBasTar = YY.HesapBaslangicTar.Date;
            DateTime tmpBitisTar = YY.HesapBitisTar.Date.AddYears(-1);
            int ii = 0;
            while(tmpBasTar<= tmpBitisTar)
            {
                ii = ii + 1;
                YYYilBilgi2 y2 = new YYYilBilgi2();
                y2.BasTarih = tmpBasTar;
                y2.BitTarih = tmpBasTar.AddYears(1);
                y2.BitTarih = y2.BitTarih.AddDays(-1);


                var calismaYili = CalisilanYilListesi.Find(o => o.BasTarih.Date <= tmpBasTar && tmpBasTar <= o.BitTarih.Date);
                y2.GunSay = calismaYili.GunSay;

                if(y2.GunSay<20)
                {
                    if(YY.DogumTarihi.AddYears(18)> y2.BasTarih)
                    {
                        y2.GunSay = 20;
                    }
                    if(y2.BasTarih.AddYears(-50) >= YY.DogumTarihi)
                    {
                        y2.GunSay = 20;
                    }
                }

                if(YY.YerAltiCalisani== true)
                {
                    y2.GunSay += 4;
                }

                y2.Id = Guid.NewGuid().ToString().Substring(0, 16);
                y2.YilSay = calismaYili.YilSay;
               // y2.YYId = t5.YYId;
               // sayy = sayy + t5.YilSay;
                HesapYilListesi.Add(y2);


               tmpBasTar=  tmpBasTar.AddYears(1);

            }

            int sayii = 0;
            YY.HesapYillariBilgi.Clear();
            foreach(var t in HesapYilListesi)
            {
                sayii += t.GunSay;

                YY.HesapYillariBilgi.Add(t);


            }

            YY.Gun = sayii;

            YY.Gun2 = (sayii - YY.Kullanilan) + (HaftalikIzinToplam + ResmiTatilToplam + CakisanGunToplam);

        }




    private bool SayfaKontrol()
        {
            bool durum = false;


            return durum;
        }


        // İlerleme

        public ICommand IlerleCommand => new Command(OnIlerle);
        public async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //----- İlerleme....

            var sayfa = new Basamak5YYView(YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            this.HataMesaji = "";
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
