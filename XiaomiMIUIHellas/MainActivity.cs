using Android.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Android.Media;

namespace XiaomiMIUIHellas
{
	[Activity(Label = "XiaomiGR", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var imageView = FindViewById<ImageView>(Resource.Id.hellasLogoView);
			imageView.SetImageResource(Resource.Drawable.logoHD);

			var miImageButton = FindViewById<ImageButton>(Resource.Id.miImageButton);
			miImageButton.SetImageResource(Resource.Drawable.mi3png);

			var xtrvImageButton = FindViewById<ImageButton>(Resource.Id.xtrvImageButton);
			xtrvImageButton.SetImageResource(Resource.Drawable.xtrvroms);

			Button forumbutton = FindViewById<Button>(Resource.Id.websitebutton);
			Button guidesbutton = FindViewById<Button>(Resource.Id.guidesbutton);
			//var mibutton = FindViewById<ImageButton>(Resource.Id.miImageButton);

			forumbutton.Click += delegate {
				StartActivity(typeof(WebPageActivity));
			};

			guidesbutton.Click += delegate {
				StartActivity(typeof(GuidesActivity));
			};

			miImageButton.Click += delegate {
				StartActivity(typeof(MiPhones));
			};

			xtrvImageButton.Click += delegate {
				StartActivity(typeof(XtrvRoms));
			};
		}
	}
}

