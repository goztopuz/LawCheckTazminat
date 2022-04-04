using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;


namespace LawCheckTazminat.Services
{
  public  class SettingsService
    {

        public static bool IsPro
        {
            get => Preferences.Get(nameof(IsPro), false);
            set => Preferences.Set(nameof(IsPro), value);
        }

        public static string SubscriptionStatus
        {
            get => Preferences.Get(nameof(SubscriptionStatus), "DEMO");
            set => Preferences.Set(nameof(SubscriptionStatus), value);


        }
        
        public static bool IsOutofDate
        {
            get => Preferences.Get(nameof(IsOutofDate), false);
            set => Preferences.Set(nameof(IsOutofDate), value);
        }

        public static string ProPrice
        {
            get => Preferences.Get(nameof(ProPrice), string.Empty);
            set => Preferences.Set(nameof(ProPrice), value);
        }

        public static DateTime ProPriceDate
        {
            get => Preferences.Get(nameof(ProPriceDate), DateTime.UtcNow);
            set => Preferences.Set(nameof(ProPriceDate), value);

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

        public static  DateTime LastCheckDateTime
        {
            get => Preferences.Get(nameof(TransactionDate), DateTime.MinValue);
            set => Preferences.Set(nameof(TransactionDate), value);
        }

        public static DateTime FirstInstallDate
        {
            get => Preferences.Get(nameof(FirstInstallDate), DateTime.MinValue);
            set => Preferences.Set(nameof(FirstInstallDate), value);
        }

       public static string  productId = "tazminapp_subscription3";


    }
}
