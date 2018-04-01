using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class VideosViewModel : MvxViewModel<Album>
    {
        Album _owner;
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;
        readonly IFileService _fileService;

        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }

        public VideosViewModel(IDatabaseService databaseService, IFileService fileService)
        {
            _databaseService = databaseService;
            _fileService = fileService;
        }

        public override void Prepare(Album parameter)
        {
            _owner = parameter;
            Items = new MvxObservableCollection<VideoCellViewModel>();
        }

        public async override Task Initialize()
		{
            var allVideos = await _databaseService.GetVideosAsync(_owner);
            if (allVideos != null && allVideos.Any())
            {
                foreach (var video in allVideos)
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
}
