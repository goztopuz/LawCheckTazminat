using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using Plugin.InAppBilling;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.UyelikB
{
    public class UyelikViewModel : ViewModelBase
    {

        string productId = "tazminapp_subscription6";

        Helpers.AbonelikYontemi2 ab = new Helpers.AbonelikYontemi2();

       

        public UyelikViewModel()
        {

            AbonelikDurum();
        }

        bool _isAndroid;
        public bool IsAndroid
        {
            get => _isAndroid;
            set
            {
                _isAndroid = value;
                OnPropertyChanged();
            }
        }
        bool _isIos;
        public bool IsIos
        {
            get => _isIos;
            set
            {
                _isIos = value;
                OnPropertyChanged();

            }
        }

        // Properties
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

        string _txtOutofDate;
        public string TxtOutofDate
        {
            get => _txtOutofDate;
            set
            {
                _txtOutofDate = value;
                OnPropertyChanged();

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




        //Commands

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





        public ICommand PurchaseCommand => new Command(OnPurchaseNew);

        private async void OnPurchaseNew()
        {

         await   ab.SubscripteToApp();

            if(SettingsService2.AppStatus == "PRO")
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();

            }

        }
        private async void OnPurchase()
        {  
            if (!await CheckConnectivity("Çevrimdışı", "İnternet bağlantınızı kontrol edip, tekrar deneyin.."))
                return;
            BusyTitle = "";

            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();


                //Check Offline
                if (!connected)
                {
                    await App.Current.MainPage.DisplayAlert("Bağlantı Hatası", "Uygulama Mağza Bağlantı Hatası", "Tamam");
                    return;
                }
                //check purchases

                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.Subscription);

                if (purchase == null)
                {

                    return;

                }
                else if (purchase.State == PurchaseState.Purchased)
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
                    catch (Exception ex)
                    {

                    }

                    return;
                }
                throw new InAppBillingPurchaseException(PurchaseError.GeneralError);




            }
            catch (InAppBillingPurchaseException purchaseEx)
            {

                var _message = string.Empty;

                switch (purchaseEx.PurchaseError)
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
                await App.Current.MainPage.DisplayAlert("Hata !", _message, "Tamam");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Issue connecting: " + ex);

                await App.Current.MainPage.DisplayAlert("Hata !", $"Hata Oluştu. Kod: {ex.Message}", "Tamam");

            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false;
            }



        }

        public ICommand RestoreCommand => new Command(OnRestoreNew);

        private void OnRestoreNew()
        {
            ab.RestoreToPro(false);
            if (SettingsService2.AppStatus == "PRO")
            {
                return;
            }

        }

        private async void OnRestore()
        {

            RestoreCode();


        }


        // Fonksiyon- Kod Modülleri
        private void AbonelikDurum()
        {
            if(SettingsService2.AppStatus ==  "DEMO")
            {
                DemoDurum = true;
                ProDurum = false;
            }
            else
            {
                DemoDurum = false;
                ProDurum = true;
               
            }

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                IsAndroid = true;
                IsIos = false;

            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                IsAndroid = false;
                IsIos = true;


            }


            }

        private async void SayfaDurum()
        {

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

                if (App.AppStatus == "OUTOFDATE")
                {
                   // RestoreCode2();
                    TxtOutofDate = "Kullanım süresi dolmuş.";
                }
            }




            DemoDurum = true;
            ProDurum = false;


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
                    var purchase = purchases.FirstOrDefault(p => p.ProductId == productId);


                    if(purchase.AutoRenewing == false)
                    {
                        await App.Current.MainPage.DisplayAlert("İptal", "Aboneliğiniz İptal Edilmiş" , "Tamam");
                        return;
                    }


                    //--------------------------------

                    SettingsService.IsPro = true;
                    IsPro = true;

                    SettingsService.IsOutofDate = false;


                    
                    SettingsService.TransactionDate = purchase.TransactionDateUtc;

                    SettingsService.ProReceipt = purchase?.PurchaseToken ?? string.Empty;
                    SettingsService.TransactionDate = DateTime.UtcNow;


                    App.AppStatus = "PRO";
                    await App.Current.MainPage.DisplayAlert("a1",
                        purchase.State.ToString() + " - " + purchase.TransactionDateUtc.ToString(), "Tamam");
                    var ack = purchase?.IsAcknowledged ?? true;
                    if (!ack)
                    {
                        try
                        {
                            await CrossInAppBilling.Current.AcknowledgePurchaseAsync(purchase.PurchaseToken);
                            await App.Current.MainPage.DisplayAlert("Geri Yükleme",
                                                purchase.State.ToString() + "  - " + purchase.TransactionDateUtc.ToString()
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

        public async Task<bool> CheckConnectivity(string title, string message)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }

            await App.Current.MainPage.DisplayAlert(title, message, "Tamam");
            return false;
        }



    }
}
