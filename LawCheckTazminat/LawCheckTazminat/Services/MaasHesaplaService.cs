using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Services
{
    public class MaasHesaplaService
    {

        public Decimal AsgariGecimHespla(decimal ucretBrut, int esCalisiyor, int cocukSayisi)
        {

            Decimal sonuc = 0;
            double carpimYuzde = 50;

            if (esCalisiyor == (-1))
            {



            }
            else if (esCalisiyor == 0)
            {
                carpimYuzde = carpimYuzde + 10;


            }
            else if (esCalisiyor == 1)
            {

            }

            // Çocuk Sayısı
            if (cocukSayisi == 1)
            {
                carpimYuzde = carpimYuzde + 7.5;
            }
            else if (cocukSayisi == 2)
            {
                carpimYuzde = carpimYuzde + 15;

            }
            else if (cocukSayisi == 3)
            {
                carpimYuzde = carpimYuzde + 25;

            }
            else if (cocukSayisi > 3)
            {

                carpimYuzde = carpimYuzde + 25;
                int say2 = cocukSayisi - 3;
                double deger = 5 * say2;
                carpimYuzde = carpimYuzde + deger;

            }

            if (carpimYuzde > 85)
            {
                carpimYuzde = 85;
            }


            Decimal toplamBrut = 12 * ucretBrut;

            Decimal toplamBrut2 = toplamBrut * Convert.ToDecimal(carpimYuzde / 100);

            toplamBrut2 = toplamBrut2 * Convert.ToDecimal(0.15) / 12;


            return toplamBrut2;



        }

        public Decimal BruttenNetHesapla(decimal mBrut, decimal agii)
        {


            decimal nett = 0;

            decimal sgk = 0;
            decimal issizlik = 0;
            decimal verg = 0;
            decimal dmga = 0;


            sgk = mBrut * Convert.ToDecimal(0.14);
            issizlik = mBrut * Convert.ToDecimal(0.01);

            issizlik = Math.Round(issizlik, 2);

            verg = (mBrut - sgk - issizlik) * Convert.ToDecimal(0.15);


            //verg = (tmp1 - sgk - issizlik) * Convert.ToDecimal(0.15);
            dmga = mBrut * Convert.ToDecimal(0.00759);



            nett = mBrut - sgk - verg - dmga - issizlik;

            nett = nett + agii;
            return nett;
        }

    }
}
