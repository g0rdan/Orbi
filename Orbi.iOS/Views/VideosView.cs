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
        UILabel _titleLabel;
        UITableView _videosTableView;
        DeleteVideosTableViewSource _source;

        public VideosView()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            InitTitleView();
            InitTable();

            var set = this.CreateBindingSet<VideosView, VideosViewModel>();
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
            _source = new DeleteVideosTableViewSource(_videosTableView, AllVideosViewCell.Key, AllVideosViewCell.Key);
            _source.DeleteVideoAction = DeleteCell;
            _videosTableView.Source = _source;
            View.AddSubview(_videosTableView);
        }

        void DeleteCell(int index)
        {
            ViewModel.DeleteVideoCompletely(index);
        }
	}
}
