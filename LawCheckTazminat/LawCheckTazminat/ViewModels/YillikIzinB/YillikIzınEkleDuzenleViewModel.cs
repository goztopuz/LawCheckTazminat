using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class YillikIzınEkleDuzenleViewModel : ViewModelBase
    {
        public YillikIzınEkleDuzenleViewModel(YillikIzinIzinKayitlari _kk, YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;
            this.Kayit = new YillikIzinIzinKayitlari();
            this.Kayit = _kk;

            if (_kk.Id == "")
            {
                this.Kayit.BaslangicTar = _yillik.HesapBaslangicTar;
                this.Kayit.BitisTar = _yillik.HesapBaslangicTar;
            }

        }

        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }

        private YillikIzinIzinKayitlari _kayit;
        public YillikIzinIzinKayitlari Kayit
        {
            get => _kayit;
            set
            {
                _kayit = value;
                OnPropertyChanged();
            }
        }

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet()
        {

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            // Kaydet

            var aa = await TarihKontrol(Kayit.BaslangicTar, Kayit.BitisTar);

            if(aa== false)
            {
                IsBusy = false;
                return;
            }


            if (this.Kayit.Id == "" || this.Kayit==null)
            {
                // Yeni Ekleme Komutu.....

                this.Kayit.Id = Guid.NewGuid().ToString().Substring(0, 8);

                YY.IzinKaytilariBilgi.Add(Kayit);

                DateTime tmpTar = Kayit.BaslangicTar;
                while (tmpTar < Kayit.BitisTar.AddDays(1))
                {
                    YillikIzinIzinGunleri izinGun = new YillikIzinIzinGunleri();
                    izinGun.Id = Guid.NewGuid().ToString().Substring(0, 17);
                    izinGun.Tarih = tmpTar;
                    izinGun.Aciklama = Kayit.Aciklama;
                    izinGun.KisiId = YY.kisiId;
                    izinGun.KayitId = Kayit.Id;
                    izinGun.GunAdi = tmpTar.DayOfWeek.ToString();

                    YY.IzinGunleriBilgi.Add(izinGun);

                    tmpTar = tmpTar.AddDays(1);
                }


            }
            else
            {
                // Güncelleme Komutu....

                var sonuc = YY.IzinKaytilariBilgi.Where(o => o.Id == Kayit.Id).FirstOrDefault();

                sonuc.BaslangicTar = Kayit.BaslangicTar;
                sonuc.BitisTar = Kayit.BitisTar;
                sonuc.Aciklama = Kayit.Aciklama;

                var sil1 = YY.IzinGunleriBilgi.ToList();
                foreach(var t2 in sil1)
                {
                    if(t2.KayitId== t2.Id)
                    {

                        YY.IzinGunleriBilgi.Remove(t2);
                    }
                }

                DateTime tmpTar = Kayit.BaslangicTar;
                while (tmpTar < Kayit.BitisTar.AddDays(1))
                {
                    YillikIzinIzinGunleri izinGun = new YillikIzinIzinGunleri();
                    izinGun.Id = Guid.NewGuid().ToString().Substring(0, 17);
                    izinGun.Tarih = tmpTar;
                    izinGun.Aciklama = Kayit.Aciklama;
                    izinGun.KisiId = YY.kisiId;
                    izinGun.KayitId = Kayit.Id;
                    izinGun.GunAdi = tmpTar.DayOfWeek.ToString();

                    YY.IzinGunleriBilgi.Add(izinGun);

                    tmpTar = tmpTar.AddDays(1);
                }


            }

            MessagingCenter.Send<YillikIzin>(YY, "YenileYillikIzinBilgi");

            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;

        }


        async private Task<bool> TarihKontrol(DateTime t1, DateTime t2)
        {
            bool durum = true;

            string tmpCakisanId = "";
            if(t2.Date<t1.Date)
            {
                this.HataMesaji = "İzin Bitişi Başlangıncından Önce Olamaz";
                return false;
            }
            if(t1.Date< YY.IseGirisTarihi.Date)
            {
                this.HataMesaji = "İzin Tarihi - İşe Giriş Tarihinden Önce Olamaz";
                return false;
            }


            foreach (var t in YY.IzinGunleriBilgi)
            {
                if (t.Tarih.Date >= t1.Date && t.Tarih.Date <= t2.Date)
                {
                    durum = false;
                    tmpCakisanId = t.KayitId;
                }
                
            }

            if(durum== false)
            {
                string yazii = "";

                var k1 = YY.IzinKaytilariBilgi.Where(o => o.Id == tmpCakisanId).FirstOrDefault();

                if (k1 != null)
                {
                    yazii = "Girilen İzin (" + k1.BaslangicTar.ToShortDateString() + " - "
                        + k1.BitisTar.ToShortDateString() + ")  İzniyle Çakışma Göstermektedir!! Devam Etmek İstiyormusunuz?";
                }

                bool sonuc = await Application.Current.MainPage.DisplayAlert("Tarih Aralığı Çakışması", yazii, "Evet", "Hayır");

                if(sonuc==true)
                {
                    durum = true;
                }

            }

            return durum;

        }




        //-----------------------------
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
