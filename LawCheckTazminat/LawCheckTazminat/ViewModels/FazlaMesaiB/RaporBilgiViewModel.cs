 using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class RaporBilgiViewModel : ViewModelBase
    {

        public RaporBilgiViewModel(FazlaMesaiKisiIzinKayitlari _rapor, FazlaMesai _fm)
        {


            this.FM = new FazlaMesai();
            this.FM = _fm;


            if(_rapor.Id != "")
            {
                this.IzinBas = _rapor.BaslangicTar;
                this.IzinBit = _rapor.BitisTar;
                this.Aciklama = _rapor.Aciklama;

            }
            else
            {
                this.IzinBas = _fm.BasTarih;
                this.IzinBit = _fm.BasTarih;
            }
     

            this.RP = new FazlaMesaiKisiIzinKayitlari();
            this.RP = _rapor;


        }

        private FazlaMesaiKisiIzinKayitlari _rp;
        public FazlaMesaiKisiIzinKayitlari RP
        {
            get => _rp;
            set
            {
                _rp = value;
                OnPropertyChanged();
            }
        }


        private FazlaMesai _fmm;
        public FazlaMesai FM
        {
            get => _fmm;
            set
            {
                _fmm = value;
                OnPropertyChanged();
            }
        }

        private DateTime _izinBaslangic;
        public DateTime IzinBas
        {
            get => _izinBaslangic;
            set
            {
                _izinBaslangic = value;
                OnPropertyChanged();
            }
        }


        private DateTime _izinBitis;
        public DateTime IzinBit
        {
            get => _izinBitis;
            set
            {
                _izinBitis = value;
                OnPropertyChanged();
            }

            }


        public string _aciklama;
        public string Aciklama
        {
            get => _aciklama;
            set
            {
                _aciklama = value;
                OnPropertyChanged();
            }

        }


        private bool SayfaKontol()
        {
            bool _durum = true;
            if(this.IzinBas> this.IzinBit)
            {
                this.HataMesaji = "Başlangıç Tarihi Bitiş Tarihinden Sonra Olamaz! ";
                _durum = false;
            }

            return _durum;
        }



        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            // KaydetCommand...

            if(SayfaKontol()== false)
            {
                IsBusy = false;
                return;
            }

            if(RP.Id== null || RP.Id=="")
            {
                // Yeni Kayıt

                FazlaMesaiKisiIzinKayitlari kayitYeni = new FazlaMesaiKisiIzinKayitlari();
                kayitYeni.Id = Guid.NewGuid().ToString().Substring(0, 8);
                kayitYeni.BaslangicTar = IzinBas;
                kayitYeni.BitisTar = IzinBit;
                kayitYeni.Aciklama = Aciklama;

                FM.IzinKaytilariBilgi.Add(kayitYeni);

                DateTime tmpTar = IzinBas;
                while(tmpTar< IzinBit.AddDays(1))
                {
                    FazlaMesaiKisiIzinGunleri izinGun = new FazlaMesaiKisiIzinGunleri();
                    izinGun.Id = Guid.NewGuid().ToString().Substring(0, 17);
                    izinGun.Tarih = tmpTar;
                    izinGun.Aciklama = Aciklama;
                    izinGun.KisiId = FM.KisiId;
                    izinGun.KayitId = kayitYeni.Id;

                    FM.IzinGunleriBilgi.Add(izinGun);

                    tmpTar = tmpTar.AddDays(1);
                }


            }
            else
            {
                // Güncelleme

                var kayit = FM.IzinKaytilariBilgi.ToList().Find(o => o.Id == RP.Id);

                kayit.BaslangicTar = IzinBas;
                kayit.BitisTar = IzinBit;
                kayit.Aciklama = Aciklama;


                // İzin Aralığındaki Günler Temizlendi
                var silL1 = FM.IzinGunleriBilgi.ToList();
                foreach(var t in silL1)
                {
                    if(t.KayitId== kayit.Id)
                    {
                        FM.IzinGunleriBilgi.Remove(t);
                    }
                }

                // İzin Aralığındaki Günler Eklendi.
                DateTime tmpTar = IzinBas;
                while (tmpTar < IzinBit.AddDays(1))
                {
                    FazlaMesaiKisiIzinGunleri izinGun = new FazlaMesaiKisiIzinGunleri();
                    izinGun.Id = Guid.NewGuid().ToString().Substring(0, 12);
                    izinGun.Tarih = tmpTar;
                    izinGun.Aciklama = Aciklama;
                    izinGun.KisiId = FM.KisiId;
                    izinGun.KayitId = kayit.Id;

                    FM.IzinGunleriBilgi.Add(izinGun);

                    tmpTar = tmpTar.AddDays(1);
                }



            }

            this.HataMesaji = "";

            MessagingCenter.Send<FazlaMesai>(FM, "YenileFM");

            await Application.Current.MainPage.Navigation.PopModalAsync();



            IsBusy = false;

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
