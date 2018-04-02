using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Orbi.Models;
using Orbi.Services;

namespace Orbi.ViewModels
{
    public class AlbumsViewModel : MvxViewModel
    {
        readonly IMvxNavigationService _navigationService;
        readonly IDatabaseService _databaseService;
        readonly IDialogService _dialogService;

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
        public IMvxAsyncCommand CreateAlbumCommand => new MvxAsyncCommand(CreateAlbum);
        #endregion

        public AlbumsViewModel(IMvxNavigationService navigationService, IDatabaseService databaseService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
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
                    var cell = new AlbumCellViewModel
                    {
                        GUID = album.GUID,
                        Name = album.Title
                    };
                    cell.DeleteAction = () => DeleteAlbum(album);
                    Albums.Add(cell);
                }
            }
		}

		void OpenAlbum(Album album)
        {
            _navigationService.Navigate<VideosViewModel, VideoParameter>(new VideoParameter {
                IsSelecting = false,
                Owner = album
            });
        }

        void DeleteAlbum(Album album)
        {
            if (album != null)
            {
                _databaseService.DeleteAlbum(album);
                var cellCandidate = Albums.FirstOrDefault(x => x.Name == album.Title);
                if (cellCandidate != null)
                    Albums.Remove(cellCandidate);
            }
        }

        public void DeleteAlbum(int index)
        {
            var albumCellVM = Albums.ElementAt(index);
            if (albumCellVM != null)
            {
                _databaseService.DeleteAlbum(albumCellVM.GUID);
                Albums.Remove(albumCellVM);
            }
        }

        async Task CreateAlbum()
        {
            var result = await _dialogService.AskToAddAlbum();
            if (result.IsOk)
            {
                var newAlbum = new Album { Title = result.Title };
                _databaseService.AddAlbum(newAlbum);
                Albums.Add(new AlbumCellViewModel { Name = newAlbum.Title });
            }
        }
    }
}
