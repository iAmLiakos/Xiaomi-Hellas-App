
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
	[Activity(Label = "Xtrv Roms")]
	public class XtrvRoms : Activity
	{
		private WebView localWebView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.XtrvRoms);

			localWebView = FindViewById<WebView>(Resource.Id.xtrvWebView);
			localWebView.SetWebViewClient(new WebViewClient());
			localWebView.Settings.JavaScriptEnabled = true;
			localWebView.LoadUrl("https://xiaomi-miui.gr/community/wsif/index.php/Category/366-XIAOMI-%CE%A3%CF%85%CF%83%CE%BA%CE%B5%CF%85%CE%AD%CF%82/");
		}

		//public override void OnBackPressed()
		//{
		//	this.localWebView.GoBack();
		//}
	}
}
