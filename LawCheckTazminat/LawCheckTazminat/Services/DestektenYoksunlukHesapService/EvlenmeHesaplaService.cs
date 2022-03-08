using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LawCheckTazminat.Services.DestektenYoksunlukHesapService
{
   public  class EvlenmeHesaplaService
    {

        Models.DestektenYoksunluk dosyaa =  new Models.DestektenYoksunluk();

        public EvlenmeHesaplaService(Models.DestektenYoksunluk dy)
        {
            dosyaa = dy;
        }

        private double MoserEsEvlenme(int yas, string cinsiyet)
        {
            double sonuc = 0;

            if (cinsiyet == "Erkek")
            {

                if (yas < 25)
                {
                    sonuc = 90;
                }

                else if (yas < 30 && yas >= 25)
                {
                    sonuc = 85;
                }
                else if (yas < 35 && yas >= 30)
                {
                    sonuc = 50;
                }
                else if (yas < 40 && yas >= 35)
                {
                    sonuc = 30;
                }
                else if (yas >= 40)
                {
                    sonuc = 16;
                }
            }
            else
            {
                if (yas < 25)
                {
                    sonuc = 72;
                }

                else if (yas < 30 && yas >= 25)
                {
                    sonuc = 48;
                }
                else if (yas < 35 && yas >= 30)
                {
                    sonuc = 28;
                }
                else if (yas < 40 && yas >= 35)
                {
                    sonuc = 17;
                }
                else if (yas >= 40)
                {
                    sonuc = 9;
                }
            }
            return sonuc;


        }
        private double StaufferEsEvlenme(int yas, string cinsiyet)
        {
            double sonuc = 0;

            if (cinsiyet == "Erkek")
            {
                if (yas < 20)
                {
                    sonuc = 75;
                }

                else if (yas < 25 && yas >= 20)
                {
                    sonuc = 70;
                }
                else if (yas < 30 && yas >= 25)
                {
                    sonuc = 40;
                }
                else if (yas < 35 && yas >= 30)
                {
                    sonuc = 26;
                }
                else if (yas < 40 && yas >= 35)
                {
                    sonuc = 15;
                }
                else if (yas < 45 && yas >= 40)
                {
                    sonuc = 9;
                }
                else if (yas < 50 && yas >= 45)
                {
                    sonuc = 7;
                }
                else if (yas < 55 && yas >= 50)
                {
                    sonuc = 4;
                }
                else if (yas >= 55)
                {
                    sonuc = 3;
                }
            }
            else
            {
                if (yas < 20)
                {
                    sonuc = 75;
                }

                else if (yas < 25 && yas >= 20)
                {
                    sonuc = 70;
                }
                else if (yas < 30 && yas >= 25)
                {
                    sonuc = 40;
                }
                else if (yas < 35 && yas >= 30)
                {
                    sonuc = 26;
                }
                else if (yas < 40 && yas >= 35)
                {
                    sonuc = 15;
                }
                else if (yas < 45 && yas >= 40)
                {
                    sonuc = 9;
                }
                else if (yas < 50 && yas >= 45)
                {
                    sonuc = 7;
                }
                else if (yas < 55 && yas >= 50)
                {
                    sonuc = 4;
                }
                else if (yas >= 55)
                {
                    sonuc = 3;
                }

            }
            return sonuc;


        }
        private double AYIMEsEvlenme(int yas, string cinsiyet, int cocukSayisi)
        {
            double sonuc = 0;

            if (cinsiyet == "Erkek")
            {
                if (yas <= 20 && yas >= 17)
                {
                    sonuc = 90;
                }

                else if (yas <= 25 && yas >= 21)
                {
                    sonuc = 70;
                }
                else if (yas <= 30 && yas >= 26)
                {
                    sonuc = 48;
                }
                else if (yas <= 35 && yas >= 31)
                {
                    sonuc = 30;
                }
                else if (yas <= 40 && yas >= 36)
                {
                    sonuc = 15;
                }
                else if (yas <= 50 && yas >= 41)
                {
                    sonuc = 4;
                }
                else if (yas <= 55 && yas >= 51)
                {
                    sonuc = 2;
                }

            }
            else
            {
                if (yas <= 20 && yas >= 17)
                {
                    sonuc = 52;
                }

                else if (yas <= 25 && yas >= 21)
                {
                    sonuc = 40;
                }
                else if (yas <= 30 && yas >= 26)
                {
                    sonuc = 27;
                }
                else if (yas <= 35 && yas >= 31)
                {
                    sonuc = 17;
                }
                else if (yas <= 40 && yas >= 36)
                {
                    sonuc = 9;
                }
                else if (yas <= 50 && yas >= 41)
                {
                    sonuc = 2;
                }
                else if (yas <= 55 && yas >= 51)
                {
                    sonuc = 1;
                }
            }

            int dusulecekYuzde = 5 * cocukSayisi;
            sonuc = sonuc - dusulecekYuzde;
            if (sonuc < 0)
            {
                sonuc = 0;
            }
            return sonuc;

        }

        public double EsEvlenmeIhtimalHesapla()
        {
            var esBilgi = dosyaa.DestekYoksunlukYakinlar.Where(o => o.yakinlik == "Eş").FirstOrDefault();

            string tur = "";
            if(esBilgi.esEvlenmeDurumMoser==true)
            {
                tur = tur + "M";
            }
            if (esBilgi.esEvlenmeDurumStaauffer == true)
            {
                tur = tur + "S";
            }
            if (esBilgi.esEvlenmeDurumAyim == true)
            {
                tur = tur + "A";
            }

            DateTime dogumTar;
            dogumTar = esBilgi.dogumTarihi;

            string cinsiyet;
            cinsiyet = esBilgi.cinsiyet;
            int cocukSayisi =0;
            foreach (var t in dosyaa.DestekYoksunlukYakinlar)
            {
                if(t.yakinlik=="Çocuk")
                {
                    cocukSayisi = cocukSayisi +1;
                }
            }
           

            double yuzde = 0;

            //--------------------------------------------------------------------------

            int mevcutYas = 0;

            //


            // Sadece YIL alınacak küsür Yok
            if (dosyaa.yasiYuvarla == 0)
            {

                DateTime tmpDogum = dogumTar;
                int yil = 0;
                while (dosyaa.raporTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                mevcutYas = yil;



            }
            else if (dosyaa.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = dogumTar;
                int yil = 0;
                while (dosyaa.raporTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = dogumTar.AddYears(yil);

                while (tmpDogum2 <= dosyaa.raporTarihi)
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



            if (mevcutYas < 18)
            {
                mevcutYas = 18;
            }

            int yass = mevcutYas; ;



            if (tur == "MSA")
            {
                double deger1 = MoserEsEvlenme(yass, cinsiyet);
                double deger2 = StaufferEsEvlenme(yass, cinsiyet);
                double deger3 = AYIMEsEvlenme(yass, cinsiyet, cocukSayisi);


                yuzde = (deger1 + deger2 + deger3) / 3;


            }
            else if (tur == "MS")
            {
                double deger1 = MoserEsEvlenme(yass, cinsiyet);
                double deger2 = StaufferEsEvlenme(yass, cinsiyet);

                yuzde = (deger1 + deger2) / 2;
            }
            else if (tur == "MA")
            {
                double deger1 = MoserEsEvlenme(yass, cinsiyet);
                double deger3 = AYIMEsEvlenme(yass, cinsiyet, cocukSayisi);

                yuzde = (deger1 + deger3) / 2;
            }
            else if (tur == "SA")

            {
                double deger2 = StaufferEsEvlenme(yass, cinsiyet);
                double deger3 = AYIMEsEvlenme(yass, cinsiyet, cocukSayisi);


                yuzde = (deger2 + deger3) / 2;
            }
            else if (tur == "M")
            {
                double deger1 = MoserEsEvlenme(yass, cinsiyet);

                yuzde = deger1;
            }
            else if (tur == "A")
            {
                double deger3 = AYIMEsEvlenme(yass, cinsiyet, cocukSayisi);
                yuzde = deger3;

            }
            else if (tur == "S")
            {
                double deger2 = StaufferEsEvlenme(yass, cinsiyet);
                yuzde = deger2;

            }
            else
            {
                double deger3 = AYIMEsEvlenme(yass, cinsiyet, cocukSayisi);
                yuzde = deger3;

            }

            return yuzde;

        }

    }
}




