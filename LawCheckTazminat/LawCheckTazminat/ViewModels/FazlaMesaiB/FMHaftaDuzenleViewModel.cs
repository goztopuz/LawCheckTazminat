using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class FMHaftaDuzenleViewModel: ViewModelBase
    {

        FMHesaplaService fmService = new FMHesaplaService();

        public FMHaftaDuzenleViewModel(FazlaMesaiHaftalar _haftaBilgi, FazlaMesai _fmBilgi )
        {
            this.FM = new FazlaMesai();
            this.Hafta = _haftaBilgi;
            this.FM = _fmBilgi;
            this.IzinYazi = "";
            this.Islemde = false;

            IzinKontrol();
        }

        private void IzinKontrol()
        {
            var izinListe = FM.IzinGunleriBilgi.ToList();

            string izinId="";
            var tmpBas = Hafta.BasTarih;
            bool izinvar = false;
            while(tmpBas<= Hafta.BitTarih)
            {
                var kontrol = izinListe.Find(o => o.Tarih == tmpBas);
                if(kontrol != null)
                {
                    izinvar = true;
                    izinId = kontrol.KayitId;

                }
             tmpBas = tmpBas.AddDays(1);

            }

            if(izinvar== true)
            {
                var izin1 = FM.IzinKaytilariBilgi.ToList().Find(o => o.Id == izinId);

                IzinYazi = "Çalışma Haftası İçerisinde İzin Mevcut.\n"
             + izin1.BaslangicTar.ToShortDateString() + izin1.BitisTar.ToShortDateString();
            }

         


        }

        private FazlaMesaiHaftalar _hafta;
        public FazlaMesaiHaftalar Hafta
        {
            get => _hafta;
            set
            {
                _hafta = value;
                OnPropertyChanged();
            }

        }

        private FazlaMesai _fm;
        public FazlaMesai FM
        {
            get => _fm;
            set
            {
                _fm = value;
                OnPropertyChanged();
            }
        }

        private string _izinYazi;
        public string IzinYazi
        {
            get => _izinYazi;
            set
            {
                _izinYazi = value;
                OnPropertyChanged();
            }
        }

        private bool _islemde;
        public bool Islemde
        {
            get => _islemde;
            set
            {
                _islemde = value;
                OnPropertyChanged();
            }
        }


        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            this.Islemde = true;
            // Kaydet İşleminin Hesaplamanın Yapılacağı Bölüm...

            decimal fmMiktar = 0;
            fmMiktar = fmService.FMHesapla(Hafta.FazlaMesaiSozlesme,
               Hafta.FazlaMesaiOHaftadaki, Hafta.BrutUcret);


            var sonuc = FM.FMHaftalarBilgi.ToList().Find(o => o.Id == Hafta.Id);

            //------
            sonuc.MesaiUcret = fmMiktar;
            sonuc.FazlaMesaiSozlesme = Hafta.FazlaMesaiSozlesme;
            sonuc.FazlaMesaiOHaftadaki = Hafta.FazlaMesaiOHaftadaki;

              

            // Maaş Hesaplamaları....
            sonuc.BrutUcret = Hafta.BrutUcret;




            MessagingCenter.Send<FazlaMesai>(FM, "YenileFMHaftalar");
          //  MessagingCenter.Send<FazlaMesaiHaftalar>(sonuc, "YenileFMHaftalar");

            await    Application.Current.MainPage.Navigation.PopModalAsync();

            this.Islemde = false;
            IsBusy = false;

        }





        //-----
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
