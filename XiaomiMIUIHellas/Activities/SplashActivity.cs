using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;


namespace XiaomiMIUIHellas
{
	[Activity(Label = "SplashActivity", NoHistory = true, Theme="@style/Theme.FullScreen")]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		protected override void OnResume()
		{
			base.OnResume();

			Task startupWork = new Task(() =>
			{
				Task.Delay(1000);  // Simulate a bit of startup work.
			});

			startupWork.ContinueWith(t =>
			{
				StartActivity(new Intent(Application.Context, typeof(MainActivity)));
				OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);

			}, TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}
	}
}

