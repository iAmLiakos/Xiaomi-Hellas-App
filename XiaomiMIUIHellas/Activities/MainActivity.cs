using Android.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Android.Media;
using Android.Content;
using Android.Preferences;
using Android.Util;
using Android.Content.PM;
using Android.Net;
using Java.Lang;
using Java.IO;
using System.IO;
using System.Collections.Generic;

namespace XiaomiMIUIHellas
{
	[Activity(Label = "XiaomiGR", Theme = "@style/Theme.FullScreen", MainLauncher = true, Icon = "@mipmap/icon",ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		private ISharedPreferences mPrefs;
		private ISharedPreferencesEditor mEditor;
		public static string FACEBOOK_URL = "https://www.facebook.com/MiuiHellas";
		public static string FACEBOOK_PAGE_ID = "MiuiHellas";
		public static string YOUTUBE_URL = "https://www.youtube.com/channel/UCIt1QNxkauz1XgpvtuKJTuw";
		public static string TWITTER_URL = "https://twitter.com/Miuigr";
		public static string GOOGLEPLUS_URL = "https://plus.google.com/107593835557001327926";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			//this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);
			OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
			PackageManager pm = this.PackageManager;
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			//LinearLayout hiddenlayout = FindViewById<LinearLayout>(Resource.Id.hiddenLayout);
			//hiddenlayout.Visibility = Android.Views.ViewStates.Gone;
			//bool visible = false;

			//Device info front screen
			//string osVersion = Build.VERSION.Release;
			//string miuiversion = Build.User;
			try
			{
				string manufacturer = Build.Manufacturer;
				string model = Build.Model;
				//string vers = Build.Id;
				string grversion = getSystemProperty("ro.modversion");
				string grversiontrimmed = grversion.Replace("-", " ");

				TextView deviceinfo = FindViewById<TextView>(Resource.Id.deviceinfotext);
				//long mb = getMbConsumed();
				//deviceinfo.Text = manufacturer+" "+model+"\nAndroid Version: " + osVersion +"\nMIUI Version: "+grversiontrimmed;
				deviceinfo.Text = manufacturer + " " + model + "\n" + grversiontrimmed;
				//deviceinfo.Visibility = Android.Views.ViewStates.Invisible;

				PackageInfo info = pm.GetPackageInfo(this.PackageName, 0);
				string version = info.VersionName;
				int versioncode = info.VersionCode;
				TextView appversion = FindViewById<TextView>(Resource.Id.appversionnumber);
				appversion.Text = "Version"+"\n"+version;

			}
			catch (System.Exception)
			{
				TextView deviceinfo = FindViewById<TextView>(Resource.Id.deviceinfotext);
				deviceinfo.Text = "-";
			}

			//SharedPref gia monadiki emfanisi tou alertdialog
			mPrefs = PreferenceManager.GetDefaultSharedPreferences(this);
			mEditor = mPrefs.Edit();

			bool dialogshown = mPrefs.GetBoolean("DialogShown", false);
			if (!dialogshown)
			{
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("Καλωσήρθες στην mobile εφαρμογή που σχεδιάστηκε για εύκολη περιήγηση στην κοινότητα μας!").SetTitle("Xiaomi-Miui Hellas");
				builder.SetPositiveButton("Ok", (sender, e) => { });
				AlertDialog dialog = builder.Create();
				dialog.Show();

				mEditor.PutBoolean("DialogShown", true);
				mEditor.Commit();
			}

			var enterButton = FindViewById<ImageButton>(Resource.Id.websitebutton);
			var dropdown = FindViewById<ImageButton>(Resource.Id.dropdownmenubutton);
			var vipbutton = FindViewById<ImageButton>(Resource.Id.VipButton);
			var facebookbutton = FindViewById<ImageButton>(Resource.Id.FacebookButton);
			var youtubebutton = FindViewById<ImageButton>(Resource.Id.YoutubeButton);
			var twitterbutton = FindViewById<ImageButton>(Resource.Id.TwitterButton);
			var googleplusbutton = FindViewById<ImageButton>(Resource.Id.GooglePlusButton);

			//deviceinfoup.Visibility = Android.Views.ViewStates.Invisible;
			//deviceinfodown.Visibility = Android.Views.ViewStates.Invisible;

			enterButton.Click += delegate {
				StartActivity(typeof(WebPageActivity));
			};

			vipbutton.Click += delegate {
				StartActivity(typeof(VipActivity));
			};

			dropdown.Click += delegate
			{
				StartActivity(typeof(TabsActivity));
			};

			facebookbutton.Click += delegate {
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("Πρόκειται να επισκεφτείτε τη σελίδα μας στο Facebook!").SetTitle("Μήνυμα απο Xiaomi-Miui Hellas");
				builder.SetPositiveButton("ΕΝΤΑΞΕΙ", (sender, e) => {Intent fb = newFacebookIntent(pm, FACEBOOK_URL);
					StartActivity(fb); });
				builder.SetNegativeButton("ΠΙΣΩ", (sender, e) => { }	);
				AlertDialog dialog = builder.Create();
				dialog.Show();

			};
			youtubebutton.Click += delegate {
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("Πρόκειται να επισκεφτείτε τη σελίδα μας στο Youtube. Μη ξεχάσετε να κάνετε Subscribe στο κανάλι μας!").SetTitle("Μήνυμα απο Xiaomi-Miui Hellas");
				builder.SetPositiveButton("ΕΝΤΑΞΕΙ", (sender, e) =>
				{
					try
					{
						Intent ytube = new Intent(Intent.ActionView);
						ytube.SetPackage("com.google.android.youtube");
						ytube.SetData(Uri.Parse(YOUTUBE_URL));
						StartActivity(ytube);
					}
					catch (ActivityNotFoundException)
					{
						Intent ytube = new Intent(Intent.ActionView);
						ytube.SetData(Uri.Parse(YOUTUBE_URL));
						StartActivity(ytube);
					}

				});
				builder.SetNegativeButton("ΠΙΣΩ", (sender, e) => { });
				AlertDialog dialog = builder.Create();
				dialog.Show();

			};
			twitterbutton.Click += delegate {
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("Πρόκειται να επισκεφτείτε τη σελίδα μας στο Twitter. Follow us!").SetTitle("Μήνυμα απο Xiaomi-Miui Hellas");
				builder.SetPositiveButton("ΕΝΤΑΞΕΙ", (sender, e) =>
				{
					try
					{
						Intent twitter = new Intent(Intent.ActionView);
						twitter.SetPackage("com.twitter.android");
						twitter.SetData(Uri.Parse(TWITTER_URL));
						StartActivity(twitter);
					}
					catch (ActivityNotFoundException)
					{
						Intent twitter = new Intent(Intent.ActionView);
						twitter.SetData(Uri.Parse(TWITTER_URL));
						StartActivity(twitter);
					}

				});
				builder.SetNegativeButton("ΠΙΣΩ", (sender, e) => { });
				AlertDialog dialog = builder.Create();
				dialog.Show();


			};
			googleplusbutton.Click += delegate {
				
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("Πρόκειται να επισκεφτείτε τη σελίδα μας στο Google+").SetTitle("Μήνυμα απο Xiaomi-Miui Hellas");
				builder.SetPositiveButton("ΕΝΤΑΞΕΙ", (sender, e) =>
				{
					try
					{
						Intent gplus = new Intent(Intent.ActionView);
						gplus.SetPackage("com.google.android.apps.plus");
						gplus.SetData(Uri.Parse(GOOGLEPLUS_URL));
						StartActivity(gplus);
					}
					catch (ActivityNotFoundException)
					{
						Intent gplus = new Intent(Intent.ActionView);
						gplus.SetData(Uri.Parse(GOOGLEPLUS_URL));
						StartActivity(gplus);
					}

				});
				builder.SetNegativeButton("ΠΙΣΩ", (sender, e) => { });
				AlertDialog dialog = builder.Create();
				dialog.Show();


			};
			//deviceinfodown.Click += delegate {
			//	deviceinfo.Visibility = Android.Views.ViewStates.Visible;
			//	deviceinfodown.Visibility = Android.Views.ViewStates.Invisible;
			//	deviceinfoup.Visibility = Android.Views.ViewStates.Visible;
			//};
			//deviceinfoup.Click += delegate
			//{
			//	deviceinfo.Visibility = Android.Views.ViewStates.Invisible;
			//	deviceinfodown.Visibility = Android.Views.ViewStates.Visible;
			//	deviceinfoup.Visibility = Android.Views.ViewStates.Invisible;
			//};
		
		}
		public override void OnBackPressed()
		{
			//Toast.MakeText(this, "Goodbye!", ToastLength.Long).Show();
			Finish();
		}

		//Facebook intent
		public static Intent newFacebookIntent(PackageManager pm, string url) 
		{
			Uri uri = Uri.Parse(url);
			try
			{
				ApplicationInfo applicationinfo = pm.GetApplicationInfo("com.facebook.katana", 0);
				if (applicationinfo.Enabled) {
					uri = Uri.Parse("fb://facewebmodal/f?href=" + url);
				}
			}
			catch (System.Exception)
			{
				return null;
			}

			return new Intent(Intent.ActionView, uri);
		}

		public string getSystemProperty(string key) 
		{
			string miuiversion= null;

			try
			{
				var buildprop = System.IO.File.OpenRead("/system/build.prop");

				using (var streamReader = new StreamReader(buildprop)) 
				{
					//value = streamReader.ReadToEnd();
					while (!streamReader.EndOfStream) 
					{
						var line = streamReader.ReadLine();
						if (line.IndexOf(key, System.StringComparison.CurrentCultureIgnoreCase) >= 0) { miuiversion= line.Remove(0,14); }

					}
				}
			}
			catch (System.Exception)
			{
				return null;
			}
			return miuiversion;
		}

		public long getMbConsumed() 
		{
			//float result = 0;

			ActivityManager manager = (ActivityManager)this.GetSystemService(ActivityService);
			IList<Android.App.ActivityManager.RunningAppProcessInfo> runningApps = manager.RunningAppProcesses;

			long bytes = Android.Net.TrafficStats.MobileRxBytes;
			long bytes2 = TrafficStats.MobileTxBytes;
			long result = bytes + bytes2;
			return result;
		}
	}



}

