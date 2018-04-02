using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Orbi.ViewModels
{
	public class MainViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;

        public IMvxCommand OpenAllVideosCommand => new MvxCommand(OpenAllVideos);
        public IMvxCommand OpenAlbumsCommand => new MvxCommand(OpenAlbums);

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        void OpenAllVideos()
        {
            _navigationService.Navigate<VideosViewModel>();
        }

        void OpenAlbums()
        {
            _navigationService.Navigate<AlbumsViewModel>();
        }
	}
}
