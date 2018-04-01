using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
	public class MainView : MvxViewController<MainViewModel>
    {
        UIButton _allVideosBtn;
        UIButton _albumsBtn;

        public MainView()
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;
            InitViews();

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(_allVideosBtn).To(vm => vm.OpenAllVideosCommand);
            set.Bind(_albumsBtn).To(vm => vm.OpenAlbumsCommand);
            set.Apply();
		}

        void InitViews()
        {
            _allVideosBtn = new UIButton(UIButtonType.System);
            _allVideosBtn.SetTitle("Все видео", UIControlState.Normal);
            _allVideosBtn.SizeToFit();
            _albumsBtn = new UIButton(UIButtonType.System);
            _albumsBtn.SetTitle("Альбомы", UIControlState.Normal);
            _albumsBtn.SizeToFit();

            _allVideosBtn.Frame = new CGRect(new CGPoint((View.Frame.Width / 2 - _allVideosBtn.Frame.Width / 2), (View.Frame.Height / 2 - _allVideosBtn.Frame.Height / 2)), _allVideosBtn.Frame.Size);
            _albumsBtn.Frame = new CGRect(new CGPoint((View.Frame.Width / 2 - _albumsBtn.Frame.Width / 2), _allVideosBtn.Frame.Bottom + 10), _albumsBtn.Frame.Size);

            View.AddSubviews(_allVideosBtn, _albumsBtn);
        }
	}
}
