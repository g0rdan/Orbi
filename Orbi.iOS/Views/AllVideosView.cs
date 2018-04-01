using System;
using MvvmCross.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    public class AllVideosView : MvxViewController<AllVideosViewModel>
    {
        public AllVideosView()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Green;
		}
	}
}
