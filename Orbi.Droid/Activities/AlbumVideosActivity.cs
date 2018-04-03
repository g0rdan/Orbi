using System;
using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;

namespace Orbi.Droid.Activities
{
    [Activity]
    public class AlbumVideosActivity : MvxAppCompatActivity<AlbumVideosViewModel>
    {
        public AlbumVideosActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlbumVideosView);
            Title = ViewModel.Title;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.right_menu, menu);
            menu.RemoveItem(Resource.Id.done);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.add)
                ViewModel.OpenVideosForSelectCommand?.Execute();

            return false;
        }
    }
}
