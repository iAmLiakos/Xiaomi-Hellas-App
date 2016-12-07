
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
	[Activity(Label = "Hellas Community", Theme="@style/Theme.FullScreen" , WindowSoftInputMode = SoftInput.AdjustResize ,ConfigurationChanges= Android.Content.PM.ConfigChanges.Keyboard|Android.Content.PM.ConfigChanges.KeyboardHidden|Android.Content.PM.ConfigChanges.Orientation|Android.Content.PM.ConfigChanges.ScreenSize)]
	public class WebPageActivity : Activity
	{
		
		protected static TextView loadingBar;
		protected static WebView webview;
		protected static ImageButton backbutton;
		protected static ImageButton forwardbutton;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
			SetContentView(Resource.Layout.WebSite);

			loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
			backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
			forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

			//var client = new MyWebViewClient();

			webview = FindViewById<WebView>(Resource.Id.webSiteView);
			WebSettings websettings = webview.Settings;
			websettings.AllowFileAccess = true;
			websettings.AllowFileAccessFromFileURLs = true;
			webview.SetWebChromeClient(new WebChromeClient());
			webview.SetWebViewClient(new MyWebViewClient());
			webview.Settings.JavaScriptEnabled = true;
			webview.AddJavascriptInterface(new WebViewJavaScriptInterface(this), "Android");
			webview.Settings.LoadWithOverviewMode = true;
			webview.Settings.UseWideViewPort = true;
			webview.Settings.DomStorageEnabled = true;
			//webview.Settings.JavaScriptCanOpenWindowsAutomatically = false;
			//webview.Settings.SupportZoom();
			webview.Settings.SetSupportZoom(true);
			webview.Settings.BuiltInZoomControls = true;
			webview.Settings.DisplayZoomControls = false;
			//webview.LongClickable = true;
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
					Toast.MakeText(this, "Downloading...", ToastLength.Short).Show();
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				};
			};
			loadingBar.Visibility = ViewStates.Visible;
			webview.LoadUrl("https://xiaomi-miui.gr/community/");


			var homebutton = FindViewById<ImageButton>(Resource.Id.homebuttonWebSite);
			homebutton.SetImageResource(Resource.Drawable.home);
			var refreshbutton = FindViewById<ImageButton>(Resource.Id.refreshbuttonWebSite);
			refreshbutton.SetImageResource(Resource.Drawable.reload);

			//Action Bar buttons
			backbutton.SetImageResource(Resource.Drawable.backbutton);
			forwardbutton.SetImageResource(Resource.Drawable.forwardbutton);


				homebutton.Click += delegate
				{
					//StartActivity(typeof(MainActivity));
					Finish();
					OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
				};
				refreshbutton.Click += delegate
				{
					webview.Reload();
				};

				backbutton.Click += delegate
				{
					webview.GoBack();
					//webview.ScrollTo(0, 0);
				};

				forwardbutton.Click += delegate
				{
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

		/*
 * JavaScript Interface. Web code can access methods in here 
 * (as long as they have the @JavascriptInterface annotation)
 */
		public class WebViewJavaScriptInterface : Java.Lang.Object
		{

			private Context context;

			/*
			 * Need a reference to the context in order to sent a post message
			 */
			public WebViewJavaScriptInterface(Context context)
			{
				this.context = context;
			}

			/* 
			 * This method can be called from Android. @JavascriptInterface 
			 * required after SDK version 17. 
			 */
			[JavascriptInterface]
			public void makeToast(String message, bool lengthLong)
			{
				Toast.MakeText(context, message, (lengthLong ? ToastLength.Long : ToastLength.Short)).Show();
			}
		}


	}
}
