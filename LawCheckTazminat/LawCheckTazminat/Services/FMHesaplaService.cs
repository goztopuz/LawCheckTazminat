using System;
namespace LawCheckTazminat.Services
{
    public class FMHesaplaService
    {
        public FMHesaplaService()
        {
        }

        public Decimal FMHesapla(double sozlesmeSaat, double calismaSaat, decimal brutUcret)
        {
            decimal sonuc = 0;
            if (sozlesmeSaat == 45)
            {
                // Sözleşme Saati 45 ......

                double saatSay = calismaSaat -sozlesmeSaat;
                if (saatSay < 0)
                {
                    saatSay = 0;
                }

                decimal saatlikUcret = brutUcret / 225 * (decimal)1.5;

                decimal ucretFMHafta = saatlikUcret * (decimal)saatSay;

                sonuc= ucretFMHafta;



            }
            else if (sozlesmeSaat < 45)
            {
                // Sözleşme Saati 45'den Az. Fazla Çalışma Ödeneği.

                double gunlukCalismaSaati = calismaSaat / 6;

                double aylikCalismaSaati = gunlukCalismaSaati * 30;

                decimal saatlikUcret1 = brutUcret / (decimal)aylikCalismaSaati *(decimal)1.25;

                if (calismaSaat > 45)
                {
                    // 1. Bölüm Fazla Çalışma
                    decimal fazlaCalismaMiktar1;

                    double saat1 = 45 - sozlesmeSaat;

                    fazlaCalismaMiktar1 = (decimal)saat1 * saatlikUcret1 ;


                    //2. Bölüm Fazla Mesai

                    decimal fazlaCalimaMiktar2;
                    double saat2 = calismaSaat - 45;

                    decimal saatlikUcret = brutUcret / 225 * (decimal)1.5;

                    fazlaCalimaMiktar2 = (decimal)saat2 * saatlikUcret;

                    sonuc = fazlaCalismaMiktar1 + fazlaCalimaMiktar2;

                }
                else
                {

                    //1. Bölüm Fazla Çalışma

                    decimal fazlaCalismaMiktar1;

                    double saat1 = calismaSaat-sozlesmeSaat;

                    fazlaCalismaMiktar1 = (decimal)saat1 * saatlikUcret1;

                    sonuc = fazlaCalismaMiktar1;


                }






            }


            return sonuc;


        }

        public Decimal HaftalikTatilCalismaHesapla(decimal brutUcret)
        {
            decimal sonuc = 0;

            decimal gunluk = brutUcret / 30;
            sonuc = gunluk * (decimal) 1.5;

            return sonuc;
        }

    }
}
