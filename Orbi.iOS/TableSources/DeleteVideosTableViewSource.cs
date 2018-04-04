using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Orbi.iOS.TableSources
{
    public class DeleteVideosTableViewSource : MvxSimpleTableViewSource
    {
        public Action<int> DeleteVideoAction { get; set; }

        public DeleteVideosTableViewSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return AllVideosViewCell.CellHeight;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                DeleteVideoAction?.Invoke(indexPath.Row);
            }
        }
    }
}
