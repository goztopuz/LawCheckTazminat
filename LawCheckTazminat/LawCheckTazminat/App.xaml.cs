using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.AnaSayfa;
using LawCheckTazminat.Views;
using LawCheckTazminat.Views.AnaSayfaV;
using LawCheckTazminat.Views.Contacts;
using LawCheckTazminat.Views.DestektenYoksunlukV;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat
{
    public partial class App : Application
    {

        public static Repo Repository;

        public static string baglantiDB;


        public static string AppStatus="";

        public static string WordDosyasi = "";


        public static DateTime YuklemeTarihi;

        public static string Durum;


        public static bool EskiKontrol = false;

        public App(string dbPath)
        {
            InitializeComponent();

     VersionTracking.Track();
           

            Repository = new Repo(dbPath);
            baglantiDB = dbPath;

//     Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3MTYxQDMxMzkyZTMzMmUzMENSRzZqQnZuR2daYVpBQkxkVnpXRkMyNDZMVVdSMDRibWdyVDcyNnVoTVk9");


            //Models.DestektenYoksunluk kk = new Models.DestektenYoksunluk();
            // MainPage = new  AdresDefteri(kk);
            //MainPage = new DestekSon(kk);

            MainPage = new Anasayfa();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

       
    }
}
