﻿
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
	[Activity(Label = "@string/tablabel_name", WindowSoftInputMode = SoftInput.AdjustResize, ConfigurationChanges = Android.Content.PM.ConfigChanges.Keyboard | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class TabsActivity : Activity
	{
		protected static TextView loadingBar;
		protected static WebView webview;
		protected static String GUIDES_URL = "https://xiaomi-miui.gr/community/index.php/Thread/13853-%CE%93%CE%B5%CE%BD%CE%B9%CE%BA%CF%8C-%CE%B5%CF%85%CF%81%CE%B5%CF%84%CE%AE%CF%81%CE%B9%CE%BF-%CE%BF%CE%B4%CE%B7%CE%B3%CF%8E%CE%BD-%CE%B3%CE%B9%CE%B1-%CE%BA%CE%AC%CE%B8%CE%B5-%CF%83%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AE/?pageNo=1";
		protected static String ROMS_URL = "https://xiaomi-miui.gr/community/wsif/index.php/Category/366-XIAOMI-%CE%A3%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AD%CF%82/";
		protected static String MIPHONES_URL = "http://www.gsmarena.com/xiaomi-phones-80.php";

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			SetContentView(Resource.Layout.TabsLayout);

			var tab = ActionBar.NewTab();
			tab.SetText(Resource.String.guides);
			//tab.SetIcon(Resource.Drawable.youtubelogo);
			tab.TabSelected += (sender, args) =>
			{
				SetContentView(Resource.Layout.WebSite);

				loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
				ImageButton backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
				ImageButton forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

				var client = new MyWebViewClient();

				webview = FindViewById<WebView>(Resource.Id.webSiteView);
				webview.SetWebViewClient(client);
				webview.Settings.JavaScriptEnabled = true;
				webview.Settings.LoadWithOverviewMode = true;
				webview.LongClickable = true;
				//webview.Settings.UseWideViewPort = true;
				//webview.Settings.SupportZoom();
				webview.Settings.SetSupportZoom(true);
				webview.Settings.BuiltInZoomControls = true;
				webview.Settings.DisplayZoomControls = false;
				loadingBar.Visibility = ViewStates.Visible;
				webview.Download += (object sender2, DownloadEventArgs eee) =>
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
					}
				};
				webview.LoadUrl(GUIDES_URL);

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
			};
			//Second tab
			ActionBar.AddTab(tab);

			tab = ActionBar.NewTab();
			tab.SetText(Resource.String.downloads);
			//tab.SetIcon(Resource.Drawable.twitterlogo);
			tab.TabSelected += (sender, args) =>
			{
				SetContentView(Resource.Layout.WebSite);

				loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
				ImageButton backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
				ImageButton forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

				var client = new MyWebViewClient();

				webview = FindViewById<WebView>(Resource.Id.webSiteView);
				webview.SetWebViewClient(client);
				webview.Settings.JavaScriptEnabled = true;
				webview.Settings.LoadWithOverviewMode = true;
				webview.LongClickable = true;
				//webview.Settings.UseWideViewPort = true;
				//webview.Settings.SupportZoom();
				webview.Settings.SetSupportZoom(true);
				webview.Settings.BuiltInZoomControls = true;
				webview.Settings.DisplayZoomControls = false;
				loadingBar.Visibility = ViewStates.Visible;
				webview.Download += (object sender2, DownloadEventArgs eee) =>
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
					}
				};
				webview.LoadUrl(ROMS_URL);

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
				// Do something when tab is selected
			};

			ActionBar.AddTab(tab);
			tab = ActionBar.NewTab();
			tab.SetText(Resource.String.devices);
			tab.TabSelected += (sender, args) =>
			{
				SetContentView(Resource.Layout.WebSite);

				loadingBar = (TextView)FindViewById(Resource.Id.loadingtext);
				ImageButton backbutton = (ImageButton)FindViewById(Resource.Id.backpageButton);
				ImageButton forwardbutton = (ImageButton)FindViewById(Resource.Id.forwardpageButton);

				var client = new MyWebViewClient();

				webview = FindViewById<WebView>(Resource.Id.webSiteView);
				webview.SetWebViewClient(client);
				webview.Settings.JavaScriptEnabled = true;
				webview.Settings.LoadWithOverviewMode = true;
				webview.LongClickable = true;
				//webview.Settings.UseWideViewPort = true;
				//webview.Settings.SupportZoom();
				webview.Settings.SetSupportZoom(true);
				webview.Settings.BuiltInZoomControls = true;
				webview.Settings.DisplayZoomControls = false;
				loadingBar.Visibility = ViewStates.Visible;

				webview.LoadUrl(MIPHONES_URL);

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
				// Do something when tab is selected
			};

			ActionBar.AddTab(tab);
			tab = ActionBar.NewTab();
			tab.SetText("Me");
			tab.TabSelected += (sender, args) =>
			{
				SetContentView(Resource.Layout.AboutMyDevice);
				
				try
				{
					string manufacturer = Build.Manufacturer;
					string model = Build.Model;
					string board = Build.Board;
					string booloader = Build.Bootloader;
					string brand = Build.Brand;
					string cpuabi = Build.CpuAbi;
					string cpuabi2 = Build.CpuAbi2;
					string device = Build.Device;
					string display = Build.Display;
					string fingerprint = Build.Fingerprint;
					string hardware = Build.Hardware;
					string host = Build.Host;
					string id = Build.Id;
					string product = Build.Product;
					string radio = Build.Radio;
					string radioversion = Build.RadioVersion;
					string serial = Build.Serial;
					long time = Build.Time;
					string type = Build.Type;
					string user = Build.User;

					TextView text = FindViewById<TextView>(Resource.Id.aboutmydevice);
					text.Text = "manufacturer: "+manufacturer + "\n" +"model: "+ model + "\n" +"board: " + board + "\n" +"bootloader: " + booloader + "\n" +"brand: " + brand+ "\n" +"cpuabi: " + cpuabi + "\n" +"cpuabi2: " +
						cpuabi2 + "\n" +"device: " + device + "\n" +"display: " + display + "\n" +"fingerprint: " + fingerprint+ "\n" +"hardware: " + hardware+ "\n" +"host: " + host+ "\n" +"id: " + id+ "\n" +"product: " + product + "\n" +"radio: " + radio + "\n" +"radioversion: " + radioversion + "\n" +"serial: " + serial+ "\n" +"time: " + time+ "\n" +"type: " + type+ "\n" +"user: " + user;				
				}

				catch (Exception)
				{
					TextView text = FindViewById<TextView>(Resource.Id.aboutmydevice);
					text.Text = "error";
				}

			};

			ActionBar.AddTab(tab);
			// Create your application here
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
