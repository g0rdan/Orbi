
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
using MvvmCross.Droid.Support.V7.AppCompat;
using Orbi.ViewModels;

namespace Orbi.Droid.Activities
{
    [Activity(Label = "AlbumsActivity")]
    public class AlbumsActivity : MvxAppCompatActivity<AlbumsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.AlbumsView);
        }
    }
}
