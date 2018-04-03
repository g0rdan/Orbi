using System;
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
    public class SelectVideosView : MvxViewController<SelectVideosViewModel>
    {
        UIBarButtonItem _doneBtn;
        UILabel _titleLabel;
        UITableView _videosTableView;
        VideosTableSource _source;

        public SelectVideosView()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            _doneBtn = new UIBarButtonItem(UIBarButtonSystemItem.Done);
            NavigationItem.RightBarButtonItem = _doneBtn;

            InitTitleView();
            InitTable();

            var set = this.CreateBindingSet<SelectVideosView, SelectVideosViewModel>();
            set.Bind(_doneBtn).To(vm => vm.AddVideosCommand);
            set.Bind(_doneBtn).For("Enabled").To(vm => vm.DoneBtnEnabled);
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
            _videosTableView.AllowsMultipleSelection = true;
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
