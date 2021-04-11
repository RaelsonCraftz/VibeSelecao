using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vibe.Mobile.Activities.Droid;

namespace Vibe.Mobile.Droid.Activities
{
    [Activity(
        Theme = "@style/MyTheme.Splash",
        Icon = "@drawable/icLogo",
        MainLauncher = true,
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var mainActivity = new Intent(Application.Context, typeof(MainActivity));
            mainActivity.AddFlags(ActivityFlags.NoAnimation);

            StartActivity(mainActivity);
        }

        public override void OnBackPressed()
        {
            // Prevent the back button from canceling the startup process
        }
    }
}