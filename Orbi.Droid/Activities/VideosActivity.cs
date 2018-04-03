using System;
using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;

namespace Orbi.Droid.Activities
{
    [Activity]
    public class VideosActivity : MvxAppCompatActivity<VideosViewModel>
    {
        public VideosActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VideosView);
            Title = ViewModel.Title;
        }
    }
}
