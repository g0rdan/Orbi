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
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.VideosView);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.albums_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.add_album)
                ViewModel.OpenVideosForSelectCommand?.Execute();

            return false;
        }
    }
}
