using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Orbi.ViewModels;
using UIKit;

namespace Orbi.iOS.Views
{
    public class AlbumsView : MvxViewController<AlbumsViewModel>
    {
        UIBarButtonItem _addBtn;

        public AlbumsView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Blue;

            _addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.RightBarButtonItem = _addBtn;

            var set = this.CreateBindingSet<AlbumsView, AlbumsViewModel>();
            set.Bind(_addBtn).To(vm => vm.CreateAlbumCommand);
            set.Apply();
        }
    }
}
