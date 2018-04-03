using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class SelectVideosViewModel : VideosViewModel
    {
        Album _owner;
        bool _doneBtnEnabled;

        public bool DoneBtnEnabled
        {
            get { return _doneBtnEnabled; }
            set { SetProperty(ref _doneBtnEnabled, value); }
        }

        public IMvxCommand AddVideosCommand => new MvxCommand(AddVideos);
        public List<VideoCellViewModel> SelectedItems { get; set; } = new List<VideoCellViewModel>();

        public SelectVideosViewModel(
            IDatabaseService databaseService,
            IFileService fileService,
            IMvxNavigationService navigationService)
            : base(databaseService, fileService, navigationService)
        {
        }

		public override void Prepare()
		{
            base.Prepare();
            Title = "Select videos";
		}

		public override void Prepare(VideoParameter parameter)
		{
            _owner = parameter.Owner;
		}

		public void AddVideos()
        {
            if (!SelectedItems.Any())
                return;

            foreach (var item in SelectedItems)
            {
                _databaseService.AddVideo(item.GUID, _owner.GUID);
            }

            _navigationService.Close(this, new VideoAddingCallback(true));
        }

        public void AddSelectedItem(int index)
        {
            var cell = Items.ElementAt(index);
            if (cell != null)
            {
                SelectedItems.Add(cell);
                DoneBtnEnabled = SelectedItems.Any();
            }
        }

        public void RemoveSelectedItem(int index)
        {
            var cell = Items.ElementAt(index);
            if (cell != null)
            {
                SelectedItems.Remove(cell);
                DoneBtnEnabled = SelectedItems.Any();
            }
        }
    }
}
