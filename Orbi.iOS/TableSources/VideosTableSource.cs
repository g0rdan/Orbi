using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using Orbi.iOS.TableSources;
using UIKit;

namespace Orbi.iOS
{
    public class VideosTableSource : MvxSimpleTableViewSource
    {
        public Action<int> SelectedItemAction { get; set; }
        public Action<int> DeselecetdItemAction { get; set; }

        public VideosTableSource(UITableView tableView, string nibName, string cellIdentifier)
            : base(tableView, nibName, cellIdentifier)
        {
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            return AllVideosViewCell.CellHeight;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            SelectedItemAction?.Invoke(indexPath.Row);
		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
            DeselecetdItemAction?.Invoke(indexPath.Row);
		}
	}
}
