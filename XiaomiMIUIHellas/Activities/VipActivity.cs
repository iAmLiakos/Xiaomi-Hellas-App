
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
	[Activity(Label = "Vip")]
	public class VipActivity : Activity
	{
		private WebView localWebView;
		protected static TextView loadingBar;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.WebSite);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
			ImageButton backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
			ImageButton forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

			var client = new MyWebViewClient();

			localWebView = FindViewById<WebView>(Resource.Id.webSiteView);
			localWebView.SetWebViewClient(client);
			localWebView.Settings.JavaScriptEnabled = true;
			localWebView.LoadUrl("https://xiaomi-miui.gr/community/index.php/PaidSubscriptionList/");
			loadingBar.Visibility = ViewStates.Visible;

			var homebutton = FindViewById<ImageButton>(Resource.Id.homebuttonWebSite);
			homebutton.SetImageResource(Resource.Drawable.home);
			var refreshbutton = FindViewById<ImageButton>(Resource.Id.refreshbuttonWebSite);
			refreshbutton.SetImageResource(Resource.Drawable.reload);

			//Action Bar buttons
			backbutton.SetImageResource(Resource.Drawable.backbutton);
			forwardbutton.SetImageResource(Resource.Drawable.forwardbutton);


			homebutton.Click += delegate
			{
				StartActivity(typeof(MainActivity));
				Finish();
			};
			refreshbutton.Click += delegate
			{
				localWebView.Reload();
			};

			backbutton.Click += delegate
			{
				localWebView.GoBack();
			};

			forwardbutton.Click += delegate
			{
				localWebView.GoForward();
			};
		}

		public override void OnBackPressed()
		{
			this.localWebView.GoBack();
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

			}

		}
	}
}

