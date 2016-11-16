
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace XiaomiMIUIHellas
{
	[Activity(Label = "Xiaomi Phones GSMArena list", NoHistory =true)]
	public class MiPhones : Activity
	{
		private WebView localWebView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.MiPhones);

			localWebView = FindViewById<WebView>(Resource.Id.miphoneswebview);
			localWebView.SetWebViewClient(new WebViewClient());
			localWebView.Settings.JavaScriptEnabled = true;
			localWebView.CanGoBack();
			localWebView.LoadUrl("http://www.gsmarena.com/xiaomi-phones-80.php");

			Button homebutton = FindViewById<Button>(Resource.Id.homebuttonMiPhones);
			Button refreshbutton = FindViewById<Button>(Resource.Id.refreshbuttonMiPhones);

			homebutton.Click += delegate
			{
				StartActivity(typeof(MainActivity));
			};
			refreshbutton.Click += delegate
			{
				localWebView.Reload();
			};

		}

		public override void OnBackPressed()
		{
			this.localWebView.GoBack();
		}
	}
}
