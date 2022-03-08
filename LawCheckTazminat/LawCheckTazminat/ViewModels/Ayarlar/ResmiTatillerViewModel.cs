using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.Ayarlar;
using Syncfusion.Data.Extensions;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Ayarlar
{
    public class ResmiTatillerViewModel : ViewModelBase
    {

        Services.ResmiTatilService islem = new Services.ResmiTatilService();

        List<ResmiTatiller> liste1 = new List<ResmiTatiller>();


        public ResmiTatillerViewModel()
        {
            this.Yil = DateTime.Now.Year.ToString();

            this.YillarListe = new ObservableCollection<Yillar>();
            VerileriYenile();
            kkk();

        }

        private string _yil;
        public string Yil
        {
            get => _yil;
            set
            {
                _yil = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Yillar> _yillarListe;
        public ObservableCollection<Yillar> YillarListe
        {
            get => _yillarListe;
            set
            {
                _yillarListe = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<ResmiTatiller> _yildakiTatiller;
        public ObservableCollection<ResmiTatiller> YildakiTatiller
        {
            get => _yildakiTatiller;
            set
            {
                _yildakiTatiller = value;
                OnPropertyChanged();
            }
        }

        private Yillar _secili;
        public Yillar Secili
        {
            get => _secili;
            set
            {
                _secili = value;
                OnPropertyChanged();
            }
        }


        //-----
        
        public ICommand TatilTappedCommand => new Command<ResmiTatiller>(OnTatilTapped);
        async private void OnTatilTapped(ResmiTatiller _tatil)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new ResmiTatilDuzenleView(_tatil, YildakiTatiller);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

            //_navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }


        public ICommand ListeleCommand => new Command(OnListele);
        private async void OnListele(object obj)
        {
            VerileriYenile();
        }

        //Yeni Kayıt----

        //Sil Command
        public ICommand SilKayitCommand => new Command<ResmiTatiller>(OnKayitSil);
        private async void OnKayitSil(ResmiTatiller obj)
        {
            string yazii = "Kayıt Silme İşlemi Yapılacaktır. Devam Etmek İstiyormusunuz?";
            bool sonuc = await Application.Current.MainPage.DisplayAlert("Silme İşlemi", yazii, "Devam", "İptal");

            if(sonuc== false)
            {
                return;
            }

            string yazii2 = obj.tarih.ToShortDateString() + "- Tarihli Kayıt Silinenecektir. Devam Etmek İstiyormusunuz?";
            bool sonuc2 = await Application.Current.MainPage.DisplayAlert("", yazii2, "Evet", "İptal");
            if(sonuc2== false)
            {
                return;
            }

           await islem.DeleteItem(obj.Id);

            YildakiTatiller.Remove(obj);
            liste1.Remove(obj);
            //VerileriYenile();

        }

        public ICommand YeniKayitCommand => new Command(OnYeniKayit);
        async private void OnYeniKayit(object obj)
        {
            var h = new ResmiTatiller();
            h.Id = "";

            var sayfa = new Views.Ayarlar.ResmiTatilDuzenleView(h, liste1.ToObservableCollection());
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


        }

        public void kkk()
        {
            MessagingCenter.Subscribe<ResmiTatiller>(this, "ResmiTatilYenile", async (aa) =>
            {
                VerileriYenile();

                OnSec(Secili.Yil2);
            });


        }
         async private void VerileriYenile()
        {
            liste1 = await islem.GetItemS();
            List<Yillar> tmpYillar = new List<Yillar>();
            tmpYillar.Clear();


            IEnumerable<int> yills = liste1.Select(x => x.yil).Distinct().OrderByDescending(o=>o);

           
            foreach(var tt in yills)
            {

                 Yillar yy = new Yillar();
                  yy.Yill = tt.ToString();
                  yy.Yil2 = tt;
                 tmpYillar.Add(yy);

            }

            //foreach (var t in liste1)
            //{
            //    var kayit = tmpYillar.Find(o => o.Yill == t.tarih.Year.ToString());
            //    if (kayit == null)
            //    {
            //        Yillar yy = new Yillar();
            //        yy.Yill = t.tarih.Year.ToString();
            //        yy.Yil2 = t.tarih.Year;
            //        tmpYillar.Add(yy);
            //    }
            //}

           // var aaa  = tmpYillar.OrderByDescending(o => o.Yil2).ToList();

//            YillarListe.Clear();

            foreach(var t2 in tmpYillar)
            {
                var k1 = YillarListe.ToList().Find(o => o.Yil2 == t2.Yil2);
                if(k1 == null)
                {
                    YillarListe.Add(t2);
                }
            }

         
       


        }

        public ICommand SecCommand => new Command(OnSec);
        async private void OnSec(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }

            if(Secili== null)
            {
                return;
            }

            IsBusy = true;

            // Yıldaki Kayıtlari Al

            //   int Yil2 = Convesrt.ToInt32(Yil);
            int Yil2 = Secili.Yil2;
            var liste2 = liste1.Where(o => o.tarih.Year == Yil2).ToObservableCollection();

            var aa = liste2.OrderBy(o => o.tarih);
            liste2 = aa.ToObservableCollection();
                       
            this.YildakiTatiller = liste2;


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
    public class Yillar
    {

        public string Yill { get; set; }
        public int Yil2 { get; set; }

    }
}
