using System;
using CoreGraphics;
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

            _videosTableView = new UITableView(View.Frame);
            _videosTableView.TableFooterView = new UIView();
            var tableSource = new AllVideosTableSource(_videosTableView, AllVideosViewCell.Key, AllVideosViewCell.Key);
            _videosTableView.Source = tableSource;
            View.AddSubview(_videosTableView);

            var set = this.CreateBindingSet<AllVideosView, AllVideosViewModel>();
            set.Bind(tableSource).To(vm => vm.Items);
            set.Apply();
		}

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);
            _videosTableView.Frame = new CGRect(CGPoint.Empty, View.Frame.Size);
        }
	}
}
