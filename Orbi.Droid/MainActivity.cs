using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;

namespace Orbi.Droid
{
    [Activity(Label = "orbi", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);         
            // Set our view from the "main" layout resource
			SetContentView(Resource.Layout.MainView);
        }
    }
}

