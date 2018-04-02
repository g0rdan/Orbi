using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class VideosViewModel : MvxViewModel<VideoParameter>
    {
        Album _owner;
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;
        readonly IFileService _fileService;

        public string Title { get; private set; }
        public bool IsSelecting { get; private set; }
        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }
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
            Items = new MvxObservableCollection<VideoCellViewModel>();
		}

        public override void Prepare(VideoParameter parameter)
        {
            _owner = parameter.Owner;
            IsSelecting = parameter.IsSelecting;
            Title = _owner == null || IsSelecting ? "All video" : parameter.Owner?.Title;
            Items = new MvxObservableCollection<VideoCellViewModel>();
        }

        public async override Task Initialize()
		{
            var videos = _owner == null || IsSelecting ?
                await _databaseService.GetVideosAsync() :
                await _databaseService.GetVideosAsync(_owner);
            
            if (videos != null && videos.Any())
            {
                foreach (var video in videos)
                {
                    Items.Add(new VideoCellViewModel
                    {
                        Title = video.Title,
                        Data = _fileService.GetVideoFile(video.FileName)
                    });
                }
            }
		}

        public void AddVideos()
        {
            if (_owner == null)
                return;

            _navigationService.Navigate<VideosViewModel, VideoParameter>(new VideoParameter { 
                IsSelecting = true,
                Owner = _owner
            });
        }
	}

    public class VideoParameter
    {
        public bool IsSelecting { get; set; }
        public Album Owner { get; set; }
    }
}
