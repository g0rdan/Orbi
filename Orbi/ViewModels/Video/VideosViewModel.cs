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

        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }

        public VideosViewModel()
        {
        }

        public override void Prepare(Album album)
        {
            _owner = album;
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
                        FileName = video.FileName,
                        Data = video.Data
                    });
                }
            }
		}
	}
}
