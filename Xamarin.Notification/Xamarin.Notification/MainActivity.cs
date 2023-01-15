using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Support.V4.App;
using Android.Media;
using Android.Graphics;

namespace Xamarin.Notification
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;

        int count = 1;
        static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            CreateNotificationChannel();

            Button button = FindViewById<Button>(Resource.Id.click_button);
            TextView text = FindViewById<TextView>(Resource.Id.result);

            button.Click += delegate {
                text.Text = string.Format("{0} clicks!", count++);

                Bitmap bitMap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_cc_checkmark);
                var notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                        .SetSmallIcon(Resource.Drawable.ic_cc_checkmark)
                        .SetLargeIcon(bitMap)
                        .SetContentTitle("Please")
                        .SetContentText("A notification!")
                        .Build();

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);

                //NotificationManagerCompat notificationManager = NotificationManagerCompat.From(this);
                notificationManager.Notify(count, notification);

            };

            textView = FindViewById<TextView>(Resource.Id.text);
            SetAmbientEnabled();
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = "Local Notifications";
            var description = "The count from MainActivity";
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}


