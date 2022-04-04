using System;
using Xamarin.Essentials;

namespace LawCheckTazminat.Services
{
    public class SettingsService2
    {
        public static string productId = "tazminapp_subscription3";


        public static string AppStatus
        {
            get => Preferences.Get(nameof(AppStatus), "DEMO");
            set => Preferences.Set(nameof(AppStatus), value);
        }

        public static DateTime AppStatusCheckDate
        {
            get => Preferences.Get(nameof(AppStatusCheckDate), DateTime.MinValue);
            set => Preferences.Set(nameof(AppStatusCheckDate), value);
        }

        public static int NotConnectCounter
        {
            get => Preferences.Get(nameof(NotConnectCounter), 0);
            set => Preferences.Set(nameof(NotConnectCounter), value);
        }

        public static DateTime NotConnectControlDate
        {
            get => Preferences.Get(nameof(NotConnectControlDate), DateTime.MinValue);
            set => Preferences.Set(nameof(NotConnectControlDate), value);
        }


        public static string ProReceipt
        {
            get => Preferences.Get(nameof(ProReceipt), string.Empty);
            set => Preferences.Set(nameof(ProReceipt), value);
        }

        public static DateTime TransactionDate
        {
            get => Preferences.Get(nameof(TransactionDate), DateTime.MinValue);
            set => Preferences.Set(nameof(TransactionDate), value);
        }
    }
}
