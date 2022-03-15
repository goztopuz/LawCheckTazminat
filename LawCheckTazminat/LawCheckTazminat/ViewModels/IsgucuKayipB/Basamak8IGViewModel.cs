using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using System.Collections.ObjectModel;
using LawCheckTazminat.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using LawCheckTazminat.Views.IsgucuKayipV;
using LawCheckTazminat.Views.DestektenYoksunlukV;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class Basamak8IGViewModel: ViewModelBase
    {
        public Basamak8IGViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = new IsgucuKayip();
            IsGucu = _kayip;

            MaasListeOlustur();
            AsgariMaasListeOlustur();

            kkk();
            kkk2();


            if (_kayip.duzenlemede == true)
            {
                EmekliMaas = _kayip.emekliMaas;
                IsleyecekDonemMaas = _kayip.isleyecekMaas;

            }

        }

        private Models.DestektenYoksunluk _destektenYoksunluk;

        MaasHesaplaService islem2 = new MaasHesaplaService();

        ObservableCollection<MaasIsGucu> _maasListe2 = new ObservableCollection<MaasIsGucu>();
        public ObservableCollection<MaasIsGucu> MaasListe2
        {
            get => _maasListe2;
            set
            {
                _maasListe2 = value;
                OnPropertyChanged();
            }

        }

        ObservableCollection<MaasIsGucu> _maasListe3 = new ObservableCollection<MaasIsGucu>();
        public ObservableCollection<MaasIsGucu> MaasListe3
        {
            get => _maasListe3;
            set
            {
                _maasListe3 = value;
                OnPropertyChanged();
            }

        }

        List<AsgariUcretTablosu> asgariListe;

        AsgariUcretService islemAsgari = new AsgariUcretService();

        private async void AsgariMaasListeOlustur()
        {
            asgariListe = (await islemAsgari.GetItemsAsync()).ToList();

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

        string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }

        bool _agiVar;
        public bool AgiVar
        {
            get => _agiVar;
            set
            {
                _agiVar = value;
                OnPropertyChanged();
            }
        }



        decimal _emekliMaas;
        public Decimal EmekliMaas
        {
            get => _emekliMaas;
            set
            {
                _emekliMaas = value;
                OnPropertyChanged();
            }
        }

        decimal _isleyecekDonemMaas;
        public Decimal IsleyecekDonemMaas
        {
            get => _isleyecekDonemMaas;
            set
            {
                _isleyecekDonemMaas = value;
                OnPropertyChanged();
            }
        }





        private void MaasListeOlustur()
        {
            int yilBas = IsGucu.kazaTarihi.Year;
            int yilBit = IsGucu.raporTarihi.Year;
            MaasListe2.Clear();
            MaasListe3.Clear();
            int j = 0;

            for (int i = yilBas; i <= yilBit; i++)
            {

                MaasIsGucu m = new MaasIsGucu();
                m.yil = i.ToString() + "-1";

                if (j == 0)
                {
                    if (IsGucu.kazaTarihi.Month < 7)
                    {
                        MaasListe2.Add(m);
                        MaasListe3.Add(m);
                    }
                }
                else
                {
                    MaasListe2.Add(m);
                    MaasListe3.Add(m);
                }
                j = j + 1;

                DateTime testDate = new DateTime(i, 7, 1);
                if (testDate < IsGucu.raporTarihi)
                {

                    MaasIsGucu m2 = new MaasIsGucu();
                    m2.yil = i.ToString() + "-2";

                    MaasListe2.Add(m2);
                    MaasListe3.Add(m2);
                }
            }


            foreach (var t in MaasListe2)
            {
                int yil1 = Convert.ToInt32(t.yil.Substring(0, 4));
                string bol1 = t.yil.Substring(5, 1);

                if (bol1 == "1")
                {
                    t.yilBas = new DateTime(yil1, 1, 1);
                    t.yilBit = new DateTime(yil1, 6, 30);
                }
                else
                {
                    t.yilBas = new DateTime(yil1, 6, 1);
                    t.yilBit = new DateTime(yil1, 12, 31);
                }

            }

            foreach (var t in MaasListe3)
            {
                int yil1 = Convert.ToInt32(t.yil.Substring(0, 4));
                string bol1 = t.yil.Substring(5, 1);

                if (bol1 == "1")
                {
                    t.yilBas = new DateTime(yil1, 1, 1);
                    t.yilBit = new DateTime(yil1, 6, 30);
                }
                else
                {
                    t.yilBas = new DateTime(yil1, 6, 1);
                    t.yilBit = new DateTime(yil1, 12, 31);
                }

            }

            if (IsGucu.duzenlemede == true)
            {

                foreach (var t in MaasListe2)
                {
                    var kayit = IsGucu.IsGucuKayipMaaslar.ToList().Find(o => o.yil == t.yil);
                    if (kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    }
                }

                foreach (var t in MaasListe3)
                {
                    var kayit = IsGucu.IsGucuKayipMaaslar.ToList().Find(o => o.yil == t.yil);
                    if (kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    }
                }


            }

        }


        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;
        }



        private string _maasSecim;
        public string MaasSecim
        {
            get => _maasSecim;
            set
            {
                _maasSecim = value;
                OnPropertyChanged();
            }
        }


        public ICommand SecimCommand => new Command<string>(OnSecim);
        async private void OnSecim(string sec)
        {
            switch (MaasSecim)
            {
                case "":
                    break;

                case "Asgari Ücret Olarak Belirle":
                    //var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Maaşlar Asgari Ücret Olarak Atanacaktır. Eminmisiniz?", "İptal", "", "Asgari Olarak Ata");

                    //if (cevap1 == "Asgari Olarak Ata")
                    //{
                    //    AsgariOlarakAta();
                    //}

                    AsgariOlarakAta();
                    break;

                case "NET Maaşı Asg. Ücrete Oranla":
                    var sayfa = new MaasOranlaViewIsgucu(IsGucu, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
                    break;

                case "BRÜT Maaşı Asg. Ücrete Oranla":
                    var sayfa2 = new MaasOranlaBrutViewIsGucu(IsGucu, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa2);
                    break;

                default:
                    break;

            }

        }





        public ICommand BrutOranlaCommand => new Command(OnBrutOranla);
        async private void OnBrutOranla(object obj)
        {
            var sayfa = new MaasOranlaBrutViewIsGucu(IsGucu, asgariListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
        }

        public ICommand NetOranlaCommand => new Command(OnNetOranla);
        async private void OnNetOranla(object obj)
        {
            var sayfa = new MaasOranlaViewIsgucu(IsGucu, asgariListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

        }

        public ICommand AsgariBelirleCommand => new Command(OnAsgariBelirle);
        async private void OnAsgariBelirle(object obj)
        {
            var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Maaşlar Asgari Ücret Olarak Atanacaktır. Eminmisiniz?", "İptal", "", "Asgari Olarak Ata");

            if (cevap1 == "Asgari Olarak Ata")
            {
                AsgariOlarakAta();
            }
          //  Guncelle();
        }

        public ICommand KaydetBitirCommand => new Command(OnKaydetBitir);

        IsGucuKayipService islem = new IsGucuKayipService();
        private async void OnKaydetBitir(object obj)
        {
            // DESTEKTEN YOKSUNLUK KAYDET-GÜNCELLE
            IsGucu.IsGucuKayipMaaslar = MaasListe2;
            IsGucu.emekliMaas = EmekliMaas;
            IsGucu.isleyecekMaas = IsleyecekDonemMaas;


            if (EksikKontrol() == true)
            {
                return;
            }

            foreach (var t2 in MaasListe2)
            {
                if (t2.Id == null)
                {
                    t2.Id = Guid.NewGuid().ToString().Substring(0, 10);
                }
            }

            if(IsGucu.Id==null || IsGucu.Id=="")
            {
                IsGucu.Id = Guid.NewGuid().ToString().Substring(0, 10);
                IsGucu.EskiId = "";

            }
            else
            {
                IsGucu.EskiId = IsGucu.Id;
                IsGucu.Id = Guid.NewGuid().ToString().Substring(0, 10);

            }

            //----------------

            foreach (var t in IsGucu.IsGucuKayipMaaslar)
            {
                t.dosyaId = IsGucu.Id;
                t.IsgucuKayip = IsGucu;
            }

            foreach (var t2 in IsGucu.IsGucuKayipMasraflar)
            {
                t2.dosyaId = IsGucu.Id;
                t2.IsgucuKayip = IsGucu;
            }

            foreach (var t3 in IsGucu.IsGucuKayipYakinlar)
            {
                t3.dosyaId = IsGucu.Id;
                t3.IsgucuKayip = IsGucu;
            }

            foreach (var t4 in IsGucu.IsGucuKayipOranlar)
            {
                t4.dosyaId = IsGucu.Id;
                t4.IsgucuKayip = IsGucu;
            }


             islem.AddItem(IsGucu);
            //-------------------------------

            var sayfa = new DestekSonIsgucu(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



            //***************************************************

            //if (IsGucu.Id == null)
            //{

            //    IsGucu.Id = Guid.NewGuid().ToString().Substring(0, 10);



            //    foreach (var t in IsGucu.IsGucuKayipMaaslar)
            //    {
            //        t.dosyaId = IsGucu.Id;
            //    }
            //    foreach (var t2 in IsGucu.IsGucuKayipMasraflar)
            //    {
            //        t2.dosyaId = IsGucu.Id;
            //    }
            //    foreach (var t3 in IsGucu.IsGucuKayipYakinlar)
            //    {
            //        t3.dosyaId = IsGucu.Id;
            //    }
            //    foreach(var t4 in  IsGucu.IsGucuKayipOranlar)
            //    {
            //        t4.dosyaId = IsGucu.Id;
            //    }


            //    await islem.AddItemAsync(IsGucu);


            //}
            //else
            //{

            //    foreach (var t in IsGucu.IsGucuKayipMaaslar)
            //    {
            //        t.dosyaId = IsGucu.Id;
            //    }
            //    foreach (var t2 in IsGucu.IsGucuKayipMasraflar)
            //    {
            //        t2.dosyaId = IsGucu.Id;
            //    }
            //    foreach (var t3 in IsGucu.IsGucuKayipYakinlar)
            //    {
            //        t3.dosyaId = IsGucu.Id;
            //    }

            //    await islem.UpdateItem(IsGucu);

            //}



        }

        string durum = "";
        public void kkk()
        {
            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaNetAsgariIsgucu", async (aa) =>
            {
                // NetAsgariOranla(aa);

                BrutAsgariOranla(aa);
                durum = "net";
            });

        }
        public void kkk2()
        {
            
            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaBrutAsgariIsGucu", async (aa) =>
            {
                BrutAsgariOranla(aa);

                durum = "brut";
            });

        }



        private void BrutAsgariOranla(AsgariOran ao)
        {
            decimal _oran = Convert.ToDecimal(ao.oran);


            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 0;


            int yill = IsGucu.kazaTarihi.Year;


            string asgariDonem = "";

            if (IsGucu.kazaTarihi.Month > 6)
            {
                asgariDonem = yill.ToString() + "-2";

            }
            else
            {
                asgariDonem = yill.ToString() + "-1";
            }
            bool esVar = false;
            //foreach (var t in this.IsGucu.IsGucuKayipYakinlar)
            //{
            //    if (t.yakinlik == "Eş")
            //    {
            //        if (t.esCalisiyorMu == "Evet")
            //        {
            //            esDurum = 1;
            //        }

            //        esVar = true;
            //    }
            //    else if (t.yakinlik == "Çocuk")
            //    {
            //        hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
            //    }
            //}

            if(esVar== false)
            {
                esDurum = 1;
            }

            foreach (var t2 in this.MaasListe3)
            {

                var deger = asgariListe.Find(o => o.yil == t2.yil);
                Decimal oAykiAsgariUcret = 0;
                oAykiAsgariUcret = Convert.ToDecimal(deger.brut);

                //Decimal agii = 0;
                //agii = islem2.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                if (deger != null)
                {

                    Decimal mNet = 0;
                    Decimal mBrut = 0;



                    mBrut = Convert.ToDecimal(deger.brut) * Convert.ToDecimal(_oran);

                    mNet = Convert.ToDecimal(deger.net) * Convert.ToDecimal(_oran);

                  //  mNet = islem2.BruttenNetHesapla(mBrut, agii);
                    t2.brutMaas = mBrut;


                    t2.netMaas = mNet;
                    t2.ekBilgi4 = 0;


                }

                IsleyecekDonemMaas = t2.netMaas;
                EmekliMaas = deger.net;
            }

            MaasListe2.Clear();
            foreach(var tt in MaasListe3)
            {
                MaasListe2.Add(tt);
            }

            //MaasListe2 = MaasListe3;

        }







        private void NetAsgariOranla(AsgariOran ao)
        {
            decimal _oran = Convert.ToDecimal(ao.oran);

            decimal oran1 = ao.oran;
            decimal maas = ao.maas;

            var islem2 = new MaasHesaplaService();

            foreach (var t in MaasListe3)
            {



                string yill2 = t.yil;
                var deger = asgariListe.Find(o => o.yil == yill2);



                //AGİ 
                Decimal oAykiAsgariUcret = 0;
                try
                {
                    oAykiAsgariUcret = Convert.ToDecimal(deger.net);

                }
                catch (Exception ex)
                {

                }


                decimal donemselNet = oAykiAsgariUcret * oran1;

                t.netMaas = donemselNet;


                IsleyecekDonemMaas = t.netMaas;
                EmekliMaas = deger.net;
            }

            MaasListe2.Clear();
            foreach(var tt2 in MaasListe3)
            {
                MaasListe2.Add(tt2);
            }

           
         //   MaasListe2 = MaasListe3;

        }

        private void AsgariOlarakAta()
        {

            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 1;

            Decimal hesaplanacakMaas = 0;

            bool esVar = false;
            foreach (var t in this.IsGucu.IsGucuKayipYakinlar)
            {
                if (t.yakinlik == "Eş")
                {
                    if (t.esCalisiyorMu == "Hayır")
                    {
                        esDurum = 0;
                    }
                    esVar = true;
                }
                else if (t.yakinlik == "Çocuk")
                {
                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                }
            }

            if (esVar == false)
            {
                esDurum = 1;
            }


            foreach (var t in MaasListe3)
            {
                string yill = t.yil;

                var deger = asgariListe.Find(o => o.yil == yill);
                if (deger != null)
                {
                    Decimal agii = 0;

                    Decimal oAykiAsgariUcret = 0;
                    oAykiAsgariUcret = Convert.ToDecimal(deger.brut);

                    agii = islem2.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);

                    Decimal agii2 = islem2.AsgariGecimHespla(oAykiAsgariUcret, 1, 0);

                    Decimal sonn = agii - agii2;

                    t.netMaas = deger.net + sonn;
                    t.brutMaas = deger.brut;
                    t.ekBilgi4 = agii;



                }
                hesaplanacakMaas = t.netMaas;
                IsleyecekDonemMaas = hesaplanacakMaas;
                EmekliMaas = hesaplanacakMaas;
            }

            MaasListe2.Clear();

            foreach(var tt3 in MaasListe3)
            {
                MaasListe2.Add(tt3);
            }

            //MaasListe2 = MaasListe3;
        }

        private bool EksikKontrol()
        {
            bool _eksikVar = false;

            if (IsleyecekDonemMaas == 0)
            {
                _eksikVar = true;
                HataMesaji = "İşleyecek Maaş Girilmelidir.";

            }
            else if (EmekliMaas == 0)
            {
                _eksikVar = true;
                HataMesaji = "Emekli Dönem Maaşı Girilmelidir.";
            }


            foreach (var t in IsGucu.IsGucuKayipMaaslar)
            {
                if (t.netMaas == 0)
                {
                    _eksikVar = true;
                    HataMesaji = t.yil + " Dönemindeki Net Maaş Girilmelidir.";
                }
            }

            return _eksikVar;

        }



        private IsgucuKayip _isgucuKayip;
        public IsgucuKayip IsGucu
        {
            get => _isgucuKayip;
            set
            {
                _isgucuKayip = value;
                OnPropertyChanged();
            }

        }

    }

}
