using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.NetBrut
{
    public class NetBrut1ViewModel : ViewModelBase
    {
        VergiDilimlerService islem1 = new VergiDilimlerService();

        Services.AsgariUcretService islem2 = new Services.AsgariUcretService();
        MaasHesaplaService islem3 = new MaasHesaplaService();

        HesaplaVergi islem4 = new HesaplaVergi();
        public NetBrut1ViewModel()
        {
            this.Bekar = "Evli";
            this.EsCalisiyor = "ÇalışMIyor";
            this.CocukSayi = 0;
            this.VergiYil = DateTime.Now.Year;
        }

        private decimal _netMaas;
        public decimal NetMaas
        {
            get => _netMaas;
            set
            {
                _netMaas = value;
                OnPropertyChanged();
            }
        }

        private int _vergiYil;
        public int VergiYil
        {
            get => _vergiYil;
            set
            {
                _vergiYil = value;
                OnPropertyChanged();
            }
        }

        private string _esCalisiyor;
        public string EsCalisiyor
        {
            get => _esCalisiyor;
            set
            {
                _esCalisiyor = value;
                OnPropertyChanged();
            }
        }

        private int _cocukSayi;
        public int CocukSayi
        {
            get => _cocukSayi;
            set
            {
                _cocukSayi = value;
                OnPropertyChanged();
            }

        }

        private string _bekar = "Evli";
        public string Bekar
        {
            get => _bekar;
            set
            {
                _bekar = value;
                OnPropertyChanged();
            }
        }


        private bool _agiDahil;
        public bool AgiDahil
        {
            get => _agiDahil;
            set
            {
                _agiDahil = value;
                OnPropertyChanged();
            }
        }

        List<NetBrutAylar> yilToplam = new List<NetBrutAylar>();

        int kayitSayac;

        Models.VergiDilimleri yilVergiBilgi = new Models.VergiDilimleri();
        int esCalisma = 1;
        int _cs = 0;
        decimal oAykiAsgariUcret = 0;

        int Ay;
       async private void Hesapla2()
        {
            kayitSayac = 0;
            var asgariMaasListesi = await islem2.GetItemsAsync();
            vergiDilimleri = islem1.GetItems();
            decimal nett = NetMaas;

             oAykiAsgariUcret = asgariMaasListesi.ToList().Where(o=> o.yil2== VergiYil 
                                                   && o.donem==1).FirstOrDefault().brut;

    

            if (AgiDahil== true)
            {
                if (Bekar == "Bekar")
                {
                    esCalisma = 1;
                }
                else
                {
                    if (EsCalisiyor == "ÇalışMIyor")
                    {
                        esCalisma = 0;
                    }
                    else
                    {
                        esCalisma = 1;
                    }
                }
          
                _cs = CocukSayi;

                decimal agii2 = islem3.AsgariGecimHespla(oAykiAsgariUcret, esCalisma, _cs);
                nett = nett - agii2;
            }

            yilVergiBilgi = islem1.GetItems().Where(o => o.Id == VergiYil).FirstOrDefault();

            decimal brut1 = nett / Convert.ToDecimal(0.71491);
            brut1= Math.Round(brut1, 2);
            brut1 = brut1 - 10;


            decimal vergiMatrah = 0;
    
            decimal hesaplananNet50 = 0;

            yilToplam.Clear();
            matrahToplam = 0;
            // eğer hesaplanan  Net Küçükse -Brüt arttır while 
            // değilse  yani büyükse brut azalt While cümlesi...
            Ay = 1;
            hesaplananNet50 = BruttenNet(brut1);
            // 1. Ay Brut1...

            if (hesaplananNet50<nett)
            {
                while (hesaplananNet50 < nett)
                {
                    brut1 = brut1 + 50;
                    hesaplananNet50 = BruttenNet(brut1);

                }
            }
            else if(hesaplananNet50>nett)
            {
                while (hesaplananNet50 > nett)
                {
                    brut1 = brut1 - 50;
                    hesaplananNet50 = BruttenNet(brut1);
                }

            }

            brut1 = brut1 - 60;


            decimal hesaplanan10 = 0;
 
            while (hesaplanan10<nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;


            decimal hesaplanan1 = 0;
         
            while (hesaplanan1<nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            // Hesaplanan net. nett değerine eşit oluncaya kadar.

            decimal hesaplanan01 = 0;
            while(hesaplanan01 < nett)
            {
                brut1 = brut1 +(decimal) 0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            decimal hesaplanan001 = 0;
            while(hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001= BruttenNet(brut1);
            }          
             // *************1
            BruttenNetListeyeEkle(brut1);



            Ay = 2;

            // 2.Ay Brüt Hesabı...
            brut1 = brut1 - 10;

            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;
         
            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

             hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

             hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }

            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }

            BruttenNetListeyeEkle(brut1);



            Ay = 3;

            //*******************
            // 3.Ay Brut hesabı
            brut1 = brut1 - 10;

            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }


            BruttenNetListeyeEkle(brut1);

            Ay = 4;

            //*******************
            // 4. Ay Brüt Hesap
            brut1 = brut1 - 10;
            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }


            BruttenNetListeyeEkle(brut1);

            Ay = 5;
            //****************
            // 5.Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }



            BruttenNetListeyeEkle(brut1);

            Ay = 6;

            //****************
            // 6.Ay Brüt Hesap
            brut1 = brut1 - 10;



            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }


            BruttenNetListeyeEkle(brut1);


            Ay = 7;
            //****************
            // 7.Ay Brüt Hesap
            brut1 = brut1 - 10;
            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;
            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }


            BruttenNetListeyeEkle(brut1);


            Ay = 8;
            //****************
            // 8.Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }


            BruttenNetListeyeEkle(brut1);

            Ay = 9;
            //****************
            // 9.Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }

            BruttenNetListeyeEkle(brut1);


            Ay = 10;
            //****************
            // 10.Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);

            }

            BruttenNetListeyeEkle(brut1);


            Ay = 11;
            //****************
            // 11.Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);
            }
            BruttenNetListeyeEkle(brut1);


            Ay = 12;
            //****************
            // 12. Ay Brüt Hesap
            brut1 = brut1 - 10;


            hesaplananNet50 = 0;

            while (hesaplananNet50 < nett)
            {
                brut1 = brut1 + 50;
                hesaplananNet50 = BruttenNet(brut1);
            }
            brut1 = brut1 - 50;

            hesaplanan10 = 0;
            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;

            hesaplanan1 = 0;
            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);
            }

            BruttenNetListeyeEkle(brut1);


        }

        decimal matrahToplam;
        private decimal BruttenNet(decimal brut1)
        {
            decimal sgk = 0;
            decimal issizlik = 0;
            decimal verg = 0;

            var tmp1 = brut1;
            var tmp2 = tmp1;
            if (yilVergiBilgi != null)
            {
                decimal SgkTaban = yilVergiBilgi.SgkTaban;
                if (Ay > 6)
                {
                    SgkTaban = yilVergiBilgi.SgkTaban2;
                }
                decimal SgkTavan = yilVergiBilgi.SgkTavan;
                if (Ay > 6)
                {
                    SgkTavan = yilVergiBilgi.SgkTavan2;
                }

                if (tmp1 < SgkTaban)
                {
                    tmp2 = SgkTaban;
                }
                else if (tmp1 > SgkTavan)
                {
                    tmp2 = SgkTavan;
                }
            }




            sgk = tmp2 * Convert.ToDecimal(0.14);
            issizlik = tmp2 * Convert.ToDecimal(0.01);

            issizlik = Math.Round(issizlik, 2);

            verg = VergiHesaplaMaas(matrahToplam, (tmp1 - sgk - issizlik), VergiYil);

            decimal dmga = 0;
            dmga = tmp1 * Convert.ToDecimal(0.00759);


            decimal nett = 0;
            nett = tmp1 - sgk - verg - dmga - issizlik;

            return nett;

        }
        int kayitAy = 0;
        private void BruttenNetListeyeEkle(decimal brut1)
        {
            kayitAy = kayitAy + 1;
            decimal sgk = 0;
            decimal issizlik = 0;
            decimal verg = 0;

            var tmp1 = brut1;
            var tmp2 = tmp1;
            if (yilVergiBilgi != null)
            {
                decimal SgkTaban = yilVergiBilgi.SgkTaban;
                if (Ay > 6)
                {
                    SgkTaban = yilVergiBilgi.SgkTaban2;
                }
                decimal SgkTavan = yilVergiBilgi.SgkTavan;
                if (Ay > 6)
                {
                    SgkTavan = yilVergiBilgi.SgkTavan2;
                }

                if (tmp1 < SgkTaban)
                {
                    tmp2 = SgkTaban;
                }
                else if (tmp1 > SgkTavan)
                {
                    tmp2 = SgkTavan;
                }
            }


            sgk = tmp2 * Convert.ToDecimal(0.14);
            issizlik = tmp2 * Convert.ToDecimal(0.01);


            decimal matrahh = tmp1 - sgk - issizlik;
           

            issizlik = Math.Round(issizlik, 2);

            verg = VergiHesaplaMaas(matrahToplam, (tmp1 - sgk - issizlik), VergiYil);
            matrahToplam = matrahToplam + matrahh;
            decimal dmga = 0;
            dmga = tmp1 * Convert.ToDecimal(0.00759);


            decimal nett = 0;

            NetBrutAylar kayit = new NetBrutAylar();


          decimal agii = 0;
          agii = islem3.AsgariGecimHespla(oAykiAsgariUcret, esCalisma, _cs);


          //  nett = tmp1 - sgk - verg - dmga - issizlik; 
            kayit.brut = tmp1;
            kayit.ay = kayitSayac + 1;
            kayit.sgk = sgk; 
            kayit.issizlik = issizlik;
            kayit.matrah = (tmp1 - issizlik - sgk);
            //matrahToplam = kayit.matrah + matrahToplam;
            kayit.net = NetMaas;
            kayit.matrahToplam = matrahToplam;
            kayit.gelirVergisi = verg;
            kayit.damgaVergisi = dmga;
            kayit.agi = agii;

            kayit.netAgili = nett + agii;
            kayit.ay = kayitAy;
            yilToplam.Add(kayit);

        }


        List<Models.VergiDilimleri> vergiDilimleri = new List<Models.VergiDilimleri>();
        public Decimal VergiHesaplaMaas(Decimal vergiMatrah, Decimal tazToplam, int yill)
        {

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

        private decimal VergiHesabi(decimal brut1)
        {

            decimal vergii = 0;





            return vergii;
        }

        // ----Hesapla---------------
        async private void Hesapla()
        {

            yilToplam.Clear();

            Decimal oAykiAsgariUcret = 0;

            var asgariMaasListesi = await islem2.GetItemsAsync();

            int esCalisma = 0;
            if (EsCalisiyor == "Çalışıyor")
            {
                esCalisma = 1;
            }

            if (Bekar == "Bekar")
            {
                esCalisma = 1;
            }
            int _cs = 0;
            if (CocukSayi > 0)
            {
                _cs = CocukSayi;
            }



            decimal matrahToplam = 0;


            for (int i = 1; i <= 12; i++)
            {

                Decimal tmp1 = NetMaas;
                decimal tmpp = tmp1;

                NetBrutAylar kayit = new NetBrutAylar();
                kayit.ay = i;

                DateTime tarOAy = new DateTime(VergiYil, i, 1);

                string donem = "1";
                if (i > 6)
                {
                    donem = "2";
                }

                string yilBilgi = VergiYil.ToString() + "-" + donem;

                var aa = asgariMaasListesi.ToList().Find(o => o.yil == yilBilgi);

                if (aa != null)
                {
                    oAykiAsgariUcret = aa.brut;
                }


                decimal agii = islem3.AsgariGecimHespla(oAykiAsgariUcret, esCalisma, _cs);

                if (AgiDahil == true)
                {
                    kayit.netAgili = tmp1;
                    kayit.net = tmp1 - agii;
                }
                else
                {
                    kayit.net = tmp1;
                    kayit.netAgili = tmp1 + agii;

                }
      
            }

        }

        //---------------------------
        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            // ---------------
            //*********************


           Hesapla2();
            var sayfa = new Views.NetBrutV.NetBrutSon(yilToplam);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            //********
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

            this.HataMesaji = "";
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
