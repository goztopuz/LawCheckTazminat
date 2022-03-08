using System;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class FMHaftalikIzinViewModel : ViewModelBase
    {

        FMHesaplaService fmService = new FMHesaplaService();


        public FMHaftalikIzinViewModel(FazlaMesaiHaftalikIzinHaftalar _hafta, FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;

            this.Hafta = new FazlaMesaiHaftalikIzinHaftalar();
            this.Hafta = _hafta;

            this.IzinYazi = "";
            IzinKontrol();
               

        }

        private void IzinKontrol()
        {
            var izinListe = FM.IzinGunleriBilgi.ToList();

            string izinId = "";
            var tmpBas = Hafta.BasTarih;
            bool izinvar = false;
            while (tmpBas <= Hafta.BitTarih)
            {
                var kontrol = izinListe.Find(o => o.Tarih == tmpBas);
                if (kontrol != null)
                {
                    izinvar = true;
                    izinId = kontrol.KayitId;

                }
                tmpBas = tmpBas.AddDays(1);

            }

            if (izinvar == true)
            {
                var izin1 = FM.IzinKaytilariBilgi.ToList().Find(o => o.Id == izinId);

                IzinYazi = "Çalışma Haftası İçerisinde İzin Mevcut.\n"
             + izin1.BaslangicTar.ToShortDateString() + izin1.BitisTar.ToShortDateString();
            }




        }

        private FazlaMesaiHaftalikIzinHaftalar _hafta;
        public FazlaMesaiHaftalikIzinHaftalar Hafta
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

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            // Kaydet İşleminin Hesaplamanın Yapılacağı Bölüm...
            decimal fmMiktar = 0;

            fmMiktar = fmService.HaftalikTatilCalismaHesapla(Hafta.BrutUcret);

            var sonuc = FM.HaftalikIzinHaftalarBilgi.Where(o => o.Id == Hafta.Id).FirstOrDefault();

            sonuc.BrutUcret = Hafta.BrutUcret;
            sonuc.IzinGunu = Hafta.IzinGunu;
            sonuc.HaftaSonuUcret = fmMiktar;


            DayOfWeek _aa;
            if (sonuc.IzinGunu == "Pazartersi")
            {
                _aa = DayOfWeek.Monday;
            }
            else if (sonuc.IzinGunu == "Salı")
            {
                _aa = DayOfWeek.Tuesday;
            }
            else if (sonuc.IzinGunu == "Çarşamba")
            {
                _aa = DayOfWeek.Wednesday;
            }
            else if (sonuc.IzinGunu == "Perşembe")
            {
                _aa = DayOfWeek.Thursday;
            }
            else if (sonuc.IzinGunu == "Cuma")
            {
                _aa = DayOfWeek.Friday;
            }
            else if (sonuc.IzinGunu == "Cumartesi")
            {
                _aa = DayOfWeek.Saturday;
            }
            else if (sonuc.IzinGunu == "Pazar")
            {
                _aa = DayOfWeek.Sunday;
            }
            else
            {
                _aa = DayOfWeek.Sunday;
            }



            DateTime tmpBaslangcc = sonuc.BasTarih;

            while (tmpBaslangcc < sonuc.BitTarih)
            {
                if (tmpBaslangcc.DayOfWeek == _aa)
                {
                    sonuc.HaftalikIzinTarihi = tmpBaslangcc;

                }
                tmpBaslangcc = tmpBaslangcc.AddDays(1);
            }




            MessagingCenter.Send<FazlaMesai>(FM, "YenileFMHaftalikIzin");

            await Application.Current.MainPage.Navigation.PopModalAsync();


            IsBusy = false;

        }



        //-----------------------------------------
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
