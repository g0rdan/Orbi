using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Orbi.iOS
{
    public class AllVideosTableSource : MvxSimpleTableViewSource
    {
        public AllVideosTableSource(UITableView tableView, string nibName, string cellIdentifier)
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
    }
}
