using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.Kasa;
using LawCheckTazminat.Views.Ayarlar;
using LawCheckTazminat.Views.Contacts;
using LawCheckTazminat.Views.Destek;
using LawCheckTazminat.Views.Test;
using LawCheckTazminat.Views.UyelikV;
using Plugin.InAppBilling;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.AnaSayfa
{
    public class AnasayfaViewModel : ViewModelBase
    {
        // ABONELİK -PRO-DEMO-İLK YÜKLEME BÖLÜMLERİ....
        //--------------------------------------------------------


    //    string productId = "tazminapp_subscription3";

        Helpers.AbonelikYontemi ab = new Helpers.AbonelikYontemi();

        Helpers.AbonelikYontemi2 ab2 = new Helpers.AbonelikYontemi2();
        public AnasayfaViewModel()
        {



            if (VersionTracking.IsFirstLaunchEver)
            {
                ab2.RestoreToPro(true);

            }
            else if (VersionTracking.IsFirstLaunchForCurrentVersion)
            {
                // Display update notification for current version (1.0.0)
            }
            else if (VersionTracking.IsFirstLaunchForCurrentBuild)
            {
                // Display update notification for current build number (2)
            }

          //  CheckStatus();


        }

        private bool _ucretsiz = true;
        public bool UcretsizGoster
        {
            get => _ucretsiz;
            set
            {
                _ucretsiz = value;
                OnPropertyChanged();
            }
        }

        public ICommand UcretsizYaziCheck => new Command(OnUcretsizCheck);

        private void OnUcretsizCheck()
        {

            if (SettingsService2.AppStatus == "DEMO")
            {
                UcretsizGoster = true;
            }
            else
            {
                UcretsizGoster = false;

            }
        }

        public ICommand CheckStatusCommand => new Command(OnCheckStatus);

        private void OnCheckStatus()
        {
            if (IsBusy== true)
            {
                return;
            }


          //  Remarklar kalkacak
            App.AppStatus = "PRO";
         //   CheckStatus();
          
            IsBusy = false; 
        }



        private void CheckStatus()
        {
            if (SettingsService.IsPro == true)
            {
                DateTime aboneTarihi = SettingsService.TransactionDate;

                if (DateTime.UtcNow < aboneTarihi.AddMonths(6))
                {
                    App.AppStatus = "PRO";

                }
                else
                {
                    SettingsService.IsPro = false;
                    SettingsService.ProReceipt = String.Empty;
                    SettingsService.IsOutofDate = true;
                    SettingsService.TransactionDate = DateTime.MinValue;
                    App.AppStatus = "OUTOFDATE";
                }

            }
            else if (SettingsService.IsOutofDate == true)
            {
                App.AppStatus = "OUTOFDATE";

            }
        }

        private async void  BootOperations()
        {
            var ilkAcilis = Preferences.Get("FIRSTRUN", "");

            if(ilkAcilis=="")
            {
             await   FirstRunOperations();
            }
            else
            {

            }


        }

        private async Task  FirstRunOperations()
        {

            Preferences.Set("FIRSTRUN", "NO");
            Preferences.Set("PRO", "NO");
            App.AppStatus = "PRO";
          var firstusasge =  await SecureStorage.GetAsync("FIRSTUSAGE");
            if(firstusasge== null)
            {
                await SecureStorage.SetAsync("FIRSTUSAGE", "NO");
            }
            else
            {

            }

                //await   SecureStorage.SetAsync("INSTALLDAY", DateTime.Now.Day.ToString());
         //await SecureStorage.SetAsync("INSTALLMONTH", DateTime.Now.Month.ToString());
         //await SecureStorage.SetAsync("INSTALLYEAR", DateTime.Now.Year.ToString());
                               

        }
    
        private async Task AcilisIlemleri1()
        {
            try
            {
                var oncekiYukleme = await SecureStorage.GetAsync("OncekiYukleme");

                if(oncekiYukleme== null)
                {
                    //ilk yükleme ayarlari
                    try
                    {
                        await SecureStorage.SetAsync("OncekiYukleme", "Yuklenmis");
                        await SecureStorage.SetAsync("YuklemeTarihi", DateTime.Now.ToString());
                        await SecureStorage.SetAsync("AppStatus", "Demo");

                        App.AppStatus = "Demo";
                        App.YuklemeTarihi = DateTime.Now;
                        App.Durum = "Yuklu";
                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                    }

                }
                else
                {
                    // Daha Önce Yüklenmiş.. Yükleme Tarihi.- Abonelik Durumu.. Abonelik Başlangıç 

                    App.Durum = oncekiYukleme;
                    string yuklemeTarihi2= await SecureStorage.GetAsync("YuklemeTarihi");
                    App.YuklemeTarihi = Convert.ToDateTime(yuklemeTarihi2);

                    App.AppStatus = await SecureStorage.GetAsync("AppStatus");
                    if(App.AppStatus=="Demo")
                    {

                        if(App.YuklemeTarihi.AddDays(3)> DateTime.Now)
                        {
                            App.AppStatus = "SuresiGecmis";
                        }

                    }
                    if(App.AppStatus== "Pro")
                    {


                    }


                    // EĞER DEMO ve süre 3 günü geçmişse - Lisans İptal
                    // Satın Demo değilse Lisans süre kontrolü
                    // Süresi Geçmişse // Süresi Geçmiş(AppStatus)
                    // Değilse Status=Pro..
                    // Satın alındığında Durum Pro.






                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

           

            
        }


        
        //--------------------------------------------------------------
        //*************************************************************

        public ICommand DestektenYoksulukCommand => new Command(OnDestektenYoksunluk);
        async private void OnDestektenYoksunluk(object obj)
        {

            if (SettingsService2.AppStatus == "PRO")
            {
                // Ara Kontrolü Sağla....   
               if( Device.RuntimePlatform == Device.Android)
                {
                    ab2.RegularPROAppStatusControl();

                }
                else if(Device.RuntimePlatform == Device.iOS)
                {
                    ab2.IOSRegularPROAppStatusControl();
                }

            }

            if (SettingsService2.AppStatus == "NOTCONNECTED")
            {
                ab2.RestoreToPro(true);

                int sayac = SettingsService2.NotConnectCounter;

                if (SettingsService2.AppStatus == "NOTCONNECTED")
                {
                    sayac = sayac + 1;

                    SettingsService2.NotConnectCounter = sayac;
                }


            //    await App.Current.MainPage.DisplayAlert("Sayaç", sayac.ToString(), "Tamam");

                if (sayac == 8)
                    {

                        string _message1 = "Lisans Kontrolü sağlanamdı. Internet bağlantınızı kontrol edip," +
                            " Aboneliği Geri Yükleme Bölümden Aboneliğiinizi Ücretsi Aktif Edebilirsiniz!";
                        await App.Current.MainPage.DisplayAlert("Abonelik", _message1, "Tamam");

                        SettingsService2.AppStatus = "DEMO";
                        SettingsService2.NotConnectCounter = 0;

                        await UyeSayfasınaGit();
                        return;
                    }
                 }

                       

            if(SettingsService2.AppStatus == "DEMO")
            {
             
                
                string _message1 = "Bu Özelliği Kullanabilmek için Abone omalısınızl Aboneyseniz  Aboneliği Ücretsiz Geri Yükleyebilirsiniz.";

                await App.Current.MainPage.DisplayAlert("Abonelik", _message1
                         , "Tamam");

                await UyeSayfasınaGit();
                return;
          
            }


            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;



            var sayfa = new KisiAramaView("DestektenYoksunluk");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;




            // ESKİ KOD BÖLÜMÜ
            //if (App.AppStatus != "PRO")
            //{

            //    if (App.AppStatus == "OUTOFDATE")
            //    {
            //        //_message1 = "Süresi Geçmiş";

            //        await UyeSayfasınaGit();
            //        return;
            //    }

            //    string _message1 = "Bu Özelliği Kullanabilmek için Abone omalısınızl";
                
            //    await App.Current.MainPage.DisplayAlert("Abonelik", _message1
            //             , "Tamam");

            //    await UyeSayfasınaGit();
            //    return;
            //}


            //if (IsBusy == true)
            //{
            //    return;
            //}
            //IsBusy = true;



            //var sayfa = new KisiAramaView("DestektenYoksunluk");
            //await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //IsBusy = false;

        }
     
        public ICommand IsGucuKayipCommand => new Command(OnIsgucuKayip);      
        async private void OnIsgucuKayip(object obj)
        {
            if (App.AppStatus != "PRO")
            {
                await App.Current.MainPage.DisplayAlert("Abonelik",
                         "Bu Özelliği Kullanabilmek için Abone omalısınızl", "Tamam");

                await UyeSayfasınaGit();
                return;

            }

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new KisiAramaView("IsGucuKayip");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        //---------------
        public ICommand FazlaMesaiCommand => new Command(OnFazlaMesai);
        async private void OnFazlaMesai(object obj)
        {
            if (App.AppStatus != "PRO")
            {
                await App.Current.MainPage.DisplayAlert("Abonelik",
                         "Bu Özelliği Kullanabilmek için Abone omalısınızl", "Tamam");

                await UyeSayfasınaGit();
                return;

            }
            if (IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new KisiAramaView("FazlaMesai");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }

        public ICommand YillikIzinCommand => new Command(OnYillikIzin);
        async private void OnYillikIzin(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }

            var sayfa = new KisiAramaView("YillikIzin");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;
        }

        //KisiAdresCommand
        public ICommand KisiAdresCommand => new Command(OnKisiAdres);
        async private void OnKisiAdres(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new KisiAramaView("Adres");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }

        //KidemIhbar
        public ICommand KidemIhbarCommand => new Command(OnKidemIhbar);
        async private void OnKidemIhbar(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new KisiAramaView("KidemIhbar");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        // Gece Çalışma
        public ICommand GeceCalismaCommand => new Command(OnGeceCalisma);
        private async void OnGeceCalisma(object obj)
        {
            if (App.AppStatus != "PRO")
            {
                await App.Current.MainPage.DisplayAlert("Abonelik",
                         "Bu Özelliği Kullanabilmek için Abone omalısınızl", "Tamam");

                await UyeSayfasınaGit();
                return;
            }

            if (IsBusy==true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new KisiAramaView("GeceCalisma");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


        // Kasa
        public ICommand KasaCommand => new Command(OnKasa);
        async private void OnKasa(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            KasaGenelView sayfa = new KasaGenelView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;
        }

        // NET Brüt
        public ICommand NetBrutCommand => new Command(OnNetBrut);
        async private void OnNetBrut(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.NetBrutV.NetBrutSecim();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand BrutCommand => new Command(OnBrutNet);
        async private void OnBrutNet(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.NetBrutV.BrutNet1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand NetCommand => new Command(OnNetCommand);
        async private void OnNetCommand(object obj)
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.NetBrutV.NetBrut1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            IsBusy = false;
        }

        public ICommand AracDegerKaybiCommand => new Command(OnAracDegerKaybi);
        async private void OnAracDegerKaybi(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.AracDegerKaybi.AracDegerKayipView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand KayipOranCommand => new Command(OnKayipOran);
        async private void OnKayipOran(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.KayipOranV.Basamak1();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand AyarlarCommand => new Command(OnAyarlar);
        async private void OnAyarlar(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new AyarlarListe();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }

        public ICommand UyeBilgiCommand => new Command(OnUyeBilgi);
        async private void OnUyeBilgi(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            await UyeSayfasınaGit();
            IsBusy = false;

        }

        private async Task UyeSayfasınaGit()
        {
            var sayfa = new UyelikView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
        }
        public ICommand SoruCommand => new Command(OnSoru);
        async private void OnSoru(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new DestekListeView();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }

        public ICommand RaporCommand => new Command(OnRapor);
        async private void OnRapor(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new RaporTest1();
            await Application.Current.MainPage.Navigation.PushAsync(sayfa);
            IsBusy = false;
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
}
