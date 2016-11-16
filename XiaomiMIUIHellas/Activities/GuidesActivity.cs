
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
	[Activity(Label = "Ευρετήριο Οδηγών", NoHistory=true)]
	public class GuidesActivity : Activity
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
			localWebView.LoadUrl("https://xiaomi-miui.gr/community/index.php/Thread/13853-%CE%93%CE%B5%CE%BD%CE%B9%CE%BA%CF%8C-%CE%B5%CF%85%CF%81%CE%B5%CF%84%CE%AE%CF%81%CE%B9%CE%BF-%CE%BF%CE%B4%CE%B7%CE%B3%CF%8E%CE%BD-%CE%B3%CE%B9%CE%B1-%CE%BA%CE%AC%CE%B8%CE%B5-%CF%83%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AE/?pageNo=1");
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
