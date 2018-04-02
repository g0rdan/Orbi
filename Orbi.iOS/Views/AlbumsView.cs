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
        UIBarButtonItem _addBtn;
        UITableView _albumsTableView;

        public AlbumsView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = _addBtn;

            _albumsTableView = new UITableView(View.Frame);
            _albumsTableView.TableFooterView = new UIView();
            var tableSource = new AlbumsTableSource(_albumsTableView, AlbumViewCell.Key, AlbumViewCell.Key);
            tableSource.DeleteAlbumAction = DeleteAlbum;
            _albumsTableView.Source = tableSource;
            View.AddSubview(_albumsTableView);

            var set = this.CreateBindingSet<AlbumsView, AlbumsViewModel>();
            set.Bind(_addBtn).To(vm => vm.CreateAlbumCommand);
            set.Bind(tableSource).To(vm => vm.Albums);
            set.Apply();
        }

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
            base.DidRotate(fromInterfaceOrientation);
            _albumsTableView.Frame = new CGRect(CGPoint.Empty, View.Frame.Size);
		}

        void DeleteAlbum(int index)
        {
            ViewModel.DeleteAlbum(index);
        }
	}
}
