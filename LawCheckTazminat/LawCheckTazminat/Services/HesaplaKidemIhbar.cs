using System;
namespace LawCheckTazminat.Services
{
    public class HesaplaKidemIhbar
    {
        Services.HesaplaVergi islemVergi = new HesaplaVergi();

        public KıdemTazYapi KidemHesapla(DateTime baslangic2, DateTime cikis2,
            Decimal brutUcret, Double damgaVergi)
        {

            KıdemTazYapi sonucc = new KıdemTazYapi();
            sonucc.brutCarpilmisToplam = 0;
            sonucc.damgaVergiOdenecek = 0;

            sonucc.netKıdemTazminati = 0;

            sonucc.yil = 0;
            sonucc.ay = 0;
            sonucc.gun = 0;

            if (baslangic2 > cikis2)

            {
                return sonucc;
            }

            DateTime baslangic = new DateTime(baslangic2.Year, baslangic2.Month, baslangic2.Day, 0, 0, 0);
            DateTime cikis = new DateTime(cikis2.Year, cikis2.Month, cikis2.Day, 0, 0, 0);




            bool ozelAySonuDurum = false;
            if (baslangic.Day == 1)
            {
                if (cikis.AddDays(1).Month != cikis.Month)
                {
                    ozelAySonuDurum = true;
                }

                if (cikis.Day == 30)
                {

                    ozelAySonuDurum = true;
                }
            }

            Decimal gunToplami = 0;
            Decimal ayToplamı = 0;
            Decimal yillikCarpim = 0;


            int hesaplancakGunSayisi = 0;

            var fark = (cikis - baslangic);

            //Hesaplama Bitis

            int zz = fark.Days;

            hesaplancakGunSayisi = zz;


            Decimal aa;
            int tamyil = 0;
            int artikGun = 0;

            int ii = 0;
            Decimal brutKıdemTazminati;

            //tamyıl Hesapla

            DateTime bass2;
            bass2 = baslangic;
            while (cikis.AddDays(1) >= bass2)
            {
                tamyil = tamyil + 1;
                bass2 = bass2.AddYears(1);

            }

            tamyil = tamyil - 1;

            if (tamyil == 0)
            {
                return sonucc;
            }

            //artıkay hesapla
            DateTime dd;
            dd = baslangic.AddYears(tamyil);



            DateTime tmpTar = dd;
            while (tmpTar < cikis)
            {
                ii = ii + 1;
                tmpTar = tmpTar.AddMonths(1);
            }


            if (ozelAySonuDurum == true)
            {

                ii = ii + 1;
            }


            dd = dd.AddMonths(ii - 1);



            bool ayOzelDurum = false;

            if (dd.AddMonths(1) == cikis.AddDays(1))
            {
                ayOzelDurum = true;

            }
            if (ayOzelDurum == true)
            {
                ii = ii + 1;

            }


            //artıkgun hesapla

            DateTime ttx = dd;
            if (ozelAySonuDurum == false && ayOzelDurum == false)
            {
                //Ay sonu 30dan farklı olma durumunda ve sonrakiaya geçilecekse

                bool aysonu31 = false;
                bool subat28 = false;
                bool subat29 = false;




                while (dd < cikis)
                {
                    if (dd.Day != 31)
                    {
                        artikGun = artikGun + 1;

                    }
                    dd = dd.AddDays(1);

                }

                if (ttx.Month == 2)
                {
                    if (ttx.Day >= 28)
                    {
                        DateTime x1 = new DateTime(ttx.Year, ttx.Month + 1, 1, 0, 0, 0);

                        DateTime x2 = x1.AddDays(-1);
                        if (x2.Day == 28)
                        {
                            artikGun = artikGun + 2;

                        }
                        if (x2.Day == 29)
                        {
                            artikGun = artikGun + 1;

                        }

                    }

                }

                artikGun = artikGun + 1;



            }


            ii = ii - 1;


            if (baslangic.Day == cikis.Day)
            {
                ii = ii + 1;
                artikGun = 1;
            }


            yillikCarpim = brutUcret * tamyil;
            ayToplamı = (brutUcret / 12) * (ii);
            gunToplami = (brutUcret / 365) * artikGun;

            //KıdemTazYapi sonucc = new KıdemTazYapi();

            //bitiş

            brutKıdemTazminati = yillikCarpim + gunToplami + ayToplamı;

            sonucc.brutCarpilmisToplam = brutKıdemTazminati;
            sonucc.damgaVergiOdenecek = (brutKıdemTazminati * Convert.ToDecimal(damgaVergi));

            sonucc.netKıdemTazminati = brutKıdemTazminati - sonucc.damgaVergiOdenecek;

            sonucc.yil = tamyil;
            sonucc.ay = ii;
            sonucc.gun = artikGun;

            return sonucc;
        }

