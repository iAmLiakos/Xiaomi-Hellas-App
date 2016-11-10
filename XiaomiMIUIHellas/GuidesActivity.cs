
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
	[Activity(Label = "Ευρετήριο Οδηγών")]
	public class GuidesActivity : Activity
	{
		private WebView localWebView;
		protected static TextView loadingBar;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.Guides);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext2);

			var client = new MyWebViewClient();

			localWebView = FindViewById<WebView>(Resource.Id.guideswebview);
			localWebView.SetWebViewClient(client);
			localWebView.Settings.JavaScriptEnabled = true;
			localWebView.CanGoBack();
			loadingBar.Visibility = ViewStates.Visible;
			localWebView.LoadUrl("https://xiaomi-miui.gr/community/index.php/Thread/13853-%CE%93%CE%B5%CE%BD%CE%B9%CE%BA%CF%8C-%CE%B5%CF%85%CF%81%CE%B5%CF%84%CE%AE%CF%81%CE%B9%CE%BF-%CE%BF%CE%B4%CE%B7%CE%B3%CF%8E%CE%BD-%CE%B3%CE%B9%CE%B1-%CE%BA%CE%AC%CE%B8%CE%B5-%CF%83%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AE/?postID=143618#post143618");
		
			Button homebutton = FindViewById<Button>(Resource.Id.homebuttonGuides);
			Button refreshbutton = FindViewById<Button>(Resource.Id.refreshbuttonGuides);

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
