
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XiaomiMIUIHellas
{
	[Activity(Label = "Helpers")]
	public class Helpers : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		}
		public void getSystemPrefs() 
		{
			TextView deviceinfo = FindViewById<TextView>(Resource.Id.deviceinfotext);
			PackageManager pm = this.PackageManager;
		try
			{

				string manufacturer = Build.Manufacturer;
				string model = Build.Model;
				PackageInfo info = pm.GetPackageInfo(this.PackageName, 0);
				string version = info.VersionName;
				int versioncode = info.VersionCode;
				TextView appversion = FindViewById<TextView>(Resource.Id.appversionnumber);
				appversion.Text = "V" + version;
				deviceinfo.Text = manufacturer + " " + model;

			}
			catch (System.Exception)
			{
				deviceinfo.Text = "-";
				return;
			}

			try
			{
				string manufacturer = Build.Manufacturer;
				string model = Build.Model;
				//string vers = Build.Id;
				string grversion = getSystemProperty("ro.modversion");
				string grversiontrimmed = grversion.Replace("-", " ");
				deviceinfo.Text = manufacturer + " " + model + "\n" + grversiontrimmed;

			}
			catch (System.Exception)
			{
				try
				{
					string manufacturer = Build.Manufacturer;
					string model = Build.Model;
				}
				catch (System.Exception)
				{
					deviceinfo.Text = "-";
					return;
				}

			}
		}

		public string getSystemProperty(string key)
		{
			string miuiversion = null;

			try
			{
				var buildprop = System.IO.File.OpenRead("/system/build.prop");

				using (var streamReader = new StreamReader(buildprop))
				{
					//value = streamReader.ReadToEnd();
					while (!streamReader.EndOfStream)
					{
						var line = streamReader.ReadLine();
						if (line.IndexOf(key, System.StringComparison.CurrentCultureIgnoreCase) >= 0) { miuiversion = line.Remove(0, 14); }

					}
				}
			}
			catch (System.Exception)
			{
				return null;
			}
			return miuiversion;
		}
	}
}
