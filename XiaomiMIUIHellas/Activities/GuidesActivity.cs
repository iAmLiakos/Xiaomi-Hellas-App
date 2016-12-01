
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
		private WebView webview;
		protected static TextView loadingBar;
		protected static String GUIDES_URL = "https://xiaomi-miui.gr/community/index.php/Thread/13853-%CE%93%CE%B5%CE%BD%CE%B9%CE%BA%CF%8C-%CE%B5%CF%85%CF%81%CE%B5%CF%84%CE%AE%CF%81%CE%B9%CE%BF-%CE%BF%CE%B4%CE%B7%CE%B3%CF%8E%CE%BD-%CE%B3%CE%B9%CE%B1-%CE%BA%CE%AC%CE%B8%CE%B5-%CF%83%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AE/?pageNo=1";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			this.Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

			SetContentView(Resource.Layout.WebSite);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
			ImageButton backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
			ImageButton forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

			setupWebview(GUIDES_URL);

			var homebutton = FindViewById<ImageButton>(Resource.Id.homebuttonWebSite);
			homebutton.SetImageResource(Resource.Drawable.home);
			var refreshbutton = FindViewById<ImageButton>(Resource.Id.refreshbuttonWebSite);
			refreshbutton.SetImageResource(Resource.Drawable.reload);

			//Action Bar buttons
			backbutton.SetImageResource(Resource.Drawable.backbutton);
			forwardbutton.SetImageResource(Resource.Drawable.forwardbutton);


			homebutton.Click += delegate
			{
				Finish();
				//StartActivity(typeof(MainActivity));

			};
			refreshbutton.Click += delegate
			{
				webview.Reload();
			};

			backbutton.Click += delegate
			{
				webview.GoBack();
			};

			forwardbutton.Click += delegate
			{
				webview.GoForward();
			};
		}

		public void setupWebview(string url) 
		{
			var client = new MyWebViewClient();

			webview = FindViewById<WebView>(Resource.Id.webSiteView);
			webview.SetWebViewClient(client);
			webview.Settings.JavaScriptEnabled = true;
			webview.Settings.LoadWithOverviewMode = true;
			webview.LongClickable = true;
			webview.Download += (object sender, DownloadEventArgs eee) =>
			{
				try
				{
					var source = Android.Net.Uri.Parse(eee.Url);
					var request = new DownloadManager.Request(source);
					request.AllowScanningByMediaScanner();
					request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
					request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, source.LastPathSegment);
					var manager = (DownloadManager)this.GetSystemService(Context.DownloadService);
					manager.Enqueue(request);
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				};
			};
			//webview.Settings.UseWideViewPort = true;
			//webview.Settings.SupportZoom();
			webview.Settings.SetSupportZoom(true);
			webview.Settings.BuiltInZoomControls = true;
			webview.Settings.DisplayZoomControls = false;
			loadingBar.Visibility = ViewStates.Visible;
			webview.LoadUrl(url);
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

			}

		}
	}
}
