using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class AllVideosViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;

        public MvxObservableCollection<VideoCellViewModel> Items { get; set; }

        public IMvxCommand<Video> OpenVideoCommand => new MvxCommand<Video>(OpenVideo);

        public AllVideosViewModel(IMvxNavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

		public override void Prepare()
		{
            base.Prepare();
            Items = new MvxObservableCollection<VideoCellViewModel>();
		}

        public async override Task Initialize()
		{
            var allVideos = await _databaseService.GetVideosAsync();
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

		void OpenVideo(Video video)
        {
            throw new NotImplementedException();
        }
	}
}
