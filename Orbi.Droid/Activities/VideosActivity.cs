using System;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;

namespace Orbi.Droid.Activities
{
    [Activity(Label = "VideosActivity")]
    public class VideosActivity : MvxAppCompatActivity<VideosViewModel>
    {
        public VideosActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.VideosView);
        }
    }
}
