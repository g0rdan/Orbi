
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views.Attributes;
using Orbi.ViewModels;

namespace Orbi.Droid.Activities
{
    [Activity]
    public class AlbumsActivity : MvxAppCompatActivity<AlbumsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlbumsView);
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
                ViewModel.CreateAlbumCommand?.ExecuteAsync();

            return false;
		}
	}
}
