using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views;
using LawCheckTazminat.Views.Contacts;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using LawCheckTazminat.Views.FazlaMesaiV;
using LawCheckTazminat.Views.GeceCalisma;
using LawCheckTazminat.Views.IsgucuKayipV;
using LawCheckTazminat.Views.KidemIhbarV;
using LawCheckTazminat.Views.YillikIzinV;
using Syncfusion.Data.Extensions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.Contacts
{
    public class KisiAramaViewModel : ViewModelBase
    {

        ContactsService islem = new ContactsService();
        string _tur;
        public KisiAramaViewModel(string tur)
        {
            VerileriAl();
            _tur = tur;
            if(_tur == "Adres")
            {
                ButtonVisible = false;
            }else
            {
                ButtonVisible = true;
            }

            if(_tur == "DestektenYoksunluk")
            {
                Baslik = "Destekten Yoksunluk";
            }
            else if(_tur == "IsGucuKayip")
            {
                Baslik = "İş Gücü Kaybı";
            }
            else if( _tur == "FazlaMesai")
            {
                Baslik = "Fazla Mesai";
            }
            else if( _tur == "YillikIzin")
            {
                Baslik = "Yıllık İzin";
            }
            else if(_tur == "KidemIhbar")
            {
                Baslik = "Kıdem - İhbar";
            }
            else if(_tur == "GeceCalisma")
            {
                Baslik = "Gece Çalışma";
            }
            else if(_tur == "Adres")
            {
                Baslik = "Kişi - Adres";
            }
            kkk();

        }


        private string _baslik ="";
        public string Baslik
        {
            get => _baslik;
            set
            {
                _baslik = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Contact> _kisiListe;
        public ObservableCollection<Contact> Kisiler
        {
            get => _kisiListe;
            set
            {
                _kisiListe = value;
                OnPropertyChanged();
            }
        }

        public bool _butonVisible = false;
        public bool ButtonVisible
        {
            get => _butonVisible;
            set
            {
                _butonVisible = value;
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
        private async void VerileriAl()
        {
            //Kisiler.Clear();

            var aa = await islem.GetItemsAsync();

            Kisiler = aa.ToObservableCollection();

        }

        public ICommand TestCommand => new Command(OnTestCommand);

         private void OnTestCommand(object obj)
        {
            var aa = Kisiler; 
        }

        public ICommand IptalCommand => new Command(OnIptalCommand);   
        private async void OnIptalCommand(object ob)
        {
         await   Application.Current.MainPage.Navigation.PopModalAsync();
        }
       
        public ICommand YeniKisiCommand => new Command(OnYeniKisiCommand);       
        private async void OnYeniKisiCommand(object obj)
        {
         if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            Contact kk = new Contact();
           var sayfa= new  AdresDefteri(kk);

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }



        public ICommand IlerleCommand => new Command(OnIerle);
        private async void OnIerle(object ob)
        {
         
             if (_tur == "DestektenYoksunluk")
            {
                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                //---------------------------------------------------------------
                var sayfa = new Wiz1DYView("11111");
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
                //-----------------------------------------------------------

                IsBusy = false;

            }
            else if (_tur == "IsGucuKayip")
            {
                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                IsgucuKayip _isgucu = new IsgucuKayip();

                _isgucu.musteriId = "11111";
                var sayfa = new Views.IsgucuKayipV.Basamak1IGView(_isgucu);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


                //-----------------------------------------------------------

                IsBusy = false;

            }
            else if (_tur == "FazlaMesai")
            {
                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                //-----
                // FM Wiz1

                FazlaMesai _fazlaMesai = new FazlaMesai();
                _fazlaMesai.KisiId = "11111";
                var sayfa = new Basamak1FMView(_fazlaMesai);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

                //------

                IsBusy = false;



            }
            else if (_tur == "YillikIzin")
            {
                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                //-----
                // FM Wiz1

                YillikIzin _yillikIzin = new YillikIzin();
                _yillikIzin.kisiId = "11111";

                var sayfa = new Basamak1YYView(_yillikIzin);

                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
                //------
                IsBusy = false;


            }
            else if (_tur == "KidemIhbar")
            {

                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                KidemIhbar kidem = new KidemIhbar();
                kidem.Id = "";
                kidem.kisiId = "11111";

                var sayfa = new Basamak1IKView(kidem);

                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

                IsBusy = false;

            }
            else if (_tur == "GeceCalisma")
            {
                if (IsBusy == true)
                {
                    return;
                }
                IsBusy = true;

                //-----
                // FM Wiz1

                GeceCalisma _geceCalisma = new GeceCalisma();
                _geceCalisma.KisiId = "11111";
                var sayfa = new Basamak1GCview(_geceCalisma);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

                //------


                IsBusy = false;

            }

        }
        public ICommand KisiTappedCommand => new Command<Contact>(OnKisiTapped);


        async private void OnKisiTapped(Contact _kisi)
        {
           if(_tur=="Adres")
            {
                App.EskiKontrol = true;
                var sayfa = new AdresDefteri(_kisi);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
            else if(_tur== "DestektenYoksunluk")
            {
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlariDYView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            }else  if(_tur=="IsGucuKayip")
            {
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlariIGView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            }
           else if(_tur == "FazlaMesai")
            {
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlariFMView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            }
           else if(_tur== "YillikIzin")
            {
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlariYYView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
           else if(_tur== "KidemIhbar")
            {
                //Kidem Kişi Kayıtları.....
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlariKIView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            }
           else if(_tur== "GeceCalisma")
            {
                App.EskiKontrol = true;

                var sayfa = new KisiKayitlarGCView(_kisi.Id);
                await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            }



        }



        public void kkk()
        {
            MessagingCenter.Subscribe<string>(this, "Yenile", async (aa) =>
            {
                VerileriAl();
            });

        }

    }
}
