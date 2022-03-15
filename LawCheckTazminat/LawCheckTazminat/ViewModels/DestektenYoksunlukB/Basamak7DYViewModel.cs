using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class Basamak7DYViewModel :ViewModelBase
    {
        public Basamak7DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;
            MaasListeOlustur();
            AsgariMaasListeOlustur();

            kkk();
            kkk2();

            if(dyy.Duzenlemede== true)
            {
                EmekliMaas = dyy.emekliMaas;
                IsleyecekDonemMaas = dyy.isleyecekMaas;

            }

            AgiVar = this.AktifDestek.AgiDus;


       
        }
   
        private Models.DestektenYoksunluk _destektenYoksunluk;
        ObservableCollection<Maas>  _maasListe2= new ObservableCollection<Maas>();

        MaasHesaplaService islem2 = new MaasHesaplaService();

        public ObservableCollection<Maas> MaasListe2
        {
            get => _maasListe2;
            set
            {
                _maasListe2 = value;
                OnPropertyChanged();
            }

        }
        ObservableCollection<Maas> _maasListe3 = new ObservableCollection<Maas>();

        public ObservableCollection<Maas> MaasListe3
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
            asgariListe =(await   islemAsgari.GetItemsAsync()).ToList();
            
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
      
        private  void MaasListeOlustur()
        {
            int yilBas = AktifDestek.vefatTarihi.Year;
            int yilBit = AktifDestek.raporTarihi.Year;
            MaasListe2.Clear();
            MaasListe3.Clear();
            int j = 0;
            
            for(int i= yilBas; i<= yilBit; i++)
            {
           
                Maas m = new Maas();
                m.yil = i.ToString() + "-1";
           
                if(j==0)
                {
                    if(AktifDestek.vefatTarihi.Month<7)
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
                if(testDate < AktifDestek.raporTarihi)
                {

                    Maas m2 = new Maas();
                    m2.yil = i.ToString() + "-2";
              
                    MaasListe2.Add(m2);
                    MaasListe3.Add(m2);
                }
            }


            foreach (var t in MaasListe2)
            {
                int yil1 = Convert.ToInt32(  t.yil.Substring(0, 4));
                string bol1 = t.yil.Substring(5, 1);

                if(bol1=="1")
                {
                    t.yilBas =new DateTime(yil1, 1, 1);
                    t.yilBit = new DateTime(yil1, 6, 30);
                }else
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

            if(AktifDestek.Duzenlemede==true)
            {

                foreach(var t in MaasListe2)
                {
                    var kayit = AktifDestek.DestektekYoksunlukMaaslar.ToList().Find(o => o.yil == t.yil);
                    if(kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    } 
                }

                foreach (var t in MaasListe3)
                {
                    var kayit = AktifDestek.DestektekYoksunlukMaaslar.ToList().Find(o => o.yil == t.yil);
                    if (kayit != null)
                    {
                        t.netMaas = kayit.netMaas;
                        t.brutMaas = kayit.brutMaas;
                    }
                }


            }

        }

      

        public Models.DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;

            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
        }

        public ICommand MaasGuncelle => new Command(Guncelle);
        async private void Guncelle()
        {
            //var tmp1 = AktifDestek.DestektekYoksunlukMaaslar;
            ////AktifDestek.DestektekYoksunlukMaaslar.Add(m);
            //var tmp2 = AktifDestek.DestektekYoksunlukMaaslar;
            //tmp1[1].brutMaas = tmp1[1].brutMaas + 100;

            //AktifDestek.DestektekYoksunlukMaaslar = tmp1;

            Maas m= new Maas();
            m.Id = "sil";
            AktifDestek.DestektekYoksunlukMaaslar.Add(m);

            var aa=   AktifDestek.DestektekYoksunlukMaaslar.Where(o => o.Id == "sil").FirstOrDefault();
            AktifDestek.DestektekYoksunlukMaaslar.Remove(aa);


        }


   
        public ICommand CellGuncelle => new Command<MaasCell>(OnMaasGuncelle);

        async private void OnMaasGuncelle(MaasCell mc)
        {
            
           
            if(mc.sutun==2)
            {
                AktifDestek.DestektekYoksunlukMaaslar[mc.satır].brutMaas = Convert.ToDecimal(mc.deger);
            }else if( mc.sutun==3)
            {
                AktifDestek.DestektekYoksunlukMaaslar[mc.satır].netMaas = Convert.ToDecimal(mc.deger);
            }
            Maas m = new Maas();
            m.Id = "sil";
            AktifDestek.DestektekYoksunlukMaaslar.Add(m);

            var aa = AktifDestek.DestektekYoksunlukMaaslar.Where(o => o.Id == "sil").FirstOrDefault();
            AktifDestek.DestektekYoksunlukMaaslar.Remove(aa);


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

        public ICommand BekarDurumKontrolCommand => new Command(OnBekarDurumKontrol);

        private async void OnBekarDurumKontrol(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            if (AktifDestek.BekarCocukDurum == true)
            {
                IlerlemeIslemi();

            }
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
            switch(MaasSecim)
            {
                case "":
                    break;

                case "Asgari Ücret Olarak Belirle":
                    AsgariOlarakAta();
                    //var cevap1 = await Application.Current.MainPage.DisplayActionSheet("Maaşlar Asgari Ücret Olarak Atanacaktır. Eminmisiniz?", "İptal", "", "Asgari Olarak Ata");

                    //if (cevap1 == "Asgari Olarak Ata")
                    //{
                    //    AsgariOlarakAta();
                    //}
                    Guncelle();

                    break;

                case "NET Maaşı Asg. Ücrete Oranla":

                    var sayfa = new MaasOranlaView(AktifDestek, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
                    break;

                case "BRÜT Maaşı Asg. Ücrete Oranla":     
                    var sayfa2 = new MaasOranlaBrutView(AktifDestek, asgariListe);
                    await Application.Current.MainPage.Navigation.PushModalAsync(sayfa2);
                    break;

               default:
                    break;

            }

        }

        public ICommand BrutOranlaCommand => new Command(OnBrutOranla);
        async private void OnBrutOranla(object obj)
        {
            var sayfa = new MaasOranlaBrutView(AktifDestek, asgariListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
        }

        public ICommand NetOranlaCommand => new Command(OnNetOranla);
        async private void OnNetOranla(object obj)
        {
            var sayfa = new MaasOranlaView(AktifDestek, asgariListe);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

        }

        public ICommand AsgariBelirleCommand => new Command(OnAsgariBelirle);
        async  private void OnAsgariBelirle(object obj)
        {
            var cevap1 =await  Application.Current.MainPage.DisplayActionSheet("Maaşlar Asgari Ücret Olarak Atanacaktır. Eminmisiniz?", "İptal", "", "Asgari Olarak Ata");

            if(cevap1== "Asgari Olarak Ata")
            {
                AsgariOlarakAta();
            }
            Guncelle();
        }

        public ICommand KaydetBitirCommand => new Command(OnKaydetBitir);

        DestektenYoksunlukService islem = new DestektenYoksunlukService();
        private async void OnKaydetBitir(object obj)
        {


            if (EksikKontrol() == true)
            {
                return;
            }

            IlerlemeIslemi();
            return;

            // DESTEKTEN YOKSUNLUK KAYDET-GÜNCELLE
            AktifDestek.DestektekYoksunlukMaaslar = MaasListe2;
            AktifDestek.emekliMaas = EmekliMaas;
            AktifDestek.isleyecekMaas = IsleyecekDonemMaas;


            
            foreach(var t2 in MaasListe2)
            {
                if( t2.Id==null)
                {
                    t2.Id = Guid.NewGuid().ToString().Substring(0, 10);
                }
            }
        
            
            
            if(AktifDestek.Id==null)
            {
                
             AktifDestek.Id = Guid.NewGuid().ToString().Substring(0, 10);
           

                foreach(var t in AktifDestek.DestektekYoksunlukMaaslar)
                {
                    t.dosyaId = AktifDestek.Id;
                }
                foreach(var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    t2.dosyaId = AktifDestek.Id;
                }
                foreach(var t3 in AktifDestek.DestekYoksunlukYakinlar)
                {
                    t3.dosyaId = AktifDestek.Id;
                }


                await islem.AddItemAsync(AktifDestek);

            }
            else
            {

                foreach (var t in AktifDestek.DestektekYoksunlukMaaslar)
                {
                    t.dosyaId = AktifDestek.Id;
                }
                foreach (var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    t2.dosyaId = AktifDestek.Id;
                }
                foreach (var t3 in AktifDestek.DestekYoksunlukYakinlar)
                {
                    t3.dosyaId = AktifDestek.Id;
                }

                await islem.UpdateItemAsync(AktifDestek);

           
            }

            var sayfa = new DestekSon(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

        }
      

       

        private async void  IlerlemeIslemi()
        {


            AktifDestek.DestektekYoksunlukMaaslar = MaasListe2;
            AktifDestek.emekliMaas = EmekliMaas;
            AktifDestek.isleyecekMaas = IsleyecekDonemMaas;


            foreach (var t2 in MaasListe2)
            {
                if (t2.Id == null)
                {
                    t2.Id = Guid.NewGuid().ToString().Substring(0, 10);
                }
            }



            if (AktifDestek.Id == null)
            {

                AktifDestek.Id = Guid.NewGuid().ToString().Substring(0, 10);


                foreach (var t in AktifDestek.DestektekYoksunlukMaaslar)
                {
                    t.dosyaId = AktifDestek.Id;
                }
                foreach (var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    t2.dosyaId = AktifDestek.Id;
                }
                foreach (var t3 in AktifDestek.DestekYoksunlukYakinlar)
                {
                    t3.dosyaId = AktifDestek.Id;
                }


                await islem.AddItemAsync(AktifDestek);

            }
            else
            {

                foreach (var t in AktifDestek.DestektekYoksunlukMaaslar)
                {
                    t.dosyaId = AktifDestek.Id;
                }
                foreach (var t2 in AktifDestek.DosyaDestektenYoksunlukMasraf)
                {
                    t2.dosyaId = AktifDestek.Id;
                }
                foreach (var t3 in AktifDestek.DestekYoksunlukYakinlar)
                {
                    t3.dosyaId = AktifDestek.Id;
                }

                await islem.UpdateItemAsync(AktifDestek);


            }

            var sayfa = new DestekSon(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



        }
        
        
        
        string durum = "";
        public void kkk()
        {
            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaNetAsgari", async (aa) =>
            {
                // NetAsgariOranla(aa);
                BrutAsgariOranla(aa);
                durum = "net";
            });

        }
        public void kkk2()
        {
            
            MessagingCenter.Subscribe<AsgariOran>(this, "OranlaBrutAsgari", async (aa) =>
            {
                BrutAsgariOranla(aa);
                durum = "brut";
            });

        }


        private void BrutAsgariOranla(AsgariOran ao)
        {
            decimal _oran = Convert.ToDecimal(ao.oran);


            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 1;


            int yill =AktifDestek.vefatTarihi.Year;


            string asgariDonem = "";

            if (AktifDestek.vefatTarihi.Month > 6)
            {
                asgariDonem = yill.ToString() + "-2";

            }
            else
            {
                asgariDonem = yill.ToString() + "-1";
            }

            foreach (var t in this.AktifDestek.DestekYoksunlukYakinlar)
            {
                if(t.yakinlik =="Eş")
                {
                    if(t.esCalisiyorMu=="Hayır")
                    {
                        esDurum = 0;
                    }
                }
                else if(t.yakinlik=="Çocuk")
                {
                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                }
            }

            foreach(var t2 in this.MaasListe3)
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



                    //mNet = islem2.BruttenNetHesapla(mBrut, agii);
                    t2.brutMaas = mBrut;


                        t2.netMaas = mNet;
                        t2.ekBilgi4 = 0;

                    

                

                }

                IsleyecekDonemMaas = t2.netMaas;
                EmekliMaas = deger.net;
            }


            MaasListe2.Clear();

            foreach(var t3 in  MaasListe3)
            {
                MaasListe2.Add(t3);
            }

          //  MaasListe2 = MaasListe3;

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

            foreach(var t2 in MaasListe3)
            {
                MaasListe2.Add(t2);
                
            }


            //MaasListe2 = MaasListe3;

        }

        private void AsgariOlarakAta()
        {

            int hesabaKatilacakCocukSayisi = 0;
            int esDurum = 1;

            Decimal hesaplanacakMaas = 0;


            foreach (var t in this.AktifDestek.DestekYoksunlukYakinlar)
            {
                if (t.yakinlik == "Eş")
                {
                    if (t.esCalisiyorMu == "Hayır")
                    {
                        esDurum = 0;
                    }
                }
                else if (t.yakinlik == "Çocuk")
                {
                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
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
                hesaplanacakMaas =t.netMaas;
                IsleyecekDonemMaas = hesaplanacakMaas;
                EmekliMaas = hesaplanacakMaas;
            }
            MaasListe2.Clear();

            foreach(var t1 in MaasListe3)
            {
                MaasListe2.Add(t1);
            }

         //   MaasListe2 = MaasListe3;
        }


        private bool EksikKontrol()
        {
            bool _eksikVar = false;

            if(IsleyecekDonemMaas==0)
            {
                _eksikVar = true;
                HataMesaji = "İşleyecek Maaş Girilmelidir.";
               
            }else if(EmekliMaas==0)
            {
                _eksikVar = true;
                HataMesaji = "Emekli Dönem Maaşı Girilmelidir.";
            }


            foreach(var t in AktifDestek.DestektekYoksunlukMaaslar)
            {
                if(t.netMaas ==0)
                {
                    _eksikVar = true;
                    HataMesaji = t.yil + " Dönemindeki Net Maaş Girilmelidir.";
                }
            }

            return _eksikVar;
            
        }

    }
}
