using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class AlbumVideosViewModel : VideosViewModel
    {
        Album _owner;

        public IMvxAsyncCommand OpenVideosForSelectCommand => new MvxAsyncCommand(OpenVideosForSelect);

        public AlbumVideosViewModel(
            IDatabaseService databaseService, 
            IFileService fileService, 
            IMvxNavigationService navigationService) 
            : base(databaseService, fileService, navigationService)
        {
        }

		public override void Prepare(VideoParameter parameter)
		{
            _owner = parameter.Owner;
            Title = parameter.Owner?.Title;
            Items = new MvxObservableCollection<VideoCellViewModel>();
		}

        public async override Task Initialize()
        {
            if (_owner == null)
                return;

            var videos = await _databaseService.GetVideosAsync(_owner);
            FillItems(videos);
        }

        public async Task OpenVideosForSelect()
        {
            if (_owner == null)
                return;

            var result = await _navigationService.Navigate<SelectVideosViewModel, VideoParameter, VideoAddingCallback>(new VideoParameter
            {
                Owner = _owner
            });

            // When we're coming back from previous view
            if (result?.UpdateItems ?? false)
            {
                Items.Clear();
                await Initialize();
            }
        }

		public void DeleteVideo(int index)
		{
            var cell = Items.ElementAt(index);
            if (cell != null)
            {
                _databaseService.DeleteVideo(cell.GUID, _owner.GUID);
                Items.Remove(cell);
            }
		}
	}
}
