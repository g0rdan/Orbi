using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class AlbumsListViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;

        public MvxObservableCollection<AlbumCellViewModel> Albums { get; set; }

        #region Commands
        /// <summary>
        /// Obviously the command for deleting an exist album
        /// </summary>
        public IMvxCommand<Album> DeleteAlbumCommand => new MvxCommand<Album>(DeleteAlbum);
        /// <summary>
        /// The command opens the list view model with videos inside
        /// </summary>
        public IMvxCommand<Album> OpenAlbumCommand => new MvxCommand<Album>(OpenAlbum);
        /// <summary>
        /// Creating a new album in new view model
        /// </summary>
        public IMvxCommand CreateAlbumCommand => new MvxCommand(CreateAlbum);
        #endregion

        public AlbumsListViewModel(IMvxNavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

        public override void Prepare()
        {
            Albums = new MvxObservableCollection<AlbumCellViewModel>();
        }

        public async override Task Initialize()
		{
            var albums = await _databaseService.GetAlbumsAsync();
            if (albums != null && albums.Any())
            {
                foreach (var album in albums)
                {
                    Albums.Add(new AlbumCellViewModel
                    {
                        Name = album.Title
                    });
                }
            }
		}

		void OpenAlbum(Album album)
        {
            _navigationService.Navigate<VideosViewModel, Album>(album);
        }

        void CreateAlbum()
        {
            _navigationService.Navigate<CreateAlbumViewModel>();
        }

        void DeleteAlbum(Album album)
        {
            if (album != null)
            {
                _databaseService.DeleteAlbum(album);
            }
        }
    }
}
