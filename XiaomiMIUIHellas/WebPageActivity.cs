
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
	[Activity(Label = "Xiaomi Hellas Community")]
	public class WebPageActivity : Activity
	{
		
		protected static TextView loadingBar;
		public WebView webview;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.WebSite);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);


			var client = new MyWebViewClient();

			webview = FindViewById<WebView>(Resource.Id.webSiteView);
			webview.SetWebViewClient(client);
			webview.Settings.JavaScriptEnabled = true;
			webview.CanGoBack();
			loadingBar.Visibility = ViewStates.Visible;
			webview.LoadUrl("https://xiaomi-miui.gr/community/");

			Button homebutton = FindViewById<Button>(Resource.Id.homebuttonWebSite);
			Button refreshbutton = FindViewById<Button>(Resource.Id.refreshbuttonWebSite);

			homebutton.Click += delegate {
				StartActivity(typeof(MainActivity));
			};
			refreshbutton.Click += delegate {
				webview.Reload();
			};


		}
		public override void OnBackPressed()
		{
			this.webview.GoBack();
		}


		private class MyWebViewClient : WebViewClient
		{
			public override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
			{
				base.OnPageStarted(view, url, favicon);
				loadingBar.Visibility = ViewStates.Visible;

			}

			public override void OnPageFinished(WebView view, string url)
			{
				base.OnPageFinished(view, url);
				loadingBar.Visibility = ViewStates.Invisible;

			}

		}
	}
}