        public IhbarTazYapi IhbarHesapla(DateTime baslangic2, DateTime cikis2, Decimal brutUcret, Double damgaVergi,
                   Double gelirVergi, int vergiYil, Decimal vergiMatrah, Decimal ekGelir)
        {
            //hafta sayısı hesapla
            IhbarTazYapi sonucc = new IhbarTazYapi();
            sonucc.brutIhbarTazminati = 0;
            sonucc.damgaVergiOdenecek = 0;
            sonucc.gelirVergiOdenecek = 0;
            sonucc.haftaSayisi = 0;
            sonucc.netIhbarTazminati = 0;

            if (baslangic2 > cikis2)
            {
                return sonucc;
            }
            int hesaplancakHaftaSayisi = 0;

          

            DateTime baslangic = new DateTime(baslangic2.Year, baslangic2.Month, baslangic2.Day, 0, 0, 0);
            DateTime cikis = new DateTime(cikis2.Year, cikis2.Month, cikis2.Day, 0, 0, 0);

            var fark = (cikis - baslangic);


            DateTime tar1 = baslangic.AddMonths(6);
            DateTime tar2 = baslangic.AddMonths(18);
            DateTime tar3 = baslangic.AddMonths(36);
            tar1 = tar1.AddDays(-1);
            tar2 = tar2.AddDays(-1);
            tar3 = tar3.AddDays(-1);

            if (tar1 > cikis)
            {
                hesaplancakHaftaSayisi = 2;

                if (baslangic.Day == 1)
                {

                    if (baslangic.Month == 2)
                    {

                        if (baslangic.AddMonths(6).AddDays(-1) == cikis)
                        {
                            hesaplancakHaftaSayisi = 4;
                        }
                    }
                    else
                    {
                        if (cikis.Day == 30)

                        {
                            hesaplancakHaftaSayisi = 4;
                        }

                        if (cikis.Day == 31)
                        {
                            hesaplancakHaftaSayisi = 4;
                        }
                    }

                }


            }

            if (tar1 <= cikis && tar2 > cikis)
            {
                hesaplancakHaftaSayisi = 4;

                if (baslangic.Day == 1)
                {


                    if (baslangic.Month == 2)
                    {

                        if (baslangic.AddMonths(18).AddDays(-1) == cikis)
                        {
                            hesaplancakHaftaSayisi = 6;
                        }


                    }
                    else
                    {
                        if (cikis.Day == 30)

                        {
                            hesaplancakHaftaSayisi = 6;
                        }

                        if (cikis.Day == 31)
                        {
                            hesaplancakHaftaSayisi = 6;
                        }
                    }
                }

            }

            if (tar2 <= cikis && tar3 >= cikis)
            {
                hesaplancakHaftaSayisi = 6;

                if (baslangic.Day == 1)
                {

                    if (baslangic.Month == 2)
                    {

                        if (baslangic.AddMonths(36).AddDays(-1) == cikis)
                        {
                            hesaplancakHaftaSayisi = 8;
                        }


                    }
                    else
                    {
                        if (cikis.Day == 30)

                        {
                            hesaplancakHaftaSayisi = 8;
                        }

                        if (cikis.Day == 31)
                        {
                            hesaplancakHaftaSayisi = 8;
                        }
                    }

                }

            }

            if (tar3 <= cikis)
            {
                hesaplancakHaftaSayisi = 8;
            }

            //Hesaplama Bitis



            Decimal gunlukUcret;
            Decimal haftalikUcret;
            Decimal brutIhbarTazminati;

            Decimal ekGelirAy = ekGelir / 12;

            brutUcret = brutUcret + ekGelirAy;



            gunlukUcret = (brutUcret / 30);

            haftalikUcret = (gunlukUcret * 7);

            brutIhbarTazminati = hesaplancakHaftaSayisi * haftalikUcret;

            Decimal vergiOdenecek = 0;
            //(brutIhbarTazminati *Convert.ToDecimal(gelirVergi));

            if (vergiYil >= 2006)
            {

                vergiOdenecek = islemVergi.VergiHesapla(vergiMatrah, brutIhbarTazminati, vergiYil);

                //  vergiOdenecek = VergiHesapla(vergiMatrah, brutIhbarTazminati, vergiYil);
            }

   

            sonucc.gelirVergiOdenecek = vergiOdenecek;

            sonucc.damgaVergiOdenecek = (brutIhbarTazminati * Convert.ToDecimal(damgaVergi));

            sonucc.netIhbarTazminati = brutIhbarTazminati - (sonucc.gelirVergiOdenecek + sonucc.damgaVergiOdenecek);

            sonucc.brutIhbarTazminati = brutIhbarTazminati;

            sonucc.haftaSayisi = hesaplancakHaftaSayisi;

            return sonucc;


        }



    }

    public class KıdemTazYapi
    {

        public Decimal netKıdemTazminati { get; set; }

        public Decimal damgaVergiOdenecek { get; set; }

        public Decimal brutCarpilmisToplam { get; set; }

        public string sure { get; set; }

        public int yil { get; set; }
        public int ay { get; set; }
        public int gun { get; set; }

    }



    public class IhbarTazYapi
    {
        public Decimal netIhbarTazminati { get; set; }

        public Decimal gelirVergiOdenecek { get; set; }

        public Decimal damgaVergiOdenecek { get; set; }

        public Decimal brutIhbarTazminati { get; set; }

        public int haftaSayisi { get; set; }

    }
}
