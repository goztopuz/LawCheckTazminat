using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.NetBrut
{

   
    public class BrutNet1ViewModel : ViewModelBase
    {

        Services.AsgariUcretService islem2 = new Services.AsgariUcretService();
        MaasHesaplaService islem3 = new MaasHesaplaService();

        HesaplaVergi islem4 = new HesaplaVergi();

        VergiDilimlerService islem1 = new VergiDilimlerService();
        
        public BrutNet1ViewModel()
        {

            this.Bekar = "Evli";
            this.EsCalisiyor = "ÇalışMIyor";
            this.CocukSayi = 0;
            this.VergiYil = DateTime.Now.Year;

        }

        private decimal _brutMaas;
        public decimal BrutMaas
        {
            get => _brutMaas;
            set
            {
                _brutMaas = value;
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
        public int  CocukSayi
        {
            get => _cocukSayi;
            set
            {
                _cocukSayi = value;
                OnPropertyChanged();
            }

        }

        private string _bekar= "Evli";
        public string Bekar
        {
            get => _bekar;
            set
            {
                _bekar = value;
                OnPropertyChanged();
            }
        }


        List<NetBrutAylar> yilToplam = new List<NetBrutAylar>();

        async private void Hesapla()
        {

            yilToplam.Clear();
            Decimal oAykiAsgariUcret = 0;

            var asgariMaasListesi = await islem2.GetItemsAsync();

            int esCalisma = 0;
            if(EsCalisiyor=="Çalışıyor")
            {
                esCalisma = 1;
            }

            if(Bekar=="Bekar")
            {
                esCalisma = 1;
            }
             int _cs = 0;
            if(CocukSayi>0)
            {
                _cs = CocukSayi;
            }


            // her ay için asgari ücret Hesaplanacak
            // islem3.AsgariGecimHespla()

            decimal matrahToplam = 0;

            var yilVergiBilgi = islem1.GetItems().Where(o => o.Id == VergiYil).FirstOrDefault();
            for (int i = 1; i <= 12; i++)
            {

                NetBrutAylar kayit = new NetBrutAylar();
                kayit.ay = i;

                Decimal tmp1 = BrutMaas;
                decimal tmpp = tmp1;
                //tmp1 = tmp1 * Convert.ToDecimal(1.1);

                decimal nett = 0;

                decimal sgk = 0;
                decimal issizlik = 0;
                decimal verg = 0;
                decimal dmga = 0;

                var tmp2 = tmp1;


                if (yilVergiBilgi != null)
                {
                    decimal SgkTaban = yilVergiBilgi.SgkTaban;
                    if (i > 6)
                    {
                        SgkTaban = yilVergiBilgi.SgkTaban2;
                    }
                    decimal SgkTavan = yilVergiBilgi.SgkTavan;
                    if (i > 6)
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



                // O ay ki asgari ücret...........

                DateTime tarOAy = new DateTime(VergiYil, i, 1);

                string donem = "1";
                if(i>6)
                {
                    donem = "2";
                }

                string yilBilgi = VergiYil.ToString() + "-" + donem;

                var aa = asgariMaasListesi.ToList().Find(o => o.yil == yilBilgi);

                if( aa != null)
                {
                    oAykiAsgariUcret = aa.brut;
                }


                decimal agii = islem3.AsgariGecimHespla(oAykiAsgariUcret, esCalisma, _cs);
                
                verg = islem4.VergiHesaplaMaas(matrahToplam, (tmp1 - sgk - issizlik), VergiYil);


                //verg = (tmp1 - sgk - issizlik) * Convert.ToDecimal(0.15);
                dmga = tmp1 * Convert.ToDecimal(0.00759);



                nett = tmp1 - sgk - verg - dmga - issizlik;




                kayit.brut = BrutMaas;
                kayit.sgk = sgk;
                kayit.issizlik = issizlik;
                kayit.matrah = (tmp1 - issizlik - sgk);
                matrahToplam = kayit.matrah + matrahToplam;
                kayit.net = nett;
                kayit.matrahToplam = matrahToplam;
                kayit.gelirVergisi = verg;
                kayit.damgaVergisi = dmga;
                kayit.agi = agii;

                kayit.netAgili = nett + agii;

                yilToplam.Add(kayit);


            }


        }


        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;


            // ---------------
            //*********************


            Hesapla();
            var sayfa = new Views.NetBrutV.BrutNetSon(yilToplam);
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



    public class NetBrutAylar
    {
        public int ay { get; set; }
        public string ay2 { get; set; }

        public decimal brut { get; set; }
        public decimal sgk { get; set; }
        public decimal issizlik { get; set; }
        public decimal matrahToplam { get; set; }
        public decimal matrah { get; set; }
        public decimal gelirVergisi { get; set; }

        public decimal damgaVergisi { get; set; }
        public decimal agi { get; set; }

        public decimal net { get; set; }
        public decimal net2 { get; set; }
        public decimal netAgili { get; set; }

    }
}
