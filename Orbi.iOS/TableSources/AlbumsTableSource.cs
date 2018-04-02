using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Orbi.iOS
{
    public class AlbumsTableSource : MvxSimpleTableViewSource
    {
        public Action<int> DeleteAlbumAction { get; set; }

        public AlbumsTableSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return AlbumViewCell.CellHeight;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                DeleteAlbumAction?.Invoke(indexPath.Row);
            }
        }
    }
}
