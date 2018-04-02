using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    public class AlbumsView : MvxViewController<AlbumsViewModel>
    {
        UILabel _titleLabel;
        UIBarButtonItem _addBtn;
        UITableView _albumsTableView;
        AlbumsTableSource _source;

        public AlbumsView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = _addBtn;

            InitTitleView();
            InitTable();

            var set = this.CreateBindingSet<AlbumsView, AlbumsViewModel>();
            set.Bind(_titleLabel).To(vm => vm.Title);
            set.Bind(_addBtn).To(vm => vm.CreateAlbumCommand);
            set.Bind(_source).To(vm => vm.Albums);
            set.Apply();
        }

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
            base.DidRotate(fromInterfaceOrientation);
            _albumsTableView.Frame = new CGRect(CGPoint.Empty, View.Frame.Size);
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
            _albumsTableView = new UITableView(View.Frame);
            _albumsTableView.TableFooterView = new UIView();
            _source = new AlbumsTableSource(_albumsTableView, AlbumViewCell.Key, AlbumViewCell.Key);
            _source.DeleteAlbumAction = DeleteAlbum;
            _albumsTableView.Source = _source;
            View.AddSubview(_albumsTableView);
        }

        void DeleteAlbum(int index)
        {
            ViewModel.DeleteAlbum(index);
        }
	}
}
