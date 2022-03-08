using LawCheckTazminat.Models;
using LawCheckTazminat.Services.DestektenYoksunlukHesapService;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.DestektenYoksunlukB
{
    public class Basamak5DYViewModel : ViewModelBase
    {



       

        public Basamak5DYViewModel(DestektenYoksunluk dyy)
        {
            this.AktifDestek = new DestektenYoksunluk();
            this.AktifDestek = dyy;
           
            //this.AktifDestek.esEvlenmeYuzdesi

            foreach(var t in this.AktifDestek.DestekYoksunlukYakinlar)
            {
                if(t.yakinlik=="Eş")
                {
                    this.EsVar = true;
                }
                if (t.yakinlik == "Çocuk")
                {
                    cocukSayisi = cocukSayisi + 1;
                }
            }

            if(this.EsVar == true)
            {
                Yakin yakinn = this.AktifDestek.DestekYoksunlukYakinlar.Where(o => o.yakinlik == "Eş").FirstOrDefault();

                this.YakinBilgi = yakinn;

                this.IsEsEvlenmeA = yakinn.esEvlenmeDurumAyim;
                this.IsEsEvlenmeM = yakinn.esEvlenmeDurumMoser;
                this.IsEsEvlenmeS = yakinn.esEvlenmeDurumStaauffer;
            }

            EsEvlenmeHesapla();
        }

        int cocukSayisi;


        private Yakin _yakin;
        public Yakin YakinBilgi
        {
            get => _yakin;
                set
            {
                _yakin = value;
                OnPropertyChanged();
            }
        }

        private DestektenYoksunluk _destektenYoksunluk;
        public DestektenYoksunluk AktifDestek
        {
            get => _destektenYoksunluk;
            set
            {
                _destektenYoksunluk = value;
                OnPropertyChanged();
            }
        }

        private double _evlenmeYuzde;

        public double EvlenmeYuzde
        {
            get => _evlenmeYuzde;
            set
            {
                _evlenmeYuzde = value;
                OnPropertyChanged();
            }
        }


        private bool _esVar;
        public bool EsVar
        {
            get => _esVar;
            set
            {
                _esVar = value;
                OnPropertyChanged();
            }
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

        bool _isEsEvlenmeA = false;
        public bool IsEsEvlenmeA
        {
            get => _isEsEvlenmeA;
            set
            {
                _isEsEvlenmeA = value;
                var kisi = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.Id == YakinBilgi.Id).FirstOrDefault();
                kisi.esEvlenmeDurumAyim = _isEsEvlenmeA;
                OnPropertyChanged();
            }
        }
        bool _isEsEvlenmeM = false;
        public bool IsEsEvlenmeM
        {
            get => _isEsEvlenmeM;
            set
            {
                _isEsEvlenmeM = value;
                var kisi = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.Id == YakinBilgi.Id).FirstOrDefault();
                kisi.esEvlenmeDurumMoser = _isEsEvlenmeM;
                OnPropertyChanged();
            }
        }
        bool _isEsEvlenmeS = false;
        public bool IsEsEvlenmeS
        {
            get => _isEsEvlenmeS;
            set
            {
                _isEsEvlenmeS = value;

                var kisi = AktifDestek.DestekYoksunlukYakinlar.Where(o => o.Id == YakinBilgi.Id).FirstOrDefault();
                kisi.esEvlenmeDurumStaauffer = _isEsEvlenmeS;

                OnPropertyChanged();
            }
        }
       
        // HESAPLAMA BÖLÜMÜ....
        public ICommand HesaplaCommand => new Command(OnHesapla);     
        private void OnHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            //-------------------------------------
            EsEvlenmeHesapla();
            IsBusy = false;
        }

        private void EsEvlenmeHesapla()
        {
            if(EsVar ==true)
            {

            }
            else
            {
                return;
            }


            EvlenmeHesaplaService islem = new EvlenmeHesaplaService(this.AktifDestek);
            var aa = islem.EsEvlenmeIhtimalHesapla();
            this.EvlenmeYuzde = aa;
            this.AktifDestek.esEvlenmeYuzdesi = Convert.ToDecimal(aa);
            //------------------------------------------------

      

        }


        //İLERLEME
        public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var basamak6 = new Basamak6DYView(AktifDestek);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak6);

            IsBusy = false;
        }

        public ICommand IptalCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }

    
    }
}
