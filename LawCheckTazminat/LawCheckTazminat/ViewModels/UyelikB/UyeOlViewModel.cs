using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Plugin.InAppBilling;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.UyelikB
{
   public class UyeOlViewModel : ViewModelBase
    {
        string productId = "tazminapp_subscription6";

        public UyeOlViewModel()
        {



           PriceGet();
        }



        string _busyTitle = "";
        public string BusyTitle
        {
            get => _busyTitle;
            set
            {
                _busyTitle = value;
                OnPropertyChanged();
            }

        }

        private string _proPrice = Services.SettingsService.ProPrice;

        public string ProPrice
        {
            get => _proPrice;
            set
            {
                _proPrice = value;
                OnPropertyChanged();
            }
        }


        string _txtOutofDate ;
        public string TxtOutofDate
        {
            get => _txtOutofDate;
            set
            {
                _txtOutofDate = value;
                OnPropertyChanged();

            }
        }

        private async void  PriceGet()
        {

            if (IsBusy == true)
            {
                return;
            }
            if (IsPro == true)
            {
                ProDurum = true;
                DemoDurum = false;
                StatusYazi = "Tam Sürüm";
                AbonelikTarihi = SettingsService.TransactionDate.ToShortDateString();

                return;

            }
            else
            {
                StatusYazi = "Sınırlı kullanım";

                if( App.AppStatus =="OUTOFDATE")
                {
                    RestoreCode2();
                    TxtOutofDate = "Kullanım süresi dolmuş.";
                }
            }

           


            DemoDurum = true;
            ProDurum = false;


            if (string.IsNullOrWhiteSpace(SettingsService.ProPrice)
                || SettingsService.ProPriceDate.AddDays(7) < DateTime.UtcNow)
            {

            }
            else
            {
                return;
            }

            BusyTitle = "İşlem Yapılıyor...";

            IsBusy = true;


            try
            {



#if DEBUG
                SettingsService.ProPrice = "239.99";
                ProPrice = SettingsService.ProPrice;
               
                return;
#endif







                // Check Offline

                 var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (connected == false)
                {
                    return;
                }



                var items = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productId);

                var item = items.FirstOrDefault(i => i.ProductId == productId);

                if (items != null)
                {

                    SettingsService.ProPrice = item.LocalizedPrice;
                    ProPrice = SettingsService.ProPrice;
                    SettingsService.ProPriceDate = DateTime.UtcNow;

                }




            }
            catch (Exception ex)
            {

                // Error..........

            }
            finally
            {

           //     await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false;
            }

        }

        private string _abonelikTarihi;
        public string AbonelikTarihi
        {
            get => _abonelikTarihi;
            set
            {
                _abonelikTarihi = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetPriceCommand => new Command(OnGetPrice);
        private async void OnGetPrice()
        {
            if(IsBusy== true)
            {
                return;
            }
            if(IsPro == true)
            {
                ProDurum = true;
                DemoDurum = false;
                AbonelikTarihi = SettingsService.TransactionDate.ToShortDateString();
               
                return;

            }

            DemoDurum = true;
            ProDurum = false;


            if (string.IsNullOrWhiteSpace(SettingsService.ProPrice)
                || SettingsService.ProPriceDate.AddDays(7) < DateTime.UtcNow)
            {

            }
            else
            {
                return;
            }

            BusyTitle = "İşlem Yapılıyor...";

            IsBusy = true;

            try
            {


                SettingsService.ProPrice = "239.99";
                ProPrice = SettingsService.ProPrice;
                return;



                // Check Offline

                var connected = await CrossInAppBilling.Current.ConnectAsync();

              if( connected == false)
                {
                    return;
                }



                var items = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productId);

                var item = items.FirstOrDefault(i => i.ProductId == productId);

               if( items != null)
                {

                    SettingsService.ProPrice = item.LocalizedPrice;
                    ProPrice = SettingsService.ProPrice;
                    SettingsService.ProPriceDate = DateTime.UtcNow;

                }


                

            }
            catch (Exception ex)
            {

                // Error..........

            }
            finally
            {

                await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false;
            }

        }

        public ICommand PurchaseCommand2 => new Command(OnTestPurchase1);
        private  void OnTestPurchase1()
        {
            SettingsService.IsPro = true;
            SettingsService.IsOutofDate = false;

            IsPro = true;
            App.AppStatus = "PRO";
            SettingsService.TransactionDate = DateTime.UtcNow;
            App.Current.MainPage.DisplayAlert("Aboenlik", "Aboneliğiniz Başlamıştır", "Tamam");
            App.Current.MainPage.Navigation.PopModalAsync();


        }

        public ICommand PurchaseCommand => new Command(OnPurchase);
        private async void OnPurchase()
        {

            if (IsBusy == true)
            {
                return;
            }
            if (!await CheckConnectivity("Çevrimdışı", "İnternet bağlantınızı kontrol edip, tekrar deneyin.."))
                return;
            BusyTitle = "";

            IsBusy = true;



            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();


                //Check Offline
                if (! connected)
                {
                    await App.Current.MainPage.DisplayAlert("Bağlantı Hatası", "Uygulama Mağza Bağlantı Hatası", "Tamam");
                    return;
                }
                //check purchases

                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.Subscription);

                if(purchase == null)
                {

                    return;
                
                }
                else if(purchase.State == PurchaseState.Purchased)
                {

                    SettingsService.ProReceipt = purchase.PurchaseToken ?? string.Empty;
           
                    SettingsService.IsPro = true;
                    IsPro = true;
                    SettingsService.IsOutofDate = false;

                    SettingsService.TransactionDate = purchase.TransactionDateUtc;

                    try
                    {
                        await CrossInAppBilling.Current.AcknowledgePurchaseAsync(purchase.PurchaseToken);
                    }
                    catch(Exception ex)
                    {

                    }

                    return;
                }
                throw new InAppBillingPurchaseException(PurchaseError.GeneralError);

               


            }
            catch (InAppBillingPurchaseException purchaseEx)
            {

                var _message = string.Empty;

                switch(purchaseEx.PurchaseError)
                {

                    case PurchaseError.AppStoreUnavailable:
                        _message = " Uygulama Mağzasına Bağlanamadı. ";
                        break;
                    case PurchaseError.BillingUnavailable:
                        _message = " Ödeme Servisinde Sorun Oluştu. ";
                        break;
                    case PurchaseError.PaymentInvalid:
                        _message = " Ödeme Geçersiz. ";
                        break;
                    case PurchaseError.PaymentNotAllowed:
                        _message = " Ödeme Kabul Edilmedi. ";
                        break;
                    case PurchaseError.UserCancelled:
                        _message = " İşlem Edildi. ";
                        break;

                    default:
                        _message = "";
                        break;
                }


                if (string.IsNullOrWhiteSpace(_message))
                    return;


                Console.WriteLine("Issue connecting: " + purchaseEx);
                await App.Current.MainPage.DisplayAlert("Hata !", _message,"Tamam");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Issue connecting: " + ex);

                await App.Current.MainPage.DisplayAlert("Hata !", $"Hata Oluştu. Kod: {ex.Message}","Tamam");

            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false;
            }

        
           
        }
        private void RestoreCode2()
        {
            IsPro = true;
            SettingsService.IsPro = true;
            SettingsService.IsOutofDate = false;
            SettingsService.TransactionDate = DateTime.Now;
            App.AppStatus = "PRO";
            App.Current.MainPage.DisplayAlert("Aboenlik", "Aboneliğiniz Geri Yüklenmiştir", "Tamam");
            App.Current.MainPage.Navigation.PopModalAsync();

        }
        private async void RestoreCode()
        {
            if (IsBusy == true)
            {
                return;
            }
            if (!await CheckConnectivity("Çevrimdışı", "Lütfen İnternet Bağlantınızı Kontrtol Edin"))
                return;

            BusyTitle = "İşlem Yapılıyor";

            IsBusy = true;


            try
            {

                //Check Offline

                var connected = await CrossInAppBilling.Current.ConnectAsync();
                if (!connected)
                {
                    await App.Current.MainPage.DisplayAlert("Bağlantı Hatası", "Uygulama mağzasına bağlanılamadı", "Tamam");
                    return;
                }


                //check purchases
                await App.Current.MainPage.DisplayAlert("Hata",
"a0", "Tamam");

                var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.Subscription);


                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {

                    //--------------------------------

                    SettingsService.IsPro = true;
                    IsPro = true;
                    SettingsService.IsOutofDate = false;

                    var purchase = purchases.FirstOrDefault(p => p.ProductId == productId);

                    SettingsService.TransactionDate = purchase.TransactionDateUtc;

                    SettingsService.ProReceipt = purchase?.PurchaseToken ?? string.Empty;

                   

                    App.AppStatus = "PRO";
                    await App.Current.MainPage.DisplayAlert("a1",
 purchase.State.ToString() +" - " + purchase.TransactionDateUtc.ToString()    , "Tamam");
                    var ack = purchase?.IsAcknowledged ?? true;
                    if (!ack)
                    {
                        try
                        {
                            await CrossInAppBilling.Current.AcknowledgePurchaseAsync(purchase.PurchaseToken);
                            await App.Current.MainPage.DisplayAlert("Geri Yükleme",
                                                purchase.State.ToString() +"  - " + purchase.TransactionDateUtc.ToString()                               
                                                , "Tamam");

                        }
                        catch (Exception ex)
                        {
                            await App.Current.MainPage.DisplayAlert("Hata",
                     "Hata", "Tamam");
                        }

                    }



                    //*********************
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Bulunamadı",
                        "Önceki Satın Almanız Bulunamadı, Satın Al ile Yeniden Abonelik Başlatabilirsiniz", "Tamam");

                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Hata !", "Abonelik Geri Yüklemesi Sırasında" +
                    $"Hata Oluştu :  Kod: {ex.Message}", "Tamam");

            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false; await App.Current.MainPage.DisplayAlert("Hata",
            "a3", "Tamam");
            }
        }


        public ICommand RestoreCommand2 => new Command(OnRestore2);
        private async void OnRestore2()
        {
            RestoreCode2();
        }

        public ICommand RestoreCommand => new Command(OnRestore);
        private async void OnRestore()
        {

            RestoreCode();


        }





        public ICommand BitisCommand => new Command(OnBitis);

        private async void OnBitis(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }


        private bool _demoDurum;
        public bool DemoDurum
        {
            get => _demoDurum;
            set
            {
                _demoDurum = value;
                OnPropertyChanged();
            }
        }

        private bool _proDurum;
        public bool ProDurum
        {
            get => _proDurum;
            set
            {
                _proDurum = value;
                OnPropertyChanged();
            }
        }

        private string _ucretYazi;
        public string UcretYazi
        {
            get => _ucretYazi;
            set
            {
                _ucretYazi = value;
                OnPropertyChanged();
            }
        }



        private string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
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



        public async Task<bool> CheckConnectivity(string title, string message)
        {
            if(Connectivity.NetworkAccess== NetworkAccess.Internet)
            {
                return true;
            }

            await App.Current.MainPage.DisplayAlert(title, message,"Tamam");
            return false;
        }

        //public async Task<bool> CheckConnectivity(string title, string message)
        //{
        //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        //        return true;

        //    await DisplayAlert(title, message);
        //    return false;
        //}






















        // ****************************************************************************************************


        //---------------------------------------------------------------------------------

        private void Islemler1()
        {

            string _status = App.AppStatus;

            switch (_status)
            {
                case "PRO":
                    ProDurum = true;
                    DemoDurum = false;
                    break;

                case "DEMO":
                    ProDurum = false;
                    DemoDurum = true;
                    StatusYazi = "Demo";
                    break;

                case "OUTOFDATE":
                    ProDurum = false;
                    DemoDurum = true;
                    StatusYazi = "Süresi Dolmuş";
                    break;

                default:

                    ProDurum = false;
                    DemoDurum = true;
                    break;


            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        // ABONELiK ÜCETİ ÇEK

        public async Task AbonelikUcretiCek()
        {
            bool hasKey = Preferences.ContainsKey("ProPrice");
            bool hasKey2 = Preferences.ContainsKey("PriceDate");
            bool ucretCek = false;
            if(hasKey==true && hasKey2==true)
            {
                string pPrice = Preferences.Get("ProPrice", "1");
                string priceDate2 = Preferences.Get("PriceDate", "2");
                DateTime priceDate = Convert.ToDateTime(priceDate2);

                DateTime dtt = DateTime.Now.AddDays(3);

                dtt = dtt.AddHours(4);

                if (priceDate.AddDays(3)>dtt && !string.IsNullOrWhiteSpace(pPrice))
                {

                    UcretYazi = pPrice;
                    Ucret = Convert.ToDecimal(pPrice);
                    return;
                }
                else
                {
                    ucretCek = true;
                }
            }
            else
            {
                ucretCek = true;
            }



            if (ucretCek== true)
            {

            #if DEBUG

                Preferences.Set("ProPrice", "194.98");
                Preferences.Set("PriceDate", DateTime.Now.ToString());
                string pPrice = Preferences.Get("ProPrice", "1");
                UcretYazi = pPrice;
                Ucret = Convert.ToDecimal(pPrice);
                return;
             #endif
                try
                {
                    var connected = await CrossInAppBilling.Current.ConnectAsync();

                    if (!connected)
                    {
                        return;
                    }

                    var items = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productId);


                    var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.Subscription);


                    foreach(var t in purchases)
                    {
                        
                        
                    }


                   foreach(var t2 in items)
                    {
                       
                    }

                    var item = items.FirstOrDefault(i => i.ProductId == productId);
                    if (item != null)
                    {
                        var bbb = item.LocalizedPrice;

                        Ucret = Convert.ToDecimal(bbb);
                         Preferences.Set("ProPrice", bbb);
                         Preferences.Set("PriceDate", DateTime.Now.ToString());

                    }
                }
                catch (Exception ex)
                {
                    //it is alright that we couldn't get the price
                }
                finally
                {
                //    await CrossInAppBilling.Current.DisconnectAsync();
                    IsBusy = false;
                }
            }


        }


        // ABONE OL

        public async Task AboneOl()
        {

        }

        // ABONELİK Aktif Et(Mevcut Uyelik)


      //PRO'yu RESTORE


        //  RetrieveProStatus


        private string _statusYazi;
        public string StatusYazi
        {
            get => _statusYazi;
            set
            {
                _statusYazi = value;
                OnPropertyChanged();
            }
        }
      

        private decimal _ucret;
        public decimal Ucret
        {
            get => _ucret;
            set
            {
                _ucret = value;
                OnPropertyChanged();
            }
        }


        public ICommand UyeOlCommand => new Command(OnUyeOl);
        private async void OnUyeOl()
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;


            IsBusy = false;
        }

        private async void OnUyelikGeriYukle()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;


            IsBusy = false;
        }
        
        



    }
}
