using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Orbi.iOS
{
    public class AlbumsTableSource : MvxSimpleTableViewSource
    {
        public AlbumsTableSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = base.GetOrCreateCellFor(tableView, indexPath, item);
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return AlbumViewCell.CellHeight;
        }
    }
}
