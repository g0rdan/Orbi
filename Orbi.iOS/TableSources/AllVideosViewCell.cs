using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.TableSources
{
    public partial class AllVideosViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("AllVideosViewCell");
        public static readonly UINib Nib;

        static AllVideosViewCell()
        {
            Nib = UINib.FromName("AllVideosViewCell", NSBundle.MainBundle);
        }

        protected AllVideosViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() => {
                var set = this.CreateBindingSet<AllVideosViewCell, VideoCellViewModel>();
                set.Bind(FilenameLabel).To(vm => vm.FileName);
                set.Apply();
            });
        }

		public override void Draw(CGRect rect)
		{
            base.Draw(rect);
            FilenameLabel.Frame = new CGRect(new CGPoint(10, (rect.Height - FilenameLabel.Frame.Height) / 2), FilenameLabel.Frame.Size);
		}
	}
}
