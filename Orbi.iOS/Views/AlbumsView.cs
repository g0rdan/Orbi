using System;
using MvvmCross.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    public class AlbumsView : MvxViewController<AlbumsViewModel>
    {
        public AlbumsView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Blue;
        }
    }
}
