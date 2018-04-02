using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;
using Acr.UserDialogs;

namespace Orbi.Droid
{
    [Activity(Label = "orbi", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);         
            UserDialogs.Init(this);
			SetContentView(Resource.Layout.MainView);
        }
    }
}

