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

        public string Title { get; private set; } = "All video";
        public bool IsSelecting { get; set; }
        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }

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
            Title = parameter.Owner?.Title;
            IsSelecting = parameter.IsSelecting;
            Items = new MvxObservableCollection<VideoCellViewModel>();
        }

        public async override Task Initialize()
		{
            var videos = _owner == null ?
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
	}

    public class VideoParameter
    {
        public bool IsSelecting { get; set; }
        public Album Owner { get; set; }
    }
}
