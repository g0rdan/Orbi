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
        protected readonly IMvxNavigationService _navigationService;
        protected readonly IDatabaseService _databaseService;
        protected readonly IFileService _fileService;

        public string Title { get; protected set; } = "All video";
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
            throw new NotImplementedException();
        }

        public async override Task Initialize()
        {
            var videos = await _databaseService.GetVideosAsync();
            FillItems(videos);
        }

        protected void FillItems(IEnumerable<Video> videos)
        {
            if (videos != null && videos.Any())
            {
                foreach (var video in videos)
                {
                    var cell = new VideoCellViewModel
                    {
                        GUID = video.GUID,
                        Title = video.Name,
                        Data = _fileService.GetVideoFile(video.FileName)
                    };
                    cell.DeleteAction = () => DeleteVideoCompletely(cell);
                    Items.Add(cell);
                }
            }
        }

        public void DeleteVideoCompletely(int index)
		{
            var cell = Items.ElementAt(index);
            DeleteVideoCompletely(cell);
		}

        public void DeleteVideoCompletely(VideoCellViewModel cell)
        {
            if (cell != null)
            {
                Items.Remove(cell);
                var video = _databaseService.GetVideo(cell.GUID);
                DeleteVideoCompletely(video);    
            }
        }
		/// <summary>
		/// Removing video from database and from disk
		/// </summary>
        void DeleteVideoCompletely(Video video)
        {
            if (video != null)
            {
                _databaseService.DeleteVideo(video);
                _fileService.DeleteFile(video.FileName);
            }
        }
    }

    public class VideoParameter
    {
        public Album Owner { get; set; }
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
