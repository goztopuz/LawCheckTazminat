using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class YakinKisiEkleViewModel : ViewModelBase
    {
        private Models.DestektenYoksunluk _destektenYoksunluk;
        private Models.Yakin _yakin;

        string yakinDurum="";

        public YakinKisiEkleViewModel(ObservableCollection<Yakin> Liste, Yakin _yy, DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;
           
            
            this.Yakin = new Yakin();
            this.Yakin = _yy;

       

            Cinsiyet = this.Yakin.cinsiyet;
            EsCalisma = this.Yakin.esCalisiyorMu;

            KisiListe = Liste;

             if(this.Yakin.soyad==null)
            {
                this.Yakin.soyad = AktifDestek.soyad;
             }
             if(this.Yakin.soyad=="")
            {
                this.Yakin.soyad = AktifDestek.soyad;
            }

            if(this.Yakin.dogumTarihi != null)
            {
                if(this.Yakin.dogumTarihi.Year !=1)
                {
                    this.DogumGun = this.Yakin.dogumTarihi.Day;
                    this.DogumAy = this.Yakin.dogumTarihi.Month;
                    this.DogumYil = this.Yakin.dogumTarihi.Year;

                    this._dogumGun2 = this.Yakin.dogumTarihi.Day.ToString();
                    this._dogumAy2 = this.Yakin.dogumTarihi.Month.ToString();


                }

            }

          if(_yy.yakinlik=="Eş")
            {
                yakinDurum = "Eş";
            }
        
        }

        
        public Models.Yakin Yakin
        {
            get => _yakin;

            set
            {
                _yakin = value;

                OnPropertyChanged();
            }

        }

        ObservableCollection<Yakin> _kisiListe;
        public ObservableCollection<Yakin> KisiListe
        {
            get => _kisiListe;

            set
            {
                _kisiListe = value;
                OnPropertyChanged();
            }
        }

        DestektenYoksunluk _aktifDestek;
        public DestektenYoksunluk AktifDestek
        {
            get => _aktifDestek;
            set
            {
                _aktifDestek = value;
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


        public bool _elleGirVisible;
        public bool ElleGirVisible
        {
            get => _elleGirVisible;
            set
            {
                _elleGirVisible = value;
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

        private bool SayfaKontrol()
        {
            bool durum = true;
            // Yakınlık seçimi

            if (Yakin.yakinlik=="" || Yakin.yakinlik== null)
            {
                durum = false;
                this.HataMesaji = "Yakınlık Seçilmelidir";
                return durum;
            }


            //kişi Adı
            if(Yakin.ad=="")
            {
                durum = false;
                this.HataMesaji = "Kişi Adı Girilmelidir";

                return durum;
            }

            
            // ÇocukDoğum Tarihi Anne veya Babadan daha Büyük olma durumu
            if(Yakin.yakinlik=="Çocuk")
            {
                if (Yakin.dogumTarihi <= AktifDestek.dogumTarihi)
                {
                    durum = false;

                    this.HataMesaji = "Çocuk Doğum Tarihi  Daha Büyük Olamaz";

                    return durum;

                }             
                var kisi2 = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.yakinlik == "Eş").FirstOrDefault();
                if(kisi2!= null)
                {
                    if(Yakin.dogumTarihi<kisi2.dogumTarihi)
                    {
                        durum = false;
                        this.HataMesaji = "Çocuk Doğum Tarihi Eşin Doğum Tarihinden Küçük Olamaz";
                        return durum;
                    }
                }
            }


            // 2.Eş ve Eşin  yaşının Çocuklardan küçük girilmesi Kontrolü
            if(Yakin.yakinlik=="Eş")
            {
               foreach(var t in AktifDestek.DestekYoksunlukYakinlar)
                {
                    if(t.yakinlik=="Çocuk")
                    {
                        if(t.dogumTarihi< Yakin.dogumTarihi)
                        {
                            durum = false;
                            this.HataMesaji = "Eşin Yaşı Çocuğun Yaşından Küçük";
                            return durum;
                        }
                    }
                }


                if (yakinDurum != "Eş")
                {
                    var kisi2 = AktifDestek.DestekYoksunlukYakinlar.
                        Where(o => o.yakinlik == "Eş").FirstOrDefault();

                    if (kisi2 != null)
                    {
                        durum = false;
                        this.HataMesaji = "Eş Bilgisi Girilmiş. 2. eş bilgisi girilemez";
                        return durum;
                    }
                }

            }


            if (Yakin.yakinlik.ToLower() == "anne")
            {
                var kisi2 = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.yakinlik == "Anne").FirstOrDefault();
                if (kisi2 != null)
                {
                    durum = false;
                    this.HataMesaji = "Kaytılı Anne Bilgisi Mevcut";
                    return durum;
                }
            }

            if (Yakin.yakinlik.ToLower() == "baba")
            {
                var kisi2 = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.yakinlik == "Baba").FirstOrDefault();
                if (kisi2 != null)
                {
                    durum = false;
                    this.HataMesaji = "Kayıtlı Baba Bilgisi Mevcut";

                    return durum;
                }
            }



            return durum;
        }

        public ICommand KaydetCommand => new Command(OnKaydet);

        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            //try
            //{
            //    var dTar = new DateTime(DogumYil, DogumAy, DogumGun);
            //    Yakin.dogumTarihi = dTar;

                
            //}
            //catch
            //{
            //    this.IsBusy = false;
            //    this.HataMesaji = "Doğum Tarihi Hatası";
            //    return;
            //}

            Yakin.cinsiyet = Cinsiyet;
            Yakin.esCalisiyorMu = EsCalisma;


           if( SayfaKontrol()== false)
            {
                this.IsBusy = false;
                return;
            }

            // Ekleme KodlarıEklenecek...
            if (Yakin.Id==null || Yakin.Id== "")
            {
                Yakin.Id = Guid.NewGuid().ToString().Substring(0, 8).ToString();
                KisiListe.Add(Yakin);

            }else
            {
                Yakin yakinn = KisiListe.Where(o => o.Id == Yakin.Id).FirstOrDefault();
                yakinn = Yakin;
            }

            MessagingCenter.Send<ObservableCollection<Yakin>>(KisiListe, "Yenile");

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;


        }

    }
}
