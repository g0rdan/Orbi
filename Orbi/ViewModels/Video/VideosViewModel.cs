using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class VideosViewModel : MvxViewModel<VideoParameter, VideoAddingCallback>
    {
        Album _owner;
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;
        readonly IFileService _fileService;

        bool _doneBtnEnabled;
        public bool DoneBtnEnabled
        {
            get { return _doneBtnEnabled; }
            set { SetProperty(ref _doneBtnEnabled, value); }
        }
        public string Title { get; private set; }
        public VideoScreenType ScreenType { get; set; }
        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }
        public List<VideoCellViewModel> SelectedItems { get; set; } = new List<VideoCellViewModel>();
        public IMvxAsyncCommand OpenVideosForSelectCommand => new MvxAsyncCommand(OpenVideosForSelect);
        public IMvxCommand AddVideosCommand => new MvxCommand(AddVideos);

        public VideosViewModel(
            IDatabaseService databaseService,
            IFileService fileService,
            IMvxNavigationService navigationService
        )
        {
            _databaseService = databaseService;
            _fileService = fileService;
            _navigationService = navigationService;
        }

        // Calls in case when we're opening VM without parameters, when want to
        // watch all videos
        public override void Prepare()
        {
            ScreenType = VideoScreenType.All;
            Items = new MvxObservableCollection<VideoCellViewModel>();
        }

        public override void Prepare(VideoParameter parameter)
        {
            _owner = parameter.Owner;
            if (_owner != null)
                ScreenType = VideoScreenType.Custom;
            if (parameter.IsSelecting)
                ScreenType = VideoScreenType.Select;
            Title = ScreenType == VideoScreenType.Custom ? parameter.Owner?.Title : "All video";
            Items = new MvxObservableCollection<VideoCellViewModel>();
        }

        public async override Task Initialize()
        {
            var videos = ScreenType == VideoScreenType.Custom ?
                await _databaseService.GetVideosAsync(_owner) :
                await _databaseService.GetVideosAsync();

            if (videos != null && videos.Any())
            {
                foreach (var video in videos)
                {
                    Items.Add(new VideoCellViewModel
                    {
                        GUID = video.GUID,
                        Title = video.Name,
                        Data = _fileService.GetVideoFile(video.FileName)
                    });
                }
            }
        }

        public async Task OpenVideosForSelect()
        {
            if (_owner == null)
                return;
            
            var result = await _navigationService.Navigate<VideosViewModel, VideoParameter, VideoAddingCallback>(new VideoParameter
            {
                IsSelecting = true,
                Owner = _owner
            });

            if (result.UpdateItems)
                await Initialize();
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

    public class VideoParameter
    {
        public bool IsSelecting { get; set; }
        public Album Owner { get; set; }
    }

    public enum VideoScreenType
    {
        All,
        Custom,
        Select
    }

    public class VideoAddingCallback 
    {
        public bool UpdateItems { get; set; } = false;

        public VideoAddingCallback(bool update)
        {
            UpdateItems = update;
        }
    }
}
