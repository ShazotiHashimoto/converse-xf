﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Converse.Helpers;
using Xamarin.Forms.Platform.Android;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Plugin.Permissions;
using Converse.Droid.Firebase;

namespace Converse.Droid
{
    [Activity(Label = "@string/ApplicationName",
              Icon = "@mipmap/ic_launcher",
              Theme = "@style/MyTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            global::FFImageLoading.ImageService.Instance.Initialize();
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());
            Crashlytics.Crashlytics.HandleManagedExceptions();

            LoadApplication(new App(new AndroidInitializer()));

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }
            //If debug you should reset the token each time.
//#if DEBUG
//            FirebasePushNotificationManager.Initialize(this, true);
//#else
              FirebasePushNotificationManager.Initialize(this, new ConversePushNotificationHandler(), false);
//#endif
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent); 
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {

            }
        }

        bool _ignoreNewFocus;
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            var focused = CurrentFocus;
            _ignoreNewFocus = (focused?.Parent is EditorRenderer entryRenderer && entryRenderer.Element.AutomationId == "ChatMessageEditor");
            var result = base.DispatchTouchEvent(ev);
            _ignoreNewFocus = false;
            return result;
        }

        public override View CurrentFocus
        {
            get
            {
                if (_ignoreNewFocus)
                {
                    return null;
                }

                return base.CurrentFocus;
            }
        }
    }
}
