
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
	[Activity(Label = "Xiaomi Hellas Community", ConfigurationChanges= Android.Content.PM.ConfigChanges.Keyboard|Android.Content.PM.ConfigChanges.KeyboardHidden|Android.Content.PM.ConfigChanges.Orientation|Android.Content.PM.ConfigChanges.ScreenSize)]
	public class WebPageActivity : Activity
	{
		
		protected static TextView loadingBar;
		protected static WebView webview;
		protected static ImageButton backbutton;
		protected static ImageButton forwardbutton;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.WebSite);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
			backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
			forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

			var client = new MyWebViewClient();

			webview = FindViewById<WebView>(Resource.Id.webSiteView);
			webview.SetWebViewClient(client);
			webview.Settings.JavaScriptEnabled = true;
			webview.Settings.LoadWithOverviewMode = true;
			//webview.Settings.UseWideViewPort = true;
			//webview.Settings.SupportZoom();
			webview.Settings.SetSupportZoom(true);
			webview.Settings.BuiltInZoomControls = true;
			webview.Settings.DisplayZoomControls = false;
			loadingBar.Visibility = ViewStates.Visible;
			webview.LoadUrl("https://xiaomi-miui.gr/community/");

			var homebutton = FindViewById<ImageButton>(Resource.Id.homebuttonWebSite);
			homebutton.SetImageResource(Resource.Drawable.home);
			var refreshbutton = FindViewById<ImageButton>(Resource.Id.refreshbuttonWebSite);
			refreshbutton.SetImageResource(Resource.Drawable.reload);

			//Action Bar buttons
			backbutton.SetImageResource(Resource.Drawable.backbutton);
			forwardbutton.SetImageResource(Resource.Drawable.forwardbutton);

			                                           
			homebutton.Click += delegate {
				//StartActivity(typeof(MainActivity));
				Finish();
			};
			refreshbutton.Click += delegate {
				webview.Reload();
			};

			backbutton.Click += delegate {
				webview.GoBack();
				//webview.ScrollTo(0, 0);
			};

			forwardbutton.Click += delegate {
				webview.GoForward();
				//webview.ScrollTo(0, 0);
			};
		}

		public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.Back && webview.CanGoBack())
			{
				webview.GoBack();
				return true;
			}

			return base.OnKeyDown(keyCode, e);
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
				loadingBar.Visibility = ViewStates.Gone;
				//webview.ScrollTo(0, 0);
			}

		}

	}
}
