﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawCheckTazminat.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash",
         MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => SimualteStartup());
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        async void SimualteStartup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}