using System;
namespace LawCheckTazminat.Services
{
    public class HesaplaVergi
    {
        public HesaplaVergi()
        {
        }
        VergiDilimlerService islem1 = new VergiDilimlerService();

        public Decimal VergiHesapla(Decimal vergiMatrah, Decimal tazToplam, int yill)
        {

            var vergiDilimleri = islem1.GetItems();



            if (yill == 0)
            {
                return Convert.ToDecimal(0);
            }
            Decimal vergiSonuc = 0;
            Decimal toplamMiktar = 0;

            toplamMiktar = vergiMatrah + tazToplam;


            var islemYil = vergiDilimleri.Find(o => o.Id == yill);



            if (islemYil == null)
            {
                islemYil = vergiDilimleri.Find(o => o.Id == DateTime.Now.Year);

            }

            // Yeni HesaplamaBurasıııı


            if (toplamMiktar <= islemYil.Vd1Miktar)
            {

                vergiSonuc = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.15)));


            }
            else if (toplamMiktar > islemYil.Vd1Miktar && toplamMiktar <= islemYil.Vd2Miktar)
            {
                // 2. Dilim Kontrolü
                Decimal bol1 = 0;
                Decimal bol2 = 0;

                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vv = 0;



                if (vergiMatrah > islemYil.Vd1Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.2)));

                }
                else
                {
                    bol2 = toplamMiktar - islemYil.Vd1Miktar.Value;
                    bol1 = tazToplam - bol2;


                    Vbol1 = bol1 * (decimal)0.15;
                    Vbol2 = bol2 * (decimal)0.2;

                    Vv = Vbol1 + Vbol2;
                }
                vergiSonuc = Vv;

            }
            else if (toplamMiktar > islemYil.Vd2Miktar && toplamMiktar <= islemYil.Vd3Miktar)
            {
                // 3. Dilim Kontrolü
                Decimal bol1 = 0;
                Decimal bol2 = 0;
                Decimal bol3 = 0;

                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vbol3 = 0;
                Decimal Vv = 0;

                if (vergiMatrah > islemYil.Vd2Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.27)));

                }
                else
                {
                    if (vergiMatrah <= islemYil.Vd1Miktar)
                    {
                        bol1 = islemYil.Vd1Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd2Miktar.Value - islemYil.Vd1Miktar.Value;
                        bol3 = tazToplam - (bol1 + bol2);

                        Vbol1 = bol1 * (decimal)0.15;
                        Vbol2 = bol2 * (decimal)0.20;
                        Vbol3 = bol3 * (decimal)0.27;

                        Vv = Vbol1 + Vbol2 + Vbol3;



                    }
                    else if (vergiMatrah <= islemYil.Vd2Miktar && vergiMatrah > islemYil.Vd1Miktar)
                    {

                        bol1 = islemYil.Vd2Miktar.Value - vergiMatrah;
                        bol2 = tazToplam - bol1;
                        Vbol1 = bol1 * (decimal)0.2;
                        Vbol2 = bol2 * (decimal)0.27;
                        Vv = Vbol1 + Vbol2;

                    }




                }


                vergiSonuc = Vv;


            }
            else if (toplamMiktar > islemYil.Vd3Miktar)
            {


                Decimal bol1 = 0;
                Decimal bol2 = 0;
                Decimal bol3 = 0;
                Decimal bol4 = 0;


                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vbol3 = 0;
                Decimal Vbol4 = 0;


                Decimal Vv = 0;

                if (vergiMatrah > islemYil.Vd3Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.35)));

                }
                else
                {
                    if (vergiMatrah <= islemYil.Vd1Miktar)
                    {
                        bol1 = islemYil.Vd1Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd2Miktar.Value - islemYil.Vd1Miktar.Value;
                        bol3 = (islemYil.Vd3Miktar.Value - islemYil.Vd2Miktar.Value);
                        bol4 = tazToplam - (bol1 + bol2 + bol3);

                        Vbol1 = bol1 * (decimal)0.15;
                        Vbol2 = bol2 * (decimal)0.20;
                        Vbol3 = bol3 * (decimal)0.27;
                        Vbol4 = bol4 * (decimal)0.35;

                        Vv = Vbol1 + Vbol2 + Vbol3 + Vbol4;



                    }
                    else if (vergiMatrah <= islemYil.Vd2Miktar && vergiMatrah > islemYil.Vd1Miktar)
                    {

                        bol1 = islemYil.Vd2Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd3Miktar.Value - islemYil.Vd2Miktar.Value;
                        bol3 = tazToplam - (bol1 + bol2);
                        Vbol1 = bol1 * (decimal)0.2;
                        Vbol2 = bol2 * (decimal)0.27;
                        Vbol3 = bol3 * (decimal)0.35;
                        Vv = Vbol1 + Vbol2 + Vbol3;

                    }
                    else if (vergiMatrah > islemYil.Vd2Miktar)
                    {
                        bol1 = islemYil.Vd3Miktar.Value - vergiMatrah;
                        bol2 = tazToplam - bol1;

                        Vbol1 = bol1 * (decimal)0.27;
                        Vbol2 = bol2 * (decimal)0.35;

                        Vv = Vbol1 + Vbol2;


                    }





                }

                vergiSonuc = Vv;
            }






            //-----------------------------------------------------------------------
            //*******************************************************************************************************************



            // Yeni Vergi Sonu







            return vergiSonuc;

        }


        public Decimal VergiHesaplaMaas(Decimal vergiMatrah, Decimal tazToplam, int yill)
        {

            var vergiDilimleri = islem1.GetItems();



            if (yill == 0)
            {
                return Convert.ToDecimal(0);
            }
            Decimal vergiSonuc = 0;
            Decimal toplamMiktar = 0;

            toplamMiktar = vergiMatrah + tazToplam;


            var islemYil = vergiDilimleri.Find(o => o.Id == yill);



            if (islemYil == null)
            {
                islemYil = vergiDilimleri.Find(o => o.Id == 2021);

            }

            // Yeni HesaplamaBurasıııı


            if (toplamMiktar <= islemYil.Vd1Miktar)
            {

                vergiSonuc = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.15)));


            }
            else if (toplamMiktar > islemYil.Vd1Miktar && toplamMiktar <= islemYil.Vd2Miktar)
            {
                // 2. Dilim Kontrolü
                Decimal bol1 = 0;
                Decimal bol2 = 0;

                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vv = 0;



                if (vergiMatrah > islemYil.Vd1Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.2)));

                }
                else
                {
                    bol2 = toplamMiktar - islemYil.Vd1Miktar.Value;
                    bol1 = tazToplam - bol2;


                    Vbol1 = bol1 * (decimal)0.15;
                    Vbol2 = bol2 * (decimal)0.2;

                    Vv = Vbol1 + Vbol2;
                }
                vergiSonuc = Vv;

            }
            else if (toplamMiktar > islemYil.Vd2Miktar && toplamMiktar <= islemYil.Vd3Miktar)
            {
                // 3. Dilim Kontrolü
                Decimal bol1 = 0;
                Decimal bol2 = 0;
                Decimal bol3 = 0;

                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vbol3 = 0;
                Decimal Vv = 0;

                if (vergiMatrah > islemYil.Vd2Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.27)));

                }
                else
                {
                    if (vergiMatrah <= islemYil.Vd1Miktar)
                    {
                        bol1 = islemYil.Vd1Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd2Miktar.Value - islemYil.Vd1Miktar.Value;
                        bol3 = tazToplam - (bol1 + bol2);

                        Vbol1 = bol1 * (decimal)0.15;
                        Vbol2 = bol2 * (decimal)0.20;
                        Vbol3 = bol3 * (decimal)0.27;

                        Vv = Vbol1 + Vbol2 + Vbol3;



                    }
                    else if (vergiMatrah <= islemYil.Vd2Miktar && vergiMatrah > islemYil.Vd1Miktar)
                    {

                        bol1 = islemYil.Vd2Miktar.Value - vergiMatrah;
                        bol2 = tazToplam - bol1;
                        Vbol1 = bol1 * (decimal)0.2;
                        Vbol2 = bol2 * (decimal)0.27;
                        Vv = Vbol1 + Vbol2;

                    }




                }


                vergiSonuc = Vv;


            }
            else if (toplamMiktar > islemYil.Vd3Miktar)
            {


                Decimal bol1 = 0;
                Decimal bol2 = 0;
                Decimal bol3 = 0;
                Decimal bol4 = 0;


                Decimal Vbol1 = 0;
                Decimal Vbol2 = 0;
                Decimal Vbol3 = 0;
                Decimal Vbol4 = 0;


                Decimal Vv = 0;

                if (vergiMatrah > islemYil.Vd3Miktar)
                {
                    Vv = Convert.ToDecimal(tazToplam * (Convert.ToDecimal(0.35)));

                }
                else
                {
                    if (vergiMatrah <= islemYil.Vd1Miktar)
                    {
                        bol1 = islemYil.Vd1Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd2Miktar.Value - islemYil.Vd1Miktar.Value;
                        bol3 = (islemYil.Vd3Miktar.Value - islemYil.Vd2Miktar.Value);
                        bol4 = tazToplam - (bol1 + bol2 + bol3);

                        Vbol1 = bol1 * (decimal)0.15;
                        Vbol2 = bol2 * (decimal)0.20;
                        Vbol3 = bol3 * (decimal)0.27;
                        Vbol4 = bol4 * (decimal)0.35;

                        Vv = Vbol1 + Vbol2 + Vbol3 + Vbol4;



                    }
                    else if (vergiMatrah <= islemYil.Vd2Miktar && vergiMatrah > islemYil.Vd1Miktar)
                    {

                        bol1 = islemYil.Vd2Miktar.Value - vergiMatrah;
                        bol2 = islemYil.Vd3Miktar.Value - islemYil.Vd2Miktar.Value;
                        bol3 = tazToplam - (bol1 + bol2);
                        Vbol1 = bol1 * (decimal)0.2;
                        Vbol2 = bol2 * (decimal)0.27;
                        Vbol3 = bol3 * (decimal)0.35;
                        Vv = Vbol1 + Vbol2 + Vbol3;

                    }
                    else if (vergiMatrah > islemYil.Vd2Miktar)
                    {
                        bol1 = islemYil.Vd3Miktar.Value - vergiMatrah;
                        bol2 = tazToplam - bol1;

                        Vbol1 = bol1 * (decimal)0.27;
                        Vbol2 = bol2 * (decimal)0.35;

                        Vv = Vbol1 + Vbol2;


                    }





                }

                vergiSonuc = Vv;
            }






            //-----------------------------------------------------------------------
            //*******************************************************************************************************************



            // Yeni Vergi Sonu







            return vergiSonuc;

        }

    }
}
