using System;
using System.Linq;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Orbi.iOS.TableSources;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    [MvxChildPresentation]
    public class VideosView : MvxViewController<VideosViewModel>
    {
        UIBarButtonItem _rightBtn;
        UILabel _titleLabel;
        UITableView _videosTableView;
        VideosTableSource _source;

        public VideosView()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            switch (ViewModel.ScreenType)
            {
                case VideoScreenType.Select:
                    _rightBtn = new UIBarButtonItem(UIBarButtonSystemItem.Done);
                    break;
                case VideoScreenType.Custom:
                    _rightBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
                    break;
            }

            if (_rightBtn != null)
                NavigationItem.RightBarButtonItem = _rightBtn;

            InitTitleView();
            InitTable();

            var set = this.CreateBindingSet<VideosView, VideosViewModel>();

            switch (ViewModel.ScreenType)
            {
                case VideoScreenType.Select:
                    set.Bind(_rightBtn).To(vm => vm.AddVideosCommand);
                    set.Bind(_rightBtn).For("Enabled").To(vm => vm.DoneBtnEnabled);
                    break;
                case VideoScreenType.Custom:
                    set.Bind(_rightBtn).To(vm => vm.OpenVideosForSelectCommand);
                    break;
            }

            set.Bind(_titleLabel).To(vm => vm.Title);
            set.Bind(_source).To(vm => vm.Items);
            set.Apply();
		}

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            _videosTableView.Frame = new CGRect(CGPoint.Empty, View.Frame.Size);
        }

        void InitTitleView()
        {
            _titleLabel = new UILabel(new CGRect((NavigationController.View.Frame.Width - 200) / 2, (NavigationController.View.Frame.Height - 44) / 2, 200, 44))
            {
                TextColor = UIColor.Gray,
                TextAlignment = UITextAlignment.Center
            };
            NavigationItem.TitleView = _titleLabel;
        }

        void InitTable()
        {
            _videosTableView = new UITableView(View.Frame);
            _videosTableView.TableFooterView = new UIView();
            _source = new VideosTableSource(_videosTableView, AllVideosViewCell.Key, AllVideosViewCell.Key);
            _source.SelectedItemAction = ItemSelected;
            _source.DeselecetdItemAction = ItemDeselected;
            _videosTableView.Source = _source;
            _videosTableView.AllowsMultipleSelection = ViewModel.ScreenType == VideoScreenType.Select;
            View.AddSubview(_videosTableView);
        }

        void ItemSelected(int index)
        {
            ViewModel.AddSelectedItem(index);
        }

        void ItemDeselected(int index)
        {
            ViewModel.RemoveSelectedItem(index);
        }
	}
}
