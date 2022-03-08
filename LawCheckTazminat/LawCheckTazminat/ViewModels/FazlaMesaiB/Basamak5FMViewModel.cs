using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak5FMViewModel:ViewModelBase
    {
        public Basamak5FMViewModel(FazlaMesai _fm)
        {

            this.FM = new FazlaMesai();
            this.FM = _fm;
            MaasListeOlustur();

            AsgariMaasListeOlustur();

            kkk();
            kkk2();

        }



        private DateTime _baslangicTar;
        public DateTime BaslangicTar
        {
            get => _baslangicTar;
            set
            {
                _baslangicTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _bitisTar;
        public DateTime BitisTar
        {
            get => _bitisTar;
            set
            {
                _bitisTar = value;
                OnPropertyChanged();
            }
        }


        DateTime tmpYilBas;
        DateTime tmpYilBit;

        ObservableCollection<MaasFazlaMesai> _maasListe2 = new ObservableCollection<MaasFazlaMesai>();
        public ObservableCollection<MaasFazlaMesai> MaasListe2
        {
            get => _maasListe2;
            set
            {
                _maasListe2 = value;
                OnPropertyChanged();
            }

        }

        ObservableCollection<MaasFazlaMesai> _maasListe3 = new ObservableCollection<MaasFazlaMesai>();
        public ObservableCollection<MaasFazlaMesai> MaasListe3
        {
            get => _maasListe3;
            set
            {
                _maasListe3 = value;
                OnPropertyChanged();
            }

        }



        // Asgari Ücret Listesi
        List<AsgariUcretTablosu> asgariListe;

        AsgariUcretService islemAsgari = new AsgariUcretService();
        private async void AsgariMaasListeOlustur()
        {
            asgariListe = (await islemAsgari.GetItemsAsync()).ToList();

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
                    var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Maaşlar Asgari Ücret Olarak Atanacaktır. Eminmisiniz?", "İptal", "", "Asgari Olarak Ata");
                    if (cevap1 == "Asgari Olarak Ata")
                    {
                        AsgariOlarakAta();
                    }
                    break;

                case "NET Maaşı Asg. Ücrete Oranla":
                    var sayfa = new MaasOranlaFMView(FM, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
                    break;

                case "BRÜT Maaşı Asg. Ücrete Oranla":
                    var sayfa2 = new MaasOranlaBrutViewFM(FM, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa2);
                    break;

                default:
                    break;

            }

        }




        public ICommand BrutOranlaCommand => new Command(OnBrutOranla);
        async private void OnBrutOranla(object obj)
        {
            var sayfa = new MaasOranlaBrutViewFM(FM, asgariListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
        }

        public ICommand NetOranlaCommand => new Command(OnNetOranla);
        async private void OnNetOranla(object obj)
        {
            var sayfa = new MaasOranlaFMView(FM, asgariListe);
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
            // Guncelle();
        }

        private void AsgariOlarakAta()
        {

          

            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 1;

            Decimal hesaplanacakMaas = 0;


            hesabaKatilacakCocukSayisi = FM.CocukSayisi;

            if (FM.Bekar == "Evli")
            {
                if (FM.EsCalisiyor == "ÇalışMIyor")
                {
                    esDurum = 0;
                }
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
            }

            MaasListe2.Clear();

            foreach (var tt3 in MaasListe3)
            {
                MaasListe2.Add(tt3);
            }

            //MaasListe2 = MaasListe3;
        }




        public void kkk()
        {
            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaNetAsgariFM", async (aa) =>
            {
                //  NetAsgariOranla(aa);
                BrutAsgariOranla(aa);
            });

        }
        public void kkk2()
        {

            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaBrutAsgariFM", async (aa) =>
            {
                BrutAsgariOranla(aa);
            });

        }



//---------------------------------------------------------


        private FazlaMesai _fazlaMesai;

        public FazlaMesai FM
        {
            get => _fazlaMesai;
            set
            {
                _fazlaMesai = value;
                OnPropertyChanged();
            }
        }


        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            FM.MaasBilgi = MaasListe2;

            foreach(var t in FM.MaasBilgi)
            {
                t.Id = Guid.NewGuid().ToString().Substring(0, 8);
            }

            this.HataMesaji = "";

            var basamak6 = new Basamak6FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak6);

            IsBusy = false;

        }


        // Maaş
       

        private void MaasListeOlustur()
        {
            tmpYilBas = FM.BasTarih;

            if(tmpYilBas > FM.BasTarihMesai)
            {
                tmpYilBas = FM.BasTarihMesai;
            }

            if(tmpYilBas> FM.BasTarihHaftaSonu)
            {
                tmpYilBas = FM.BasTarihHaftaSonu;
            }
            
            if(tmpYilBas>FM.BasTarihResmiTatil)
            {
                tmpYilBas = FM.BasTarihResmiTatil;
            }
            BaslangicTar = tmpYilBas;
            tmpYilBit = FM.BitTarih;
            if(FM.BitTarihMesai>tmpYilBit)
            {
                tmpYilBit = FM.BitTarihMesai;
            }

            if(FM.BitTarihHaftaSonu> tmpYilBit)
            {
                tmpYilBit = FM.BitTarihHaftaSonu;
            }

            if(FM.BitTarihResmiTatil> tmpYilBit)
            {
                tmpYilBit = FM.BitTarihResmiTatil;
            }
            BitisTar= tmpYilBit;
            int yilBas = tmpYilBas.Year;
            int yilBit = tmpYilBit.Year ;

            MaasListe2.Clear();
            MaasListe3.Clear();
            int j = 0;

            for (int i = yilBas; i <= yilBit; i++)
            {

                MaasFazlaMesai m = new MaasFazlaMesai();
                m.yil = i.ToString() + "-1";

                if (j == 0)
                {
                    if (tmpYilBas.Month < 7)
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
                if (testDate < tmpYilBit)
                {

                    MaasFazlaMesai m2 = new MaasFazlaMesai();
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

            if (FM.duzenlemede == true)
            {

                foreach (var t in MaasListe2)
                {
                    var kayit = FM.MaasBilgi.ToList().Find(o => o.yil == t.yil);
                    if (kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    }
                }

                foreach (var t in MaasListe3)
                {
                    var kayit = FM.MaasBilgi.ToList().Find(o => o.yil == t.yil);
                    if (kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    }
                }


            }


        }


        // Brüt Oranla
        MaasHesaplaService islem2 = new MaasHesaplaService();

        private void BrutAsgariOranla(AsgariOran ao)
        {
            decimal _oran = Convert.ToDecimal(ao.oran);


            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 1;
            hesabaKatilacakCocukSayisi = FM.CocukSayisi;

            if (FM.Bekar == "Evli")
            {
                if (FM.EsCalisiyor == "ÇalışMIyor")
                {
                    esDurum = 0;
                }
            }

            int yill = tmpYilBas.Year;


            string asgariDonem = "";

            if (tmpYilBas.Month > 6)
            {
                asgariDonem = yill.ToString() + "-2";

            }
            else
            {
                asgariDonem = yill.ToString() + "-1";
            }

       

            foreach (var t2 in this.MaasListe3)
            {

                var deger = asgariListe.Find(o => o.yil == t2.yil);
                Decimal oAykiAsgariUcret = 0;
                oAykiAsgariUcret = Convert.ToDecimal(deger.brut);

                Decimal agii = 0;
                agii = islem2.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                if (deger != null)
                {

                    Decimal mNet = 0;
                    Decimal mBrut = 0;

            

                        mBrut = Convert.ToDecimal(deger.brut) * Convert.ToDecimal(_oran);
                        mNet = islem2.BruttenNetHesapla(mBrut, agii);
                        t2.brutMaas = mBrut;


                        t2.netMaas = mNet;
                        t2.ekBilgi4 = agii;

                   
                }

                
            }

            MaasListe2.Clear();
            foreach (var tt in MaasListe3)
            {
                MaasListe2.Add(tt);
            }

            //MaasListe2 = MaasListe3;

        }


        //Commandler2
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

        public bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        string _hataMesaji = "";
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }

    }
}
