using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS
{
    public partial class AlbumViewCell : MvxTableViewCell
    {
        public static float CellHeight = 50;

        public static readonly NSString Key = new NSString(nameof(AlbumViewCell));
        public static readonly UINib Nib;

        static AlbumViewCell()
        {
            Nib = UINib.FromName(nameof(AlbumViewCell), NSBundle.MainBundle);
        }

        protected AlbumViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() => {
                var set = this.CreateBindingSet<AlbumViewCell, AlbumCellViewModel>();
                set.Bind(TitleLabel).To(vm => vm.Name);
                set.Apply();
            });
        }

		public override void Draw(CGRect rect)
		{
            base.Draw(rect);
            TitleLabel.Frame = new CGRect(new CGPoint(10, (CellHeight - TitleLabel.Frame.Height) / 2), TitleLabel.Frame.Size);
		}
	}
}
