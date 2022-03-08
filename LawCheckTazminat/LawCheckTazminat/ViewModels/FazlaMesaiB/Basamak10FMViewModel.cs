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
    public class Basamak10FMViewModel : ViewModelBase
    {
        public Basamak10FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;

            ListeResmi = new ObservableCollection<IzinCakisanGun>();
            ListeHaftalik = new ObservableCollection<IzinCakisanGun>();
            ListeHaftaResmi = new ObservableCollection<IzinCakisanGun>();

            ResmiListeOlustur();
            HaftalikListeOIustur();
            ResmiTatilHaftalikOlustur();

            ResmiTatilYazi = false;
            HaftalikYazi = false;
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

        private bool _resmitatilYaziVisible = false;
        public bool ResmiTatilYazi
        {
            get => _resmitatilYaziVisible;
            set
            {
                _resmitatilYaziVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _haftalikYaziVisible = false;
        public bool HaftalikYazi
        {
            get => _haftalikYaziVisible;
            set
            {
                _haftalikYaziVisible = value;
                OnPropertyChanged();
            }
        }


        private void  ResmiTatilHaftalikOlustur()
        {
            //Tüm haftaları ve gez.. İzin Günleri Resmi Tatille Çakışanları Listele
            ListeHaftaResmi.Clear();
            foreach (var  t in FM.HaftalikIzinHaftalarBilgi)
            {
                if(t.Aktif==true)
                { 
                var kayit = FM.ResmiTatilBilgi.Where(o => o.tarih.Date == t.HaftalikIzinTarihi.Date).FirstOrDefault();

                if(kayit != null)
                {
                    IzinCakisanGun izin1 = new IzinCakisanGun();
                    izin1.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    izin1.kayitId = kayit.Id;
                    izin1.tur = "HaftalıkResmi";
                    izin1.Aciklama = kayit.Aciklama;
                    izin1.Tarih = t.HaftalikIzinTarihi;
                    izin1.Secili = false;
                    izin1.TarihBilgi = izin1.Tarih.ToShortDateString() + "-" + izin1.Tarih.DayOfWeek;

                    ListeHaftaResmi.Add(izin1);
;                }

                }
            }


        }

        private void ResmiListeOlustur()
        {
            ListeResmi.Clear();
            foreach(var t in FM.IzinGunleriBilgi)
            {
                DateTime _tarih = t.Tarih.Date;

                var kayit = FM.ResmiTatilBilgi.ToList().Find(o => o.tarih.Date == _tarih);
                if(kayit != null)
                {
                    //Resmi Kayıtlar Listesine Ekle.......
                    IzinCakisanGun izin1 = new IzinCakisanGun();
                    izin1.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    izin1.kayitId = kayit.Id;
                    izin1.tur = "Resmi";
                    izin1.Aciklama = kayit.Aciklama;
                    izin1.Secili = false;
                    izin1.Tarih = kayit.tarih;
                    izin1.TarihBilgi = izin1.Tarih.ToShortDateString() + "-" + izin1.Tarih.DayOfWeek;

                    ListeResmi.Add(izin1);

                }

            }
        }
        private void HaftalikListeOIustur()
        {
            ListeHaftalik.Clear();
            foreach(var t in FM.IzinGunleriBilgi)
            {
                DateTime _tarih = t.Tarih.Date;

                var kayit = FM.HaftalikIzinHaftalarBilgi.ToList().Find(o => o.HaftalikIzinTarihi.Date == _tarih);

                if(kayit != null)
                {
                    // Haftalık Kayıt Listesine Ekle.......

                    
                    IzinCakisanGun izin1 = new IzinCakisanGun();
                    izin1.Id = Guid.NewGuid().ToString().Substring(0, 6);
                    izin1.kayitId = kayit.Id;
                    izin1.tur = "Haftalik";
                    izin1.Aciklama = kayit.Aciklama1;
                    izin1.Secili = false;
                    izin1.Tarih = kayit.HaftalikIzinTarihi;
                    izin1.TarihBilgi = izin1.Tarih.ToShortDateString() + "-" + izin1.Tarih.DayOfWeek;

                    ListeHaftalik.Add(izin1);

                }

            }

        }

        private ObservableCollection<IzinCakisanGun> _listeResmi;
        public ObservableCollection<IzinCakisanGun> ListeResmi
        {
            get => _listeResmi;
            set
            {
                _listeResmi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<IzinCakisanGun> _listeHaftalik;
        public ObservableCollection<IzinCakisanGun> ListeHaftalik
        {
            get => _listeHaftalik;
            set
            {
                _listeHaftalik = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<IzinCakisanGun> _listeHaftaResmi;
        public ObservableCollection<IzinCakisanGun> ListeHaftaResmi
        {

            get => _listeHaftaResmi;
            set
            {
                _listeHaftaResmi = value;
                OnPropertyChanged();
            }
        }

        //------------------------------------------------------

        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            // Kaydet Ve İlerle.....

            foreach(var t in ListeResmi)
            {
               
                    var kayit = FM.ResmiTatilBilgi.Where(o => o.Id == t.kayitId).FirstOrDefault();
                    if (kayit != null)
                    {
                    if (t.Secili== false)
                    {
                        kayit.AktifDurumu = 0;

                    }else if(t.Secili==true)
                    {
                        kayit.AktifDurumu = 1;
                    }
                        

                    }
                
               
            }

            foreach( var t2 in ListeHaftalik)
            {
                
              var kayit2 = FM.HaftalikIzinHaftalarBilgi.Where(o => o.Id == t2.kayitId).FirstOrDefault();

                if(kayit2 != null)
                { 
                    if(t2.Secili== false)
                    {
                        kayit2.Aktif = false;
                    }
                    else if( t2.Secili== true)
                    {
                        kayit2.Aktif = true;
                    }

                }

            }

            foreach(var t3 in ListeHaftaResmi)
            {

                var kayit3 = FM.ResmiTatilBilgi.Where(o => o.Id == t3.kayitId).FirstOrDefault();
                if(kayit3 != null)
                {
                    if(t3.Secili== false)
                    {
                        kayit3.AktifDurumu = 0;

                    }else if( t3.Secili==true)
                    {
                        kayit3.AktifDurumu = 1;
                    }
                }

            }


            var sayfa = new Basamak10BFMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);




            IsBusy = false;
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

    public class IzinCakisanGun
    {
        public string Id { get; set; }
        public string FazlaMesaiId { get; set; }
        public string tur { get; set; }
        public string kayitId { get; set; }

        public bool Secili { get; set; }
        public  string TarihBilgi { get; set; }
        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }


    }


}
