using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;
using System.Linq;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class KisiKayitlariFMViewModel :ViewModelBase
    {
        string _id = "";

        FazlaMesaiService islem = new FazlaMesaiService();

        public KisiKayitlariFMViewModel(string _kisiId)
        {
            _id = _kisiId;
            KayitlariAl();

        }

         private void KayitlariAl()
        {

            var sonuc =  islem.KisiKayitlari(_id);

         //   var sonuc = islem.KisiKayitlari2(_id);

            Kayitlar = sonuc.ToObservableCollection();


            foreach(var t2 in Kayitlar)
            {
                ListeFazlaMesai kk = new ListeFazlaMesai();
                DateTime bas;
                DateTime bit;

                bas = t2.BasTarihMesai;
                if(t2.BasTarihResmiTatil< bas )
                {
                    bas = t2.BasTarihResmiTatil;
                }
                if(t2.BasTarihHaftaSonu<bas)
                {
                    bas = t2.BasTarihHaftaSonu;
                }
                //--------------
                bit = t2.BitTarihMesai;
                if(t2.BitTarihResmiTatil> bit)
                {
                    bit = t2.BitTarihResmiTatil;
                }
                if(t2.BitTarihHaftaSonu>bit)
                {
                    bit = t2.BitTarihHaftaSonu;
                }
                t2.Ek1 = bas.ToShortDateString() + " - " + bit.ToShortDateString();
                t2.Ek3 = (t2.ToplamNet + t2.ToplamHsonuNet + t2.ToplamResmiTatilNet);
              
            }

        }

        private ObservableCollection<FazlaMesai> _kayitlar;

        public ObservableCollection<FazlaMesai> Kayitlar
        {
            get => _kayitlar;
            set
            {
                _kayitlar = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ListeFazlaMesai> _kayitlar2;
        public ObservableCollection<ListeFazlaMesai> Kayitlar2
        {
            get => _kayitlar2;
            set
            {
                _kayitlar2 = value;
                OnPropertyChanged();
            }
        }

        public ICommand FMSilCommand => new Command<string>(OnFMSil);

        private async void OnFMSil(string idd)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var tmp = Kayitlar.ToList();
            var sonucc = tmp.Find(o => o.Id == idd);
            Kayitlar.Remove(sonucc);

            islem.DeleteItem(idd);

            IsBusy = false;
        }

        public ICommand TumuCommand => new Command(OnTumu);
        private void OnTumu()
        {
           var liste=islem.GetItemSHaftalar();
        }

        public ICommand EskiKayitKontrolCommand => new Command(OnEskiKayitKontrol);
        private async void OnEskiKayitKontrol(object obj)
        {
            if (Kayitlar.Count == 0)
            {

                FazlaMesai _fazlaMesai = new FazlaMesai();
                _fazlaMesai.KisiId = _id;
                var sayfa = new Basamak1FMView(_fazlaMesai);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
        }

   



        public ICommand YeniKayitCommand => new Command(OnYeniKayit);

        private async void OnYeniKayit(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            //-----
            // FM Wiz1

            FazlaMesai _fazlaMesai = new FazlaMesai();
            _fazlaMesai.KisiId = _id;
            var sayfa = new Basamak1FMView(_fazlaMesai);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);      

            //------

            IsBusy = false;


        }

        public ICommand IptalCommand => new Command(OnIptal);

        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }

        public ICommand KayitTappedCommand => new Command<FazlaMesai>(OnKayitTapped);

        async private void OnKayitTapped(FazlaMesai _fm)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var kayit = await islem.GetItem(_fm.Id);

            var sonn = new Basamak11FMView(kayit);
            await Application.Current.MainPage.Navigation.PushModalAsync(sonn);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
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



    }

    public class ListeFazlaMesai
    {
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public string TarihYazi { get; set; }
        public decimal Toplam { get; set; }

    }
}
