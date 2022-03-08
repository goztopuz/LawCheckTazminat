using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak9FMViewModel : ViewModelBase
    {

        LawCheckTazminat.Services.ResmiTatilService islem = new Services.ResmiTatilService();

        List<MaasFazlaMesai> MaasListe = new List<MaasFazlaMesai>();

        public Basamak9FMViewModel(FazlaMesai _fmm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fmm;

            Liste = new List<TatilBilgi>();
            GenelListe = new ObservableCollection<TatilBilgi>();
            Liste2 = new ObservableCollection<FazlaMesaiResmiTatil>();

            Baslangic = this.FM.BasTarihResmiTatil;
            Bitis = this.FM.BitTarihResmiTatil;
            MaasListe = this.FM.MaasBilgi.ToList();

            TarihAraligindakiIzinler();
            
        }


        private decimal _toplam;
        public decimal Toplam
        {
            get => _toplam;
            set
            {
                _toplam = value;
                OnPropertyChanged();
            }
        }

        private List<TatilBilgi> _liste;
        public List<TatilBilgi> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TatilBilgi> _genelListe;
        public ObservableCollection<TatilBilgi> GenelListe
        {
            get => _genelListe;
            set
            {
                _genelListe = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<FazlaMesaiResmiTatil> _liste2;
      public ObservableCollection<FazlaMesaiResmiTatil> Liste2
        {
            get => _liste2;
            set
            {
                _liste2 = value;
                OnPropertyChanged();
            }
        }


        async private void TarihAraligindakiIzinler()
        {

            var tumResmiTatiller = await islem.GetItemS();


            foreach (var t in tumResmiTatiller)
            {
                if (t.tarih >= Baslangic && t.tarih <= Bitis)
                {
                    TatilBilgi tt2 = new TatilBilgi();
                    tt2.Idd = Guid.NewGuid().ToString().Substring(0, 9);
                    tt2.Tarih = t.tarih;
                    tt2.Aciklama = t.aciklama;

                    if (t.tur == 1)
                    {
                        if (FM.TumResmiTatllerdeCalisti == true)
                        {
                            tt2.Secili = true;
                        }

                    }
                    else if (t.tur == 2)
                    {
                        if (FM.TumDiniBayramlardaCalisti == true)
                        {
                            tt2.Secili = true;
                        }
                    }

                    Liste.Add(tt2);

                }

            }

           var aa= Liste.OrderByDescending(o => o.Tarih);

            foreach(var t in aa)
            {
                string yilYazi = "";
                yilYazi = t.Tarih.Date.Year.ToString() + "-";

                string bolum2 = "1";
                if (t.Tarih.Date.Month > 6)
                {
                    bolum2 = "2";
                }
                yilYazi = yilYazi + bolum2;

                var maas = MaasListe.Find(o => o.yil == yilYazi).brutMaas;

                decimal gunlukUcret = (maas / 30);
                decimal tatilUcreti = ((decimal)gunlukUcret) *((decimal) 1.5);
                t.Miktar1 = tatilUcreti;

                GenelListe.Add(t);
            }


        }


        private DateTime _baslangic;
        public DateTime Baslangic
        {
            get => _baslangic;
            set
            {
                _baslangic = value;
                OnPropertyChanged();
            }
        }

        private DateTime _bitis;
        public DateTime Bitis
        {
            get => _bitis;
            set
            {
                _bitis = value;
                OnPropertyChanged();
            }
        }


        private FazlaMesai _fazlamesai;
        public FazlaMesai FM
        {
            get => _fazlamesai;
            set
            {
                _fazlamesai = value;
                OnPropertyChanged();

            }
        }

        // İlerle Command
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            //-------
            Toplaa();
            var sayfa10 = new Basamak10FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa10);

         //   Liste2 = FM.ResmiTatilBilgi;



            IsBusy = false;
        }


        private void Toplaa()
        {
            FM.ResmiTatilBilgi.Clear();
            decimal _araToplam = 0;
            foreach (var t in Liste)
            {

                if (t.Secili == true)
                {
                    // Toplam
                    string yilYazi = "";
                    yilYazi = t.Tarih.Date.Year.ToString() + "-";

                    string bolum2 = "1";
                    if(t.Tarih.Date.Month>6)
                    {
                        bolum2 = "2";
                    }
                    yilYazi = yilYazi + bolum2;

                    var maas = MaasListe.Find(o => o.yil == yilYazi).brutMaas;

                    decimal gunlukUcret = (maas / 30);
                    decimal tatilUcreti = gunlukUcret * ((decimal)gunlukUcret);

                    FazlaMesaiResmiTatil fmResmi = new FazlaMesaiResmiTatil();
                    fmResmi.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    fmResmi.Aciklama = t.Aciklama;
                    fmResmi.tarih = t.Tarih;
                    fmResmi.Miktar = t.Miktar1;
                    fmResmi.AktifDurumu = 1;

                    _araToplam = _araToplam + tatilUcreti;
                    
                    FM.ResmiTatilBilgi.Add(fmResmi);


                    
                }

                Toplam = _araToplam;
            }


        }


        public ICommand YenidenToplaCommand => new Command(OnYenidenTopla);
        async private void OnYenidenTopla(object obj)
        {
            Toplaa();
        }

        // ----------------------------
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
    public class TatilBilgi
    {
        public string Idd { get; set; }
        public bool Secili { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }

        public decimal Miktar1 { get; set; }

    }
}
