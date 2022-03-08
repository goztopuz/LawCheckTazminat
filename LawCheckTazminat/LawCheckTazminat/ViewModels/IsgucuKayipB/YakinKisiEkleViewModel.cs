using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class YakinKisiEkleViewModel:ViewModelBase
    {

        string yakinDurum = "";
        public YakinKisiEkleViewModel(IsgucuKayip _kayip, YakinIsgucu _yy, ObservableCollection<YakinIsgucu> Liste)
        {

            this.IsGucu = new IsgucuKayip();
            this.IsGucu = _kayip;

            this.Yakin = new YakinIsgucu();
            this.Yakin = _yy;

            Cinsiyet = this.Yakin.cinsiyet;
            EsCalisma = this.Yakin.esCalisiyorMu;


            if (_yy.yakinlik == "Eş")
            {
                yakinDurum = "Eş";
            }
            if (this.Yakin.soyad == null)
            {
                this.Yakin.soyad = IsGucu.soyad;
            }
            if (this.Yakin.soyad == "")
            {
                this.Yakin.soyad = IsGucu.soyad;
            }

            YakinListe = Liste;
            if (this.Yakin.dogumTarihi != null)
            {
                if (this.Yakin.dogumTarihi.Year != 1)
                {
                    this.DogumGun = this.Yakin.dogumTarihi.Day;
                    this.DogumAy = this.Yakin.dogumTarihi.Month;
                    this.DogumYil = this.Yakin.dogumTarihi.Year;

                    this._dogumGun2 = this.Yakin.dogumTarihi.Day.ToString();
                    this._dogumAy2 = this.Yakin.dogumTarihi.Month.ToString();


                }

            }




        }


       private IsgucuKayip _isgucukayip;

        public IsgucuKayip IsGucu
        {
            get => _isgucukayip;
            set
            {
                _isgucukayip = value;
                OnPropertyChanged();
            }
        }

        private YakinIsgucu _yakin;

        public YakinIsgucu Yakin
        {
            get => _yakin;
            set
            {
                _yakin = value;
                OnPropertyChanged();
            }

        }

        private ObservableCollection<YakinIsgucu> _yakinListe;
        public ObservableCollection<YakinIsgucu> YakinListe
        {
            get => _yakinListe;
            set
            {
                _yakinListe = value;
                OnPropertyChanged();
            }

        }


        string _cinsiyet;
        public string Cinsiyet
        {
            get => _cinsiyet;
            set
            {
                _cinsiyet = value;
                OnPropertyChanged();
            }
        }

        string _esCalisma;
        public string EsCalisma
        {
            get => _esCalisma;
            set
            {
                _esCalisma = value;
                OnPropertyChanged();
            }
        }

        int _dogumGun;
        public int DogumGun
        {
            get => _dogumGun;
            set
            {
                _dogumGun = value;
                OnPropertyChanged();
            }
        }

        string _dogumGun2;
        public string DogumGun2
        {
            get => _dogumGun2;
            set
            {
                _dogumGun2 = value;
                OnPropertyChanged();
            }
        }

        int _dogumAy;
        public int DogumAy
        {
            get => _dogumAy;
            set
            {
                _dogumAy = value;
                OnPropertyChanged();
            }
        }

        string _dogumAy2;
        public string DogumAy2
        {
            get => _dogumAy2;
            set
            {
                _dogumAy2 = value;
                OnPropertyChanged();
            }
        }

        int _dogumYil;
        public int DogumYil
        {

            get => _dogumYil;
            set
            {
                _dogumYil = value;

            }
        }



        private bool SayfaKontrol()
        {
            bool durum = true;
            // Yakınlık seçimi

            if (Yakin.yakinlik == "")
            {
                durum = false;
                this.HataMesaji = "Yakınlık Seçilmelidir";
            }


            //kişi Adı
            if (Yakin.ad == "")
            {
                durum = false;
                this.HataMesaji = "Kişi Adı Girilmelidir";
            }


            // ÇocukDoğum Tarihi Anne veya Babadan daha Büyük olma durumu
            if (Yakin.yakinlik == "Çocuk")
            {
                if (Yakin.dogumTarihi <= IsGucu.dogumTarihi)
                {
                    durum = false;

                    this.HataMesaji = "Doğum Tarihi Babadan Büyük Olamaz";

                }
                var kisi2 = IsGucu.IsGucuKayipYakinlar.Where(o => o.yakinlik == "Eş").FirstOrDefault();
                if (kisi2 != null)
                {
                    if (Yakin.dogumTarihi < kisi2.dogumTarihi)
                    {
                        durum = false;
                        this.HataMesaji = "Çocu Doğum Tarihi Eşin Doğum Tarihinden Küçük Olamaz";
                    }
                }
            }


            // 2.Eş ve Eşin  yaşının Çocuklardan küçük girilmesi Kontrolü
            if (Yakin.yakinlik == "Eş")
            {
                foreach (var t in IsGucu.IsGucuKayipYakinlar)
                {
                    if (t.yakinlik == "Çocuk")
                    {
                        if (t.dogumTarihi > Yakin.dogumTarihi)
                        {
                            durum = false;
                            this.HataMesaji = "Eşin Yaşı Çocuğun Yaşından Küçük";
                        }
                    }
                }


                if (yakinDurum != "Eş")
                {
                    var kisi2 = IsGucu.IsGucuKayipYakinlar.
                        Where(o => o.yakinlik == "Eş").FirstOrDefault();

                    if (kisi2 != null)
                    {
                        durum = false;
                        this.HataMesaji = "Eş Bilgisi Girilmiş. 2. eş bilgisi girilemez";
                    }
                }
            }



            // 


            return durum;
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

        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var dTar = new DateTime(DogumYil, DogumAy, DogumGun);
                Yakin.dogumTarihi = dTar;
            }
            catch
            {
                this.HataMesaji = "Doğum Tarihi Hatası";
                this.IsBusy = false;
                return;
            }

            Yakin.cinsiyet = Cinsiyet;
            Yakin.esCalisiyorMu = EsCalisma;


            if (SayfaKontrol() == false)
            {
                this.IsBusy = false;
                return;
            }



            // Ekleme KodlarıEklenecek...
            if (Yakin.Id == null || Yakin.Id == null)
            {
                Yakin.Id = Guid.NewGuid().ToString().Substring(0, 8).ToString();
                YakinListe.Add(Yakin);

            }
            else
            {
                //YakinIsgucu yakinn = IsGucu.IsGucuKayipYakinlar.ToList().Find(o => o.Id == Yakin.Id).FirstOrDefault();

                YakinIsgucu yakinn = YakinListe.ToList().Find(o => o.Id == Yakin.Id);

                yakinn = Yakin;

            }
            
            MessagingCenter.Send<ObservableCollection<YakinIsgucu>>(YakinListe, "YenileYakin");

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;


        }

    }
}
