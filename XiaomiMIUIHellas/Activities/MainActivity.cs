using Android.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Android.Media;
using Android.Content;
using Android.Preferences;

namespace XiaomiMIUIHellas
{
	[Activity(Label = "XiaomiGR", MainLauncher = true, NoHistory = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private ISharedPreferences mPrefs;
		private ISharedPreferencesEditor mEditor;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			LinearLayout hiddenlayout = FindViewById<LinearLayout>(Resource.Id.hiddenLayout);
			hiddenlayout.Visibility = Android.Views.ViewStates.Gone;
			bool visible = false;

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


			//var imageView = FindViewById<ImageView>(Resource.Id.hellasLogoView);
			//imageView.SetImageResource(Resource.Drawable.hellaslogo);

			var enterButton = FindViewById<ImageButton>(Resource.Id.websitebutton);
			//enterButton.SetImageResource(Resource.Drawable.loginblack);

			var dropdown = FindViewById<ImageButton>(Resource.Id.dropdownmenubutton);
			//dropdown.SetImageResource(Resource.Drawable.buttonscrolldown);

			var miImageButton = FindViewById<ImageButton>(Resource.Id.miImageButton);
			//miImageButton.SetImageResource(Resource.Drawable.mi3png);

			var xtrvButton = FindViewById<Button>(Resource.Id.xtrvImageButton);

			//Button bookmarksbutton = FindViewById<Button>(Resource.Id.bookmarkButton);

			Button guidesbutton = FindViewById<Button>(Resource.Id.guidesbutton);
			var vipbutton = FindViewById<ImageButton>(Resource.Id.VipButton);
			//vipbutton.SetImageResource(Resource.Drawable.supportbutton);
			//var mibutton = FindViewById<ImageButton>(Resource.Id.miImageButton);

			enterButton.Click += delegate {
				StartActivity(typeof(WebPageActivity));
			};

			guidesbutton.Click += delegate {
				StartActivity(typeof(GuidesActivity));
			};

			miImageButton.Click += delegate {
				StartActivity(typeof(MiPhones));
			};

			xtrvButton.Click += delegate {
				StartActivity(typeof(XtrvRoms));
			};

			vipbutton.Click += delegate {
				StartActivity(typeof(VipActivity));
			};

			dropdown.Click += delegate {
				if (!visible) 
				{
					hiddenlayout.Visibility = Android.Views.ViewStates.Visible;
					visible = true;
					return;
				}
				else
					hiddenlayout.Visibility = Android.Views.ViewStates.Gone;
					visible = false;
			};
		
		}
		public override void OnBackPressed()
		{
			Toast.MakeText(this, "Goodbye!", ToastLength.Long).Show();
			Finish();
		}
	}

}

