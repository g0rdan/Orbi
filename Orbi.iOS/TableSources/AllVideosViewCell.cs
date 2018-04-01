using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Orbi.iOS.Converters;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.TableSources
{
    public partial class AllVideosViewCell : MvxTableViewCell
    {
        public static float CellHeight = 200f;
        public static readonly NSString Key = new NSString(nameof(AllVideosViewCell));
        public static readonly UINib Nib;

        static AllVideosViewCell()
        {
            Nib = UINib.FromName(nameof(AllVideosViewCell), NSBundle.MainBundle);
        }

        protected AllVideosViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() => {
                var set = this.CreateBindingSet<AllVideosViewCell, VideoCellViewModel>();
                set.Bind(ImageView).For(view => view.Image).To(vm => vm.Data).WithConversion(nameof(ImageConverter));
                set.Bind(FilenameLabel).To(vm => vm.Title);
                set.Apply();
            });
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            ImageView.Frame = new CGRect(CGPoint.Empty, new CGSize(CellHeight, CellHeight));
            FilenameLabel.Frame = new CGRect(new CGPoint(ImageView.Frame.Right + 10, (rect.Height - FilenameLabel.Frame.Height) / 2), FilenameLabel.Frame.Size);
        }
    }
}
