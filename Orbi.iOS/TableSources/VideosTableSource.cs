using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using Orbi.iOS.TableSources;
using UIKit;

namespace Orbi.iOS
{
    public class VideosTableSource : MvxSimpleTableViewSource
    {
        public VideosTableSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = base.GetOrCreateCellFor(tableView, indexPath, item);
            //cell.SeparatorInset = UIEdgeInsets.Zero;
            //cell.LayoutMargins = UIEdgeInsets.Zero;
            //cell.PreservesSuperviewLayoutMargins = false;
            //cell.Accessory = ((ItemCellViewModel)item).IsLink ?
                //UITableViewCellAccessory.DisclosureIndicator :
                //UITableViewCellAccessory.None;
            return cell;
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            return AllVideosViewCell.CellHeight;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            
		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
            
		}
	}
}
