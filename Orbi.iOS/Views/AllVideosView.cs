using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Orbi.iOS.TableSources;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    public class AllVideosView : MvxViewController<AllVideosViewModel>
    {
        UITableView _videosTableView;

        public AllVideosView()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Green;

            _videosTableView = new UITableView(View.Frame);
            _videosTableView.TableFooterView = new UIView();
            //_videosTableView.RowHeight = UITableView.AutomaticDimension;
            //FirstTableView.EstimatedRowHeight = 140;
            var tableSource = new AllVideosTableSource(_videosTableView, AllVideosViewCell.Key, AllVideosViewCell.Key);
            _videosTableView.Source = tableSource;

            View.AddSubview(_videosTableView);

            var set = this.CreateBindingSet<AllVideosView, AllVideosViewModel>();
            set.Bind(tableSource).To(vm => vm.Items);
            //set.Bind(tableSource).For(s => s.SelectionChangedCommand).To(vm => vm.ClickToItemViewCell);
            set.Apply();
		}
	}
}
