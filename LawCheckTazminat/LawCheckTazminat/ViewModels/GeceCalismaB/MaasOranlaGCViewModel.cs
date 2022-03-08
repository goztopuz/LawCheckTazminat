using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using LawCheckTazminat.ViewModels.NetBrut;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class MaasOranlaGCViewModel : ViewModelBase
    {

        List<AsgariUcretTablosu> aL2 = new List<AsgariUcretTablosu>();

        int cocukSay = 0;
        int esCalismaa = 1;
        MaasHesaplaService islem2 = new MaasHesaplaService();


        public MaasOranlaGCViewModel(GeceCalisma _gc, List<AsgariUcretTablosu> aL)
        {

            aL2 = aL;

            Ay = DateTime.Now.Month;
            Yil = DateTime.Now.Year;

            AgiDahil = true;


            cocukSay = _gc.CocukSayisi;

            if (_gc.Bekar == "Evli")
            {
                if (_gc.EsCalisiyor == "ÇalışMIyor")
                {
                    esCalismaa = 0;
                }
            }

        }



        int _yil;
        public int Yil
        {
            get => _yil; 
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }

        int _ay;
        public int Ay
        {
            get => _ay;
            set
            {
                _ay = value;
                OnPropertyChanged();
            }
        }

        decimal _oAykiAsgari;
        public decimal OAykiAsgari
        {
            get => _oAykiAsgari;
            set
            {
                _oAykiAsgari = value;
                OnPropertyChanged();
            }
        }

        decimal _netMaas;
        public decimal NetMaas
        {
            get => _netMaas;
            set
            {
                _netMaas = value;
                OnPropertyChanged();
            }
        }

        decimal _oran;
        public decimal Oran
        {
            get => _oran;
            set
            {
                _oran = value;
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

        private decimal _agii;
        public decimal AgiDegeri
        {
            get => _agii;
            set
            {
                _agii = value;
                OnPropertyChanged();
            }
        }

        decimal _brutUcret;
        public decimal BrutUcret
        {
            get => _brutUcret;
            set
            {
                _brutUcret = value;
                OnPropertyChanged();
            }
        }



        decimal _olayTarihiAgisiz;
        public decimal OlayTarihiAgisiz
        {
            get => _olayTarihiAgisiz;
            set
            {
                _olayTarihiAgisiz = value;
                OnPropertyChanged();
            }
        }



        public ICommand OranlaCommand => new Command(OnOranla);
        async private void OnOranla(object obj)
        {
            if (IsBusy == true)
            {
                return; ;
            }

            IsBusy = true;
            int _donem = 1;
            if (Ay > 6)
            {
                _donem = 2;
            }
            OAykiAsgari = aL2.Find(o => o.yil2 == Yil && o.donem == _donem).net;

            decimal _oran2;

            _oran2 = NetMaas / OAykiAsgari;
            Oran = _oran2;
            IsBusy = false;
        }

        public ICommand KullanCommand => new Command(OnKullan);
        async private void OnKullan(object obj)
        {
            if (IsBusy == true)
            {
                return; ;
            }

            IsBusy = true;

            //-------------------------------
            var ao = new AsgariOran();
            ao.maas = NetMaas;
            ao.oran = Oran;

            MessagingCenter.Send<AsgariOran>(ao, "OranlaNetAsgariGC");
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;


        }





        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {

            if (IsBusy == true)
            {
                return; ;
            }

            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
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



        // *********************************************************************
        //--------------------------------------------------------------------


        private decimal AgiHesapla()
        {
            decimal agiSonuc = 0;
            int hesabaKatilacakCocukSayisi = cocukSay; ;
            int esDurum = 1;


            esDurum = esCalismaa;

            agiSonuc = islem2.AsgariGecimHespla(OAykiAsgari, esDurum, hesabaKatilacakCocukSayisi);
            return agiSonuc;

        }

        private void BrutUcretHesapla()
        {

            Hesapla2();

        }


        List<NetBrutAylar> yilToplam = new List<NetBrutAylar>();

        int kayitSayac;

        Models.VergiDilimleri yilVergiBilgi = new Models.VergiDilimleri();
        int esCalisma = 1;
        int _cs = 0;
        decimal oAykiAsgariUcret = 0;
        decimal matrahToplam;
        List<Models.VergiDilimleri> vergiDilimleri = new List<Models.VergiDilimleri>();


        private async void Hesapla2()
        {
            VergiDilimlerService islem1 = new VergiDilimlerService();

            Services.AsgariUcretService islem3 = new Services.AsgariUcretService();
            kayitSayac = 0;
            var asgariMaasListesi = await islem3.GetItemsAsync();
            vergiDilimleri = islem1.GetItems();
            decimal nett = OlayTarihiAgisiz;

            oAykiAsgariUcret = asgariMaasListesi.ToList().Where(o => o.yil2 == Yil
                                                  && o.donem == 1).FirstOrDefault().brut;





            yilVergiBilgi = islem1.GetItems().Where(o => o.Id == Yil).FirstOrDefault();

            decimal brut1 = nett / Convert.ToDecimal(0.71491);
            brut1 = Math.Round(brut1, 2);
            brut1 = brut1 - 10;


            decimal vergiMatrah = 0;

            decimal hesaplananNet50 = 0;

            yilToplam.Clear();
            matrahToplam = 0;

            // eğer hesaplanan  Net Küçükse -Brüt arttır while 
            // değilse  yani büyükse brut azalt While cümlesi...

            hesaplananNet50 = BruttenNet(brut1);



            // 1. Ay Brut1...

            if (hesaplananNet50 < nett)
            {
                while (hesaplananNet50 < nett)
                {
                    brut1 = brut1 + 50;
                    hesaplananNet50 = BruttenNet(brut1);

                }
            }
            else if (hesaplananNet50 > nett)
            {
                while (hesaplananNet50 > nett)
                {
                    brut1 = brut1 - 50;
                    hesaplananNet50 = BruttenNet(brut1);
                }

            }

            brut1 = brut1 - 60;


            decimal hesaplanan10 = 0;

            while (hesaplanan10 < nett)
            {
                brut1 = brut1 + 10;
                hesaplanan10 = BruttenNet(brut1);
            }
            brut1 = brut1 - 10;


            decimal hesaplanan1 = 0;

            while (hesaplanan1 < nett)
            {
                brut1 = brut1 + 1;
                hesaplanan1 = BruttenNet(brut1);
            }
            brut1 = brut1 - 1;

            // Hesaplanan net. nett değerine eşit oluncaya kadar.

            decimal hesaplanan01 = 0;
            while (hesaplanan01 < nett)
            {
                brut1 = brut1 + (decimal)0.1;
                hesaplanan01 = BruttenNet(brut1);
            }
            brut1 = brut1 - (decimal)0.1;

            decimal hesaplanan001 = 0;
            while (hesaplanan001 <= nett)
            {
                brut1 = brut1 + (decimal)0.01;
                hesaplanan001 = BruttenNet(brut1);
            }
            // *************1
            BruttenNetListeyeEkle(brut1);




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


            BrutUcret = yilToplam[Ay - 1].brut;

        }

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

            verg = VergiHesaplaMaas(matrahToplam, (tmp1 - sgk - issizlik), Yil);

            decimal dmga = 0;
            dmga = tmp1 * Convert.ToDecimal(0.00759);


            decimal nett = 0;
            nett = tmp1 - sgk - verg - dmga - issizlik;

            return nett;

        }

        private void BruttenNetListeyeEkle(decimal brut1)
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


            decimal matrahh = tmp1 - sgk - issizlik;


            issizlik = Math.Round(issizlik, 2);

            verg = VergiHesaplaMaas(matrahToplam, (tmp1 - sgk - issizlik), Yil);
            matrahToplam = matrahToplam + matrahh;
            decimal dmga = 0;
            dmga = tmp1 * Convert.ToDecimal(0.00759);


            decimal nett = 0;

            NetBrutAylar kayit = new NetBrutAylar();


            decimal agii = 0;
            agii = islem2.AsgariGecimHespla(oAykiAsgariUcret, esCalisma, _cs);


            //  nett = tmp1 - sgk - verg - dmga - issizlik; 
            kayit.brut = tmp1;
            kayit.ay = kayitSayac + 1;
            kayit.sgk = sgk;
            kayit.issizlik = issizlik;
            kayit.matrah = (tmp1 - issizlik - sgk);
            //matrahToplam = kayit.matrah + matrahToplam;
            kayit.net = OlayTarihiAgisiz + AgiDegeri;
            kayit.matrahToplam = matrahToplam;
            kayit.gelirVergisi = verg;
            kayit.damgaVergisi = dmga;
            kayit.agi = agii;

            kayit.netAgili = nett + agii;

            yilToplam.Add(kayit);

        }

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



    }
}
